USE [LibraryManagementSystem]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/22/2017 12:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[__EFMigrationsHistory] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[BranchHours]    Script Date: 11/22/2017 12:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BranchHours](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BranchId] [int] NULL,
	[CloseTime] [int] NOT NULL,
	[DayOfWeek] [int] NOT NULL,
	[OpenTime] [int] NOT NULL,
 CONSTRAINT [PK_BranchHours] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[BranchHours] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[CheckoutHistories]    Script Date: 11/22/2017 12:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckoutHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CheckedIn] [datetime2](7) NULL,
	[CheckedOut] [datetime2](7) NOT NULL,
	[LibraryAssetId] [int] NOT NULL,
	[LibraryCardId] [int] NOT NULL,
 CONSTRAINT [PK_CheckoutHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[CheckoutHistories] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Checkouts]    Script Date: 11/22/2017 12:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Checkouts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LibraryAssetId] [int] NOT NULL,
	[LibraryCardId] [int] NULL,
	[Since] [datetime2](7) NOT NULL,
	[Until] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Checkouts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Checkouts] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Holds]    Script Date: 11/22/2017 12:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HoldPlaced] [datetime2](7) NOT NULL,
	[LibraryAssetId] [int] NULL,
	[LibraryCardId] [int] NULL,
 CONSTRAINT [PK_Holds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Holds] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[LibraryAssets]    Script Date: 11/22/2017 12:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryAssets](
	[Author] [nvarchar](max) NULL,
	[DeweyIndex] [nvarchar](max) NULL,
	[ISBN] [nvarchar](max) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cost] [decimal](18, 2) NOT NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[LocationId] [int] NULL,
	[NumberOfCopies] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Year] [int] NOT NULL,
	[Director] [nvarchar](max) NULL,
 CONSTRAINT [PK_LibraryAssets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[LibraryAssets] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[LibraryBranches]    Script Date: 11/22/2017 12:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryBranches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Name] [nvarchar](30) NOT NULL,
	[OpenDate] [datetime2](7) NOT NULL,
	[Telephone] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_LibraryBranches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[LibraryBranches] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[LibraryCards]    Script Date: 11/22/2017 12:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryCards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Fees] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_LibraryCards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[LibraryCards] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Patrons]    Script Date: 11/22/2017 12:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patrons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[Gender] [nvarchar](max) NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[Telephone] [nvarchar](max) NULL,
	[HomeLibraryBranchId] [int] NULL,
	[LibraryCardId] [int] NOT NULL,
 CONSTRAINT [PK_Patrons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Patrons] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 11/22/2017 12:44:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Statuses] TO  SCHEMA OWNER 
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171029171949_Initial Migration', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171029175955_Add Models v1', N'2.0.0-rtm-26452')
SET IDENTITY_INSERT [dbo].[BranchHours] ON 

INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (1, 1, 14, 1, 7)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (2, 1, 18, 2, 7)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (3, 1, 18, 3, 7)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (4, 1, 18, 4, 7)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (5, 1, 18, 5, 7)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (6, 1, 18, 6, 7)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (7, 1, 14, 7, 7)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (8, 2, 20, 1, 6)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (9, 2, 20, 2, 6)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (10, 2, 20, 3, 6)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (11, 2, 20, 4, 6)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (12, 2, 20, 5, 6)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (13, 2, 20, 6, 6)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (14, 2, 20, 7, 6)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (15, 3, 22, 1, 5)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (16, 3, 18, 2, 5)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (17, 3, 18, 3, 5)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (18, 3, 18, 4, 5)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (19, 3, 18, 5, 5)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (20, 3, 22, 6, 5)
INSERT [dbo].[BranchHours] ([Id], [BranchId], [CloseTime], [DayOfWeek], [OpenTime]) VALUES (21, 3, 22, 7, 5)
SET IDENTITY_INSERT [dbo].[BranchHours] OFF
SET IDENTITY_INSERT [dbo].[CheckoutHistories] ON 

INSERT [dbo].[CheckoutHistories] ([Id], [CheckedIn], [CheckedOut], [LibraryAssetId], [LibraryCardId]) VALUES (30, CAST(N'2017-11-07T15:37:03.5636592' AS DateTime2), CAST(N'2017-11-07T15:21:40.9114781' AS DateTime2), 3, 1)
INSERT [dbo].[CheckoutHistories] ([Id], [CheckedIn], [CheckedOut], [LibraryAssetId], [LibraryCardId]) VALUES (31, CAST(N'2017-11-07T15:37:17.3754820' AS DateTime2), CAST(N'2017-11-07T15:37:03.7116261' AS DateTime2), 3, 2)
INSERT [dbo].[CheckoutHistories] ([Id], [CheckedIn], [CheckedOut], [LibraryAssetId], [LibraryCardId]) VALUES (32, CAST(N'2017-11-07T15:37:20.2372214' AS DateTime2), CAST(N'2017-11-07T15:37:17.4056961' AS DateTime2), 3, 4)
INSERT [dbo].[CheckoutHistories] ([Id], [CheckedIn], [CheckedOut], [LibraryAssetId], [LibraryCardId]) VALUES (33, CAST(N'2017-11-07T15:38:26.6494317' AS DateTime2), CAST(N'2017-11-07T15:37:32.0593940' AS DateTime2), 3, 3)
INSERT [dbo].[CheckoutHistories] ([Id], [CheckedIn], [CheckedOut], [LibraryAssetId], [LibraryCardId]) VALUES (34, CAST(N'2017-11-07T15:38:33.5978113' AS DateTime2), CAST(N'2017-11-07T15:38:26.7946895' AS DateTime2), 3, 4)
INSERT [dbo].[CheckoutHistories] ([Id], [CheckedIn], [CheckedOut], [LibraryAssetId], [LibraryCardId]) VALUES (35, CAST(N'2017-11-07T15:38:37.2853000' AS DateTime2), CAST(N'2017-11-07T15:38:33.6291632' AS DateTime2), 3, 7)
INSERT [dbo].[CheckoutHistories] ([Id], [CheckedIn], [CheckedOut], [LibraryAssetId], [LibraryCardId]) VALUES (36, NULL, CAST(N'2017-11-07T16:51:50.9533039' AS DateTime2), 4, 8)
INSERT [dbo].[CheckoutHistories] ([Id], [CheckedIn], [CheckedOut], [LibraryAssetId], [LibraryCardId]) VALUES (1036, NULL, CAST(N'2017-11-21T14:22:34.7699812' AS DateTime2), 1, 4)
INSERT [dbo].[CheckoutHistories] ([Id], [CheckedIn], [CheckedOut], [LibraryAssetId], [LibraryCardId]) VALUES (2036, NULL, CAST(N'2017-11-21T16:28:31.2117825' AS DateTime2), 3, 4)
SET IDENTITY_INSERT [dbo].[CheckoutHistories] OFF
SET IDENTITY_INSERT [dbo].[Checkouts] ON 

INSERT [dbo].[Checkouts] ([Id], [LibraryAssetId], [LibraryCardId], [Since], [Until]) VALUES (36, 4, 8, CAST(N'2017-11-07T16:51:50.9533039' AS DateTime2), CAST(N'2017-12-07T16:51:50.9533039' AS DateTime2))
INSERT [dbo].[Checkouts] ([Id], [LibraryAssetId], [LibraryCardId], [Since], [Until]) VALUES (1036, 1, 4, CAST(N'2017-11-21T14:22:34.7699812' AS DateTime2), CAST(N'2017-12-21T14:22:34.7699812' AS DateTime2))
INSERT [dbo].[Checkouts] ([Id], [LibraryAssetId], [LibraryCardId], [Since], [Until]) VALUES (2036, 3, 4, CAST(N'2017-11-21T16:28:31.2117825' AS DateTime2), CAST(N'2017-12-21T16:28:31.2117825' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Checkouts] OFF
SET IDENTITY_INSERT [dbo].[Holds] ON 

INSERT [dbo].[Holds] ([Id], [HoldPlaced], [LibraryAssetId], [LibraryCardId]) VALUES (26, CAST(N'2017-11-07T16:52:01.8257889' AS DateTime2), 4, 4)
INSERT [dbo].[Holds] ([Id], [HoldPlaced], [LibraryAssetId], [LibraryCardId]) VALUES (1026, CAST(N'2017-11-21T14:22:44.1997543' AS DateTime2), 1, 2)
SET IDENTITY_INSERT [dbo].[Holds] OFF
SET IDENTITY_INSERT [dbo].[LibraryAssets] ON 

INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Jane Austen', N'823.123', N'9781519202987', 1, CAST(18.00 AS Decimal(18, 2)), N'Book', N'/images/emma.png', 2, 1, 1, N'Emma', 1815, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Charlotte Brontë', N'822.133', N'9781519133977', 2, CAST(18.00 AS Decimal(18, 2)), N'Book', N'/images/janeeyre.png', 3, 1, 2, N'Jane Eyre', 1847, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Herman Melville', N'821.153', N'9780746062760', 3, CAST(12.99 AS Decimal(18, 2)), N'Book', N'/images/mobydick.png', 2, 1, 1, N'Moby Dick', 1851, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'James Joyce', N'822.556', N'9788854139343', 4, CAST(24.00 AS Decimal(18, 2)), N'Book', N'/images/ulysses.png', 2, 3, 1, N'Ulysses', 1922, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Plato', N'820.119', N'9780758320209', 5, CAST(11.00 AS Decimal(18, 2)), N'Book', N'/images/republic.png', 3, 2, 2, N'Republic', -380, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Bram Stoker', N'821.526', N'9781623750282', 6, CAST(18.00 AS Decimal(18, 2)), N'Book', N'/images/dracula.png', 3, 4, 2, N'Dracula', 1897, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Don Delillo', N'822.436', N'9780670803736', 7, CAST(12.99 AS Decimal(18, 2)), N'Book', N'/images/default.png', 2, 1, 2, N'White Noise', 1985, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'James Baldwin', N'821.325', N'9780552084574', 8, CAST(16.00 AS Decimal(18, 2)), N'Book', N'/images/default.png', 2, 2, 2, N'Another Country', 1962, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Virginia Woolf', N'822.888', N'9781904919582', 9, CAST(11.00 AS Decimal(18, 2)), N'Book', N'/images/thewaves.png', 1, 1, 2, N'The Waves', 1931, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Alice Walker', N'820.298', N'9780151191543', 10, CAST(11.99 AS Decimal(18, 2)), N'Book', N'/images/default.png', 1, 2, 2, N'The Color Purple', 1982, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Gabriel García Márquez', N'821.544', N'9789631420494', 11, CAST(12.50 AS Decimal(18, 2)), N'Book', N'/images/default.png', 1, 1, 2, N'One Hundred Years of Solitude', 1967, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Alice Monroe', N'821.444', N'9788702163452', 12, CAST(22.00 AS Decimal(18, 2)), N'Book', N'/images/default.png', 1, 1, 2, N'Friend of My Youth', 1990, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Virginia Woolf', N'820.111', N'9780795310522', 13, CAST(13.50 AS Decimal(18, 2)), N'Book', N'/images/tothelighthouse.png', 1, 5, 2, N'To the Lighthouse', 1927, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (N'Virginia Woolf', N'821.254', N'9785457626126', 14, CAST(15.99 AS Decimal(18, 2)), N'Book', N'/images/mrsdalloway.png', 3, 1, 2, N'Mrs Dalloway', 1925, NULL)
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (NULL, NULL, NULL, 15, CAST(24.00 AS Decimal(18, 2)), N'Video', N'/images/default.png', 1, 1, 2, N'Blue Velvet', 1986, N'David Lynch')
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (NULL, NULL, NULL, 16, CAST(24.00 AS Decimal(18, 2)), N'Video', N'/images/default.png', 1, 1, 2, N'Trois Coleurs: Rouge', 1994, N'Krzysztof Kieslowski')
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (NULL, NULL, NULL, 17, CAST(30.00 AS Decimal(18, 2)), N'Video', N'/images/default.png', 1, 1, 2, N'Citizen Kane', 1941, N'Orson Welles')
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (NULL, NULL, NULL, 18, CAST(28.00 AS Decimal(18, 2)), N'Video', N'/images/default.png', 2, 1, 2, N'Spirited Away', 2002, N'Hayao Miyazaki')
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (NULL, NULL, NULL, 19, CAST(23.00 AS Decimal(18, 2)), N'Video', N'/images/default.png', 2, 1, 2, N'The Departed', 2006, N'Martin Scorsese')
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (NULL, NULL, NULL, 20, CAST(17.99 AS Decimal(18, 2)), N'Video', N'/images/default.png', 2, 1, 2, N'Taxi Driver', 1976, N'Martin Scorsese')
INSERT [dbo].[LibraryAssets] ([Author], [DeweyIndex], [ISBN], [Id], [Cost], [Discriminator], [ImageUrl], [LocationId], [NumberOfCopies], [StatusId], [Title], [Year], [Director]) VALUES (NULL, NULL, NULL, 21, CAST(18.00 AS Decimal(18, 2)), N'Video', N'/images/default.png', 3, 1, 2, N'Casablanca', 1943, N'Michael Curtiz')
SET IDENTITY_INSERT [dbo].[LibraryAssets] OFF
SET IDENTITY_INSERT [dbo].[LibraryBranches] ON 

INSERT [dbo].[LibraryBranches] ([Id], [Address], [Description], [ImageUrl], [Name], [OpenDate], [Telephone]) VALUES (1, N'88 Lakeshore Dr', N'The oldest library branch in Lakeview, the Lake Shore Branch was opened in 1975. Patrons of all ages enjoy the wide selection of literature available at Lake Shore library. The coffee shop is open during library hours of operation.', N'/images/branches/1.png', N'Lake Shore Branch', CAST(N'1975-05-13T00:00:00.0000000' AS DateTime2), N'555-1234')
INSERT [dbo].[LibraryBranches] ([Id], [Address], [Description], [ImageUrl], [Name], [OpenDate], [Telephone]) VALUES (2, N'123 Skyline Dr', N'The Mountain View branch contains the largest collection of technical and language learning books in the region. This branch is within walking distance to the University campus', N'/images/branches/2.png', N'Mountain View Branch', CAST(N'1998-06-01T00:00:00.0000000' AS DateTime2), N'555-1235')
INSERT [dbo].[LibraryBranches] ([Id], [Address], [Description], [ImageUrl], [Name], [OpenDate], [Telephone]) VALUES (3, N'540 Inventors Circle', N'The newest Lakeview Library System branch, Pleasant Hill has high-speed wireless access for all patrons and hosts Chess Club every Monday and Wednesday evening at 6 PM.', N'/images/branches/3.png', N'Pleasant Hill Branch', CAST(N'2017-09-20T00:00:00.0000000' AS DateTime2), N'555-1236')
SET IDENTITY_INSERT [dbo].[LibraryBranches] OFF
SET IDENTITY_INSERT [dbo].[LibraryCards] ON 

INSERT [dbo].[LibraryCards] ([Id], [Created], [Fees]) VALUES (1, CAST(N'2017-06-20T00:00:00.0000000' AS DateTime2), CAST(12.00 AS Decimal(18, 2)))
INSERT [dbo].[LibraryCards] ([Id], [Created], [Fees]) VALUES (2, CAST(N'2017-06-20T00:00:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[LibraryCards] ([Id], [Created], [Fees]) VALUES (3, CAST(N'2017-06-21T00:00:00.0000000' AS DateTime2), CAST(0.50 AS Decimal(18, 2)))
INSERT [dbo].[LibraryCards] ([Id], [Created], [Fees]) VALUES (4, CAST(N'2017-06-21T00:00:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[LibraryCards] ([Id], [Created], [Fees]) VALUES (5, CAST(N'2017-06-21T00:00:00.0000000' AS DateTime2), CAST(3.50 AS Decimal(18, 2)))
INSERT [dbo].[LibraryCards] ([Id], [Created], [Fees]) VALUES (6, CAST(N'2017-06-23T00:00:00.0000000' AS DateTime2), CAST(1.50 AS Decimal(18, 2)))
INSERT [dbo].[LibraryCards] ([Id], [Created], [Fees]) VALUES (7, CAST(N'2017-06-23T00:00:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[LibraryCards] ([Id], [Created], [Fees]) VALUES (8, CAST(N'2017-06-23T00:00:00.0000000' AS DateTime2), CAST(8.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[LibraryCards] OFF
SET IDENTITY_INSERT [dbo].[Patrons] ON 

INSERT [dbo].[Patrons] ([Id], [Address], [DateOfBirth], [FirstName], [Gender], [LastName], [Telephone], [HomeLibraryBranchId], [LibraryCardId]) VALUES (1, N'165 Peace St', CAST(N'1986-07-10T00:00:00.0000000' AS DateTime2), N'Jane', NULL, N'Patterson', N'555-1234', 1, 1)
INSERT [dbo].[Patrons] ([Id], [Address], [DateOfBirth], [FirstName], [Gender], [LastName], [Telephone], [HomeLibraryBranchId], [LibraryCardId]) VALUES (2, N'324 Shadow Ln', CAST(N'1984-03-12T00:00:00.0000000' AS DateTime2), N'Margaret', NULL, N'Smith', N'555-7785', 2, 2)
INSERT [dbo].[Patrons] ([Id], [Address], [DateOfBirth], [FirstName], [Gender], [LastName], [Telephone], [HomeLibraryBranchId], [LibraryCardId]) VALUES (3, N'18 Stone Cir', CAST(N'1956-02-10T00:00:00.0000000' AS DateTime2), N'Tomas', NULL, N'Galloway', N'555-3467', 2, 3)
INSERT [dbo].[Patrons] ([Id], [Address], [DateOfBirth], [FirstName], [Gender], [LastName], [Telephone], [HomeLibraryBranchId], [LibraryCardId]) VALUES (4, N'246 Jennifer St', CAST(N'1997-01-17T00:00:00.0000000' AS DateTime2), N'Mary', NULL, N'Li', N'555-1223', 3, 4)
INSERT [dbo].[Patrons] ([Id], [Address], [DateOfBirth], [FirstName], [Gender], [LastName], [Telephone], [HomeLibraryBranchId], [LibraryCardId]) VALUES (5, N'1465 Williamson St', CAST(N'1952-09-16T00:00:00.0000000' AS DateTime2), N'Dan', NULL, N'Carter', N'555-8884', 3, 5)
INSERT [dbo].[Patrons] ([Id], [Address], [DateOfBirth], [FirstName], [Gender], [LastName], [Telephone], [HomeLibraryBranchId], [LibraryCardId]) VALUES (6, N'785 Park Ave', CAST(N'1994-03-24T00:00:00.0000000' AS DateTime2), N'Aruna', NULL, N'Adhiban', N'555-9988', 3, 6)
INSERT [dbo].[Patrons] ([Id], [Address], [DateOfBirth], [FirstName], [Gender], [LastName], [Telephone], [HomeLibraryBranchId], [LibraryCardId]) VALUES (7, N'5654 Main St', CAST(N'2001-11-23T00:00:00.0000000' AS DateTime2), N'Raj', NULL, N'Prasad', N'555-7894', 1, 7)
INSERT [dbo].[Patrons] ([Id], [Address], [DateOfBirth], [FirstName], [Gender], [LastName], [Telephone], [HomeLibraryBranchId], [LibraryCardId]) VALUES (8, N'1352 Bicycle Ct', CAST(N'1981-10-16T00:00:00.0000000' AS DateTime2), N'Tatyana', NULL, N'Ponomaryova', N'555-4568', 3, 8)
SET IDENTITY_INSERT [dbo].[Patrons] OFF
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([Id], [Description], [Name]) VALUES (1, N'A library asset that has been checked out', N'Checked Out')
INSERT [dbo].[Statuses] ([Id], [Description], [Name]) VALUES (2, N'A library asset that is available for loan', N'Available')
INSERT [dbo].[Statuses] ([Id], [Description], [Name]) VALUES (3, N'A library asset that has been lost', N'Lost')
INSERT [dbo].[Statuses] ([Id], [Description], [Name]) VALUES (4, N'A library asset that has been placed On Hold for loan', N'On Hold')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
ALTER TABLE [dbo].[Patrons] ADD  DEFAULT ((0)) FOR [LibraryCardId]
GO
ALTER TABLE [dbo].[BranchHours]  WITH CHECK ADD  CONSTRAINT [FK_BranchHours_LibraryBranches_BranchId] FOREIGN KEY([BranchId])
REFERENCES [dbo].[LibraryBranches] ([Id])
GO
ALTER TABLE [dbo].[BranchHours] CHECK CONSTRAINT [FK_BranchHours_LibraryBranches_BranchId]
GO
ALTER TABLE [dbo].[CheckoutHistories]  WITH CHECK ADD  CONSTRAINT [FK_CheckoutHistories_LibraryAssets_LibraryAssetId] FOREIGN KEY([LibraryAssetId])
REFERENCES [dbo].[LibraryAssets] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CheckoutHistories] CHECK CONSTRAINT [FK_CheckoutHistories_LibraryAssets_LibraryAssetId]
GO
ALTER TABLE [dbo].[CheckoutHistories]  WITH CHECK ADD  CONSTRAINT [FK_CheckoutHistories_LibraryCards_LibraryCardId] FOREIGN KEY([LibraryCardId])
REFERENCES [dbo].[LibraryCards] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CheckoutHistories] CHECK CONSTRAINT [FK_CheckoutHistories_LibraryCards_LibraryCardId]
GO
ALTER TABLE [dbo].[Checkouts]  WITH CHECK ADD  CONSTRAINT [FK_Checkouts_LibraryAssets_LibraryAssetId] FOREIGN KEY([LibraryAssetId])
REFERENCES [dbo].[LibraryAssets] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Checkouts] CHECK CONSTRAINT [FK_Checkouts_LibraryAssets_LibraryAssetId]
GO
ALTER TABLE [dbo].[Checkouts]  WITH CHECK ADD  CONSTRAINT [FK_Checkouts_LibraryCards_LibraryCardId] FOREIGN KEY([LibraryCardId])
REFERENCES [dbo].[LibraryCards] ([Id])
GO
ALTER TABLE [dbo].[Checkouts] CHECK CONSTRAINT [FK_Checkouts_LibraryCards_LibraryCardId]
GO
ALTER TABLE [dbo].[Holds]  WITH CHECK ADD  CONSTRAINT [FK_Holds_LibraryAssets_LibraryAssetId] FOREIGN KEY([LibraryAssetId])
REFERENCES [dbo].[LibraryAssets] ([Id])
GO
ALTER TABLE [dbo].[Holds] CHECK CONSTRAINT [FK_Holds_LibraryAssets_LibraryAssetId]
GO
ALTER TABLE [dbo].[Holds]  WITH CHECK ADD  CONSTRAINT [FK_Holds_LibraryCards_LibraryCardId] FOREIGN KEY([LibraryCardId])
REFERENCES [dbo].[LibraryCards] ([Id])
GO
ALTER TABLE [dbo].[Holds] CHECK CONSTRAINT [FK_Holds_LibraryCards_LibraryCardId]
GO
ALTER TABLE [dbo].[LibraryAssets]  WITH CHECK ADD  CONSTRAINT [FK_LibraryAssets_LibraryBranches_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[LibraryBranches] ([Id])
GO
ALTER TABLE [dbo].[LibraryAssets] CHECK CONSTRAINT [FK_LibraryAssets_LibraryBranches_LocationId]
GO
ALTER TABLE [dbo].[LibraryAssets]  WITH CHECK ADD  CONSTRAINT [FK_LibraryAssets_Statuses_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LibraryAssets] CHECK CONSTRAINT [FK_LibraryAssets_Statuses_StatusId]
GO
ALTER TABLE [dbo].[Patrons]  WITH CHECK ADD  CONSTRAINT [FK_Patrons_LibraryBranches_HomeLibraryBranchId] FOREIGN KEY([HomeLibraryBranchId])
REFERENCES [dbo].[LibraryBranches] ([Id])
GO
ALTER TABLE [dbo].[Patrons] CHECK CONSTRAINT [FK_Patrons_LibraryBranches_HomeLibraryBranchId]
GO
ALTER TABLE [dbo].[Patrons]  WITH CHECK ADD  CONSTRAINT [FK_Patrons_LibraryCards_LibraryCardId] FOREIGN KEY([LibraryCardId])
REFERENCES [dbo].[LibraryCards] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Patrons] CHECK CONSTRAINT [FK_Patrons_LibraryCards_LibraryCardId]
GO
