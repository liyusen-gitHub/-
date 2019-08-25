USE [OrBitXI]
GO

INSERT INTO [dbo].[OrderDetail]
           ([OrderDetailId]
           ,[ProductOrderId]
           ,[OrdId]
           ,[OlnId]
           ,[ProductName]
           ,[ProductDescription]
           ,[Wide]
           ,[High]
           ,[Deth]
           ,[Remark]
           ,[Ctime])
     VALUES
           (<OrderDetailId, char(12),>
           ,<ProductOrderId, char(12),>
           ,<OrdId, int,>
           ,<OlnId, int,>
           ,<ProductName, nvarchar(100),>
           ,<ProductDescription, nvarchar(100),>
           ,<Wide, float,>
           ,<High, float,>
           ,<Deth, float,>
           ,<Remark, nvarchar(100),>
           ,<Ctime, datetime,>)
GO

USE [OrBitXI]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Pro_OrderDetail_ADD]
(@OrderDetailId CHAR(12)
           ,@ProductOrderId char(12)
           ,@OrdId int
           ,@OlnId int
           ,@ProductName nvarchar(100)
           ,@ProductDescription nvarchar(100)
           ,@Wide float
           ,@High float
           ,@Deth float
           ,@Remark nvarchar(100)
           ,@Ctime datetime)
AS
BEGIN TRY
	INSERT INTO OrderDetail(ProductOrderId,OrdId,ProductName,ProductDescription,Wide,High,Deth,Remark)
	values(@ProductOrderId,@OrdId,@ProductName,@ProductDescription,@Wide,@High,@Deth,@Remark)
	PRINT 1
	END TRY

BEGIN CATCH
SET NOCOUNT ON;
DECLARE @MGS VARCHAR(500)=error_message()
PRINT 0
END CATCH





