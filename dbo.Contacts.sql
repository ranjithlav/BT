USE [BTPoc]
GO

/****** Object: Table [dbo].[Contacts] Script Date: 6/4/2019 5:53:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Contacts];


GO
CREATE TABLE [dbo].[Contacts] (
    [ContactId]    INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]    VARCHAR (150) NULL,
    [LastName]     VARCHAR (150) NULL,
    [BusinessName] VARCHAR (500) NULL,
    [Type]         VARCHAR (10)  NULL,
    [CreatedDate]  DATETIME2 (7) NULL,
    [UpdatedDate]  DATETIME2 (7) NULL
);


