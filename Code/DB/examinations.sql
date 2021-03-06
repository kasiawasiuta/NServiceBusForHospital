USE [HospitalKSR]
GO
/****** Object:  Table [dbo].[Examinations]    Script Date: 2015-06-21 18:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Examinations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientDieseaseId] [int] NOT NULL,
	[ExaminationType] [int] NOT NULL,
	[LogType] [int] NOT NULL,
	[Comment] [nvarchar](200) NOT NULL,
	[WhenExamined] [datetime] NOT NULL,
 CONSTRAINT [PK_Examinations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Examinations] ADD  CONSTRAINT [DF_Examinations_WhenExamined]  DEFAULT (getdate()) FOR [WhenExamined]
GO
ALTER TABLE [dbo].[Examinations]  WITH CHECK ADD  CONSTRAINT [FK_Examinations_PatientsDieseases] FOREIGN KEY([PatientDieseaseId])
REFERENCES [dbo].[PatientsDieseases] ([Id])
GO
ALTER TABLE [dbo].[Examinations] CHECK CONSTRAINT [FK_Examinations_PatientsDieseases]
GO
