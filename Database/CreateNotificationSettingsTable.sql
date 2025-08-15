-- Create NotificationSettings table for storing user notification preferences
CREATE TABLE [dbo].[NotificationSettings](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [EmployeeId] [int] NOT NULL,
    [NotificationType] [nvarchar](50) NOT NULL,
    [IsEnabled] [bit] NOT NULL DEFAULT 1,
    [ThresholdValue] [nvarchar](100) NULL,
    [AdditionalSettings] [nvarchar](max) NULL,
    [CreatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [ModifiedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [IsDeleted] [bit] NOT NULL DEFAULT 0,
    [DeletedDate] [datetime] NULL,
    CONSTRAINT [PK_NotificationSettings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NotificationSettings_EMPLOYEE] FOREIGN KEY([EmployeeId]) REFERENCES [dbo].[EMPLOYEE] ([Id])
);

-- Create index for better performance
CREATE NONCLUSTERED INDEX [IX_NotificationSettings_EmployeeId] ON [dbo].[NotificationSettings]
(
    [EmployeeId] ASC
);

CREATE NONCLUSTERED INDEX [IX_NotificationSettings_NotificationType] ON [dbo].[NotificationSettings]
(
    [NotificationType] ASC
);

-- Insert default notification settings for all existing employees
INSERT INTO [dbo].[NotificationSettings] ([EmployeeId], [NotificationType], [IsEnabled], [ThresholdValue])
SELECT 
    e.[Id],
    'SalesThreshold',
    1,
    '1000'
FROM [dbo].[EMPLOYEE] e
WHERE NOT EXISTS (
    SELECT 1 FROM [dbo].[NotificationSettings] ns 
    WHERE ns.[EmployeeId] = e.[Id] AND ns.[NotificationType] = 'SalesThreshold'
);

INSERT INTO [dbo].[NotificationSettings] ([EmployeeId], [NotificationType], [IsEnabled], [ThresholdValue])
SELECT 
    e.[Id],
    'LowStockAlert',
    1,
    '10'
FROM [dbo].[EMPLOYEE] e
WHERE NOT EXISTS (
    SELECT 1 FROM [dbo].[NotificationSettings] ns 
    WHERE ns.[EmployeeId] = e.[Id] AND ns.[NotificationType] = 'LowStockAlert'
);

INSERT INTO [dbo].[NotificationSettings] ([EmployeeId], [NotificationType], [IsEnabled])
SELECT 
    e.[Id],
    'FailedTransactions',
    1
FROM [dbo].[EMPLOYEE] e
WHERE NOT EXISTS (
    SELECT 1 FROM [dbo].[NotificationSettings] ns 
    WHERE ns.[EmployeeId] = e.[Id] AND ns.[NotificationType] = 'FailedTransactions'
);

INSERT INTO [dbo].[NotificationSettings] ([EmployeeId], [NotificationType], [IsEnabled])
SELECT 
    e.[Id],
    'SystemErrors',
    1
FROM [dbo].[EMPLOYEE] e
WHERE NOT EXISTS (
    SELECT 1 FROM [dbo].[NotificationSettings] ns 
    WHERE ns.[EmployeeId] = e.[Id] AND ns.[NotificationType] = 'SystemErrors'
);

GO