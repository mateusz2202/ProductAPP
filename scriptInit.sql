
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Product' AND type = 'U')
BEGIN
	CREATE TABLE [dbo].[Product](
		[Id] [int] PRIMARY KEY IDENTITY,
		[Code] [nvarchar](500) NOT NULL,
		[Name] [nvarchar](800) NOT NULL,
		[Price] [decimal](28, 5) NOT NULL,
		[CreatedBy] varchar(128) NOT NULL,
		[CreatedOn] datetime2 NOT NULL,
		[ModifiedBy] varchar(128)  NULL,
		[ModifiedOn] datetime2  NULL
	)
END
