
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/04/2013 21:24:21
-- Generated from EDMX file: C:\Users\Stacy.Gay\Documents\GitHub\SimpleStock\SimpleStock.Data\Inventory.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Inventory];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(200)  NOT NULL,
    [Password] nvarchar(200)  NOT NULL,
    [FirstName] nvarchar(100)  NOT NULL,
    [LastName] nvarchar(100)  NOT NULL,
    [CompanyId] int  NOT NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [Address] nvarchar(200)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [State] nvarchar(50)  NOT NULL,
    [PostalCode] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Stores'
CREATE TABLE [dbo].[Stores] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CompanyId] int  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(500)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Units] nvarchar(max)  NOT NULL,
    [Cost] decimal(18,0)  NOT NULL,
    [Price] nvarchar(max)  NOT NULL,
    [StoreId] int  NOT NULL,
    [ProductCategoryId] int  NOT NULL
);
GO

-- Creating table 'ProductCategories'
CREATE TABLE [dbo].[ProductCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [StoreId] int  NOT NULL
);
GO

-- Creating table 'Inventories'
CREATE TABLE [dbo].[Inventories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Amount] float  NOT NULL,
    [Sold] float  NOT NULL,
    [Lost] float  NOT NULL,
    [ProductId] int  NOT NULL
);
GO

-- Creating table 'StoreUser'
CREATE TABLE [dbo].[StoreUser] (
    [Stores_Id] int  NOT NULL,
    [Users_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stores'
ALTER TABLE [dbo].[Stores]
ADD CONSTRAINT [PK_Stores]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductCategories'
ALTER TABLE [dbo].[ProductCategories]
ADD CONSTRAINT [PK_ProductCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [Date] in table 'Inventories'
ALTER TABLE [dbo].[Inventories]
ADD CONSTRAINT [PK_Inventories]
    PRIMARY KEY CLUSTERED ([Id], [Date] ASC);
GO

-- Creating primary key on [Stores_Id], [Users_Id] in table 'StoreUser'
ALTER TABLE [dbo].[StoreUser]
ADD CONSTRAINT [PK_StoreUser]
    PRIMARY KEY CLUSTERED ([Stores_Id], [Users_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CompanyId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_CompanyUser]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyUser'
CREATE INDEX [IX_FK_CompanyUser]
ON [dbo].[Users]
    ([CompanyId]);
GO

-- Creating foreign key on [CompanyId] in table 'Stores'
ALTER TABLE [dbo].[Stores]
ADD CONSTRAINT [FK_CompanyStore]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyStore'
CREATE INDEX [IX_FK_CompanyStore]
ON [dbo].[Stores]
    ([CompanyId]);
GO

-- Creating foreign key on [Stores_Id] in table 'StoreUser'
ALTER TABLE [dbo].[StoreUser]
ADD CONSTRAINT [FK_StoreUser_Store]
    FOREIGN KEY ([Stores_Id])
    REFERENCES [dbo].[Stores]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_Id] in table 'StoreUser'
ALTER TABLE [dbo].[StoreUser]
ADD CONSTRAINT [FK_StoreUser_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreUser_User'
CREATE INDEX [IX_FK_StoreUser_User]
ON [dbo].[StoreUser]
    ([Users_Id]);
GO

-- Creating foreign key on [StoreId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_StoreProduct]
    FOREIGN KEY ([StoreId])
    REFERENCES [dbo].[Stores]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreProduct'
CREATE INDEX [IX_FK_StoreProduct]
ON [dbo].[Products]
    ([StoreId]);
GO

-- Creating foreign key on [StoreId] in table 'ProductCategories'
ALTER TABLE [dbo].[ProductCategories]
ADD CONSTRAINT [FK_StoreProductCategory]
    FOREIGN KEY ([StoreId])
    REFERENCES [dbo].[Stores]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreProductCategory'
CREATE INDEX [IX_FK_StoreProductCategory]
ON [dbo].[ProductCategories]
    ([StoreId]);
GO

-- Creating foreign key on [ProductCategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ProductCategoryProduct]
    FOREIGN KEY ([ProductCategoryId])
    REFERENCES [dbo].[ProductCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategoryProduct'
CREATE INDEX [IX_FK_ProductCategoryProduct]
ON [dbo].[Products]
    ([ProductCategoryId]);
GO

-- Creating foreign key on [ProductId] in table 'Inventories'
ALTER TABLE [dbo].[Inventories]
ADD CONSTRAINT [FK_ProductInventory]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductInventory'
CREATE INDEX [IX_FK_ProductInventory]
ON [dbo].[Inventories]
    ([ProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------