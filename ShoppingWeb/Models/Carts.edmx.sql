
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/22/2017 18:21:55
-- Generated from EDMX file: D:\Newproject\ShoppingWeb\ShoppingWeb\Models\Carts.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Carts];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderOrderDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetailSet] DROP CONSTRAINT [FK_OrderOrderDetail];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSet];
GO
IF OBJECT_ID(N'[dbo].[OrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderSet];
GO
IF OBJECT_ID(N'[dbo].[OrderDetailSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetailSet];
GO
IF OBJECT_ID(N'[dbo].[ProductCommets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductCommets];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CategoryId] int  NOT NULL,
    [Price] decimal(28,3)  NOT NULL,
    [PublishDate] datetime  NOT NULL,
    [Status] bit  NOT NULL,
    [DefaultImageId] bigint  NULL,
    [Quantity] int  NOT NULL,
    [DefaultImageURL] nvarchar(max)  NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [RecieverName] nvarchar(max)  NOT NULL,
    [RecieverPhone] nvarchar(max)  NOT NULL,
    [RecieverAddress] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderDetailSet'
CREATE TABLE [dbo].[OrderDetailSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderId] int  NOT NULL,
    [Price] decimal(28,3)  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Quantity] int  NOT NULL
);
GO

-- Creating table 'ProductCommets'
CREATE TABLE [dbo].[ProductCommets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [ProductId] int  NOT NULL,
    [Stars] int  NOT NULL
);
GO

-- Creating table 'CategorySet'
CREATE TABLE [dbo].[CategorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderDetailSet'
ALTER TABLE [dbo].[OrderDetailSet]
ADD CONSTRAINT [PK_OrderDetailSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductCommets'
ALTER TABLE [dbo].[ProductCommets]
ADD CONSTRAINT [PK_ProductCommets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CategorySet'
ALTER TABLE [dbo].[CategorySet]
ADD CONSTRAINT [PK_CategorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [OrderId] in table 'OrderDetailSet'
ALTER TABLE [dbo].[OrderDetailSet]
ADD CONSTRAINT [FK_OrderOrderDetail]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderDetail'
CREATE INDEX [IX_FK_OrderOrderDetail]
ON [dbo].[OrderDetailSet]
    ([OrderId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------