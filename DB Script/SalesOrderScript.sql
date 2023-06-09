USE [SalesOrder]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 4/20/2023 4:10:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderName] [varchar](150) NULL,
	[State] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubElements]    Script Date: 4/20/2023 4:10:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubElements](
	[SubElementsId] [int] IDENTITY(1,1) NOT NULL,
	[WindowId] [int] NULL,
	[Type] [varchar](150) NULL,
	[Width] [int] NULL,
	[Height] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Windows]    Script Date: 4/20/2023 4:10:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Windows](
	[WindowId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[WindowName] [varchar](150) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Sp_Order]    Script Date: 4/20/2023 4:10:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_Order]
	@OrderId int = null,
	@OrderName nvarchar(100) = null,
	@State nvarchar(20) = null,
	@Type nvarchar(20) = null,
	@Width int=null,
	@Height int=null,
	@WndowId int = null,
	@SubId int = null,
	@WindowName nvarchar(100) = null,
	@actionType nvarchar(30) 
AS
BEGIN
	
 IF (@actionType = 'Insert')
	BEGIN
		Insert into [Orders]([OrderName],[State])
		VALUES(@OrderName,@State)  
	END
	ELSE IF (@actionType = 'Update')
	BEGIN
		Update [Orders]
			Set [OrderName] = @OrderName,
				[State] = @State		
			Where OrderId = @OrderId
	END

	ELSE IF (@actionType = 'GetAll')
	BEGIN
		SELECT [OrderId]
			  ,[OrderName]
			  ,[State]		  
		  FROM [Orders]
	END

  ELSE IF (@actionType = 'GetAllWindow')
	BEGIN
		SELECT c.[WindowId]
			  ,c.[WindowName]
			  ,c.[OrderId]
			  ,p.OrderName
		  FROM [Windows] c inner join [Orders] p on p.OrderId = c.OrderId
		  
	END

	ELSE IF (@actionType = 'InsertWndow')
	BEGIN
		Insert into [Windows]([OrderId],[WindowName])
		VALUES(@OrderId ,@WindowName)   
	END

	ELSE IF (@actionType = 'UpdateWindow')
	BEGIN
		Update [Windows]
			Set [WindowName] = @WindowName,
				[OrderId] =@OrderId
			Where WindowId = @WndowId
	END

	 ELSE IF (@actionType = 'GetAllElement')
	BEGIN
		SELECT c.SubElementsId
		      ,c.[Type]
			  ,c.[Width]
			  ,c.[Height]
			  ,p.WindowName
		  FROM [SubElements] c inner join [Windows] p on p.WindowId = c.WindowId
		  
	END

	ELSE IF (@actionType = 'InsertElement')
	BEGIN
		Insert into [SubElements]([WindowId],[Type],[Width],[Height])
		VALUES(@WndowId ,@Type,@Width,@Height)   
	END

	ELSE IF (@actionType = 'UpdateElement')
	BEGIN
		Update [SubElements]
			Set [WindowId] = @WndowId,
				[Type] =@Type,
				[Width] =@Width,
				[Height] =@Height
			Where SubelementsId = @SubId
	END

	ELSE IF (@actionType = 'GetDetailsInfo')
	BEGIN
		SELECT c.[WindowId]
			  ,c.[WindowName]
			  ,c.[OrderId]
			  ,p.OrderName
			  ,p.state
			  ,s.Type
			  ,s.Width
			  ,s.Height
		  FROM [Windows] c 
		  inner join [Orders] p on p.OrderId = c.OrderId
		  inner join [SubElements] s on s.WindowId = c.WindowId
		  Where p.OrderId =@OrderId
		  
	END

	ELSE IF (@actionType = 'GetDetailsInfoUpdate')
	BEGIN
		  UPDATE t1
		  SET t1.state = @State
		  FROM [dbo].[Orders] AS t1
		  INNER JOIN [dbo].[Windows] AS t2  ON t1.OrderId = t2.OrderId
		  INNER JOIN [dbo].[SubElements] AS t3  ON t3.WindowId = t2.WindowId
		  WHERE t1.OrderId = @OrderId;

		  UPDATE t2
		  SET t2.WindowName=@WindowName
		  FROM [dbo].[Orders] AS t1
		  INNER JOIN [dbo].[Windows] AS t2  ON t1.OrderId = t2.OrderId
		  INNER JOIN [dbo].[SubElements] AS t3  ON t3.WindowId = t2.WindowId
		  WHERE t1.OrderId = @OrderId;

		  UPDATE t3
		  SET t3.Width=@Width,
		  t3.Height=@Height
		  FROM [dbo].[Orders] AS t1
		  INNER JOIN [dbo].[Windows] AS t2  ON t1.OrderId = t2.OrderId
		  INNER JOIN [dbo].[SubElements] AS t3  ON t3.WindowId = t2.WindowId
		  WHERE t1.OrderId = @OrderId;
	  
	END

END
GO
