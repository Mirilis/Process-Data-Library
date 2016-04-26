
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/26/2016 18:20:07
-- Generated from EDMX file: C:\Users\Mirilis\Documents\Visual Studio 2015\Projects\ProcessCardDataManagerLibrary\ProcessCardDataManagerLibrary\ProcessCardDataFramework.edmx
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

IF OBJECT_ID(N'[dbo].[FK_ProcessCardDataTemplateProcessCardType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DataTemplates] DROP CONSTRAINT [FK_ProcessCardDataTemplateProcessCardType];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcessCardNamesProcessCardType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_ProcessCardNamesProcessCardType];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcessCardDataProcessCardDataTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Data] DROP CONSTRAINT [FK_ProcessCardDataProcessCardDataTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_DataRevision]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Data] DROP CONSTRAINT [FK_DataRevision];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Data] DROP CONSTRAINT [FK_DocumentData];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO
IF OBJECT_ID(N'[dbo].[Data]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Data];
GO
IF OBJECT_ID(N'[dbo].[Revisions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Revisions];
GO
IF OBJECT_ID(N'[dbo].[Templates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Templates];
GO
IF OBJECT_ID(N'[dbo].[DataTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DataTemplates];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Template_Id] int  NOT NULL
);
GO

-- Creating table 'Data'
CREATE TABLE [dbo].[Data] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] tinyint  NOT NULL,
    [Template_Id] int  NOT NULL,
    [Revision_Id] int  NOT NULL,
    [Document_Id] int  NOT NULL
);
GO

-- Creating table 'Revisions'
CREATE TABLE [dbo].[Revisions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'Templates'
CREATE TABLE [dbo].[Templates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DataTemplates'
CREATE TABLE [dbo].[DataTemplates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Template_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Data'
ALTER TABLE [dbo].[Data]
ADD CONSTRAINT [PK_Data]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Revisions'
ALTER TABLE [dbo].[Revisions]
ADD CONSTRAINT [PK_Revisions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Templates'
ALTER TABLE [dbo].[Templates]
ADD CONSTRAINT [PK_Templates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DataTemplates'
ALTER TABLE [dbo].[DataTemplates]
ADD CONSTRAINT [PK_DataTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Template_Id] in table 'DataTemplates'
ALTER TABLE [dbo].[DataTemplates]
ADD CONSTRAINT [FK_ProcessCardDataTemplateProcessCardType]
    FOREIGN KEY ([Template_Id])
    REFERENCES [dbo].[Templates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcessCardDataTemplateProcessCardType'
CREATE INDEX [IX_FK_ProcessCardDataTemplateProcessCardType]
ON [dbo].[DataTemplates]
    ([Template_Id]);
GO

-- Creating foreign key on [Template_Id] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_ProcessCardNamesProcessCardType]
    FOREIGN KEY ([Template_Id])
    REFERENCES [dbo].[Templates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcessCardNamesProcessCardType'
CREATE INDEX [IX_FK_ProcessCardNamesProcessCardType]
ON [dbo].[Documents]
    ([Template_Id]);
GO

-- Creating foreign key on [Template_Id] in table 'Data'
ALTER TABLE [dbo].[Data]
ADD CONSTRAINT [FK_ProcessCardDataProcessCardDataTemplate]
    FOREIGN KEY ([Template_Id])
    REFERENCES [dbo].[DataTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcessCardDataProcessCardDataTemplate'
CREATE INDEX [IX_FK_ProcessCardDataProcessCardDataTemplate]
ON [dbo].[Data]
    ([Template_Id]);
GO

-- Creating foreign key on [Revision_Id] in table 'Data'
ALTER TABLE [dbo].[Data]
ADD CONSTRAINT [FK_DataRevision]
    FOREIGN KEY ([Revision_Id])
    REFERENCES [dbo].[Revisions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DataRevision'
CREATE INDEX [IX_FK_DataRevision]
ON [dbo].[Data]
    ([Revision_Id]);
GO

-- Creating foreign key on [Document_Id] in table 'Data'
ALTER TABLE [dbo].[Data]
ADD CONSTRAINT [FK_DocumentData]
    FOREIGN KEY ([Document_Id])
    REFERENCES [dbo].[Documents]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentData'
CREATE INDEX [IX_FK_DocumentData]
ON [dbo].[Data]
    ([Document_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------