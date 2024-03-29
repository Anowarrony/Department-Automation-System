USE [master]
GO
/****** Object:  Database [DepartmentAuomationtDatabase]    Script Date: 1/26/2016 8:03:23 PM ******/
CREATE DATABASE [DepartmentAuomationtDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DepartmentAuomationtDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DepartmentAuomationtDatabase.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DepartmentAuomationtDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DepartmentAuomationtDatabase_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DepartmentAuomationtDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DepartmentAuomationtDatabase]
GO
/****** Object:  Table [dbo].[AdminLoginTable]    Script Date: 1/26/2016 8:03:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdminLoginTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AdminLoginTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Backlog]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Backlog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseCode] [varchar](80) NOT NULL,
	[StudentId] [varchar](100) NOT NULL,
	[YearTermId] [int] NOT NULL,
 CONSTRAINT [PK_Backlok] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BacklokRegistration]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BacklokRegistration](
	[BacklokRegId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [varchar](100) NULL,
	[CourseCode] [varchar](300) NULL,
 CONSTRAINT [PK_BacklocRegTable] PRIMARY KEY CLUSTERED 
(
	[BacklokRegId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Course]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseCode] [varchar](100) NOT NULL,
	[CoursTitle] [varchar](150) NOT NULL,
	[Credit] [float] NOT NULL,
	[YearTermId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CurrentSemister]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrentSemister](
	[CurrentSemId] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[YearTermId] [int] NOT NULL,
 CONSTRAINT [PK_CurrentSemister] PRIMARY KEY CLUSTERED 
(
	[CurrentSemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentShortName] [varchar](20) NULL,
	[DepartmentFullName] [varchar](100) NOT NULL,
	[FacultyId] [int] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Faculty](
	[FacultyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Faculty] PRIMARY KEY CLUSTERED 
(
	[FacultyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GpaTbl]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GpaTbl](
	[GpaId] [int] IDENTITY(1,1) NOT NULL,
	[Gpa] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Gpa] PRIMARY KEY CLUSTERED 
(
	[GpaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Grade]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Grade](
	[GradeId] [int] IDENTITY(1,1) NOT NULL,
	[LetterGrade] [varchar](50) NOT NULL,
	[GradePoint] [float] NOT NULL,
 CONSTRAINT [PK_Grade] PRIMARY KEY CLUSTERED 
(
	[GradeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Hall]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Hall](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HallName] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Login]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Login](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[UniversityId] [varchar](80) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistrationDate]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistrationDate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[DepartmentId] [int] NULL,
	[DaysLeft] [int] NULL,
	[AllowReg] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RegistrationDetail]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegistrationDetail](
	[RegistrationDetailId] [int] IDENTITY(1,1) NOT NULL,
	[YearTermId] [int] NOT NULL,
	[StudentId] [varchar](50) NOT NULL,
	[TotalCreditTaken] [float] NOT NULL,
	[DeptId] [int] NOT NULL,
	[RegisteredDate] [datetime] NULL,
	[Residential] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RegistrationDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Result]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Result](
	[ResultId] [int] IDENTITY(1,1) NOT NULL,
	[CourseCode] [varchar](50) NOT NULL,
	[StudentId] [varchar](150) NOT NULL,
	[Cgpa] [float] NULL,
	[YearTermId] [int] NOT NULL,
	[GradeId] [int] NOT NULL,
 CONSTRAINT [PK_Result] PRIMARY KEY CLUSTERED 
(
	[ResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Session]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Session](
	[SessionId] [int] IDENTITY(1,1) NOT NULL,
	[SessionName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentFee]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentFee](
	[FeesId] [int] IDENTITY(1,1) NOT NULL,
	[AdmissionFee] [int] NOT NULL,
	[CreditHourFee] [int] NOT NULL,
	[StudentTrustFund] [int] NOT NULL,
	[HallRent] [int] NOT NULL,
	[CentralLibraryFee] [int] NOT NULL,
	[SeminarLibraryFee] [int] NOT NULL,
	[NonResidentialTransfortFee] [int] NOT NULL,
	[ResidentialTransfortFee] [int] NOT NULL,
	[LateFee] [int] NOT NULL,
	[Others] [int] NULL,
	[BacklogFeePerCourse] [int] NOT NULL,
 CONSTRAINT [PK_StudentFee] PRIMARY KEY CLUSTERED 
(
	[FeesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StudentInformation]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[FacultyId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[SessionId] [int] NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Gender] [varchar](50) NOT NULL,
	[Nationality] [varchar](50) NOT NULL,
	[PresentAddress] [varchar](50) NOT NULL,
	[ParmanentAddress] [varchar](50) NOT NULL,
	[StudentImage] [image] NOT NULL,
	[HallId] [int] NULL,
 CONSTRAINT [PK_StudentInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Temp]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Temp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Gpa] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Temp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Test]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Test](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cou] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[YearTerm]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[YearTerm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[YearTermName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_YearTerm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[StudentResultDetails]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[StudentResultDetails]
as
select Name,DepartmentFullName,r.StudentId,YearTermName,sum(Credit) TotalCompletedCredit,sum(Credit *GradePoint) as TotalGpa,sum(Credit *GradePoint)/sum(Credit) as Cgpa from dbo.Course  c
join dbo.Result r on c.CourseCode=r.CourseCode
join dbo.StudentInformation s on r.StudentId=s.StudentId
join dbo.Department d on s.DepartmentId=d.DepartmentId
join dbo.YearTerm y on c.Yeartermid=y.id
join dbo.Grade g on r.GradeId=g.GradeId
group by r.StudentId,DepartmentFullName,Name,YearTermName


GO
/****** Object:  View [dbo].[SudentResultView]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[SudentResultView]
as
select Name,DepartmentName,r.StudentId,YearTermName,sum(Credit) TotalCompletedCredit,sum(Credit *GradePoint) as TotalGpa,sum(Credit *GradePoint)/sum(Credit) as Cgpa from dbo.Course  c
join dbo.Result r on c.CourseCode=r.CourseCode
join dbo.StudentInformation s on r.StudentId=s.StudentId
join dbo.Department d on s.DepartmentId=d.DepartmentId
join dbo.YearTerm y on c.Yeartermid=y.id
join dbo.Grade g on r.GradeId=g.GradeId
group by r.StudentId,DepartmentName,Name,YearTermName

GO
/****** Object:  View [dbo].[view_StdentDetails]    Script Date: 1/26/2016 8:03:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[view_StdentDetails] As 
select s.name as StdentName,StudentId,f.Name as FacltyName,DepartmentFullName as department,SessionName as Session,s.HallId as hallId from [dbo].[StudentInformation] s
join [dbo].[Department] d on s.DepartmentId=d.DepartmentId
join [dbo].[Session] ss on s.SessionId=ss.SessionId
join [dbo].[Faculty] f on s.FacultyId=f.FacultyId 
GO
USE [master]
GO
ALTER DATABASE [DepartmentAuomationtDatabase] SET  READ_WRITE 
GO
