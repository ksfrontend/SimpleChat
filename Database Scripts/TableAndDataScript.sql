CREATE DATABASE [ChatAppDB]

GO
USE [ChatAppDB]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 2021-01-22 1:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [bigint] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL,
	[ReceiverId] [int] NOT NULL,
	[TextMessage] [nvarchar](500) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2021-01-22 1:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserTable] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Name], [IsActive], [CreatedDate], [ModifiedDate]) VALUES (2, N'mathew', N'123456', N'Mathew', 1, CAST(N'2021-01-20T00:00:00.000' AS DateTime), CAST(N'2021-01-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Name], [IsActive], [CreatedDate], [ModifiedDate]) VALUES (3, N'paul', N'123456', N'Paul', 1, CAST(N'2021-01-20T00:00:00.000' AS DateTime), CAST(N'2021-01-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Name], [IsActive], [CreatedDate], [ModifiedDate]) VALUES (4, N'justin', N'123456', N'Justin', 1, CAST(N'2021-01-20T00:00:00.000' AS DateTime), CAST(N'2021-01-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Name], [IsActive], [CreatedDate], [ModifiedDate]) VALUES (5, N'aaron', N'123456', N'Aaron', 1, CAST(N'2021-01-20T00:00:00.000' AS DateTime), CAST(N'2021-01-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Name], [IsActive], [CreatedDate], [ModifiedDate]) VALUES (6, N'charles', N'123456', N'Charles', 1, CAST(N'2021-01-20T00:00:00.000' AS DateTime), CAST(N'2021-01-20T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  StoredProcedure [dbo].[GetChatList]    Script Date: 2021-01-22 1:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    CREATE PROCEDURE [dbo].[GetChatList] 
    	@SenderId bigint,
    	@ReceiverId bigint
    AS
    BEGIN
    	select * 
    	from Messages
    	where (SenderId = @SenderId AND ReceiverId=@ReceiverId) OR (SenderId=@ReceiverId AND ReceiverId=@SenderId) 
    	order by MessageId ASC
    END
GO
