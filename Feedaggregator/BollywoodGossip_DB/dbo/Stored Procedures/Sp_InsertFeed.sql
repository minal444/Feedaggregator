CREATE PROC [dbo].[Sp_InsertFeed]    
(    
 @Title nvarchar(max),    
 @Description nvarchar(max),    
 @ImageURL varchar(max),    
 @PublishDate datetime,    
 @RedirectURL varchar(max),    
 @Source int,    
 @CreatedBy varchar(100),    
 @Active bit,    
 @FeedSourceXML varchar(max),
 @UTCPublishDate datetime
)    
AS    
BEGIN    
 IF NOT EXISTS(SELECT 'X' FROM Feeds (nolock) WHERE Title = @Title)    
 BEGIN    
  INSERT INTO Feeds (Title, Description, ImageURL, PublishDate, RedirectURL, Source, CreatedBy, CreatedDate,Active,SourceXML,UTCPublishDate,UpdatedBy,UpdatedDate)    
  VALUES(@Title, @Description, @ImageURL, @PublishDate, @RedirectURL, @Source, @CreatedBy, GETUTCDATE(),@Active,@FeedSourceXML,@UTCPublishDate,@CreatedBy,GETUTCDATE())    
  
  SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]
  
 END    
END

