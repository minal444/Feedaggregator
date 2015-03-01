create PROC [dbo].[Sp_InsertErrorLog]    
(    
	@ErrorDescription	varchar(max),
	@Exception	nvarchar(max),
	@Stacktrace	nvarchar(max),
	@ErrorMetadata	nvarchar(max),
	@ErrorSource	varchar(max)
 
)    
AS    
BEGIN    
  INSERT INTO ErrorLog(ErrorDescription,Exception,Stacktrace,ErrorMetadata,CreatedDate,UpdateDate,ErrorSource)
  VALUES(@ErrorDescription,@Exception,@Stacktrace,@ErrorMetadata,GETUTCDATE(),null,@ErrorSource)    
END

