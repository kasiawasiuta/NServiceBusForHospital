USE [HospitalKSR]
GO
/****** Object:  Table [dbo].[Dieseases]    Script Date: 2015-06-11 15:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dieseases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Dieseases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Patients]    Script Date: 2015-06-11 15:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id] [int] NOT NULL,
	[Age] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[PasswordSalt] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PatientsDieseases]    Script Date: 2015-06-11 15:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientsDieseases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[DieseaseId] [int] NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_PatientsDieseases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Patients] ([Id], [Age], [Name], [Password], [PasswordSalt]) VALUES (2, 12, N'Kasia', N'test', N'test')
ALTER TABLE [dbo].[PatientsDieseases]  WITH CHECK ADD  CONSTRAINT [FK_PatientsDieseases_Dieseases] FOREIGN KEY([DieseaseId])
REFERENCES [dbo].[Dieseases] ([Id])
GO
ALTER TABLE [dbo].[PatientsDieseases] CHECK CONSTRAINT [FK_PatientsDieseases_Dieseases]
GO
ALTER TABLE [dbo].[PatientsDieseases]  WITH CHECK ADD  CONSTRAINT [FK_PatientsDieseases_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([Id])
GO
ALTER TABLE [dbo].[PatientsDieseases] CHECK CONSTRAINT [FK_PatientsDieseases_Patients]
GO


CREATE TABLE [dbo].[DirectorMessages](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Comment] [nvarchar](200) NOT NULL,
	[When] [datetime] NOT NULL,
	ExtDirectorMessageId [int] NOT NULL)