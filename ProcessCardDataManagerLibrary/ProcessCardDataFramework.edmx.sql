
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/26/2016 14:46:12
-- Generated from EDMX file: C:\Users\ahoover\documents\visual studio 2010\Projects\ProcessCardDataManagerLibrary\ProcessCardDataManagerLibrary\ProcessCardDataFramework.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ProcessCardDataSystem];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProcessCardNamesProcessCardData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProcessCardDatas] DROP CONSTRAINT [FK_ProcessCardNamesProcessCardData];
GO
IF OBJECT_ID(N'[dbo].[FK_RevisionInformationProcessCardData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProcessCardDatas] DROP CONSTRAINT [FK_RevisionInformationProcessCardData];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcessCardDataTemplateProcessCardType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProcessCardDataTemplates] DROP CONSTRAINT [FK_ProcessCardDataTemplateProcessCardType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ProcessCardNames]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcessCardNames];
GO
IF OBJECT_ID(N'[dbo].[ProcessCardDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcessCardDatas];
GO
IF OBJECT_ID(N'[dbo].[RevisionInformations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RevisionInformations];
GO
IF OBJECT_ID(N'[dbo].[ProcessCardTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcessCardTypes];
GO
IF OBJECT_ID(N'[dbo].[ProcessCardDataTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcessCardDataTemplates];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProcessCardNames'
CREATE TABLE [dbo].[ProcessCardNames] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [pcFileName] nvarchar(max)  NOT NULL,
    [ProcessCardType_Id] int  NOT NULL
);
GO

-- Creating table 'ProcessCardDatas'
CREATE TABLE [dbo].[ProcessCardDatas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProcessCardNamesId] int  NOT NULL,
    [pcVariableName] nvarchar(max)  NOT NULL,
    [pcVariableType] nvarchar(max)  NOT NULL,
    [pcVariableValue] tinyint  NOT NULL,
    [RevisionInformationId] int  NOT NULL
);
GO

-- Creating table 'RevisionInformations'
CREATE TABLE [dbo].[RevisionInformations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RevisedBy] nvarchar(max)  NOT NULL,
    [RevisedDate] datetime  NOT NULL
);
GO

-- Creating table 'ProcessCardTypes'
CREATE TABLE [dbo].[ProcessCardTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [pcType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProcessCardDataTemplates'
CREATE TABLE [dbo].[ProcessCardDataTemplates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ptVariableName] nvarchar(max)  NOT NULL,
    [ptVariableType] nvarchar(max)  NOT NULL,
    [ProcessCardType_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProcessCardNames'
ALTER TABLE [dbo].[ProcessCardNames]
ADD CONSTRAINT [PK_ProcessCardNames]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProcessCardDatas'
ALTER TABLE [dbo].[ProcessCardDatas]
ADD CONSTRAINT [PK_ProcessCardDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RevisionInformations'
ALTER TABLE [dbo].[RevisionInformations]
ADD CONSTRAINT [PK_RevisionInformations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProcessCardTypes'
ALTER TABLE [dbo].[ProcessCardTypes]
ADD CONSTRAINT [PK_ProcessCardTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProcessCardDataTemplates'
ALTER TABLE [dbo].[ProcessCardDataTemplates]
ADD CONSTRAINT [PK_ProcessCardDataTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProcessCardNamesId] in table 'ProcessCardDatas'
ALTER TABLE [dbo].[ProcessCardDatas]
ADD CONSTRAINT [FK_ProcessCardNamesProcessCardData]
    FOREIGN KEY ([ProcessCardNamesId])
    REFERENCES [dbo].[ProcessCardNames]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcessCardNamesProcessCardData'
CREATE INDEX [IX_FK_ProcessCardNamesProcessCardData]
ON [dbo].[ProcessCardDatas]
    ([ProcessCardNamesId]);
GO

-- Creating foreign key on [RevisionInformationId] in table 'ProcessCardDatas'
ALTER TABLE [dbo].[ProcessCardDatas]
ADD CONSTRAINT [FK_RevisionInformationProcessCardData]
    FOREIGN KEY ([RevisionInformationId])
    REFERENCES [dbo].[RevisionInformations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RevisionInformationProcessCardData'
CREATE INDEX [IX_FK_RevisionInformationProcessCardData]
ON [dbo].[ProcessCardDatas]
    ([RevisionInformationId]);
GO

-- Creating foreign key on [ProcessCardType_Id] in table 'ProcessCardDataTemplates'
ALTER TABLE [dbo].[ProcessCardDataTemplates]
ADD CONSTRAINT [FK_ProcessCardDataTemplateProcessCardType]
    FOREIGN KEY ([ProcessCardType_Id])
    REFERENCES [dbo].[ProcessCardTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcessCardDataTemplateProcessCardType'
CREATE INDEX [IX_FK_ProcessCardDataTemplateProcessCardType]
ON [dbo].[ProcessCardDataTemplates]
    ([ProcessCardType_Id]);
GO

-- Creating foreign key on [ProcessCardType_Id] in table 'ProcessCardNames'
ALTER TABLE [dbo].[ProcessCardNames]
ADD CONSTRAINT [FK_ProcessCardNamesProcessCardType]
    FOREIGN KEY ([ProcessCardType_Id])
    REFERENCES [dbo].[ProcessCardTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcessCardNamesProcessCardType'
CREATE INDEX [IX_FK_ProcessCardNamesProcessCardType]
ON [dbo].[ProcessCardNames]
    ([ProcessCardType_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------