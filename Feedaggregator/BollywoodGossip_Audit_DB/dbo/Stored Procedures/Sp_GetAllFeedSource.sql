CREATE PROC [dbo].[Sp_GetAllFeedSource]
AS
BEGIN
	SELECT 
		FeedSourceID, 
		SourceName, 
		SourceURL,
		SiteURL,
		Active,
		isnull(StartDate,'') as StartDate,
		isnull(EndDate,'') as EndDate,
		isnull(CreatedDate,'') as CreatedDate,
		isnull(UpdatedDate,'') as UpdatedDate,
		isnull(SiteURL,'') as SiteURL,
		isnull(LogoURL,'') as LogoURL,
		isnull(ImageSource,'') as ImageSource,
		isnull(ImageNameSpace,'') as ImageNameSpace,
		isnull(DescriptionNameSpace,'') as DescriptionNameSpace
	FROM FeedSource (nolock)
END

