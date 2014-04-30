USE [MicroLite]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[GivenName] [varchar](50) NOT NULL,
	[FamilyName] [varchar](50) NOT NULL,
	[PostCode] [varchar](50) NOT NULL,
	[Data] [varchar](max) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

INSERT INTO [MicroLite].[dbo].[Customers]
           ([GivenName]
           ,[FamilyName]
           ,[PostCode]
           ,[Data])
     VALUES
           ('David'
           ,'Steele'
           ,'PE1 3DU'
           ,'{"json_class":"Customer","CustomerId":1,"GivenName":"David","FamilyName":"Steele","PostCode":"PE1 3DU","DateOfBirth":"1950/08/11","AddressLine1":"11 Lynton Road","AddressLine2":"","TownCity":"Peterborough"}')

           ,('Dan'
           ,'Hawkeswood'
           ,'PE1 3DU'
           ,'{"json_class":"Customer","CustomerId":2,"GivenName":"Dan","FamilyName":"Hawkeswood","PostCode":"PE1 3DU","DateOfBirth":"1985/05/14","AddressLine1":"11 Lynton Road","AddressLine2":"","TownCity":"Peterborough"}')
           
           ,('Michael'
           ,'Steele'
           ,'CW11 9BJ'
           ,'{"json_class":"Customer","CustomerId":3,"GivenName":"Michael","FamilyName":"Steele","PostCode":"CW11 9BJ","DateOfBirth":"1955/06/20","AddressLine1":"73 Queen''s Drive","AddressLine2":"","TownCity":"Sandbach"}')
           
           ,('Dan'
           ,'Steele'
           ,'CW11 9BJ'
           ,'{"json_class":"Customer","CustomerId":4,"GivenName":"Dan","FamilyName":"Steele","PostCode":"CW11 9BJ","DateOfBirth":"1980/03/03","AddressLine1":"73 Queen''s Drive","AddressLine2":"","TownCity":"Sandbach"}')
GO


