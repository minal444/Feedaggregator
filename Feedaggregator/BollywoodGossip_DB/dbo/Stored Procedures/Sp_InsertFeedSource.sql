Create Proc [dbo].[Sp_InsertFeedSource]
(
	@Id int = null,
	@SourceName varchar(100),
	@SourceURL varchar(1000),
	@Active bit,
	@StartDate datetime,
	@EndDate datetime =null,
	@SiteURL varchar(1000) = null,
	@LogoURL varchar(1000) = null,
	@Imagesource varchar(100) = null,
	@ImageNameSpace varchar(1000) = null,
	@DescriptionNameSpace varchar(500) = null
)
as
begin
	if(@Id is null)
	begin
		insert into FeedSource (SourceName, SourceURL,Active,StartDate,EndDate,CreatedDate,UpdatedDate,SiteURL
								,LogoURL,Imagesource,ImageNameSpace,DescriptionNameSpace)
						values (@SourceName,@SourceURL,@Active,@StartDate,@EndDate,GETUTCDATE(),null,@SiteURL
								,@LogoURL,@Imagesource,@ImageNameSpace,@DescriptionNameSpace)
	end
	else 
	begin
		update FeedSource set SourceName= @SourceName,
			SourceURL =@SourceURL ,
			Active = @Active,
			StartDate = @StartDate,
			EndDate= @EndDate,
			UpdatedDate = GETUTCDATE(),
			SiteURL = @SiteURL,
			LogoURL = @LogoURL,
			Imagesource= @Imagesource,
			ImageNameSpace=@ImageNameSpace,
			DescriptionNameSpace=@DescriptionNameSpace
	where FeedSourceID = @Id
	end
end

