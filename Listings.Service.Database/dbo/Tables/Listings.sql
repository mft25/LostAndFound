CREATE TABLE [dbo].[Listings] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Disabled] BIT           DEFAULT ((0)) NOT NULL,
    [Text]     VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



