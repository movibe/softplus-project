USE [Softplus_OL]
GO

-- =============================================
-- Author:  <Rasel Ahmmed>
-- Create date: <2013-06-06>
-- Description: <Get Connection String>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetConnectionString] 
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
 DECLARE @svrUserId NVARCHAR(50)
 
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
 SELECT @specId=(SELECT SpecID from InternetUsers where UserName =@userName and Password=@password)
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
  EXEC [dbo].[sp_GetConnectionFromSpec] @specId, @connString out, @result out, @message out  
  RETURN 0; 
  
 END
 /* END OF SEARCH IN INTERNET USER TABLE */
 
 ELSE
 /* SEARCH IN INTRANET USER TABLE */
 BEGIN
  DECLARE @InternetUserId NVARCHAR(20) 
  SELECT @svrUserId=(SELECT SrvUserID from IntranetUsers where UserName =@userName and Password=@password)
  IF(@@ERROR<>0 )
  BEGIN
   SET @result='Fail'
   SET @message='Could not found Server User Id'
   RETURN 0;
  END
  IF(@svrUserId='')
  BEGIN
   SET @result='Fail'
   SET @message='Could not found Server User Id'
   RETURN 0;  
  END
  
  SELECT @InternetUserId=(SELECT TOP 1 InetUserID from UserMapping where SrvUserID = @svrUserId) 
  IF(@@ERROR<>0 )
  BEGIN
   SET @result='Fail'
   SET @message='Could not found Server User Id'
   RETURN 0;
  END
  IF(@svrUserId='')
  BEGIN
   SET @result='Fail'
   SET @message='Could not found Internet User Id'
   RETURN 0;  
  END
  
  SELECT @specId=(SELECT SpecID from InternetUsers where InetUserID =@InternetUserId)
  IF(@@ERROR<>0 )
  BEGIN
   SET @result='Fail'
   SET @message='Could not found Specification Id for Intranet User'
   RETURN 0;
  END 
  IF(@specId='')
  BEGIN
   SET @result='Fail'
   SET @message='Could not found Intranet User Id'
   RETURN 0;  
  END    
  EXEC [dbo].[sp_GetConnectionFromSpec] @specId, @connString out, @result out,@message out  
  RETURN 0;    
  
 END
 /* END SEARCH IN INTRANET USER TABLE */
 
END


GO


