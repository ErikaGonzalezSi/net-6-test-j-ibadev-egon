USE [master]
GO
/****** Object:  Database [EERP]    Script Date: 29/08/2022 12:23:32 p. m. ******/
CREATE DATABASE [EERP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EERP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\EERP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EERP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\EERP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT

USE [EERP]
GO
/****** Object:  Table [dbo].[Survey]    Script Date: 29/08/2022 12:23:32 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey](
	[IdSurvey] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[IdSurveyType] [int] NOT NULL,
	[Template] [varchar](max) NOT NULL,
	[Disable] [bit] NOT NULL,
	[Published] [bit] NOT NULL,
 CONSTRAINT [PK_Survey] PRIMARY KEY CLUSTERED 
(
	[IdSurvey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SurveyType]    Script Date: 29/08/2022 12:23:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyType](
	[IdSurveyType] [int] IDENTITY(1,1) NOT NULL,
	[NameTypeSurvey] [varchar](20) NOT NULL,
 CONSTRAINT [PK_SurveyType] PRIMARY KEY CLUSTERED 
(
	[IdSurveyType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SurveyType] ON 

INSERT [dbo].[SurveyType] ([IdSurveyType], [NameTypeSurvey]) VALUES (3, N'Attendance')
INSERT [dbo].[SurveyType] ([IdSurveyType], [NameTypeSurvey]) VALUES (4, N'Case')
INSERT [dbo].[SurveyType] ([IdSurveyType], [NameTypeSurvey]) VALUES (2, N'Check list')
INSERT [dbo].[SurveyType] ([IdSurveyType], [NameTypeSurvey]) VALUES (1, N'Survey')
SET IDENTITY_INSERT [dbo].[SurveyType] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Unique_Title]    Script Date: 29/08/2022 12:23:33 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Unique_Title] ON [dbo].[Survey]
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Unique_NameType]    Script Date: 29/08/2022 12:23:33 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Unique_NameType] ON [dbo].[SurveyType]
(
	[NameTypeSurvey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Survey] ADD  CONSTRAINT [DF_Survey_Disable]  DEFAULT ((0)) FOR [Disable]
GO
ALTER TABLE [dbo].[Survey] ADD  CONSTRAINT [DF_Survey_Published]  DEFAULT ((0)) FOR [Published]
GO
ALTER TABLE [dbo].[Survey]  WITH CHECK ADD  CONSTRAINT [FK_Survey_SurveyType] FOREIGN KEY([IdSurveyType])
REFERENCES [dbo].[SurveyType] ([IdSurveyType])
GO
ALTER TABLE [dbo].[Survey] CHECK CONSTRAINT [FK_Survey_SurveyType]
GO
USE [master]
GO
ALTER DATABASE [EERP] SET  READ_WRITE 
GO
