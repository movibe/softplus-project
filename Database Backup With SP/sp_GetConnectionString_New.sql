USE [Softplus_OL]
GO

-- =============================================
-- Author:  <Rasel Ahmmed>
-- Create date: <2013-06-06>
-- Description: <Get Connection String by one sp and useing join>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetConnectionString_New] 
 @userName VARCHAR(20),
 @password VARCHAR(20),
 @connString NVARCHAR(200) out,
 @result NVARCHAR(100) out,
 @message NVARCHAR(100) out
AS
BEGIN
 SET NOCOUNT ON;

 DECLARE @specId INT
 DECLARE @serverName NVARCHAR(50)
 DECLARE @databaseName NVARCHAR(50)
 
 SET @connString=''
 IF(@userName='')
 BEGIN
  SET @result='Fail'
  SET @message='User name is requied'
  RETURN 0;  
 END 
 IF(@password='')
 BEGIN
  SET @result='Fail'
  SET @message='Password is requied'
  RETURN 0;  
 END  
 
 /* SEARCH IN INTERNET USER TABLE */
 SELECT @specId=(SELECT SpecID FROM InternetUsers WHERE UserName =@userName and Password=@password)
  IF(@@ERROR<>0 )
  BEGIN
   SET @result='Fail'
   SET @message='Could not found Specification Id'
   RETURN 0;
  END 
 IF(@specId='')
 BEGIN
  SET @result='Fail'
  SET @message='Could not found SpecId for Internet User'
  RETURN 0;  
 END    
  
 IF @specId IS NOT NULL
 BEGIN

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
   SET @message='Server name is null'
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
   SET @message='Database name is null'
   RETURN 0;  
  END
 
   SET @connString='SQL Server: '+  @serverName + ', ' + 'Database Name: ' + @databaseName;
  SET @result='Success'
  SET @message='Connection string populated successfully'
   RETURN 0; 
  
 END
 /* END OF SEARCH IN INTERNET USER TABLE */
 
 ELSE
 /* SEARCH IN INTRANET USER TABLE */
 BEGIN
       
  SELECT @specId =(SELECT t3.SpecID FROM IntranetUsers t1 JOIN UserMapping t2 ON t2.SrvUserID = t1.SrvUserID JOIN InternetUsers t3 ON t3.InetUserID = t2.InetUserID WHERE t1.UserName = @userName AND t1.Password =@password)
  IF(@@ERROR<>0 )
  BEGIN
   SET @result='Fail'
   SET @message='Could not found Specification Id'
   RETURN 0;
  END 
 IF(@specId='')
 BEGIN
  SET @result='Fail'
  SET @message='Could not found SpecId for Intranet User'
  RETURN 0;  
 END 
       
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
   SET @message='Server name is null'
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
   SET @message='Database name is null'
   RETURN 0;  
  END
 
   SET @connString='SQL Server: '+  @serverName + ', ' + 'Database Name: ' + @databaseName;
  SET @result='Success'
  SET @message='Connection string populated successfully'
   RETURN 0;    
  
 END
 /* END SEARCH IN INTRANET USER TABLE */
 
END



GO


