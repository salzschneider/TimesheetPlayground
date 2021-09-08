USE [TimesheetPlayground]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 2021-09-08 9:54:10 PM ******/
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
/****** Object:  Table [dbo].[Timesheet]    Script Date: 2021-09-08 9:54:10 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 2021-09-08 9:54:10 PM ******/
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
/****** Object:  Table [dbo].[WorkedHours]    Script Date: 2021-09-08 9:54:10 PM ******/
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

INSERT [dbo].[Projects] ([Id], [Name]) VALUES (1, N'FirstProject21')
INSERT [dbo].[Projects] ([Id], [Name]) VALUES (2, N'SecondProject1')
INSERT [dbo].[Projects] ([Id], [Name]) VALUES (6, N'ThirdProject1')
SET IDENTITY_INSERT [dbo].[Projects] OFF
GO
SET IDENTITY_INSERT [dbo].[Timesheet] ON 

INSERT [dbo].[Timesheet] ([Id], [UserId], [Status], [StartDate]) VALUES (30, 1, N'Submitted', CAST(N'2021-03-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Timesheet] ([Id], [UserId], [Status], [StartDate]) VALUES (33, 1, N'Draft', CAST(N'2021-04-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Timesheet] ([Id], [UserId], [Status], [StartDate]) VALUES (34, 1, N'Approved', CAST(N'2021-06-01T00:00:00.000' AS DateTime))
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
SET IDENTITY_INSERT [dbo].[WorkedHours] ON 

INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (71, 34, 1, 11, 1)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (72, 34, 1, 2, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (73, 34, 1, 1, 3)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (74, 34, 1, 1, 4)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (75, 34, 2, 1, 1)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (76, 34, 2, 2, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (77, 34, 2, 2, 3)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (78, 34, 2, 1, 4)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (79, 34, 1, 0, 7)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (80, 34, 1, 0, 8)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (81, 34, 1, 0, 9)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (82, 34, 1, 0, 10)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (83, 34, 1, 0, 11)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (84, 34, 2, 0, 7)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (85, 34, 2, 0, 8)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (86, 34, 2, 0, 9)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (87, 34, 2, 0, 10)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (88, 34, 2, 0, 11)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (89, 36, 1, 0, 1)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (90, 36, 1, 0, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (91, 36, 1, 0, 3)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (92, 36, 1, 0, 4)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (93, 36, 1, 0, 5)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (94, 36, 2, 0, 1)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (95, 36, 2, 0, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (96, 36, 2, 0, 3)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (97, 36, 2, 0, 4)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (98, 36, 2, 0, 5)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (99, 36, 6, 0, 1)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (100, 36, 6, 0, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (101, 36, 6, 0, 3)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (102, 36, 6, 0, 4)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (103, 36, 6, 0, 5)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (104, 34, 6, 2, 1)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (105, 34, 6, 2, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (106, 34, 6, 2, 3)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (107, 34, 6, 0, 4)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (108, 34, 6, 0, 7)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (109, 34, 6, 0, 8)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (110, 34, 6, 0, 9)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (111, 34, 6, 0, 10)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (112, 34, 6, 0, 11)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (113, 34, 1, 0, 28)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (114, 34, 1, 0, 29)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (115, 34, 1, 0, 30)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (116, 34, 2, 0, 28)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (117, 34, 2, 0, 29)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (118, 34, 2, 0, 30)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (119, 34, 6, 0, 28)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (120, 34, 6, 0, 29)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (121, 34, 6, 0, 30)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (122, 33, 1, 0, 26)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (123, 33, 1, 0, 27)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (124, 33, 1, 0, 28)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (125, 33, 1, 0, 29)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (126, 33, 1, 0, 30)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (127, 33, 2, 0, 26)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (128, 33, 2, 0, 27)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (129, 33, 2, 0, 28)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (130, 33, 2, 0, 29)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (131, 33, 2, 0, 30)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (132, 33, 6, 0, 26)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (133, 33, 6, 0, 27)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (134, 33, 6, 0, 28)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (135, 33, 6, 0, 29)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (136, 33, 6, 0, 30)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (137, 30, 1, 0, 1)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (138, 30, 1, 0, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (139, 30, 1, 0, 3)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (140, 30, 1, 0, 4)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (141, 30, 1, 0, 5)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (142, 30, 2, 0, 1)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (143, 30, 2, 0, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (144, 30, 2, 0, 3)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (145, 30, 2, 0, 4)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (146, 30, 2, 0, 5)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (147, 30, 6, 0, 1)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (148, 30, 6, 0, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (149, 30, 6, 0, 3)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (150, 30, 6, 0, 4)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (151, 30, 6, 0, 5)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (152, 33, 1, 1, 5)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (153, 33, 1, 0, 6)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (154, 33, 1, 0, 7)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (155, 33, 1, 0, 8)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (156, 33, 1, 0, 9)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (157, 33, 2, 0, 5)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (158, 33, 2, 0, 6)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (159, 33, 2, 0, 7)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (160, 33, 2, 0, 8)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (161, 33, 2, 0, 9)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (162, 33, 6, 0, 5)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (163, 33, 6, 0, 6)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (164, 33, 6, 0, 7)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (165, 33, 6, 0, 8)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (166, 33, 6, 0, 9)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (167, 33, 1, 12, 1)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (168, 33, 1, 0, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (169, 33, 2, 2, 1)
GO
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (170, 33, 2, 0, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (171, 33, 6, 0, 1)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (172, 33, 6, 0, 2)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (173, 33, 1, 0, 12)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (174, 33, 1, 0, 13)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (175, 33, 1, 0, 14)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (176, 33, 1, 0, 15)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (177, 33, 1, 0, 16)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (178, 33, 2, 0, 12)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (179, 33, 2, 0, 13)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (180, 33, 2, 0, 14)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (181, 33, 2, 0, 15)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (182, 33, 2, 0, 16)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (183, 33, 6, 0, 12)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (184, 33, 6, 0, 13)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (185, 33, 6, 0, 14)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (186, 33, 6, 0, 15)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (187, 33, 6, 0, 16)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (188, 30, 1, 0, 22)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (189, 30, 1, 0, 23)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (190, 30, 1, 0, 24)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (191, 30, 1, 0, 25)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (192, 30, 1, 0, 26)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (193, 30, 2, 0, 22)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (194, 30, 2, 0, 23)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (195, 30, 2, 0, 24)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (196, 30, 2, 0, 25)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (197, 30, 2, 0, 26)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (198, 30, 6, 0, 22)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (199, 30, 6, 0, 23)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (200, 30, 6, 0, 24)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (201, 30, 6, 0, 25)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (202, 30, 6, 0, 26)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (203, 35, 1, 0, 24)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (204, 35, 1, 0, 25)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (205, 35, 1, 0, 26)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (206, 35, 1, 0, 27)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (207, 35, 1, 0, 28)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (208, 35, 2, 0, 24)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (209, 35, 2, 0, 25)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (210, 35, 2, 0, 26)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (211, 35, 2, 0, 27)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (212, 35, 2, 0, 28)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (213, 35, 6, 0, 24)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (214, 35, 6, 0, 25)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (215, 35, 6, 0, 26)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (216, 35, 6, 0, 27)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (217, 35, 6, 0, 28)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (218, 35, 1, 0, 31)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (219, 35, 2, 0, 31)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (220, 35, 6, 0, 31)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (221, 30, 1, 0, 8)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (222, 30, 1, 0, 9)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (223, 30, 1, 0, 10)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (224, 30, 1, 0, 11)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (225, 30, 1, 0, 12)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (226, 30, 2, 0, 8)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (227, 30, 2, 0, 9)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (228, 30, 2, 0, 10)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (229, 30, 2, 0, 11)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (230, 30, 2, 0, 12)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (231, 30, 6, 0, 8)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (232, 30, 6, 0, 9)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (233, 30, 6, 0, 10)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (234, 30, 6, 0, 11)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (235, 30, 6, 0, 12)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (236, 33, 1, 0, 19)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (237, 33, 1, 0, 20)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (238, 33, 1, 0, 21)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (239, 33, 1, 0, 22)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (240, 33, 1, 0, 23)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (241, 33, 2, 0, 19)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (242, 33, 2, 0, 20)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (243, 33, 2, 0, 21)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (244, 33, 2, 0, 22)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (245, 33, 2, 0, 23)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (246, 33, 6, 0, 19)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (247, 33, 6, 0, 20)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (248, 33, 6, 0, 21)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (249, 33, 6, 0, 22)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (250, 33, 6, 0, 23)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (251, 34, 1, 2, 21)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (252, 34, 1, 1, 22)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (253, 34, 1, 0, 23)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (254, 34, 1, 0, 24)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (255, 34, 1, 0, 25)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (256, 34, 2, 3, 21)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (257, 34, 2, 0, 22)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (258, 34, 2, 0, 23)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (259, 34, 2, 0, 24)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (260, 34, 2, 0, 25)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (261, 34, 6, 0, 21)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (262, 34, 6, 23, 22)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (263, 34, 6, 0, 23)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (264, 34, 6, 0, 24)
INSERT [dbo].[WorkedHours] ([Id], [TimesheetId], [ProjectId], [Hours], [WorkDay]) VALUES (265, 34, 6, 0, 25)
SET IDENTITY_INSERT [dbo].[WorkedHours] OFF
GO
/****** Object:  Index [UC_TimesheetUserIdStartDate]    Script Date: 2021-09-08 9:54:10 PM ******/
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
