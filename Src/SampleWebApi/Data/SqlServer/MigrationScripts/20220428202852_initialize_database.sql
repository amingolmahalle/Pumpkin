IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
CREATE TABLE [__EFMigrationsHistory] (
    [MigrationId] nvarchar(150) NOT NULL,
    [ProductVersion] nvarchar(32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Fullname] nvarchar(30) NOT NULL,
    [MobileNumber] char(11) NOT NULL,
    [NationalCode] char(10) NOT NULL,
    [Email] nvarchar(30) NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    [Status] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [ModifiedAt] datetime2 NOT NULL,
    [ModifiedBy] bigint NOT NULL,
    [Deleted] bit NOT NULL,
    [RemovedAt] datetime2 NULL,
    [RemovedBy] bigint NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BirthDate', N'CreatedAt', N'CreatedBy', N'Deleted', N'Email', N'Fullname', N'MobileNumber', N'ModifiedAt', N'ModifiedBy', N'NationalCode', N'RemovedAt', N'RemovedBy', N'Status') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [BirthDate], [CreatedAt], [CreatedBy], [Deleted], [Email], [Fullname], [MobileNumber], [ModifiedAt], [ModifiedBy], [NationalCode], [RemovedAt], [RemovedBy], [Status])
VALUES (10001, '0001-01-01T00:00:00.0000000', '2022-01-01T10:00:00.0000000Z', CAST(10001 AS bigint), CAST(0 AS bit), N'user1@gmail.com', N'user 1', N'09120000001', '2022-01-01T10:00:00.0000000Z', CAST(10001 AS bigint), '5820005546', NULL, NULL, CAST(0 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BirthDate', N'CreatedAt', N'CreatedBy', N'Deleted', N'Email', N'Fullname', N'MobileNumber', N'ModifiedAt', N'ModifiedBy', N'NationalCode', N'RemovedAt', N'RemovedBy', N'Status') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BirthDate', N'CreatedAt', N'CreatedBy', N'Deleted', N'Email', N'Fullname', N'MobileNumber', N'ModifiedAt', N'ModifiedBy', N'NationalCode', N'RemovedAt', N'RemovedBy', N'Status') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [BirthDate], [CreatedAt], [CreatedBy], [Deleted], [Email], [Fullname], [MobileNumber], [ModifiedAt], [ModifiedBy], [NationalCode], [RemovedAt], [RemovedBy], [Status])
VALUES (10002, '0001-01-01T00:00:00.0000000', '2022-01-01T10:00:00.0000000Z', CAST(10001 AS bigint), CAST(0 AS bit), N'user2@gmail.com', N'user 2', N'09120000002', '2022-01-01T10:00:00.0000000Z', CAST(10001 AS bigint), '5820005545', NULL, NULL, CAST(0 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BirthDate', N'CreatedAt', N'CreatedBy', N'Deleted', N'Email', N'Fullname', N'MobileNumber', N'ModifiedAt', N'ModifiedBy', N'NationalCode', N'RemovedAt', N'RemovedBy', N'Status') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220428202852_initialize_database', N'6.0.4');
GO

COMMIT;
GO
