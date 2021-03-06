USE [master]
GO
/****** Object:  Database [infrasol]    Script Date: 6/12/2019 8:29:48 PM ******/
CREATE DATABASE [infrasol]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'infrasol', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\infrasol.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'infrasol_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\infrasol_log.ldf' , SIZE = 7168KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [infrasol] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [infrasol].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [infrasol] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [infrasol] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [infrasol] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [infrasol] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [infrasol] SET ARITHABORT OFF 
GO
ALTER DATABASE [infrasol] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [infrasol] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [infrasol] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [infrasol] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [infrasol] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [infrasol] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [infrasol] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [infrasol] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [infrasol] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [infrasol] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [infrasol] SET  DISABLE_BROKER 
GO
ALTER DATABASE [infrasol] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [infrasol] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [infrasol] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [infrasol] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [infrasol] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [infrasol] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [infrasol] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [infrasol] SET RECOVERY FULL 
GO
ALTER DATABASE [infrasol] SET  MULTI_USER 
GO
ALTER DATABASE [infrasol] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [infrasol] SET DB_CHAINING OFF 
GO
ALTER DATABASE [infrasol] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [infrasol] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [infrasol]
GO
/****** Object:  Table [dbo].[BrandMaster]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandMaster](
	[BrandID] [int] IDENTITY(1,1) NOT NULL,
	[Brand] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClientMaster]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientMaster](
	[ClientID] [nvarchar](50) NOT NULL,
	[ClientName] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](50) NULL,
	[ContactPerson] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[EmployeeID] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClientserviceMaster]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientserviceMaster](
	[ClientServicingID] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [nvarchar](50) NOT NULL,
	[EmployeeID] [nvarchar](50) NOT NULL,
	[SurveyEngineerID] [nvarchar](50) NULL,
	[SurveyDone] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CurrencyMaster]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CurrencyMaster](
	[CurrencyID] [int] IDENTITY(1,1) NOT NULL,
	[Currency] [varchar](20) NOT NULL,
	[LatestValue] [numeric](10, 2) NOT NULL,
	[LatestValueDate] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DepartmentMaster]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentMaster](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[Department] [nvarchar](50) NOT NULL,
	[HomePage] [nvarchar](50) NULL,
	[MasterPage] [nvarchar](50) NULL,
	[Category] [nchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeUserMaster]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeUserMaster](
	[userID] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[EmailID] [nvarchar](50) NULL,
	[Active] [bit] NULL,
	[UserType] [nchar](10) NULL,
	[EmployeeName] [nvarchar](50) NULL,
	[Designation] [nvarchar](50) NULL,
	[DepartmentID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemGroupMaster]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemGroupMaster](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemMainMaster]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemMainMaster](
	[MItemID] [int] IDENTITY(1,1) NOT NULL,
	[Item] [nvarchar](50) NULL,
	[Unit] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ItemMaster]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemMaster](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[MItemID] [int] NOT NULL,
	[MItem] [nvarchar](20) NOT NULL,
	[BrandID] [int] NULL,
	[Brand] [nvarchar](20) NOT NULL,
	[ItemName] [nvarchar](60) NOT NULL,
	[PartNumber] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](20) NOT NULL,
	[Unit] [nchar](10) NULL,
	[groupID] [int] NULL,
 CONSTRAINT [PK_ItemMaster] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuotationHeader]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuotationHeader](
	[QuotationID] [int] IDENTITY(1,1) NOT NULL,
	[QuotationDate] [datetime] NULL,
	[SurveyID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SupplierMaster]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierMaster](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SurveyClientItemDetails]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyClientItemDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NOT NULL,
	[ItemID] [int] NOT NULL,
	[Quantity] [numeric](18, 2) NOT NULL,
	[ClientID] [nvarchar](50) NULL,
	[SurveyEngineerID] [nvarchar](50) NULL,
	[Remark] [nvarchar](max) NULL,
	[CheckedDate] [datetime] NULL,
	[QuotedPriceTotal] [numeric](18, 2) NOT NULL,
	[QuotedPriceUnit] [numeric](18, 2) NOT NULL,
	[ActualUtilisation] [numeric](18, 2) NOT NULL,
	[UtilizeUpdateDate] [datetime] NULL,
	[SupplierID] [int] NULL,
	[SalesMargin] [float] NULL,
	[PurchaseCost] [numeric](18, 2) NOT NULL,
	[ChangeStatus] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[surveyClientItemHead]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[surveyClientItemHead](
	[SurveyID] [int] IDENTITY(1,1) NOT NULL,
	[SubmittedforSurvey] [bit] NOT NULL,
	[SubmittedDate] [datetime] NOT NULL,
	[StartDate] [datetime] NULL,
	[FinishDate] [datetime] NULL,
	[ClientID] [nvarchar](50) NULL,
	[SurveyEngineerID] [nvarchar](50) NULL,
	[ProjectStatus] [bit] NOT NULL,
	[ProjectFinishDate] [datetime] NULL,
	[SubmittedForQuotation] [bit] NOT NULL,
	[quotationdone] [bit] NOT NULL,
	[QuotationSubmittedDate] [datetime] NULL,
	[ProjectEngineerID] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserAccessMenu]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccessMenu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuID] [int] NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[Sequence] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserMenu]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMenu](
	[menuID] [int] NOT NULL,
	[MenuName] [nvarchar](50) NULL,
	[hyperLink] [nvarchar](50) NULL,
	[sequence] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SupplierMaster]    Script Date: 6/12/2019 8:29:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierMaster](
	[SupplierID] [int] NOT NULL,
	[SupplierName] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ClientserviceMaster] ADD  CONSTRAINT [DF_ClientserviceMaster_SurveyDone]  DEFAULT ((0)) FOR [SurveyDone]
GO
ALTER TABLE [dbo].[ItemMaster] ADD  CONSTRAINT [DF_ItemMaster_MItem]  DEFAULT (' ') FOR [MItem]
GO
ALTER TABLE [dbo].[ItemMaster] ADD  CONSTRAINT [DF_ItemMaster_Brand]  DEFAULT (' ') FOR [Brand]
GO
ALTER TABLE [dbo].[ItemMaster] ADD  CONSTRAINT [DF_ItemMaster_ItemName]  DEFAULT (' ') FOR [ItemName]
GO
ALTER TABLE [dbo].[ItemMaster] ADD  CONSTRAINT [DF_ItemMaster_PartNumber]  DEFAULT (' ') FOR [PartNumber]
GO
ALTER TABLE [dbo].[ItemMaster] ADD  CONSTRAINT [DF_ItemMaster_Description]  DEFAULT (' ') FOR [Description]
GO
ALTER TABLE [dbo].[ItemMaster] ADD  CONSTRAINT [DF_ItemMaster_Unit]  DEFAULT (' ') FOR [Unit]
GO
ALTER TABLE [dbo].[SurveyClientItemDetails] ADD  CONSTRAINT [DF_SurveyClientItemDetails_QuotedPriceTotal]  DEFAULT ((0.00)) FOR [QuotedPriceTotal]
GO
ALTER TABLE [dbo].[SurveyClientItemDetails] ADD  CONSTRAINT [DF_SurveyClientItemDetails_QuotedPriceUnit]  DEFAULT ((0.00)) FOR [QuotedPriceUnit]
GO
ALTER TABLE [dbo].[SurveyClientItemDetails] ADD  CONSTRAINT [DF_SurveyClientItemDetails_ActualUtilisation]  DEFAULT ((0.00)) FOR [ActualUtilisation]
GO
ALTER TABLE [dbo].[SurveyClientItemDetails] ADD  CONSTRAINT [DF_SurveyClientItemDetails_PurchaseCost]  DEFAULT ((0)) FOR [PurchaseCost]
GO
ALTER TABLE [dbo].[SurveyClientItemDetails] ADD  CONSTRAINT [DF_SurveyClientItemDetails_ChangeStatus]  DEFAULT ((0)) FOR [ChangeStatus]
GO
ALTER TABLE [dbo].[surveyClientItemHead] ADD  CONSTRAINT [DF_surveyClientItemHead_SubmittedforSurvey]  DEFAULT ((1)) FOR [SubmittedforSurvey]
GO
ALTER TABLE [dbo].[surveyClientItemHead] ADD  CONSTRAINT [DF_surveyClientItemHead_SubmittedDate]  DEFAULT (getdate()) FOR [SubmittedDate]
GO
ALTER TABLE [dbo].[surveyClientItemHead] ADD  CONSTRAINT [DF_surveyClientItemHead_ProjectStatus]  DEFAULT ((0)) FOR [ProjectStatus]
GO
ALTER TABLE [dbo].[surveyClientItemHead] ADD  CONSTRAINT [DF_surveyClientItemHead_SubmittedForQuotation]  DEFAULT ((0)) FOR [SubmittedForQuotation]
GO
ALTER TABLE [dbo].[surveyClientItemHead] ADD  CONSTRAINT [DF_surveyClientItemHead_quotationdone]  DEFAULT ((0)) FOR [quotationdone]
GO
USE [master]
GO
ALTER DATABASE [infrasol] SET  READ_WRITE 
GO
