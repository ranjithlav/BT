USE [BTPoc]
GO

/****** Object: Table [dbo].[Addresses] Script Date: 6/5/2019 12:23:49 AM ******/
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


GO
CREATE NONCLUSTERED INDEX [idx_Addresses_Zipcode]
    ON [dbo].[Addresses]([Zipcode] ASC);