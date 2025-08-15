-- Historical Sales Data Migration Script
-- Purpose: Group existing sales items by original SalesId or timestamps into TransactionId
-- This script should be run after implementing the cart-based sales system

-- Step 1: Add TransactionId column to SALES table if not exists
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SALES]') AND name = 'TransactionId')
BEGIN
    ALTER TABLE [dbo].[SALES] ADD [TransactionId] NVARCHAR(50) NULL;
END
GO

-- Step 2: Create a temporary table to map old SalesId to new TransactionId
IF OBJECT_ID('tempdb..#SalesIdMapping') IS NOT NULL
    DROP TABLE #SalesIdMapping;

CREATE TABLE #SalesIdMapping (
    OldSalesId INT,
    NewTransactionId NVARCHAR(50),
    SalesDate DATETIME,
    CustomerID INT
);

-- Step 3: Generate unique TransactionId for each group of sales
-- Group by SalesDate, CustomerID, and time window (within 5 minutes) to identify related sales
WITH SalesGroups AS (
    SELECT 
        ID,
        SalesDate,
        CustomerID,
        -- Group sales that occur within 5 minutes of each other for the same customer
        ROW_NUMBER() OVER (
            PARTITION BY CustomerID, 
            CAST(SalesDate AS DATE),
            DATEPART(HOUR, CAST(SalesDate AS DATETIME)),
            DATEPART(MINUTE, CAST(SalesDate AS DATETIME)) / 5
            ORDER BY SalesDate
        ) as GroupSequence
    FROM [dbo].[SALES]
    WHERE TransactionId IS NULL -- Only process records without TransactionId
),
TransactionGroups AS (
    SELECT 
        ID,
        SalesDate,
        CustomerID,
        -- Create a unique identifier for each transaction group (shortened to avoid truncation)
        CONCAT(
            'TXN-',
            FORMAT(SalesDate, 'yyyyMMdd'),
            '-',
            CustomerID,
            '-',
            RIGHT('000' + CAST(DATEPART(HOUR, CAST(SalesDate AS DATETIME)) AS VARCHAR), 2),
            RIGHT('0' + CAST(DATEPART(MINUTE, CAST(SalesDate AS DATETIME)) / 5 AS VARCHAR), 1)
        ) as NewTransactionId
    FROM SalesGroups
    WHERE GroupSequence = 1 -- Only the first item in each group gets a new TransactionId
)
INSERT INTO #SalesIdMapping (OldSalesId, NewTransactionId, SalesDate, CustomerID)
SELECT 
    s.ID,
    tg.NewTransactionId,
    s.SalesDate,
    s.CustomerID
FROM [dbo].[SALES] s
INNER JOIN TransactionGroups tg ON (
    s.CustomerID = tg.CustomerID
    AND s.SalesDate >= tg.SalesDate
    AND s.SalesDate <= DATEADD(MINUTE, 5, CAST(tg.SalesDate AS DATETIME))
)
WHERE s.TransactionId IS NULL;

-- Step 4: Update SALES table with new TransactionId values
UPDATE s
SET s.TransactionId = sm.NewTransactionId
FROM [dbo].[SALES] s
INNER JOIN #SalesIdMapping sm ON s.ID = sm.OldSalesId;

-- Step 5: Handle any remaining records without TransactionId (fallback)
-- These are likely isolated sales that don't group with others
UPDATE [dbo].[SALES]
SET TransactionId = CONCAT('TXN-S-', ID)
WHERE TransactionId IS NULL;

-- Step 6: Create index on TransactionId for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SALES]') AND name = 'IX_SALES_TransactionId')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SALES_TransactionId] ON [dbo].[SALES] ([TransactionId]);
END
GO

-- Step 7: Verification queries
PRINT 'Migration completed. Running verification queries...'

-- Count of records with TransactionId
SELECT 
    'Records with TransactionId' as Description,
    COUNT(*) as Count
FROM [dbo].[SALES]
WHERE TransactionId IS NOT NULL;

-- Count of records without TransactionId (should be 0)
SELECT 
    'Records without TransactionId' as Description,
    COUNT(*) as Count
FROM [dbo].[SALES]
WHERE TransactionId IS NULL;

-- Sample of TransactionId groupings
SELECT TOP 10
    TransactionId,
    COUNT(*) as ItemCount,
    MIN(SalesDate) as FirstSaleDate,
    MAX(SalesDate) as LastSaleDate,
    CustomerID
FROM [dbo].[SALES]
WHERE TransactionId IS NOT NULL
GROUP BY TransactionId, CustomerID
ORDER BY ItemCount DESC;

-- Clean up temporary table
DROP TABLE #SalesIdMapping;

PRINT 'Historical sales data migration completed successfully!';
PRINT 'All existing sales records now have TransactionId values for cart-based grouping.';