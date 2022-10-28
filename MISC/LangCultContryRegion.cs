using Xunit;

namespace UnitTestProject
{
    public class LangCultContryRegion
    {
        [Fact]
        public void DbScriptByTemplate_LangCultContryRegion()
        {
            string path = @"C:\LangCultContryRegion\";
            string filename = "DEPLOY_LangCultContryRegion_17-NOV-2021";
            string fileToSearch = @"C:\LangCultContryRegion\LangCultContryRegion.csv";			

			string[] list = { "#BU#", "#CC#", "#LangValue#", "#CultValue#", "#LangDesc#" };

            new ScriptHelper().GenerateFile(path, filename,
                fileToSearch, "USE [DcDb]", list,
                GetLangCultContryRegion_INSERT(), ";", "DEPLOY");
        }

        public string GetLangCultContryRegion_INSERT()
        {
            return @"DECLARE @bucode#BU##LangValue# as UNIQUEIDENTIFIER 
DECLARE @TableCode#CC#_#LangValue# as INT
SELECT @bucode#BU##LangValue#=id from table where RTRIM(LTRIM(code)) = '#BU#'
SELECT @TableCode#CC#_#LangValue#=id from TableRegion where RTRIM(LTRIM(TableCode)) = '#CC#'

IF ( NOT EXISTS ( SELECT TOP 1 1 from [dbo].[Language] WHERE [Code] = '#LangValue#'))
	BEGIN
		BEGIN TRY
			INSERT INTO [dbo].[Language] ([Code],[Description]) VALUES('#LangValue#', '#LangValue#')
			PRINT 'SUCCESS: #number#. missing Language: #LangValue# was successfully INSERTED into [Language] table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: #number#. missing Language: #LangValue# was was NOT INSERTED into [Language] table due to: ' + ERROR_MESSAGE()
		END CATCH
	END


-- #number#. 
-- LanguageTableRegion: #CC# '#BU#' #LangValue#  #LangDesc#
IF ( NOT EXISTS ( 
	SELECT TOP 1 1 from LanguageTableRegion WHERE TableRegionId = @TableCode#CC#_#LangValue# 	
	AND BusinessUnitId = @bucode#BU##LangValue#  AND [Value] = '#LangValue#' ))

	BEGIN
		BEGIN TRY
			INSERT INTO [dbo].[LanguageTableRegion] ([Value] ,[IsDefault] ,[TableRegionId] ,[BusinessUnitId])
			VALUES ('#LangValue#', 0, @TableCode#CC#_#LangValue#, @bucode#BU##LangValue# )

			PRINT 'SUCCESS: #number#. Language: #LangValue# with Table code: #CC# and BuCode: #BU# was successfully INSERTED into LanguageTableRegion table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: #number#. Language: #LangValue# with Table code: #CC# and BuCode: #BU# was NOT INSERTED into the LanguageTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN	
		IF ( EXISTS ( 
			SELECT TOP 1 1 from LanguageTableRegion WHERE TableRegionId = @TableCode#CC#_#LangValue# 	
			AND BusinessUnitId = @bucode#BU##LangValue#  AND [Value] = '#LangValue#' ))
			
			PRINT 'SUCCESS: #number#. Language: #LangValue# with Table code: #CC# and BuCode: #BU# was ALREADY INSERTED into LanguageTableRegion table'
		ELSE
			PRINT 'FAILED: #number#. Language: #LangValue# with Table code: #CC# and BuCode: #BU# was NOT INSERTED into LanguageTableRegion table'
	END

-- CultureTableRegion: #CC# '#BU#' #LangValue#  #LangDesc#  #CultValue#
IF ( NOT EXISTS ( 
	SELECT TOP 1 1 from CultureTableRegion 
	WHERE 
		TableRegionId = @TableCode#CC#_#LangValue# 	
		AND BusinessUnitId = @bucode#BU##LangValue#  
		AND [TableCode] = '#CC#' 
		AND [LanguageCode] = '#LangValue#' 
		AND [Value] = '#CultValue#' ))
	BEGIN
		BEGIN TRY
			INSERT INTO [dbo].[CultureTableRegion] ([TableCode],[Value],[IsDefault],[Description],[LocalDescription],[TableRegionId],[LanguageCode],[BusinessUnitId])
			 VALUES ('#CC#', '#CultValue#', 0, '#LangDesc#', '', @TableCode#CC#_#LangValue#, '#LangValue#', @bucode#BU##LangValue#)

			PRINT 'SUCCESS: #number#. CultureTable Value: #CultValue# with Table code: #CC# and BuCode: #BU# was successfully INSERTED into CultureTableRegion table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: #number#. CultureTable Value: #CultValue# with Table code: #CC# and BuCode: #BU# was NOT INSERTED into the CultureTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN
		IF ( EXISTS ( 
		SELECT TOP 1 1 from CultureTableRegion 
		WHERE 
			TableRegionId = @TableCode#CC#_#LangValue# 	
			AND BusinessUnitId = @bucode#BU##LangValue#  
			AND [TableCode] = '#CC#' 
			AND [LanguageCode] = '#LangValue#' 
			AND [Value] = '#CultValue#' ))
			
			PRINT 'SUCCESS: #number#. CultureTable Value: #CultValue# with Table code: #CC# and BuCode: #BU# was ALREADY INSERTED into CultureTableRegion table'
		ELSE
			PRINT 'FAILED: #number#. CultureTable Value: #CultValue# with Table code: #CC# and BuCode: #BU# was NOT INSERTED into the CultureTableRegion table'
	END

";
        }

        [Fact]
        public void ScriptValidation_LangCultContryRegion()
        {
			string path = @"C:\LangCultContryRegion\";
			string filename = "VALIDATION_LangCultContryRegion_17-NOV-2021";
			string fileToSearch = @"C:\LangCultContryRegion\LangCultContryRegion.csv";

			//				3838;		AT;		EN;			 en - AT;		English
			string[] list = { "#BU#", "#CC#", "#LangValue#", "#CultValue#", "#LangDesc#" };

            new ScriptHelper().GenerateFile(path, filename,
                fileToSearch, "USE [DcDb]", list,
                LangCultContryRegion_VALIDATION(), ";", "VALIDATION");
        }

        public string LangCultContryRegion_VALIDATION()
        {
            return @"DECLARE @bucode#BU##LangValue# as UNIQUEIDENTIFIER 
DECLARE @TableCode#CC#_#LangValue# as INT
SELECT @bucode#BU##LangValue#=id from table where RTRIM(LTRIM(code)) = '#BU#'
SELECT @TableCode#CC#_#LangValue#=id from TableRegion where RTRIM(LTRIM(TableCode)) = '#CC#'

-- #number#. 
-- LanguageTableRegion: #CC# '#BU#' #LangValue#  #LangDesc#
BEGIN
	IF ( EXISTS ( SELECT TOP 1 1 from LanguageTableRegion WHERE TableRegionId = @TableCode#CC#_#LangValue# 	
			AND BusinessUnitId = @bucode#BU##LangValue#  AND [Value] = '#LangValue#' ))			
		PRINT 'SUCCESS: #number#. Language: #LangValue# with Table code: #CC# and BuCode: #BU# was INSERTED into LanguageTableRegion table'
	ELSE
		PRINT 'FAILED: #number#. Language: #LangValue# with Table code: #CC# and BuCode: #BU# was NOT INSERTED into LanguageTableRegion table'
END

-- CultureTableRegion: #CC# '#BU#' #LangValue#  #LangDesc#  #CultValue#
BEGIN
	IF ( EXISTS ( 
	SELECT TOP 1 1 from CultureTableRegion 
	WHERE 
		TableRegionId = @TableCode#CC#_#LangValue# 	
		AND BusinessUnitId = @bucode#BU##LangValue#  
		AND [TableCode] = '#CC#' 
		AND [LanguageCode] = '#LangValue#' 
		AND [Value] = '#CultValue#' ))
			
		PRINT 'SUCCESS: #number#. CultureTable Value: #CultValue# with Table code: #CC# and BuCode: #BU# was INSERTED into CultureTableRegion table'
	ELSE
		PRINT 'FAILED: #number#. CultureTable Value: #CultValue# with Table code: #CC# and BuCode: #BU# was NOT INSERTED into the CultureTableRegion table'
END

";
        }


		public string GetLangCultContryRegion_Rollback()
		{
			return @"DECLARE @bucode#BU##LangValue# as UNIQUEIDENTIFIER 
DECLARE @TableCode#CC#_#LangValue# as INT
SELECT @bucode#BU##LangValue#=id from table where RTRIM(LTRIM(code)) = '#BU#'
SELECT @TableCode#CC#_#LangValue#=id from TableRegion where RTRIM(LTRIM(TableCode)) = '#CC#'

-- #number#. 
-- LanguageTableRegion: #CC# '#BU#' #LangValue#  #LangDesc#
IF ( EXISTS ( 
	SELECT TOP 1 1 from LanguageTableRegion WHERE TableRegionId = @TableCode#CC#_#LangValue# 	
	AND BusinessUnitId = @bucode#BU##LangValue#  AND [Value] = '#LangValue#' ))

	BEGIN
		BEGIN TRY
			DELETE from LanguageTableRegion 
			WHERE TableRegionId = @TableCode#CC#_#LangValue# 	
			AND BusinessUnitId = @bucode#BU##LangValue#  
			AND [Value] = '#LangValue#'

			PRINT 'SUCCESS: #number#. Language: #LangValue# with Table code: #CC# and BuCode: #BU# was successfully rolled back from LanguageTableRegion table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: #number#. Language: #LangValue# with Table code: #CC# and BuCode: #BU# was NOT rolled back from LanguageTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN	
		IF ( NOT EXISTS ( 
			SELECT TOP 1 1 from LanguageTableRegion WHERE TableRegionId = @TableCode#CC#_#LangValue# 	
			AND BusinessUnitId = @bucode#BU##LangValue#  AND [Value] = '#LangValue#' ))
			
			PRINT 'SUCCESS: #number#. Language: #LangValue# with Table code: #CC# and BuCode: #BU# was ALREADY rolled back from LanguageTableRegion table'
		ELSE
			PRINT 'FAILED: #number#. Language: #LangValue# with Table code: #CC# and BuCode: #BU# was NOT rolled back from LanguageTableRegion table'
	END

-- CultureTableRegion: #CC# '#BU#' #LangValue#  #LangDesc#  #CultValue#
IF ( EXISTS ( 
	SELECT TOP 1 1 from CultureTableRegion 
	WHERE 
		TableRegionId = @TableCode#CC#_#LangValue# 	
		AND BusinessUnitId = @bucode#BU##LangValue#  
		AND [TableCode] = '#CC#' 
		AND [LanguageCode] = '#LangValue#' 
		AND [Value] = '#CultValue#' ))
	BEGIN
		BEGIN TRY
			DELETE from CultureTableRegion 
			WHERE TableRegionId = @TableCode#CC#_#LangValue# 	
			AND BusinessUnitId = @bucode#BU##LangValue#  
			AND [TableCode] = '#CC#' 
			AND [LanguageCode] = '#LangValue#' 
			AND [Value] = '#CultValue#'

			PRINT 'SUCCESS: #number#. CultureTable Value: #CultValue# with Table code: #CC# and BuCode: #BU# was rolled back from CultureTableRegion table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: #number#. CultureTable Value: #CultValue# with Table code: #CC# and BuCode: #BU# was NOT rolled back from CultureTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	BEGIN
		IF ( NOT EXISTS ( 
		SELECT TOP 1 1 from CultureTableRegion 
		WHERE 
			TableRegionId = @TableCode#CC#_#LangValue# 	
			AND BusinessUnitId = @bucode#BU##LangValue#  
			AND [TableCode] = '#CC#' 
			AND [LanguageCode] = '#LangValue#' 
			AND [Value] = '#CultValue#' ))
			
			PRINT 'SUCCESS: #number#. CultureTable Value: #CultValue# with Table code: #CC# and BuCode: #BU# was ALREADY rolled back from CultureTableRegion table'
		ELSE
			PRINT 'FAILED: #number#. CultureTable Value: #CultValue# with Table code: #CC# and BuCode: #BU# was NOT rolled back from CultureTableRegion table'
	END

";
		}

		[Fact]
		public void ScriptRollback_LangCultContryRegion()
		{
			string path = @"C:\LangCultContryRegion\";
			string filename = "ROLLBACK_LangCultContryRegion_17-NOV-2021";
			string fileToSearch = @"C:\LangCultContryRegion\LangCultContryRegion.csv";

			//				3838;		AT;		EN;			 en - AT;		English
			string[] list = { "#BU#", "#CC#", "#LangValue#", "#CultValue#", "#LangDesc#" };

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "USE [DcDb]", list,
				GetLangCultContryRegion_Rollback(), ";", "ROLLBACK");
		}
	}
}


