USE [BTPoc]
GO

/****** Object: Table [dbo].[Addresses] Script Date: 6/4/2019 5:52:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Addresses];


GO
CREATE TABLE [dbo].[Addresses] (
    [AddressId]   INT           IDENTITY (1, 1) NOT NULL,
    [ContactId]   INT           NOT NULL,
    [Street]      VARCHAR (500) NULL,
    [City]        VARCHAR (150) NULL,
    [State]       VARCHAR (100) NULL,
    [Zipcode]     VARCHAR (10)  NULL,
    [CreatedDate] DATETIME2 (7) NULL,
    [UpdatedDate] DATETIME2 (7) NULL
);


