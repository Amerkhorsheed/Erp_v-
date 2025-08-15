# Historical Sales Data Migration

This directory contains scripts for migrating historical sales data to support the new cart-based sales system with TransactionId grouping.

## Overview

The ERP system has been updated to use a cart-based sales approach where multiple items in a single sale transaction share the same `TransactionId`. This migration script groups existing historical sales data by logical transaction boundaries.

## Migration Script

### `MigrateHistoricalSalesData.sql`

This script performs the following operations:

1. **Adds TransactionId column** to the SALES table if it doesn't exist
2. **Groups existing sales** by customer, date, and time proximity (within 5-minute windows)
3. **Assigns unique TransactionId** values to each logical transaction group
4. **Creates database index** on TransactionId for improved performance
5. **Provides verification queries** to confirm successful migration

## How to Run the Migration

### Prerequisites
- Ensure you have database administrator privileges
- Back up your database before running the migration
- Verify that the new cart-based sales system is deployed

### Execution Steps

1. **Backup Database**
   ```sql
   BACKUP DATABASE [YourDatabaseName] TO DISK = 'C:\Backup\PreMigration_Backup.bak'
   ```

2. **Run Migration Script**
   - Open SQL Server Management Studio
   - Connect to your database
   - Open `MigrateHistoricalSalesData.sql`
   - Execute the script

3. **Verify Results**
   The script includes verification queries that will show:
   - Total records with TransactionId
   - Any records without TransactionId (should be 0)
   - Sample groupings to review

## Migration Logic

### Grouping Strategy
The script groups sales records using the following criteria:
- **Same Customer**: Sales must belong to the same customer
- **Same Date**: Sales must occur on the same date
- **Time Proximity**: Sales within 5-minute windows are grouped together
- **Sequential Processing**: Items are processed in chronological order

### TransactionId Format
- **Grouped Sales**: `TXN-YYYYMMDD-CustomerID-HourGroup-GUID`
- **Single Sales**: `TXN-SINGLE-SalesID-GUID`

## Post-Migration Verification

After running the migration, verify the following:

1. **All records have TransactionId**
   ```sql
   SELECT COUNT(*) FROM SALES WHERE TransactionId IS NULL
   -- Should return 0
   ```

2. **Reasonable grouping**
   ```sql
   SELECT 
       TransactionId,
       COUNT(*) as ItemCount,
       CustomerID,
       MIN(SalesDate) as StartTime,
       MAX(SalesDate) as EndTime
   FROM SALES
   GROUP BY TransactionId, CustomerID
   ORDER BY ItemCount DESC
   ```

3. **Index exists**
   ```sql
   SELECT name FROM sys.indexes 
   WHERE object_id = OBJECT_ID('SALES') 
   AND name = 'IX_SALES_TransactionId'
   ```

## Rollback Plan

If you need to rollback the migration:

```sql
-- Remove TransactionId column
ALTER TABLE SALES DROP COLUMN TransactionId;

-- Drop the index
DROP INDEX IX_SALES_TransactionId ON SALES;

-- Restore from backup if needed
RESTORE DATABASE [YourDatabaseName] FROM DISK = 'C:\Backup\PreMigration_Backup.bak'
```

## Support

If you encounter issues during migration:
1. Check the SQL Server error log
2. Verify database permissions
3. Ensure sufficient disk space
4. Review the verification queries output

## Notes

- The migration is designed to be idempotent (can be run multiple times safely)
- Existing TransactionId values are preserved
- The script uses temporary tables to minimize locking
- Performance impact should be minimal for most database sizes