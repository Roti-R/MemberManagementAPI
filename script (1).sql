USE [master]
GO
/****** Object:  Database [QuanLyHoiVien]    Script Date: 1/4/2024 12:44:05 AM ******/
CREATE DATABASE [QuanLyHoiVien]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyHoiVien', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuanLyHoiVien.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyHoiVien_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuanLyHoiVien_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QuanLyHoiVien] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyHoiVien].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyHoiVien] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyHoiVien] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyHoiVien] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuanLyHoiVien] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyHoiVien] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [QuanLyHoiVien] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanLyHoiVien] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyHoiVien] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyHoiVien] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyHoiVien] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyHoiVien] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyHoiVien] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyHoiVien] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanLyHoiVien', N'ON'
GO
ALTER DATABASE [QuanLyHoiVien] SET QUERY_STORE = OFF
GO
USE [QuanLyHoiVien]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 1/4/2024 12:44:05 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 1/4/2024 12:44:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[MemberID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Management]    Script Date: 1/4/2024 12:44:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Management](
	[ManagerID] [uniqueidentifier] NOT NULL,
	[OrgID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Management] PRIMARY KEY CLUSTERED 
(
	[ManagerID] ASC,
	[OrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 1/4/2024 12:44:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[BirthDate] [datetime2](7) NULL,
	[JoinDate] [datetime2](7) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[CurrentOrganizationID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 1/4/2024 12:44:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[OrgID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[ParentID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[OrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230808073525_Init', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230821015501_UpdateFirstFK', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230821040217_update', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230821042140_addFK', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230821065748_AddManageFK', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230821065926_ForgotOrganizationManager', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230821092539_UPDATEFK', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230821093148_updateFKLastTime', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230830201751_addnotnullProp', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230830203958_addnotnullprop2', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231020123825_Teoteoteo', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231020123922_updateConnectionString', N'6.0.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231022114759_RemoveRequireOrgOfMember', N'6.0.20')
GO
INSERT [dbo].[Account] ([Username], [Password], [MemberID]) VALUES (N'admin', N'a4ayc/80/OGda4BO/1o/V0etpOqiLx1JwB5S3beHW0s=', N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84')
GO
INSERT [dbo].[Management] ([ManagerID], [OrgID]) VALUES (N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84', N'd00b787b-ea19-4ca7-12a0-08dbd64372ba')
INSERT [dbo].[Management] ([ManagerID], [OrgID]) VALUES (N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84', N'97a6cfce-4eb2-4913-ba62-08dbd64595f5')
INSERT [dbo].[Management] ([ManagerID], [OrgID]) VALUES (N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84', N'bffd86f0-efbf-468a-6ada-08dbd653a9fd')
INSERT [dbo].[Management] ([ManagerID], [OrgID]) VALUES (N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84', N'bc63c274-d74d-406c-5c58-08dbd65427e8')
INSERT [dbo].[Management] ([ManagerID], [OrgID]) VALUES (N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84', N'567a1ec8-f29b-4143-a020-08dbd659e92a')
INSERT [dbo].[Management] ([ManagerID], [OrgID]) VALUES (N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84', N'e3da2ab2-61ef-4c35-61d4-08dbd69d0ad9')
INSERT [dbo].[Management] ([ManagerID], [OrgID]) VALUES (N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84', N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84')
GO
INSERT [dbo].[Member] ([MemberID], [Name], [PhoneNumber], [BirthDate], [JoinDate], [Address], [CurrentOrganizationID]) VALUES (N'f6062da1-bc32-40a3-6b9e-08dbd689c989', N'Doanh', N'0335700395', NULL, CAST(N'2023-11-5T08:12:28.2984301' AS DateTime2), N'', N'd00b787b-ea19-4ca7-12a0-08dbd64372ba')
INSERT [dbo].[Member] ([MemberID], [Name], [PhoneNumber], [BirthDate], [JoinDate], [Address], [CurrentOrganizationID]) VALUES (N'8cae758b-0d10-4790-1d74-08dbd68fc8d3', N'DoanhTR', N'0918525147', NULL, CAST(N'2023-12-10T08:55:24.0771710' AS DateTime2), N'', NULL)
INSERT [dbo].[Member] ([MemberID], [Name], [PhoneNumber], [BirthDate], [JoinDate], [Address], [CurrentOrganizationID]) VALUES (N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84', N'Quản trị viên', NULL, NULL, CAST(N'2023-12-27T13:42:32.6200000' AS DateTime2), NULL, N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84')
GO
INSERT [dbo].[Organization] ([OrgID], [Name], [Type], [ParentID]) VALUES (N'd00b787b-ea19-4ca7-12a0-08dbd64372ba', N'Bình Phước', N'tinh', N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84')
INSERT [dbo].[Organization] ([OrgID], [Name], [Type], [ParentID]) VALUES (N'97a6cfce-4eb2-4913-ba62-08dbd64595f5', N'Hà Nội', N'tinh', N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84')
INSERT [dbo].[Organization] ([OrgID], [Name], [Type], [ParentID]) VALUES (N'bffd86f0-efbf-468a-6ada-08dbd653a9fd', N'Bù Đăng', N'huyen', N'd00b787b-ea19-4ca7-12a0-08dbd64372ba')
INSERT [dbo].[Organization] ([OrgID], [Name], [Type], [ParentID]) VALUES (N'bc63c274-d74d-406c-5c58-08dbd65427e8', N'Minh Hưng', N'xa', N'bffd86f0-efbf-468a-6ada-08dbd653a9fd')
INSERT [dbo].[Organization] ([OrgID], [Name], [Type], [ParentID]) VALUES (N'567a1ec8-f29b-4143-a020-08dbd659e92a', N'Đồng Xoài', N'huyen', N'd00b787b-ea19-4ca7-12a0-08dbd64372ba')
INSERT [dbo].[Organization] ([OrgID], [Name], [Type], [ParentID]) VALUES (N'e3da2ab2-61ef-4c35-61d4-08dbd69d0ad9', N'Tân phú', N'xa', N'567a1ec8-f29b-4143-a020-08dbd659e92a')
INSERT [dbo].[Organization] ([OrgID], [Name], [Type], [ParentID]) VALUES (N'92e8c2b2-97d9-4d6d-a9b7-48cb0d039a84', N'The First', N'root', NULL)
GO
/****** Object:  Index [IX_Management_OrgID]    Script Date: 1/4/2024 12:44:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_Management_OrgID] ON [dbo].[Management]
(
	[OrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Member_CurrentOrganizationID]    Script Date: 1/4/2024 12:44:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_Member_CurrentOrganizationID] ON [dbo].[Member]
(
	[CurrentOrganizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Organization_ParentID]    Script Date: 1/4/2024 12:44:05 AM ******/
CREATE NONCLUSTERED INDEX [IX_Organization_ParentID] ON [dbo].[Organization]
(
	[ParentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [MemberID]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Member_MemberID] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Member] ([MemberID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Member_MemberID]
GO
ALTER TABLE [dbo].[Management]  WITH CHECK ADD  CONSTRAINT [FK_Management_Member_ManagerID] FOREIGN KEY([ManagerID])
REFERENCES [dbo].[Member] ([MemberID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Management] CHECK CONSTRAINT [FK_Management_Member_ManagerID]
GO
ALTER TABLE [dbo].[Management]  WITH CHECK ADD  CONSTRAINT [FK_Management_Organization_OrgID] FOREIGN KEY([OrgID])
REFERENCES [dbo].[Organization] ([OrgID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Management] CHECK CONSTRAINT [FK_Management_Organization_OrgID]
GO
ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_Organization_CurrentOrganizationID] FOREIGN KEY([CurrentOrganizationID])
REFERENCES [dbo].[Organization] ([OrgID])
GO
ALTER TABLE [dbo].[Member] CHECK CONSTRAINT [FK_Member_Organization_CurrentOrganizationID]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK_Organization_Organization_ParentID] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Organization] ([OrgID])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK_Organization_Organization_ParentID]
GO
USE [master]
GO
ALTER DATABASE [QuanLyHoiVien] SET  READ_WRITE 
GO
