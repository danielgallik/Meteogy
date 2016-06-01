
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/10/2016 19:40:46
-- Generated from EDMX file: C:\Users\Daniel\Source\Repos\Meteogy\Meteogy\Models\EDBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Measureme__Locat__01142BA1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Measurement] DROP CONSTRAINT [FK__Measureme__Locat__01142BA1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Location]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Location];
GO
IF OBJECT_ID(N'[dbo].[Measurement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Measurement];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Location'
CREATE TABLE [dbo].[Location] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Latitude] varchar(50)  NOT NULL,
    [Longitude] varchar(50)  NOT NULL
);
GO

-- Creating table 'Measurement'
CREATE TABLE [dbo].[Measurement] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LocationId] int  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [Humidity] float  NULL,
    [Temperature] float  NULL,
    [Smog] float  NULL,
    [WindSpeed] float  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Location'
ALTER TABLE [dbo].[Location]
ADD CONSTRAINT [PK_Location]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Measurement'
ALTER TABLE [dbo].[Measurement]
ADD CONSTRAINT [PK_Measurement]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LocationId] in table 'Measurement'
ALTER TABLE [dbo].[Measurement]
ADD CONSTRAINT [FK__Measureme__Locat__01142BA1]
    FOREIGN KEY ([LocationId])
    REFERENCES [dbo].[Location]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Measureme__Locat__01142BA1'
CREATE INDEX [IX_FK__Measureme__Locat__01142BA1]
ON [dbo].[Measurement]
    ([LocationId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------