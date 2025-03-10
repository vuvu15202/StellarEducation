USE [master]
GO
/****** Object:  Database [ASPNETLearningOnlv1]    Script Date: 3/1/2025 9:31:22 AM ******/
CREATE DATABASE [ASPNETLearningOnlv1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ASPNETLearningOnlv1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ASPNETLearningOnlv1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ASPNETLearningOnlv1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ASPNETLearningOnlv1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ASPNETLearningOnlv1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET ARITHABORT OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET  MULTI_USER 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET QUERY_STORE = ON
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ASPNETLearningOnlv1]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/1/2025 9:31:23 AM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](200) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[CourseId] [int] NOT NULL,
	[IsHide] [bit] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConsultationRequest]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConsultationRequest](
	[ConsultationRequestId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](7) NULL,
	[ContactName] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Message] [nvarchar](1000) NULL,
	[IsResolved] [bit] NOT NULL,
	[ResolvedById] [int] NULL,
 CONSTRAINT [PK_ConsultationRequest] PRIMARY KEY CLUSTERED 
(
	[ConsultationRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IsPrivate] [bit] NOT NULL,
	[Price] [bigint] NULL,
	[IsDelete] [bit] NOT NULL,
	[Language] [nvarchar](max) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[LecturerId] [int] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseEnroll]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseEnroll](
	[CourseEnrollID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	[EnrollDate] [date] NOT NULL,
	[ExpireDate] [date] NOT NULL,
	[LessonCurrent] [int] NOT NULL,
	[CourseStatus] [int] NOT NULL,
	[Grade] [nvarchar](max) NULL,
	[AverageGrade] [real] NULL,
	[StudentFeeId] [nvarchar](max) NULL,
	[Quiz] [nvarchar](max) NULL,
 CONSTRAINT [PK_CourseEnroll] PRIMARY KEY CLUSTERED 
(
	[CourseEnrollID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExamCandidate]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExamCandidate](
	[ExamCandidateId] [int] IDENTITY(1,1) NOT NULL,
	[candidateId] [int] NOT NULL,
	[questionBankId] [int] NOT NULL,
	[StartExamDate] [date] NULL,
	[SubmitedDate] [date] NULL,
	[SubmitedReading] [nvarchar](max) NULL,
	[SubmitedListening] [nvarchar](max) NULL,
	[SubmitedWriting] [nvarchar](max) NULL,
	[SubmitedSpeaking] [nvarchar](max) NULL,
	[bandScoreReading] [float] NULL,
	[bandScoreListening] [float] NULL,
	[bandScoreWriting] [float] NULL,
	[bandScoreSpeaking] [float] NULL,
	[overall] [float] NULL,
	[IsDelete] [bit] NOT NULL,
	[IsComplete] [bit] NOT NULL,
	[CorrectAnswersListening] [int] NULL,
	[CorrectAnswersReading] [int] NULL,
	[CorrectAnswersSpeaking] [int] NULL,
	[CorrectAnswersWriting] [int] NULL,
	[TypeExam] [nvarchar](max) NULL,
 CONSTRAINT [PK_ExamCandidate] PRIMARY KEY CLUSTERED 
(
	[ExamCandidateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lesson](
	[LessonID] [int] IDENTITY(1,1) NOT NULL,
	[LessonNum] [int] NULL,
	[CourseID] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](4000) NULL,
	[VideoUrl] [nvarchar](4000) NULL,
	[Quiz] [nvarchar](max) NULL,
	[PreviousLessioNum] [int] NULL,
	[IsDelete] [bit] NOT NULL,
	[QuestionBankId] [int] NULL,
	[VideoTime] [int] NULL,
 CONSTRAINT [PK_Lesson] PRIMARY KEY CLUSTERED 
(
	[LessonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[NotificationTitle] [nvarchar](100) NOT NULL,
	[NotificationContent] [nvarchar](1000) NOT NULL,
	[NotificationAt] [datetime2](7) NOT NULL,
	[NotificationTo] [int] NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionBank]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionBank](
	[questionBankId] [int] IDENTITY(1,1) NOT NULL,
	[examCode] [nvarchar](max) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[IsClosed] [bit] NOT NULL,
	[LecturerId] [int] NULL,
	[Reading] [nvarchar](max) NULL,
	[Listening] [nvarchar](max) NULL,
	[Writing] [nvarchar](max) NULL,
	[Speaking] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[isPrivate] [bit] NOT NULL,
 CONSTRAINT [PK_QuestionBank] PRIMARY KEY CLUSTERED 
(
	[questionBankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reply]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reply](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](200) NULL,
	[CreateDate] [datetime2](7) NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[IsHide] [bit] NOT NULL,
	[CommentId] [int] NOT NULL,
 CONSTRAINT [PK_Reply] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[ReviewID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	[Vote] [int] NOT NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentFee]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentFee](
	[StudentFeeId] [nvarchar](255) NOT NULL,
	[PaymentMethod] [nvarchar](255) NOT NULL,
	[BankCode] [nvarchar](255) NULL,
	[Amount] [nvarchar](255) NOT NULL,
	[OrderInfo] [nvarchar](255) NOT NULL,
	[ErrorCode] [nvarchar](10) NOT NULL,
	[LocalMessage] [nvarchar](255) NOT NULL,
	[DateOfPaid] [datetime] NULL,
	[CourseEnrollId] [int] NULL,
 CONSTRAINT [PK_StudentFee] PRIMARY KEY CLUSTERED 
(
	[StudentFeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Phone] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Active] [bit] NOT NULL,
	[EnrollDate] [datetime2](7) NULL,
	[Description] [nvarchar](1000) NULL,
	[Image] [nvarchar](255) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 3/1/2025 9:31:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241217170811_dbContextv2', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241220174148_dbContextver3', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241222141941_dbcontextver4', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241222142353_dbcontextver5', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241224085540_dbcontextver6', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241225143735_dncontextver7', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241225144154_dbcontextver8', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241226133259_dbcontextver9', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241226133450_dbcontextver10', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241229125122_dbcontextver11', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241231052356_dbcontextver12', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241231100128_dbcontextver13', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250101145518_dbcontextver14', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250102085842_dbcontextver15', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250104155536_dbcontextver16', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250105085005_dbcontextver17', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250225132440_dbcontextver18', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250225140517_dbcontextver19', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250225140855_dbcontextver20', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250225141343_dbcontextver21', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250225145134_dbcontextver22', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250225145638_dbcontextver23', N'6.0.35')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250225151938_dbcontextver24', N'6.0.35')
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [Name], [Image], [IsDelete]) VALUES (1, N'Bứt phá 4 kỹ năng', N'http://localhost:5000/uploads/755f0398-d930-4b1e-9718-e4d441ec68c5.png', 0)
INSERT [dbo].[Category] ([CategoryID], [Name], [Image], [IsDelete]) VALUES (2, N'Luyện thi IELTS Reading', N'http://localhost:5000/uploads/65668321-e7b6-4024-b5b8-a370625c7671.png', 0)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[ConsultationRequest] ON 

INSERT [dbo].[ConsultationRequest] ([ConsultationRequestId], [CreatedAt], [ContactName], [Email], [PhoneNumber], [Message], [IsResolved], [ResolvedById]) VALUES (1, CAST(N'2025-02-26T14:48:44.7822284' AS DateTime2), N'Vu truong vu', N'vuvu@gmail.com', N'0329053888', N'Xin chào, em muốn tư vấn khóa học
<br>Wishlist:<br>
- Luyện đề reading 6.0
<br>- Reading cùng Hồng Minh', 0, NULL)
SET IDENTITY_INSERT [dbo].[ConsultationRequest] OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price], [IsDelete], [Language], [UpdatedAt], [LecturerId]) VALUES (1, 1, N'Luyện đề reading 6.0', N'http://localhost:5000/uploads/final_logo.png', N'loremjdhf sihefg sdif ídf 
- dksjfb ádjf 
- ndjshb hsb 
- sfbd aksdjfb ', 1, 0, 0, N'Vietnamese', CAST(N'2025-02-28T11:50:26.0885597' AS DateTime2), 3)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price], [IsDelete], [Language], [UpdatedAt], [LecturerId]) VALUES (2, 1, N'Reading cùng Hồng Minh', N'http://localhost:5000/uploads/final_logo.png', N'Lorem', 1, 0, 0, N'Vietnamese', CAST(N'2025-02-28T09:49:58.7856944' AS DateTime2), 5)
INSERT [dbo].[Course] ([CourseID], [CategoryID], [Name], [Image], [Description], [IsPrivate], [Price], [IsDelete], [Language], [UpdatedAt], [LecturerId]) VALUES (3, 1, N'Giới thiệu khóa học ne', N'http://localhost:5000/uploads/final_logo.png', N'fgef', 1, 0, 1, N'Vietnamese', CAST(N'2025-02-28T11:46:14.3988837' AS DateTime2), 9)
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
SET IDENTITY_INSERT [dbo].[CourseEnroll] ON 

INSERT [dbo].[CourseEnroll] ([CourseEnrollID], [UserID], [CourseID], [EnrollDate], [ExpireDate], [LessonCurrent], [CourseStatus], [Grade], [AverageGrade], [StudentFeeId], [Quiz]) VALUES (1, 11, 1, CAST(N'2025-02-28' AS Date), CAST(N'0001-01-01' AS Date), 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[CourseEnroll] ([CourseEnrollID], [UserID], [CourseID], [EnrollDate], [ExpireDate], [LessonCurrent], [CourseStatus], [Grade], [AverageGrade], [StudentFeeId], [Quiz]) VALUES (2, 11, 2, CAST(N'2025-02-28' AS Date), CAST(N'0001-01-01' AS Date), 3, 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[CourseEnroll] OFF
GO
SET IDENTITY_INSERT [dbo].[ExamCandidate] ON 

INSERT [dbo].[ExamCandidate] ([ExamCandidateId], [candidateId], [questionBankId], [StartExamDate], [SubmitedDate], [SubmitedReading], [SubmitedListening], [SubmitedWriting], [SubmitedSpeaking], [bandScoreReading], [bandScoreListening], [bandScoreWriting], [bandScoreSpeaking], [overall], [IsDelete], [IsComplete], [CorrectAnswersListening], [CorrectAnswersReading], [CorrectAnswersSpeaking], [CorrectAnswersWriting], [TypeExam]) VALUES (1, 9, 1, NULL, NULL, N'[{"QuestionNo":"1","SubmitedAnswer":"Nobody"},{"QuestionNo":"2","SubmitedAnswer":"Nobody"},{"QuestionNo":"3","SubmitedAnswer":"Nobody"},{"QuestionNo":"4","SubmitedAnswer":"Nobody"},{"QuestionNo":"5","SubmitedAnswer":"Nobody"},{"QuestionNo":"6","SubmitedAnswer":"Nobody"},{"QuestionNo":"7","SubmitedAnswer":""},{"QuestionNo":"8","SubmitedAnswer":""},{"QuestionNo":"9","SubmitedAnswer":""},{"QuestionNo":"10","SubmitedAnswer":"H\u1ECDc sinh"},{"QuestionNo":"11","SubmitedAnswer":"Hi\u1EC7u tr\u01B0\u1EDFng"},{"QuestionNo":"12","SubmitedAnswer":"Nguy\u1EC5n V\u0103n A"}]', N'[]', N'[]', N'[]', 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, N'quiz')
INSERT [dbo].[ExamCandidate] ([ExamCandidateId], [candidateId], [questionBankId], [StartExamDate], [SubmitedDate], [SubmitedReading], [SubmitedListening], [SubmitedWriting], [SubmitedSpeaking], [bandScoreReading], [bandScoreListening], [bandScoreWriting], [bandScoreSpeaking], [overall], [IsDelete], [IsComplete], [CorrectAnswersListening], [CorrectAnswersReading], [CorrectAnswersSpeaking], [CorrectAnswersWriting], [TypeExam]) VALUES (2, 11, 1, NULL, NULL, N'[{"QuestionNo":"1","SubmitedAnswer":"Nobody"},{"QuestionNo":"2","SubmitedAnswer":"Nobody"},{"QuestionNo":"3","SubmitedAnswer":"Nobody"},{"QuestionNo":"4","SubmitedAnswer":"Nobody"},{"QuestionNo":"5","SubmitedAnswer":"Nobody-T\u00F4i Kh\u00F4ng Bi\u1EBFt"},{"QuestionNo":"6","SubmitedAnswer":"Nobody-T\u00F4i Kh\u00F4ng Bi\u1EBFt"},{"QuestionNo":"7","SubmitedAnswer":"la"},{"QuestionNo":"8","SubmitedAnswer":"quy"},{"QuestionNo":"9","SubmitedAnswer":"khong phai"},{"QuestionNo":"10","SubmitedAnswer":"H\u1ECDc sinh"},{"QuestionNo":"11","SubmitedAnswer":"se1685"},{"QuestionNo":"12","SubmitedAnswer":"Nguy\u1EC5n V\u0103n A"}]', N'[]', N'[]', N'[]', 2.5, 0, 0, 0, 1, 0, 1, 0, 5, 0, 0, N'quiz')
SET IDENTITY_INSERT [dbo].[ExamCandidate] OFF
GO
SET IDENTITY_INSERT [dbo].[Lesson] ON 

INSERT [dbo].[Lesson] ([LessonID], [LessonNum], [CourseID], [Name], [Description], [VideoUrl], [Quiz], [PreviousLessioNum], [IsDelete], [QuestionBankId], [VideoTime]) VALUES (1, 1, 2, N'Giới thiệu khóa học ne', N'mo ta', N'DcwUJhDJet4', NULL, 0, 0, NULL, 6)
INSERT [dbo].[Lesson] ([LessonID], [LessonNum], [CourseID], [Name], [Description], [VideoUrl], [Quiz], [PreviousLessioNum], [IsDelete], [QuestionBankId], [VideoTime]) VALUES (2, 2, 2, N'Hồi thứ nhất', N'mo tả ở đây njdnf bdbf âbaafb jf bạbf bàb hbf hbfajhfb ahfb abfb ahbfjaa bfhabkf bàn ànhBFUHBAHBSHUDBVVB JDFB jsdv', N'DcwUJhDJet4', NULL, 1, 0, NULL, 6)
INSERT [dbo].[Lesson] ([LessonID], [LessonNum], [CourseID], [Name], [Description], [VideoUrl], [Quiz], [PreviousLessioNum], [IsDelete], [QuestionBankId], [VideoTime]) VALUES (3, 3, 2, N'Hồi thứ 2', N'knr qiueb uiu bử bạiu ayw nK IWF BAUHBF NAK', N'gqOeUsoLDrE', N'Trial_FullSkill', 2, 0, 1, 6)
SET IDENTITY_INSERT [dbo].[Lesson] OFF
GO
SET IDENTITY_INSERT [dbo].[QuestionBank] ON 

INSERT [dbo].[QuestionBank] ([questionBankId], [examCode], [StartDate], [EndDate], [IsClosed], [LecturerId], [Reading], [Listening], [Writing], [Speaking], [IsDelete], [isPrivate]) VALUES (1, N'Trial_FullSkill', CAST(N'2025-02-26' AS Date), CAST(N'2026-01-26' AS Date), 0, 2, N'{"Time":"3600","Parts":[{"PartNo":"1","FileURL":"https://ieltsadvantage.com/wp-content/uploads/2015/04/mcq-2.png","FileType":"png","QuestionRange":"0-13","Title":null,"Groups":[{"GroupNo":"1","Title":"\u0110i\u1EC1n v\u00E0o \u00F4 tr\u1ED1ng...","Type":"radio","QuestionRange":"0-12","FileURL":"N/A","FileType":"N/A","Questions":[{"QuestionNo":"1","Title":"B\u1EA1n l\u00E0 ai?","Answers":["Nobody","T\u00F4i Kh\u00F4ng Bi\u1EBFt","C\u1EA3m \u01A1n","Tuy\u1EC7t qu\u00E1"],"CorrectAnswer":"Nobody","ExplainString":"explain here"},{"QuestionNo":"2","Title":"B\u1EA1n l\u00E0 ai?","Answers":["Nobody","T\u00F4i Kh\u00F4ng Bi\u1EBFt","C\u1EA3m \u01A1n","Tuy\u1EC7t qu\u00E1"],"CorrectAnswer":"Nobody","ExplainString":"explain here"},{"QuestionNo":"3","Title":"B\u1EA1n l\u00E0 ai?","Answers":["Nobody","T\u00F4i Kh\u00F4ng Bi\u1EBFt","C\u1EA3m \u01A1n","Tuy\u1EC7t qu\u00E1"],"CorrectAnswer":"Nobody","ExplainString":"explain here"}]},{"GroupNo":"2","Title":"\u0110i\u1EC1n v\u00E0o \u00F4 tr\u1ED1ng...","Type":"checkbox","QuestionRange":"13-26","FileURL":"N/A","FileType":"N/A","Questions":[{"QuestionNo":"4","Title":"B\u1EA1n l\u00E0 ai?","Answers":["Nobody","T\u00F4i Kh\u00F4ng Bi\u1EBFt","C\u1EA3m \u01A1n","Tuy\u1EC7t qu\u00E1"],"CorrectAnswer":"Nobody-T\u00F4iKh\u00F4ngBi\u1EBFt","ExplainString":"explain here"},{"QuestionNo":"5","Title":"B\u1EA1n l\u00E0 ai?","Answers":["Nobody","T\u00F4i Kh\u00F4ng Bi\u1EBFt","C\u1EA3m \u01A1n","Tuy\u1EC7t qu\u00E1"],"CorrectAnswer":"Nobody","ExplainString":"explain here"},{"QuestionNo":"6","Title":"B\u1EA1n l\u00E0 ai?","Answers":["Nobody","T\u00F4i Kh\u00F4ng Bi\u1EBFt","C\u1EA3m \u01A1n","Tuy\u1EC7t qu\u00E1"],"CorrectAnswer":"Nobody","ExplainString":"explain here"}]},{"GroupNo":"3","Title":"\u0110i\u1EC1n v\u00E0o \u00F4 tr\u1ED1ng...","Type":"text","QuestionRange":"27-30","FileURL":"N/A","FileType":"N/A","Questions":[{"QuestionNo":"7","Title":"T\u00F4i (...) th\u1EA7y gi\u00E1o.","Answers":["N/A"],"CorrectAnswer":"ban","ExplainString":"explain here"},{"QuestionNo":"8","Title":"T\u00F4i (...) th\u1EA7y gi\u00E1o.","Answers":["N/A"],"CorrectAnswer":"ban","ExplainString":"explain here"},{"QuestionNo":"9","Title":"T\u00F4i (...) th\u1EA7y gi\u00E1o.","Answers":["N/A"],"CorrectAnswer":"ban","ExplainString":"explain here"}]},{"GroupNo":"4","Title":"\u0110i\u1EC1n v\u00E0o \u00F4 tr\u1ED1ng...","Type":"dropbox","QuestionRange":"31-40","FileURL":"N/A","FileType":"N/A","Questions":[{"QuestionNo":"10","Title":"M\u1EB9 t\u00F4i l\u00E0 (...) trung t\u00E2m Stellar.","Answers":["H\u1ECDc sinh","Gi\u00E1o vi\u00EAn","Hi\u1EC7u tr\u01B0\u1EDFng"],"CorrectAnswer":"H\u1ECDc sinh","ExplainString":"explain here"},{"QuestionNo":"11","Title":"lop cua toi la (...)","Answers":["se1684","se1685","se1686"],"CorrectAnswer":"se1684","ExplainString":"explain here"},{"QuestionNo":"12","Title":"(...) l\u00E0 t\u00EAn t\u00F4i.","Answers":["Nguy\u1EC5n V\u0103n A","Nguy\u1EC5n V\u0103n B","Nguy\u1EC5n V\u0103n C"],"CorrectAnswer":"Nguy\u1EC5n V\u0103n A","ExplainString":"explain here"}]}]},{"PartNo":"2","FileURL":"/IELTS/Questions.pdf","FileType":"pdf","QuestionRange":"14-26","Title":null,"Groups":[]},{"PartNo":"3","FileURL":"https://ieltsadvantage.com/wp-content/uploads/2015/04/mcq-2.png","FileType":"png","QuestionRange":"27-40","Title":null,"Groups":[]}]}', N'{"Time":"3600","ListeningFileURL":"http://localhost:5000/uploads/ielts/audioes/31-Track31.mp3","Parts":[{"PartNo":"1","FileURL":"N/A","FileType":"N/A","QuestionRange":"0-10","Title":null,"Groups":[{"GroupNo":"1","Title":"Write NO MORE THAN TWO WORDS AND/OR A NUMBER for each answer","Type":"text","QuestionRange":"0-10","FileURL":"http://localhost:5000/uploads/ielts/previewctasection.png","FileType":"png","Questions":[{"QuestionNo":"1","Title":"Length of stay (...)","Answers":["N/A"],"CorrectAnswer":"two weeks-2 weeks","ExplainString":"explain here lorem"},{"QuestionNo":"2","Title":"Type of accommodation (...)","Answers":["N/A"],"CorrectAnswer":"family room","ExplainString":"explain here"},{"QuestionNo":"3","Title":"(...)","Answers":["N/A"],"CorrectAnswer":"Shriver","ExplainString":null},{"QuestionNo":"4","Title":"(...)","Answers":["N/A"],"CorrectAnswer":"Scotland","ExplainString":"explain here"},{"QuestionNo":"5","Title":"Contact telephone (...)","Answers":["N/A"],"CorrectAnswer":"0131 9946 5723","ExplainString":null},{"QuestionNo":"6","Title":"Favorite activity (...)","Answers":["N/A"],"CorrectAnswer":"enjoy swimming-swimming","ExplainString":null},{"QuestionNo":"7","Title":"busy but (...)","Answers":["N/A"],"CorrectAnswer":"very clean-clean","ExplainString":null},{"QuestionNo":"8","Title":"are sometimes (...)","Answers":["N/A"],"CorrectAnswer":"too helpful","ExplainString":null},{"QuestionNo":"9","Title":"(...)  and quick","Answers":["N/A"],"CorrectAnswer":"polite","ExplainString":null},{"QuestionNo":"10","Title":"need some  (...) for hire","Answers":["N/A"],"CorrectAnswer":"bikes-bicycles","ExplainString":null}]}]},{"PartNo":"2","FileURL":"N/A","FileType":"N/A","QuestionRange":"11-20","Title":null,"Groups":[{"GroupNo":"1","Title":"Write NO MORE THAN TWO WORDS AND/OR A NUMBER for each answer","Type":"text","QuestionRange":"11-15","FileURL":"N/A","FileType":"N/A","Questions":[{"QuestionNo":"11","Title":"Orana means  \u0027(...)\u0027","Answers":["N/A"],"CorrectAnswer":"welcome","ExplainString":"explain here"},{"QuestionNo":"12","Title":"The park has animals from a total of (...)","Answers":["N/A"],"CorrectAnswer":"70 species","ExplainString":"explain here"},{"QuestionNo":"13","Title":"(...) the giraffes at 12 or 3 p.m.","Answers":["N/A"],"CorrectAnswer":"handfeed-feed","ExplainString":"explain here"},{"QuestionNo":"14","Title":"Touch the animals in the (...) (good for children).","Answers":["N/A"],"CorrectAnswer":"farmyard","ExplainString":null},{"QuestionNo":"15","Title":"Watch the cheetahs doing their (...) at 3.40.","Answers":["N/A"],"CorrectAnswer":"exercise run","ExplainString":null}]},{"GroupNo":"2","Title":"Write the correct letter, A-I, next to questions 16-20.","Type":"text","QuestionRange":"16-20","FileURL":"N/A","FileType":"N/A","Questions":[{"QuestionNo":"16","Title":"New Zealand birds (...)","Answers":["N/A"],"CorrectAnswer":"B","ExplainString":null},{"QuestionNo":"17","Title":"African village (...)","Answers":["N/A"],"CorrectAnswer":"G","ExplainString":null},{"QuestionNo":"18","Title":"Picnic area (...)","Answers":["N/A"],"CorrectAnswer":"E","ExplainString":null},{"QuestionNo":"19","Title":"Afternoon walkabout meeting place (...)","Answers":["N/A"],"CorrectAnswer":"C","ExplainString":null},{"QuestionNo":"20","Title":"Jomo\u0027s Caf\u00E9 (...)","Answers":["N/A"],"CorrectAnswer":"H","ExplainString":null}]}]},{"PartNo":"3","FileURL":"N/A","FileType":"N/A","QuestionRange":"31-40","Title":null,"Groups":[{"GroupNo":"1","Title":"Write NO MORE THAN TWO WORDS AND for each answer","Type":"text","QuestionRange":"31-40","FileURL":"http://localhost:5000/uploads/ielts/listeningtest.pdf","FileType":"pdf","Questions":[{"QuestionNo":"31","Title":"Can compare a past airport to a (...)","Answers":["N/A"],"CorrectAnswer":"bus station","ExplainString":null},{"QuestionNo":"32","Title":"Now, can compare an airport to a small (...)","Answers":["N/A"],"CorrectAnswer":"city","ExplainString":"explain here"},{"QuestionNo":"33","Title":"(...) (e.g. package deals)","Answers":["N/A"],"CorrectAnswer":"mass tourism","ExplainString":"explain here"},{"QuestionNo":"34","Title":"(e.g. meetings)","Answers":["N/A"],"CorrectAnswer":"international business","ExplainString":"explain here"},{"QuestionNo":"35","Title":"Need to create a good (...)","Answers":["N/A"],"CorrectAnswer":"first impression-impression","ExplainString":"explain here"},{"QuestionNo":"36","Title":"many big (...) provide space and light (e.g. Beijing airport)","Answers":["N/A"],"CorrectAnswer":"open areas-areas","ExplainString":"explain here"},{"QuestionNo":"37","Title":"calm atmosphere with easy movement reduces (...) for passengers","Answers":["N/A"],"CorrectAnswer":"stress","ExplainString":"explain here"},{"QuestionNo":"38","Title":"the shape of the (...) on the Arctic Circle airport, Norway","Answers":["N/A"],"CorrectAnswer":"roof","ExplainString":null},{"QuestionNo":"39","Title":"the (...) outside airports in India and Thailand","Answers":["N/A"],"CorrectAnswer":"shaded garden-shaded gardens-garden-gardens","ExplainString":null},{"QuestionNo":"40","Title":"structural design reduces (...) and costs","Answers":["N/A"],"CorrectAnswer":"energy use-energy","ExplainString":null}]}]}]}', N'{"Time":"3600"}', N'{"Time":"3600"}', 0, 0)
INSERT [dbo].[QuestionBank] ([questionBankId], [examCode], [StartDate], [EndDate], [IsClosed], [LecturerId], [Reading], [Listening], [Writing], [Speaking], [IsDelete], [isPrivate]) VALUES (2, N'Trial_123', CAST(N'2025-02-28' AS Date), CAST(N'2026-12-28' AS Date), 0, 3, N'{"Time":"3600"}', N'{"Time":"3600","ListeningFileURL":"http://localhost:5000/uploads/ielts/audioes/SoundHelix-Song-1.mp3","Parts":[{"PartNo":"1","FileURL":"N/A","FileType":"N/A","QuestionRange":"0-10","Title":null,"Groups":[{"GroupNo":"1","Title":"Write NO MORE THAN TWO WORDS AND/OR A NUMBER for each answer","Type":"text","QuestionRange":"0-10","FileURL":"http://localhost:5000/uploads/ielts/listeningtest.pdf","FileType":"pdf","Questions":[{"QuestionNo":"1","Title":"Length of stay (...)","Answers":["N/A"],"CorrectAnswer":"two weeks-2 weeks","ExplainString":"explain here lorem"},{"QuestionNo":"2","Title":"Type of accommodation (...)","Answers":["N/A"],"CorrectAnswer":"family room","ExplainString":"explain here"},{"QuestionNo":"3","Title":"(...)","Answers":["N/A"],"CorrectAnswer":"Shriver","ExplainString":null},{"QuestionNo":"4","Title":"(...)","Answers":["N/A"],"CorrectAnswer":"Scotland","ExplainString":"explain here"},{"QuestionNo":"5","Title":"Contact telephone (...)","Answers":["N/A"],"CorrectAnswer":"0131 9946 5723","ExplainString":null},{"QuestionNo":"6","Title":"Favorite activity (...)","Answers":["N/A"],"CorrectAnswer":"enjoy swimming-swimming","ExplainString":null},{"QuestionNo":"7","Title":"busy but (...)","Answers":["N/A"],"CorrectAnswer":"very clean-clean","ExplainString":null},{"QuestionNo":"8","Title":"are sometimes (...)","Answers":["N/A"],"CorrectAnswer":"too helpful","ExplainString":null},{"QuestionNo":"9","Title":"(...)  and quick","Answers":["N/A"],"CorrectAnswer":"polite","ExplainString":null},{"QuestionNo":"10","Title":"need some  (...) for hire","Answers":["N/A"],"CorrectAnswer":"bikes-bicycles","ExplainString":null}]}]},{"PartNo":"2","FileURL":"N/A","FileType":"N/A","QuestionRange":"11-20","Title":null,"Groups":[{"GroupNo":"1","Title":"Write NO MORE THAN TWO WORDS AND/OR A NUMBER for each answer","Type":"text","QuestionRange":"11-15","FileURL":"N/A","FileType":"N/A","Questions":[{"QuestionNo":"11","Title":"Orana means  \u0027(...)\u0027","Answers":["N/A"],"CorrectAnswer":"welcome","ExplainString":"explain here"},{"QuestionNo":"12","Title":"The park has animals from a total of (...)","Answers":["N/A"],"CorrectAnswer":"70 species","ExplainString":"explain here"},{"QuestionNo":"13","Title":"(...) the giraffes at 12 or 3 p.m.","Answers":["N/A"],"CorrectAnswer":"handfeed-feed","ExplainString":"explain here"},{"QuestionNo":"14","Title":"Touch the animals in the (...) (good for children).","Answers":["N/A"],"CorrectAnswer":"farmyard","ExplainString":null},{"QuestionNo":"15","Title":"Watch the cheetahs doing their (...) at 3.40.","Answers":["N/A"],"CorrectAnswer":"exercise run","ExplainString":null}]},{"GroupNo":"2","Title":"Write the correct letter, A-I, next to questions 16-20.","Type":"text","QuestionRange":"16-20","FileURL":"N/A","FileType":"N/A","Questions":[{"QuestionNo":"16","Title":"New Zealand birds (...)","Answers":["N/A"],"CorrectAnswer":"B","ExplainString":null},{"QuestionNo":"17","Title":"African village (...)","Answers":["N/A"],"CorrectAnswer":"G","ExplainString":null},{"QuestionNo":"18","Title":"Picnic area (...)","Answers":["N/A"],"CorrectAnswer":"E","ExplainString":null},{"QuestionNo":"19","Title":"Afternoon walkabout meeting place (...)","Answers":["N/A"],"CorrectAnswer":"C","ExplainString":null},{"QuestionNo":"20","Title":"Jomo\u0027s Caf\u00E9 (...)","Answers":["N/A"],"CorrectAnswer":"H","ExplainString":null}]}]},{"PartNo":"3","FileURL":"N/A","FileType":"N/A","QuestionRange":"31-40","Title":null,"Groups":[{"GroupNo":"1","Title":"Write NO MORE THAN TWO WORDS AND for each answer","Type":"text","QuestionRange":"31-40","FileURL":"N/A","FileType":"N/A","Questions":[{"QuestionNo":"31","Title":"Can compare a past airport to a (...)","Answers":["N/A"],"CorrectAnswer":"bus station","ExplainString":null},{"QuestionNo":"32","Title":"Now, can compare an airport to a small (...)","Answers":["N/A"],"CorrectAnswer":"city","ExplainString":"explain here"},{"QuestionNo":"33","Title":"(...) (e.g. package deals)","Answers":["N/A"],"CorrectAnswer":"mass tourism","ExplainString":"explain here"},{"QuestionNo":"34","Title":"(e.g. meetings)","Answers":["N/A"],"CorrectAnswer":"international business","ExplainString":"explain here"},{"QuestionNo":"35","Title":"Need to create a good (...)","Answers":["N/A"],"CorrectAnswer":"first impression-impression","ExplainString":"explain here"},{"QuestionNo":"36","Title":"many big (...) provide space and light (e.g. Beijing airport)","Answers":["N/A"],"CorrectAnswer":"open areas-areas","ExplainString":"explain here"},{"QuestionNo":"37","Title":"calm atmosphere with easy movement reduces (...) for passengers","Answers":["N/A"],"CorrectAnswer":"stress","ExplainString":"explain here"},{"QuestionNo":"38","Title":"the shape of the (...) on the Arctic Circle airport, Norway","Answers":["N/A"],"CorrectAnswer":"roof","ExplainString":null},{"QuestionNo":"39","Title":"the (...) outside airports in India and Thailand","Answers":["N/A"],"CorrectAnswer":"shaded garden-shaded gardens-garden-gardens","ExplainString":null},{"QuestionNo":"40","Title":"structural design reduces (...) and costs","Answers":["N/A"],"CorrectAnswer":"energy use-energy","ExplainString":null}]}]}]}', N'{"Time":"3600"}', N'{"Time":"3600"}', 0, 1)
SET IDENTITY_INSERT [dbo].[QuestionBank] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName], [Description]) VALUES (1, N'ANONYMOUS', N'anonymous')
INSERT [dbo].[Role] ([RoleId], [RoleName], [Description]) VALUES (2, N'ADMIN', N'admin')
INSERT [dbo].[Role] ([RoleId], [RoleName], [Description]) VALUES (3, N'LECTURER', N'lecturer')
INSERT [dbo].[Role] ([RoleId], [RoleName], [Description]) VALUES (4, N'STUDENT', N'student')
INSERT [dbo].[Role] ([RoleId], [RoleName], [Description]) VALUES (5, N'STAFF', N'staff')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (1, N'anonymous', N'Vô danh', N'Vô danh', N'$2a$11$KY85pH8IDBvw7XVy91N2NuDCARR2855uEXEIqPw/3zz2rVWskJw5C', N'vuvu152028@gmail.com', N'', N'', 1, CAST(N'2024-03-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (2, N'admin', N'Vũ', N'Vu', N'$2a$11$Xlti6db/qGzBQo2CXfTtqO1Zd6yn16uJ6/SaArytuFVEjVZmeq4eW', N'vuvu152029@gmail.com', N'0329053999', N'Thạch Thất, HN', 1, CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem sem sem', N'http://localhost:5000/assetweb/lecturer/INTENSIVETRAIING.png')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (3, N'lecturer1', N'Lam', N'Bao', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'baolam@gmail.com', N'0329033545', N'HN', 1, CAST(N'2024-02-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/baolam.jpg')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (4, N'lecturer2', N'Hung', N'Duy', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'duyhung@gmail.com', N'0329033545', N'HN', 1, CAST(N'2024-02-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/duyhung.jpg')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (5, N'lecturer3', N'Minh', N'Hong', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'hongminh@gmail.com', N'0329033545', N'HN', 1, CAST(N'2024-02-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/hongminh.jpg')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (6, N'lecturer4', N'Bao', N'Quan', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'baoquan@gmail.com', N'0329033545', N'HN', 1, CAST(N'2024-02-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/quanbao.jpg')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (7, N'lecturer5', N'Long', N'Son', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'longson@gmail.com', N'0329033545', N'HN', 1, CAST(N'2024-02-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/sonlong.jpg')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (8, N'lecturer6', N'Bach', N'Viet', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'vietbach@gmail.com', N'0329033545', N'HN', 1, CAST(N'2024-02-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/vietbach.jpg')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (9, N'lecturer7', N'Ha', N'Viet', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'vietha@gmail.com', N'0329033545', N'HN', 1, CAST(N'2024-02-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/vietha.jpg')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (10, N'staff', N'Dung', N'Hoang', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'vuvu152023@gmail.com', N'3548293525', N'HN', 1, CAST(N'2024-02-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (11, N'student1', N'Dương', N'Thi Linh', N'$2a$11$8AQtlynRc8K.advHLGx4d.7qIogNXaTg6Qu/.HpRZPqoJALDn2dKu', N'vuvu15202@gmail.com', N'0329053998', N'Thạch Thất, HN', 1, CAST(N'2024-02-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (12, N'student2', N'Thang', N'Dinh', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'student2@gmail.com', N'5374567454', N'HN', 1, CAST(N'2024-02-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (13, N'student3', N'Thuy', N'Lan', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'student3@gmail.com', N'5374567454', N'HN', 1, CAST(N'2024-03-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/')
INSERT [dbo].[User] ([UserId], [UserName], [FirstName], [LastName], [Password], [Email], [Phone], [Address], [Active], [EnrollDate], [Description], [Image]) VALUES (14, N'student4', N'Quyen', N'Quyen', N'$2a$11$prPgjGIVNVIIE68GBMEKqenutXEeQlzHBijXqNePpH5cGgrbm0zJa', N'student4@gmail.com', N'5374567454', N'HN', 1, CAST(N'2024-03-01T00:00:00.0000000' AS DateTime2), N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel gravida arcu. Vestibulum feugiat, sapien ultrices fermentum congue, quam velit venenatis sem', N'http://localhost:5000/assetweb/lecturer/')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (1, 1, 1)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (2, 2, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (3, 3, 3)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (4, 4, 3)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (5, 5, 3)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (6, 6, 3)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (7, 7, 3)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (8, 8, 3)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (9, 9, 3)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (10, 10, 4)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (11, 11, 4)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (12, 12, 4)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (13, 13, 4)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (14, 14, 4)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
/****** Object:  Index [IX_ConsultationRequest_ResolvedById]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_ConsultationRequest_ResolvedById] ON [dbo].[ConsultationRequest]
(
	[ResolvedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Course_CategoryID]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Course_CategoryID] ON [dbo].[Course]
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Course_LecturerId]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Course_LecturerId] ON [dbo].[Course]
(
	[LecturerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CourseEnroll_CourseID]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_CourseEnroll_CourseID] ON [dbo].[CourseEnroll]
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CourseEnroll_UserID]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_CourseEnroll_UserID] ON [dbo].[CourseEnroll]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExamCandidate_candidateId]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_ExamCandidate_candidateId] ON [dbo].[ExamCandidate]
(
	[candidateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExamCandidate_questionBankId]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_ExamCandidate_questionBankId] ON [dbo].[ExamCandidate]
(
	[questionBankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Lesson_CourseID]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Lesson_CourseID] ON [dbo].[Lesson]
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Lesson_QuestionBankId]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Lesson_QuestionBankId] ON [dbo].[Lesson]
(
	[QuestionBankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notification_NotificationTo]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Notification_NotificationTo] ON [dbo].[Notification]
(
	[NotificationTo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuestionBank_LecturerId]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_QuestionBank_LecturerId] ON [dbo].[QuestionBank]
(
	[LecturerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reply_CommentId]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Reply_CommentId] ON [dbo].[Reply]
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Review_CourseID]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Review_CourseID] ON [dbo].[Review]
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Review_UserID]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Review_UserID] ON [dbo].[Review]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StudentFee_CourseEnrollId]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_StudentFee_CourseEnrollId] ON [dbo].[StudentFee]
(
	[CourseEnrollId] ASC
)
WHERE ([CourseEnrollId] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRole_RoleId]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserRole_RoleId] ON [dbo].[UserRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRole_UserId]    Script Date: 3/1/2025 9:31:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserRole_UserId] ON [dbo].[UserRole]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Course] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsPrivate]
GO
ALTER TABLE [dbo].[ExamCandidate] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsComplete]
GO
ALTER TABLE [dbo].[QuestionBank] ADD  DEFAULT (CONVERT([bit],(0))) FOR [isPrivate]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (CONVERT([bit],(1))) FOR [Active]
GO
ALTER TABLE [dbo].[ConsultationRequest]  WITH CHECK ADD  CONSTRAINT [FK_ConsultationRequest_User] FOREIGN KEY([ResolvedById])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ConsultationRequest] CHECK CONSTRAINT [FK_ConsultationRequest_User]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_CategoryID] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_CategoryID]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [fk_course_lecturer] FOREIGN KEY([LecturerId])
REFERENCES [dbo].[User] ([UserId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [fk_course_lecturer]
GO
ALTER TABLE [dbo].[CourseEnroll]  WITH CHECK ADD  CONSTRAINT [FK_CourseEnroll_CourseID] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[CourseEnroll] CHECK CONSTRAINT [FK_CourseEnroll_CourseID]
GO
ALTER TABLE [dbo].[CourseEnroll]  WITH CHECK ADD  CONSTRAINT [FK_CourseEnroll_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[CourseEnroll] CHECK CONSTRAINT [FK_CourseEnroll_UserID]
GO
ALTER TABLE [dbo].[ExamCandidate]  WITH CHECK ADD  CONSTRAINT [FK_ExamCandidate_QuestionBank] FOREIGN KEY([questionBankId])
REFERENCES [dbo].[QuestionBank] ([questionBankId])
GO
ALTER TABLE [dbo].[ExamCandidate] CHECK CONSTRAINT [FK_ExamCandidate_QuestionBank]
GO
ALTER TABLE [dbo].[ExamCandidate]  WITH CHECK ADD  CONSTRAINT [FK_ExamCandidate_User] FOREIGN KEY([candidateId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ExamCandidate] CHECK CONSTRAINT [FK_ExamCandidate_User]
GO
ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_CourseID] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [FK_Lesson_CourseID]
GO
ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_QuestionBank] FOREIGN KEY([QuestionBankId])
REFERENCES [dbo].[QuestionBank] ([questionBankId])
GO
ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [FK_Lesson_QuestionBank]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_User] FOREIGN KEY([NotificationTo])
REFERENCES [dbo].[User] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_User]
GO
ALTER TABLE [dbo].[QuestionBank]  WITH CHECK ADD  CONSTRAINT [FK_QuestionBank_User] FOREIGN KEY([LecturerId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[QuestionBank] CHECK CONSTRAINT [FK_QuestionBank_User]
GO
ALTER TABLE [dbo].[Reply]  WITH CHECK ADD  CONSTRAINT [FK_Reply_Comment_CommentId] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Comment] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reply] CHECK CONSTRAINT [FK_Reply_Comment_CommentId]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_CourseID] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_CourseID]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_UserID]
GO
ALTER TABLE [dbo].[StudentFee]  WITH CHECK ADD  CONSTRAINT [FK_CE_SF] FOREIGN KEY([CourseEnrollId])
REFERENCES [dbo].[CourseEnroll] ([CourseEnrollID])
GO
ALTER TABLE [dbo].[StudentFee] CHECK CONSTRAINT [FK_CE_SF]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
USE [master]
GO
ALTER DATABASE [ASPNETLearningOnlv1] SET  READ_WRITE 
GO
