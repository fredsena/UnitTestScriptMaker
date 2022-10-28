using Xunit;

namespace UnitTestProject
{
    public class NewSTATETableRegions
    {
        [Fact]
        public void DbScriptByTemplate_NewSTATETableRegions()
        {
            string path = @"C:\TableStateMappings\";
            string filename = "GenerateScript_NewSTATETableRegions_19-10-2021_TEST";
            string fileToSearch = @"C:\TableStateMappings\StateTableRegion_New States2.csv";

            string[] list = { "#TableCode#", "#TableDescription#", "#StateName#", "#StateCode#" };

            new ScriptHelper().GenerateFile(path, filename,
                fileToSearch, "USE [DcDb]", list,
                GetNewSTATETableRegions_INSERT(), ";", "DEPLOY");
        }

        public string GetNewSTATETableRegions_INSERT()
        {
            return @"-- #number#. StateCode = #StateCode# | StateName = #StateName# | TableCode = #TableCode# | TableDescription = #TableDescription# |
IF (NOT EXISTS (
		SELECT TOP 1 1 from StateTableRegion 
		WHERE LTRIM(RTRIM(StateCode)) = '#StateCode#'
		AND LTRIM(RTRIM(StateName)) = N'#StateName#'		
		AND TableRegionId = (select top 1 id from TableRegion where TableCode = '#TableCode#') ) )
	BEGIN
		BEGIN TRY			
			INSERT INTO StateTableRegion (StateCode, StateName, TableRegionId) 
			SELECT '#StateCode#' as StateCode, N'#StateName#' as StateName, (select top 1 id from TableRegion where TableCode = '#TableCode#' ) as TableRegionId
			PRINT 'SUCCESS: # #number# : StateCode = #StateCode# | StateName = ' + N'#StateName#' + ' | TableCode = #TableCode# | TableDescription = #TableDescription# | were successfully inserted into StateTableRegion table'			
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: # #number# : StateCode = #StateCode# | StateName = ' + N'#StateName#' + ' | TableCode = #TableCode# | TableDescription = #TableDescription# | were NOT inserted into StateTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	PRINT 'SUCCESS: # #number# : StateCode = #StateCode# | StateName = ' + N'#StateName#' + ' | TableCode = #TableCode# | TableDescription = #TableDescription# | were ALREADY inserted into StateTableRegion table'
GO";
        }


        [Fact]
        public void ScriptValidation_NewSTATETableRegions()
        {
            string path = @"C:\TableStateMappings\";
            string filename = "Validation_NewSTATETableRegions_19-10-2021_TEST";
            string fileToSearch = @"C:\TableStateMappings\StateTableRegion_New States2.csv";

            string[] list = { "#TableCode#", "#TableDescription#", "#StateName#", "#StateCode#" };

            new ScriptHelper().GenerateFile(path, filename,
                fileToSearch, "USE [DcDb]", list,
                NewSTATETableRegions_VALIDATION(), ";", "VALIDATION");
        }

        public string NewSTATETableRegions_VALIDATION()
        {
            return @"-- #number#. StateCode = #StateCode# | StateName = #StateName# | TableCode = #TableCode# | TableDescription = #TableDescription# |
IF ( EXISTS (
		SELECT TOP 1 1 from StateTableRegion 
		WHERE LTRIM(RTRIM(StateCode)) = '#StateCode#'
		AND LTRIM(RTRIM(StateName)) = N'#StateName#'		
		AND TableRegionId = (select top 1 id from TableRegion where TableCode = '#TableCode#' ) ) )
	PRINT 'SUCCESS: # #number# : StateCode = #StateCode# | StateName = ' + N'#StateName#' + ' | TableCode = #TableCode# | TableDescription = #TableDescription# | were successfully inserted into StateTableRegion table'			
ELSE
	PRINT 'FAILED: # #number# : StateCode = #StateCode# | StateName = ' + N'#StateName#' + ' | TableCode = #TableCode# | TableDescription = #TableDescription# | were NOT inserted into StateTableRegion table '
GO";
        }
    }
}

