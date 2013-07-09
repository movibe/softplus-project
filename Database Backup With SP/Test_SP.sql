USE [Softplus_OL]
GO

-- USER THIS SP

DECLARE @RC INT
DECLARE @userName VARCHAR(20)
DECLARE @password VARCHAR(20)
DECLARE @connString NVARCHAR(200)
DECLARE @result NVARCHAR(100)
DECLARE @message NVARCHAR(100)

-- TODO: Set parameter values here.
set @userName='sa'
set @password='shamim'

EXECUTE @RC = [Softplus_OL].[dbo].[sp_GetConnectionString] 
   @userName
  ,@password
  ,@connString OUTPUT
  ,@result OUTPUT
  ,@message OUTPUT

print @result
print @message
print @connString


-- USER THIS SP

DECLARE @RC INT
DECLARE @userName VARCHAR(20)
DECLARE @password VARCHAR(20)
DECLARE @connString NVARCHAR(200)
DECLARE @result NVARCHAR(100)
DECLARE @message NVARCHAR(100)

-- TODO: Set parameter values here.
set @userName='uttam'
set @password='uttam'

EXECUTE @RC = [Softplus_OL].[dbo].[sp_GetConnectionString_New] 
   @userName
  ,@password
  ,@connString OUTPUT
  ,@result OUTPUT
  ,@message OUTPUT

print @result
print @message
print @connString

USE [Softplus_Testing]
GO

-- USER THIS SP

DECLARE @RC2 INT
DECLARE @customerName VARCHAR(50)
DECLARE @address VARCHAR(200)
DECLARE @loadDate SMALLDATETIME
DECLARE @detailItems tbl_DetailInfoType
DECLARE @result NVARCHAR(100)
DECLARE @message NVARCHAR(100)

-- TODO: Set parameter values here.
set @customerName='uttam'
set @address='dhaka'
SET @loadDate = '1/1/2000'

INSERT INTO @detailItems(ItemSizeID,Quantity) 
VALUES (1,500), 
	(2,700), 
	(3,900)

EXECUTE @RC2 = [Softplus_Testing].[dbo].[sp_InsertMasterDetails] 
   @customerName
  ,@address
  ,@loadDate
  ,@detailItems
  ,@result OUTPUT
  ,@message OUTPUT

print @result
print @message