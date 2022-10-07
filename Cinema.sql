USE [master]
GO
/****** Object:  Database [cinema]    Script Date: 07.10.2022 13:46:19 ******/
CREATE DATABASE [cinema]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'cinema', FILENAME = N'F:\programs\SQL_DATA\cinema.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'cinema_log', FILENAME = N'F:\programs\SQL_DATA\cinema_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [cinema] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [cinema].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [cinema] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [cinema] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [cinema] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [cinema] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [cinema] SET ARITHABORT OFF 
GO
ALTER DATABASE [cinema] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [cinema] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [cinema] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [cinema] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [cinema] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [cinema] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [cinema] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [cinema] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [cinema] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [cinema] SET  DISABLE_BROKER 
GO
ALTER DATABASE [cinema] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [cinema] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [cinema] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [cinema] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [cinema] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [cinema] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [cinema] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [cinema] SET RECOVERY FULL 
GO
ALTER DATABASE [cinema] SET  MULTI_USER 
GO
ALTER DATABASE [cinema] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [cinema] SET DB_CHAINING OFF 
GO
ALTER DATABASE [cinema] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [cinema] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [cinema] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [cinema] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'cinema', N'ON'
GO
ALTER DATABASE [cinema] SET QUERY_STORE = OFF
GO
USE [cinema]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 07.10.2022 13:46:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[idBooking] [int] IDENTITY(1,1) NOT NULL,
	[Status] [bit] NOT NULL,
	[idClient] [int] NOT NULL,
	[idSession] [int] NOT NULL,
	[idSeat] [int] NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[idBooking] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 07.10.2022 13:46:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[idClient] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[phoneNumber] [nvarchar](50) NOT NULL,
	[isAdmin] [bit] NOT NULL,
	[password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[idClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hall]    Script Date: 07.10.2022 13:46:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hall](
	[idHall] [int] IDENTITY(1,1) NOT NULL,
	[numberOfRows] [int] NOT NULL,
	[numberOfSeats] [int] NOT NULL,
 CONSTRAINT [PK_Hall] PRIMARY KEY CLUSTERED 
(
	[idHall] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 07.10.2022 13:46:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[idMovie] [int] IDENTITY(1,1) NOT NULL,
	[movieName] [nvarchar](250) NOT NULL,
	[duration] [int] NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[idMovie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seat]    Script Date: 07.10.2022 13:46:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seat](
	[idSeat] [int] IDENTITY(1,1) NOT NULL,
	[idHall] [int] NOT NULL,
	[status] [bit] NOT NULL,
	[seatNumber] [int] NOT NULL,
	[rowNumber] [int] NOT NULL,
	[type] [bit] NOT NULL,
	[sofaNumber] [int] NULL,
 CONSTRAINT [PK_Seat] PRIMARY KEY CLUSTERED 
(
	[idSeat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 07.10.2022 13:46:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[idSession] [int] IDENTITY(1,1) NOT NULL,
	[dateSession] [date] NOT NULL,
	[sessionTime] [time](7) NOT NULL,
	[idMovie] [int] NOT NULL,
	[costPerChair] [int] NULL,
	[costPerSofa] [int] NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[idSession] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Booking] ON 

INSERT [dbo].[Booking] ([idBooking], [Status], [idClient], [idSession], [idSeat]) VALUES (3, 0, 2, 1, 1)
INSERT [dbo].[Booking] ([idBooking], [Status], [idClient], [idSession], [idSeat]) VALUES (4, 1, 3, 2, 2)
SET IDENTITY_INSERT [dbo].[Booking] OFF
GO
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([idClient], [name], [phoneNumber], [isAdmin], [password]) VALUES (2, N'admin', N'79877547829', 1, N'admin')
INSERT [dbo].[Client] ([idClient], [name], [phoneNumber], [isAdmin], [password]) VALUES (3, N'Александр', N'78005059955', 0, N'123')
INSERT [dbo].[Client] ([idClient], [name], [phoneNumber], [isAdmin], [password]) VALUES (7, N'Kraken', N'73458673495', 0, N'Admin123')
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Hall] ON 

INSERT [dbo].[Hall] ([idHall], [numberOfRows], [numberOfSeats]) VALUES (1, 6, 65)
SET IDENTITY_INSERT [dbo].[Hall] OFF
GO
SET IDENTITY_INSERT [dbo].[Movie] ON 

INSERT [dbo].[Movie] ([idMovie], [movieName], [duration]) VALUES (1, N'Пираты Карибского моря', 154)
INSERT [dbo].[Movie] ([idMovie], [movieName], [duration]) VALUES (2, N'Человек паук', 143)
SET IDENTITY_INSERT [dbo].[Movie] OFF
GO
SET IDENTITY_INSERT [dbo].[Seat] ON 

INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (1, 1, 0, 1, 1, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (2, 1, 0, 2, 1, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (3, 1, 0, 3, 1, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (5, 1, 0, 4, 1, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (6, 1, 0, 5, 1, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (8, 1, 0, 6, 1, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (9, 1, 0, 7, 1, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (10, 1, 0, 8, 1, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (11, 1, 0, 9, 1, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (12, 1, 0, 10, 1, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (13, 1, 0, 1, 2, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (14, 1, 0, 2, 2, 0, NULL)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (15, 1, 0, 1, 6, 1, 1)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (16, 1, 0, 2, 6, 1, 1)
INSERT [dbo].[Seat] ([idSeat], [idHall], [status], [seatNumber], [rowNumber], [type], [sofaNumber]) VALUES (17, 1, 0, 3, 6, 1, 1)
SET IDENTITY_INSERT [dbo].[Seat] OFF
GO
SET IDENTITY_INSERT [dbo].[Session] ON 

INSERT [dbo].[Session] ([idSession], [dateSession], [sessionTime], [idMovie], [costPerChair], [costPerSofa]) VALUES (1, CAST(N'2022-09-20' AS Date), CAST(N'21:05:00' AS Time), 1, 220, 550)
INSERT [dbo].[Session] ([idSession], [dateSession], [sessionTime], [idMovie], [costPerChair], [costPerSofa]) VALUES (2, CAST(N'2022-09-21' AS Date), CAST(N'12:20:00' AS Time), 2, 220, 550)
SET IDENTITY_INSERT [dbo].[Session] OFF
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Client] FOREIGN KEY([idClient])
REFERENCES [dbo].[Client] ([idClient])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Client]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Seat] FOREIGN KEY([idSeat])
REFERENCES [dbo].[Seat] ([idSeat])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Seat]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Session] FOREIGN KEY([idSession])
REFERENCES [dbo].[Session] ([idSession])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Session]
GO
ALTER TABLE [dbo].[Seat]  WITH CHECK ADD  CONSTRAINT [FK_Seat_Hall] FOREIGN KEY([idHall])
REFERENCES [dbo].[Hall] ([idHall])
GO
ALTER TABLE [dbo].[Seat] CHECK CONSTRAINT [FK_Seat_Hall]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Movie] FOREIGN KEY([idMovie])
REFERENCES [dbo].[Movie] ([idMovie])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Movie]
GO
USE [master]
GO
ALTER DATABASE [cinema] SET  READ_WRITE 
GO
