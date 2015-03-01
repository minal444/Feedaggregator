CREATE Function [dbo].[fn_GetTimeDiffference] (@mintime DATETIME,@maxtime DATETIME )
Returns varchar(20)
Begin

declare @duration int
set @duration = datediff([second], @minTime, @maxtime);

DECLARE @ret varchar(20)
   
   /*
Select @ret = H + ' ' +  M from(
SELECT CASE WHEN LEN(H) = 1
		THEN '0' + h
		ELSE h END
		as H ,
		CASE WHEN LEN(m) = 1
		THEN '0' + m
		ELSE m END
		as M/*,  
		CASE WHEN LEN(s) = 1
		THEN '0' + s
		ELSE s END 
		as S*/
		FROM
		(
select 
 
  Case When CAST(@duration / 3600 AS VARCHAR(15)) = 0 THEN '' ELSE CAST(@duration / 3600 AS VARCHAR(15))+ 'h' END as [h],
  Case when CAST(@duration % 3600 / 60 AS VARCHAR(15)) = 0 Then '' ELSE CAST(@duration % 3600 / 60 AS VARCHAR(15)) + 'm' END as [m]) AS X
  --Case When CAST(@duration % 3600 % 60 AS VARCHAR(3)) =0 Then '' Else CAST(@duration % 3600 % 60 AS VARCHAR(3)) + 's' end as [s]) AS X
  ) as Diff
  return @ret 
  */
  
  
  
Select @ret = Case when  LEN(D) > 1 Then D Else Ltrim(D + ' ' +  H + ' ' +  M) End from(
SELECT 
		CASE WHEN LEN(d) = 1
		THEN '0' + d
		ELSE d END
		as D ,
		CASE WHEN LEN(H) = 1
		THEN '0' + h
		ELSE h END
		as H ,
		CASE WHEN LEN(m) = 1
		THEN '0' + m
		ELSE m END
		as M/*,  
		CASE WHEN LEN(s) = 1
		THEN '0' + s
		ELSE s END 
		as S*/
		FROM
		(
select 
  Case When CAST(@duration / 86400 AS VARCHAR(15)) = 0 THEN '' ELSE CAST(@duration / 86400 AS VARCHAR(15))+ 'd' END as [d],
  Case When CAST((@duration%86400) / 3600 AS VARCHAR(15)) = 0 THEN '' ELSE CAST((@duration%86400) / 3600 AS VARCHAR(15))+ 'h' END as [h],
  Case when CAST(((@duration%86400) % 3600) / 60 AS VARCHAR(15)) = 0 Then '' ELSE CAST(((@duration%86400) % 3600 )/ 60 AS VARCHAR(15)) + 'm' END as [m]) AS X
  --Case When CAST(@duration % 3600 % 60 AS VARCHAR(3)) =0 Then '' Else CAST(@duration % 3600 % 60 AS VARCHAR(3)) + 's' end as [s]) AS X
  ) as Diff
  return @ret 
  End
  
  



