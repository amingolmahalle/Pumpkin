IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Fullname] nvarchar(40) NOT NULL,
    [MobileNumber] char(11) NOT NULL,
    [NationalCode] char(10) NULL,
    [Email] nvarchar(30) NULL,
    [BirthDate] datetime2 NOT NULL,
    [Status] bit NOT NULL,
    [SubmitTime] datetime2 NOT NULL,
    [LastUpdateTime] datetime2 NULL,
    [SubmitUser] int NOT NULL,
    [LastUpdateUser] int NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200419074407_CreateDatabase', N'3.1.3');

GO
