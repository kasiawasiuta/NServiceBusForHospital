USE [HospitalKSR]
GO
/****** Object:  Table [dbo].[Alergies]    Script Date: 2015-06-08 22:00:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alergies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Alergies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Patient]    Script Date: 2015-06-08 22:00:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[PasswordSalt] [nvarchar](200) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PatientAlergies]    Script Date: 2015-06-08 22:00:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientAlergies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[AlergyId] [int] NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[WhenDiagnosed] [datetime] NOT NULL,
 CONSTRAINT [PK_PatientAlergies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Alergies] ON 

INSERT [dbo].[Alergies] ([Id], [Name]) VALUES (1, N'alergy1')
INSERT [dbo].[Alergies] ([Id], [Name]) VALUES (2, N'alergy2')
INSERT [dbo].[Alergies] ([Id], [Name]) VALUES (3, N'alergy3')
SET IDENTITY_INSERT [dbo].[Alergies] OFF
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([Id], [Name], [Age], [Password], [PasswordSalt]) VALUES (1, N'joanna12345', 0, N'1/NvxpASl7ygcShtpgrSC2UHz3NjobugG218Jk88DNfop0xkcJEcLqfFBtrQsOtyLKbxC/W9THfyLBYDKVfVmg==', N'100000.ifvD5xi9Es2ER7l1JghYUovogmsjzhDMuMiovk+m+J+dtA==')
SET IDENTITY_INSERT [dbo].[Patient] OFF
SET IDENTITY_INSERT [dbo].[PatientAlergies] ON 

INSERT [dbo].[PatientAlergies] ([Id], [PatientId], [AlergyId], [Description], [WhenDiagnosed]) VALUES (1, 1, 3, N'test', CAST(0x0000A4B10168D66B AS DateTime))
SET IDENTITY_INSERT [dbo].[PatientAlergies] OFF
ALTER TABLE [dbo].[PatientAlergies]  WITH CHECK ADD  CONSTRAINT [FK_PatientAlergies_Alergies] FOREIGN KEY([AlergyId])
REFERENCES [dbo].[Alergies] ([Id])
GO
ALTER TABLE [dbo].[PatientAlergies] CHECK CONSTRAINT [FK_PatientAlergies_Alergies]
GO
ALTER TABLE [dbo].[PatientAlergies]  WITH CHECK ADD  CONSTRAINT [FK_PatientAlergies_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO
ALTER TABLE [dbo].[PatientAlergies] CHECK CONSTRAINT [FK_PatientAlergies_Patient]
GO
