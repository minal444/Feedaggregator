
CREATE  Proc [dbo].[Sp_SyncDB]
as
Begin

Declare @LastRunDate Datetime
select @LastRunDate = CONVERT(datetime,ConfigurationValue) from Configuration	 where ConfigurationName = 'LastBackupDate'

Declare @ExecutionStartDate Datetime
set @ExecutionStartDate = GETUTCDATE()
print @LastRunDate 
print @ExecutionStartDate 

IF @LASTRUNDATE ='1900-01-01 00:00:00.000'
	BEGIN
	Insert into ErrorLog   (ErrorDescription
								,Exception
								,Stacktrace
								,ErrorMetadata
								,CreatedDate
								,UpdateDate
								,ErrorSource)
		values ('No Config Entry for last rundate',@LASTRUNDATE,null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
	END
ELSE
	BEGIN	

	BEGIN --Configuration
	Begin Tran SyncDB
	
	--Begin Tran SyncConfiguration
		Begin Try
		--Insert new one
			insert into Configuration 
							select 
							ConfigurationID
							,ConfigurationName
							,ConfigurationValue
							,CreatedBy
							,CreatedDate
							,UpdatedBy
							,UpdatedDate
							from BollywoodGossip.dbo.Configuration (nolock) where CreatedDate >= @LastRunDate and CreatedDate < @ExecutionStartDate
							AND ConfigurationName != 'LastBackupDate'

		--update	
			update Configuration
					set 
							ConfigurationName = sourceConfiguration.ConfigurationName
							,ConfigurationValue = sourceConfiguration.ConfigurationValue
							,CreatedBy = sourceConfiguration.CreatedBy
							,CreatedDate = sourceConfiguration.CreatedDate
							,UpdatedBy = sourceConfiguration.UpdatedBy
							,UpdatedDate = sourceConfiguration.UpdatedDate
					From BollywoodGossip.dbo.Configuration  sourceConfiguration (nolock)
					where sourceConfiguration.CreatedDate < @LastRunDate and 
					sourceConfiguration.UpdatedDate >= @LastRunDate and 
					sourceConfiguration.UpdatedDate < @ExecutionStartDate and
					sourceConfiguration.ConfigurationName != 'LastBackupDate' and
					Configuration.ConfigurationID = sourceConfiguration.ConfigurationID
					
			--Commit Tran SyncConfiguration
		End Try
		Begin Catch
			--Rollback Tran SyncConfiguration
			Rollback Tran SyncDB
	
			Insert into ErrorLog   (ErrorDescription
									,Exception
									,Stacktrace
									,ErrorMetadata
									,CreatedDate
									,UpdateDate
									,ErrorSource)
			values ('SyncConfiguration',ERROR_MESSAGE(),null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
			Return
		End Catch

	END
	
	BEGIN --DestinationColumns
		--Begin Tran SyncDestinationColumns
		Begin Try
		
		--insert
			insert into DestinationColumns 
							select 
							DestinationColumnId
							,DestinationColumnName
							,CreatedDate
							,UpdatedDate
							from BollywoodGossip.dbo.DestinationColumns (nolock) where CreatedDate >= @LastRunDate and CreatedDate < @ExecutionStartDate

		--update	
			update DestinationColumns
					set 
						DestinationColumnName= sourceDestinationColumns.DestinationColumnName
						,CreatedDate = sourceDestinationColumns.CreatedDate
						,UpdatedDate = sourceDestinationColumns.UpdatedDate
					From BollywoodGossip.dbo.DestinationColumns  sourceDestinationColumns (nolock)
					where sourceDestinationColumns.CreatedDate < @LastRunDate and 
					sourceDestinationColumns.UpdatedDate >= @LastRunDate and 
					sourceDestinationColumns.UpdatedDate < @ExecutionStartDate and
					DestinationColumns.DestinationColumnId = sourceDestinationColumns.DestinationColumnId
			--Commit Tran SyncDestinationColumns
		End Try
		Begin Catch
			--Rollback Tran SyncDestinationColumns;
			Rollback Tran SyncDB
			Insert into ErrorLog   (ErrorDescription
									,Exception
									,Stacktrace
									,ErrorMetadata
									,CreatedDate
									,UpdateDate
									,ErrorSource)
			values ('SyncDestinationColumns',ERROR_MESSAGE(),null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
			Return
			--IF @@TRANCOUNT > 0
			
		End Catch
			
	END

	BEGIN --SourceColumns
	--Begin Tran SyncSourceColumns
		Begin Try
		--Insert new one
			insert into SourceColumns 
							select 
							SourceColumnId
							,SourceColumnName
							,FeedSourceId
							,CreatedBy
							,CreatedDate
							,UpdatedBy
							,UpdatedDate
							from BollywoodGossip.dbo.SourceColumns (nolock) where CreatedDate >= @LastRunDate and CreatedDate < @ExecutionStartDate

		--update	
			update SourceColumns
					set 
						SourceColumnName= sourceSourceColumns.SourceColumnName
						,FeedSourceId= sourceSourceColumns.FeedSourceId
						,CreatedBy= sourceSourceColumns.CreatedBy
						,CreatedDate= sourceSourceColumns.CreatedDate
						,UpdatedBy= sourceSourceColumns.UpdatedBy
						,UpdatedDate= sourceSourceColumns.UpdatedDate
					From BollywoodGossip.dbo.SourceColumns  sourceSourceColumns (nolock)
					where sourceSourceColumns.CreatedDate < @LastRunDate and 
					sourceSourceColumns.UpdatedDate >= @LastRunDate and 
					sourceSourceColumns.UpdatedDate < @ExecutionStartDate and
					SourceColumns.SourceColumnId = sourceSourceColumns.SourceColumnId
						
			--Commit Tran SyncSourceColumns
		End Try
		Begin Catch
			--Rollback Tran SyncSourceColumns
			Rollback Tran SyncDB
			Insert into ErrorLog   (ErrorDescription
									,Exception
									,Stacktrace
									,ErrorMetadata
									,CreatedDate
									,UpdateDate
									,ErrorSource)
			values ('SyncSourceColumns',ERROR_MESSAGE(),null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
			Return
		End Catch

	END

	BEGIN --SourceDestinationColumnMapping
	--Begin Tran SyncSDColumnMapping
		Begin Try
		--Insert new one
			insert into SourceDestinationColumnMapping 
							select 
							SourceColumnId
							,DestinationColumnId
							,CreatedBy
							,CreatedDate
							,UpdatedBy
							,UpdatedDate
							from BollywoodGossip.dbo.SourceDestinationColumnMapping (nolock) where CreatedDate >= @LastRunDate and CreatedDate < @ExecutionStartDate

		--update	
		--no update required as this table only have primary mapping key
			--update SourceDestinationColumnMapping
			--		set 
			--			CreatedBy= sourceSourceDestinationColumnMapping.CreatedBy
			--			,CreatedDate= sourceSourceDestinationColumnMapping.CreatedDate
			--			,UpdatedBy= sourceSourceDestinationColumnMapping.UpdatedBy
			--			,UpdatedDate= sourceSourceDestinationColumnMapping.UpdatedDate
			--		From BollywoodGossip.dbo.SourceDestinationColumnMapping  sourceSourceDestinationColumnMapping (nolock)
			--		where sourceSourceDestinationColumnMapping.CreatedDate < @LastRunDate and 
			--		sourceSourceDestinationColumnMapping.UpdatedDate >= @LastRunDate and 
			--		sourceSourceDestinationColumnMapping.UpdatedDate < @ExecutionStartDate and
			--		SourceDestinationColumnMapping.SourceColumnId = sourceSourceDestinationColumnMapping.SourceColumnId
						
			--Commit Tran SyncSDColumnMapping
		End Try
		Begin Catch
			--Rollback Tran SyncSDColumnMapping
			Rollback Tran SyncDB
			Insert into ErrorLog   (ErrorDescription
									,Exception
									,Stacktrace
									,ErrorMetadata
									,CreatedDate
									,UpdateDate
									,ErrorSource)
			values ('SyncSourceDestinationColumnMapping',ERROR_MESSAGE(),null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
			Return
		End Catch

	END

	BEGIN --DeviceConfig
	--Begin Tran SyncDeviceConfig
		Begin Try
		--Insert new one
			insert into DeviceConfig 
		select 
		DeviceConfigID
		,DeviceID
		,Notification
		,MaxFeedID
		,Version
		,CreatedDate
		,UpdatedDate
		,DeviceManufacturer
		,DeviceModel
		,OsVersion
		,CarrierName
		,NetworkType
		,Active
		from BollywoodGossip.dbo.DeviceConfig (nolock) where CreatedDate >= @LastRunDate and CreatedDate < @ExecutionStartDate

		--update	
			update DeviceConfig
			set 
			DeviceID = sourceDeviceConfig.DeviceID 
			,Notification = sourceDeviceConfig.Notification
			,MaxFeedID = sourceDeviceConfig.MaxFeedID
			,Version = sourceDeviceConfig.Version
			,CreatedDate = sourceDeviceConfig.CreatedDate
			,UpdatedDate = sourceDeviceConfig.UpdatedDate
			,DeviceManufacturer = sourceDeviceConfig.DeviceManufacturer
			,DeviceModel = sourceDeviceConfig.DeviceModel
			,OsVersion = sourceDeviceConfig.OsVersion
			,CarrierName = sourceDeviceConfig.CarrierName
			,NetworkType = sourceDeviceConfig.NetworkType
			,Active = sourceDeviceConfig.Active
			From BollywoodGossip.dbo.DeviceConfig  sourceDeviceConfig (nolock)
			where sourceDeviceConfig.CreatedDate < @LastRunDate and 
			sourceDeviceConfig.UpdatedDate >= @LastRunDate and 
			sourceDeviceConfig.UpdatedDate < @ExecutionStartDate and
			DeviceConfig.DeviceConfigID = sourceDeviceConfig.DeviceConfigID
			
			--Commit Tran SyncDeviceConfig
		End Try
		Begin Catch
			--Rollback Tran SyncDeviceConfig
			Rollback Tran SyncDB
			Insert into ErrorLog   (ErrorDescription
									,Exception
									,Stacktrace
									,ErrorMetadata
									,CreatedDate
									,UpdateDate
									,ErrorSource)
			values ('SyncDeviceConfig',ERROR_MESSAGE(),null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
			Return
		End Catch

	END

	BEGIN --FeedSource
	--Begin Tran SyncFeedSource
		Begin Try
		--Insert new one
			insert into FeedSource 
				select 
				FeedSourceID
				,SourceName
				,SourceURL
				,Active
				,StartDate
				,EndDate
				,CreatedBy
				,CreatedDate
				,UpdatedBy
				,UpdatedDate
				,SiteURL
				,LogoURL
				,Imagesource
				,ImageNameSpace
				,DescriptionNameSpace
				,Timezone
		from BollywoodGossip.dbo.FeedSource (nolock) where CreatedDate >= @LastRunDate and CreatedDate < @ExecutionStartDate

		--update	
			update FeedSource
				set 
				SourceName=sourceFeedSource.SourceName
				,SourceURL=sourceFeedSource.SourceURL
				,Active=sourceFeedSource.Active
				,StartDate=sourceFeedSource.StartDate
				,EndDate=sourceFeedSource.EndDate
				,CreatedBy=sourceFeedSource.CreatedBy
				,CreatedDate=sourceFeedSource.CreatedDate
				,UpdatedBy=sourceFeedSource.UpdatedBy
				,UpdatedDate=sourceFeedSource.UpdatedDate
				,SiteURL=sourceFeedSource.SiteURL
				,LogoURL=sourceFeedSource.LogoURL
				,Imagesource=sourceFeedSource.Imagesource
				,ImageNameSpace=sourceFeedSource.ImageNameSpace
				,DescriptionNameSpace=sourceFeedSource.DescriptionNameSpace
				,Timezone=sourceFeedSource.Timezone
				From BollywoodGossip.dbo.FeedSource  sourceFeedSource (nolock)
				where sourceFeedSource.CreatedDate < @LastRunDate and 
				sourceFeedSource.UpdatedDate >= @LastRunDate and 
				sourceFeedSource.UpdatedDate < @ExecutionStartDate and
				FeedSource.FeedSourceID=sourceFeedSource.FeedSourceID
				
			--Commit Tran SyncFeedSource
		End Try
		Begin Catch
			--Rollback Tran SyncFeedSource
			Rollback Tran SyncDB
			Insert into ErrorLog   (ErrorDescription
									,Exception
									,Stacktrace
									,ErrorMetadata
									,CreatedDate
									,UpdateDate
									,ErrorSource)
			values ('SyncFeedSource',ERROR_MESSAGE(),null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
			Return
		End Catch

	END

	BEGIN --Feeds
	--Begin Tran SyncFeeds
		Begin Try
		--Insert new one
			insert into Feeds 
				select 
				FeedID
				,Title
				,Description
				,ImageURL
				,PublishDate
				,RedirectURL
				,Source
				,CreatedBy
				,CreatedDate
				,UpdatedBy
				,UpdatedDate
				,SourceXML
				,Active
				,UTCPublishDate
		from BollywoodGossip.dbo.Feeds (nolock) where CreatedDate >= @LastRunDate and CreatedDate < @ExecutionStartDate

		--update	
			update Feeds
				set 
					Title = sourceFeedSource.Title
					,Description = sourceFeedSource.Description
					,ImageURL = sourceFeedSource.ImageURL
					,PublishDate = sourceFeedSource.PublishDate
					,RedirectURL = sourceFeedSource.RedirectURL
					,Source = sourceFeedSource.Source
					,CreatedBy = sourceFeedSource.CreatedBy
					,CreatedDate = sourceFeedSource.CreatedDate
					,UpdatedBy = sourceFeedSource.UpdatedBy
					,UpdatedDate = sourceFeedSource.UpdatedDate
					,SourceXML = sourceFeedSource.SourceXML
					,Active = sourceFeedSource.Active
					,UTCPublishDate = sourceFeedSource.UTCPublishDate
				From BollywoodGossip.dbo.Feeds  sourceFeedSource (nolock)
				where sourceFeedSource.CreatedDate < @LastRunDate and 
				sourceFeedSource.UpdatedDate >= @LastRunDate and 
				sourceFeedSource.UpdatedDate < @ExecutionStartDate and
				Feeds.FeedID = sourceFeedSource.FeedID
					
			--Commit Tran SyncFeeds
		End Try
		Begin Catch
			--Rollback Tran SyncFeeds
			Rollback Tran SyncDB
			Insert into ErrorLog   (ErrorDescription
									,Exception
									,Stacktrace
									,ErrorMetadata
									,CreatedDate
									,UpdateDate
									,ErrorSource)
			values ('SyncFeeds',ERROR_MESSAGE(),null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
			Return
		End Catch

	END

	BEGIN --FeedAuditLog
	--Begin Tran SyncFeedAuditLog
		Begin Try
		--Insert new one
			insert into FeedAuditLog 
				select 
					FeedAuditLogID
					,DeviceConfigID
					,PageNumber
					,CreatedDate
		from BollywoodGossip.dbo.FeedAuditLog (nolock) where CreatedDate >= @LastRunDate and CreatedDate < @ExecutionStartDate

		
			--Commit Tran SyncFeedAuditLog
		End Try
		Begin Catch
			--Rollback Tran SyncFeedAuditLog
			Rollback Tran SyncDB
			Insert into ErrorLog   (ErrorDescription
									,Exception
									,Stacktrace
									,ErrorMetadata
									,CreatedDate
									,UpdateDate
									,ErrorSource)
			values ('SyncFeedAuditLog',ERROR_MESSAGE(),null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
			Return
		End Catch

	END

	BEGIN --FeedDetailAuditLog
	--Begin Tran SyncFeedDetailAuditLog
		Begin Try
		--Insert new one
			insert into FeedDetailAuditLog 
				select 
					FeedDetailAuditLogID
					,DeviceConfigID
					,FeedID
					,CreatedDate
					,ViewOnWeb
		from BollywoodGossip.dbo.FeedDetailAuditLog (nolock) where CreatedDate >= @LastRunDate and CreatedDate < @ExecutionStartDate

		
			--Commit Tran SyncFeedDetailAuditLog
		End Try
		Begin Catch
			--Rollback Tran SyncFeedDetailAuditLog
			Rollback Tran SyncDB
			Insert into ErrorLog   (ErrorDescription
									,Exception
									,Stacktrace
									,ErrorMetadata
									,CreatedDate
									,UpdateDate
									,ErrorSource)
			values ('SyncFeedDetailAuditLog',ERROR_MESSAGE(),null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
			Return
		End Catch

	END

	BEGIN --Feedback
	--Begin Tran SyncFeedback
		Begin Try
		--Insert new one
			insert into Feedback 
				select 
					FeedbackID
					,Name
					,Email
					,Category
					,Feedback
					,Response
					,CreatedDate
					,DeviceConfigID
		from BollywoodGossip.dbo.Feedback (nolock) where CreatedDate >= @LastRunDate and CreatedDate < @ExecutionStartDate

		
			--Commit Tran SyncFeedback
		End Try
		Begin Catch
			--Rollback Tran SyncFeedback
			Rollback Tran SyncDB
			Insert into ErrorLog   (ErrorDescription
									,Exception
									,Stacktrace
									,ErrorMetadata
									,CreatedDate
									,UpdateDate
									,ErrorSource)
			values ('SyncFeedback',ERROR_MESSAGE(),null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
			Return
		End Catch

	END

	BEGIN --NotificationLog
	--Begin Tran SyncNotificationLog
		Begin Try
		--Insert new one
			insert into NotificationLog 
				select 
					NotificationLogID
					,DeviceConfigID
					,CreatedDate
					,NotificationLogMessage
		from BollywoodGossip.dbo.NotificationLog (nolock) where CreatedDate >= @LastRunDate and CreatedDate < @ExecutionStartDate

		
			--Commit Tran SyncNotificationLog
		End Try
		Begin Catch
			--Rollback Tran SyncNotificationLog
			Rollback Tran SyncDB
			Insert into ErrorLog   (ErrorDescription
									,Exception
									,Stacktrace
									,ErrorMetadata
									,CreatedDate
									,UpdateDate
									,ErrorSource)
			values ('SyncNotificationLog',ERROR_MESSAGE(),null,null,GETUTCDATE(),GETUTCDATE(),'[Sp_SyncDB]')
			Return
		End Catch

	END
	
	update Configuration	 set ConfigurationValue = @ExecutionStartDate, UpdatedDate = GETUTCDATE() where ConfigurationName = 'LastBackupDate'
	Commit Tran SyncDB
END

	BEGIN
		Declare @tmpTable Table (FeedID BIGINT)
		insert into @tmpTable 
			Select auditFeeds.FeedID from Feeds auditFeeds (nolock)
			Inner Join BollywoodGossip.dbo.Feeds sourcefeed (nolock) on sourcefeed.FeedID = auditFeeds.FeedID
			where auditFeeds.CreatedDate < GETUTCDATE() - 15 --and UpdatedDate < GETUTCDATE() - 15
		WHILE (Select COUNT(*) from @tmpTable) > 0
		BEGIN
			Declare @SingleFeedID BIGINT
			Declare @OutPut INT
			SELECT top 1 @SingleFeedID = FeedID from @tmpTable
			EXEC Sp_DeleteFeed @SingleFeedID ,@OutPut out
			if @OutPut = 0
			BEGIN
				Return -- Exit deleting source
			END
			Delete from @tmpTable where FeedID = @SingleFeedID
		END
		
		--Delete from BollywoodGossip.dbo.FeedAuditLog where FeedAuditLogID in 
		--	(Select FeedAuditLogID from FeedAuditLog where CreatedDate < GETUTCDATE() - 15)
		--Delete from BollywoodGossip.dbo.NotificationLog where NotificationLogID in 
		--	(Select NotificationLogID from NotificationLog where CreatedDate < GETUTCDATE() - 15)
			
		delete SourceFAL from BollywoodGossip.dbo.FeedAuditLog SourceFAL 
			Inner Join FeedAuditLog DestFAL on DestFAL.FeedAuditLogID = SourceFAL.FeedAuditLogID
			where SourceFAL.CreatedDate < GETUTCDATE() - 15
			
		delete SourceNL from BollywoodGossip.dbo.NotificationLog SourceNL 
			Inner Join NotificationLog DestNL on DestNL.NotificationLogID = SourceNL.NotificationLogID
			where SourceNL.CreatedDate < GETUTCDATE() - 15
			
	END
	

End


