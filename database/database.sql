USE [TimesheetPlayground]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 2021-09-09 10:43:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Timesheet]    Script Date: 2021-09-09 10:43:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timesheet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[StartDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Timesheet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2021-09-09 10:43:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkedHours]    Script Date: 2021-09-09 10:43:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkedHours](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TimesheetId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Hours] [int] NOT NULL,
	[WorkDay] [int] NOT NULL,
 CONSTRAINT [PK_WorkedHours] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([Id], [Name]) VALUES (1, N'FirstProject')
INSERT [dbo].[Projects] ([Id], [Name]) VALUES (2, N'SecondProject')
INSERT [dbo].[Projects] ([Id], [Name]) VALUES (6, N'ThirdProject')
INSERT [dbo].[Projects] ([Id], [Name]) VALUES (11, N'New project')
SET IDENTITY_INSERT [dbo].[Projects] OFF
GO
SET IDENTITY_INSERT [dbo].[Timesheet] ON 

INSERT [dbo].[Timesheet] ([Id], [UserId], [Status], [StartDate]) VALUES (30, 1, N'Submitted', CAST(N'2021-03-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Timesheet] ([Id], [UserId], [Status], [StartDate]) VALUES (33, 1, N'Approved', CAST(N'2021-04-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Timesheet] ([Id], [UserId], [Status], [StartDate]) VALUES (34, 1, N'Draft', CAST(N'2021-06-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Timesheet] ([Id], [UserId], [Status], [StartDate]) VALUES (35, 1, N'Draft', CAST(N'2021-05-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Timesheet] ([Id], [UserId], [Status], [StartDate]) VALUES (36, 11, N'Submitted', CAST(N'2021-03-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Timesheet] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [Password], [Role]) VALUES (1, N'User', N'111', N'Employee')
INSERT [dbo].[Users] ([Id], [Name], [Password], [Role]) VALUES (2, N'Manager', N'111', N'Manager')
INSERT [dbo].[Users] ([Id], [Name], [Password], [Role]) VALUES (11, N'User2', N'111', N'Employee')
INSERT [dbo].[Users] ([Id], [Name], [Password], [Role]) VALUES (12, N'User3', N'111', N'Employee')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [UC_TimesheetUserIdStartDate]    Script Date: 2021-09-09 10:43:40 AM ******/
ALTER TABLE [dbo].[Timesheet] ADD  CONSTRAINT [UC_TimesheetUserIdStartDate] UNIQUE NONCLUSTERED 
(
	[UserId] ASC,
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Timesheet]  WITH CHECK ADD  CONSTRAINT [FK_Timesheet_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Timesheet] CHECK CONSTRAINT [FK_Timesheet_Users]
GO
ALTER TABLE [dbo].[WorkedHours]  WITH CHECK ADD  CONSTRAINT [FK_WorkedHours_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[WorkedHours] CHECK CONSTRAINT [FK_WorkedHours_Projects]
GO
ALTER TABLE [dbo].[WorkedHours]  WITH CHECK ADD  CONSTRAINT [FK_WorkedHours_Timesheet] FOREIGN KEY([TimesheetId])
REFERENCES [dbo].[Timesheet] ([Id])
GO
ALTER TABLE [dbo].[WorkedHours] CHECK CONSTRAINT [FK_WorkedHours_Timesheet]
GO
