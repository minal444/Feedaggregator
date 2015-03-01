--exec [Sp_CheckIfUpdateRequired] '2014-07-12 14:49:43.000'
--select * from feeds order by publishdate desc
CREATE Proc [dbo].[Sp_CheckIfUpdateRequired]
(
	@PublishDate DateTime
)
As 
Begin
	If Exists(Select 'x' from Feeds (nolock) Where PublishDate >= @PublishDate)
	Begin
		Select 0
	End
	Else
	Begin
		Select 1
	End 
	
	
End

