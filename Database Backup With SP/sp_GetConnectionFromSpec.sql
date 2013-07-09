USE [Softplus_OL]
GO


-- =============================================
-- Author:  <Rasel Ahmmed>
-- Create date: <2013-06-06>
-- Description: <Get Connection From Spec>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetConnectionFromSpec] 
@specId int,
@connString NVARCHAR(200) out,
 @result NVARCHAR(100) out,
 @message NVARCHAR(100) out
AS

BEGIN

 SET NOCOUNT ON;

 DECLARE @serverName VARCHAR(50)
 DECLARE @databaseName VARCHAR(50)   
  
  SET @serverName=(SELECT SQLServer from Specification where SpecID =@specId)
  IF(@@ERROR<>0 )
  BEGIN
   SET @result='Fail'
   SET @message='Could not found server name'
   RETURN 0;
  END
  
  IF(@serverName='')
  BEGIN
   SET @result='Fail'
   SET @message='Server name is requied'
   RETURN 0;  
  END 
  SET @databaseName=(SELECT DBName from Specification where SpecID =@specId)
  
  IF(@@ERROR<>0)
  BEGIN
   SET @result='Fail'
   SET @message='Could not found database name'
   RETURN 0;
  END
  
  IF(@databaseName='')
  BEGIN
   SET @result='Fail'
   SET @message='Database name is requied'
   RETURN 0;  
  END
  
  
  SET @connString='SQL Server: '+  @serverName + ', ' + 'Database Name: ' + @databaseName;
  SET @result='Success'
  SET @message='Connection string populated successfully'
    
  RETURN 0;

END


GO


