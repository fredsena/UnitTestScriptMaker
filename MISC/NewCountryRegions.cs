using Xunit;

namespace UnitTestProject
{
    public class NewTableRegions
    {
        public string NewTableRegions_ROLLBACK()
        {
            return @"DECLARE @bucode#BU# as UNIQUEIDENTIFIER 
DECLARE @TableCode#CC# as INT
SELECT @bucode#BU#=id from table where RTRIM(LTRIM(code)) = '#BU#'
SELECT @TableCode#CC#=id from TableRegion where RTRIM(LTRIM(TableCode)) = '#CC#'

-- #number#.  #CC# #BU# #Iana#  #VV# #CN#
IF ( EXISTS ( 
	SELECT TOP 1 1 from TimezoneTableRegion 
	WHERE TableRegionId = @TableCode#CC#
	AND BusinessUnitId = @bucode#BU# ))

	BEGIN
		BEGIN TRY	
		UPDATE [dbo].[TimeZoneTableRegion]
			SET [Value] = '#VV#' ,[IanaTimeZone] = '#Iana#' WHERE TableRegionId = @TableCode#CC#  AND BusinessUnitId = @bucode#BU#	
			PRINT 'SUCCESS: Table code: #CC# with IanaTimeZone: #Iana# was successfully Rolled back on TimeZoneTableRegion table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED: Table code: #CC# with IanaTimeZone: #Iana# was NOT Rolled back on TimeZoneTableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
";
        }

        [Fact]
        public void GenerateDbScript_Rollback_NewTableRegions()
        {
            string path = @"C:\NewTableRegions\";
            string filename = "GenerateScript_NewTableRegions_19-10-2021_TEST";
            string fileToSearch = @"C:\NewTableRegions\TableRegion_New Countries.csv";

            string[] list = { "#TableCode#", "#TableDescription#", "#RegionCode#", "#RegionDescription#", "#IsDistributor#", "#OSC_RegionCode#", "#BIL_SDR_Region#", "#AddressValidationRequired#" };

            //new ScriptHelper().GenerateFile(path, filename,
            //    fileToSearch, "USE [DcDb]", list,
            //    NewTableRegions_ROLLBACK(), ",");
        }

        public string NewTableRegions_VALIDATION()
        {
            return @"--#number#
IF ( EXISTS (SELECT TOP 1 1 from TableRegion where TableCode = '#TableCode#'))	
	PRINT 'SUCCESS: New Table Region #TableCode# was successfully inserted into TableRegion table'
ELSE
	PRINT 'FAILED:  New Table Region #TableCode# was NOT inserted into TableRegion table'
";
        }

        [Fact]
        public void GenerateDbScript_Validation_NewTableRegions()
        {
            string path = @"C:\NewTableRegions\";
            string filename = "Validation_NewTableRegions_19-10-2021_TEST";
            string fileToSearch = @"C:\NewTableRegions\TableRegion_New Countries.csv";

            string[] list = { "#TableCode#", "#TableDescription#", "#RegionCode#", "#RegionDescription#", "#IsDistributor#", "#OSC_RegionCode#", "#BIL_SDR_Region#", "#AddressValidationRequired#" };

            new ScriptHelper().GenerateFile(path, filename,
                fileToSearch, "USE [DcDb]", list,
                NewTableRegions_VALIDATION(), ";");
        }

        [Fact]
        public void GenerateDbScript_NewTableRegions()
        {
            string path = @"C:\NewTableRegions\";
            string filename = "GenerateScript_NewTableRegions_19-10-2021_TEST";
            string fileToSearch = @"C:\NewTableRegions\TableRegion_New Countries.csv";

            string[] list = { "#TableCode#", "#TableDescription#", "#RegionCode#", "#RegionDescription#", "#IsDistributor#", "#OSC_RegionCode#", "#BIL_SDR_Region#", "#AddressValidationRequired#" };

            new ScriptHelper().GenerateFile(path, filename,
                fileToSearch, "USE [DcDb]", list,
                GetTemplate(), ";");
        }

        public string GetTemplate()
        {
            return @"--#number#
IF ( NOT EXISTS (SELECT TOP 1 1 from TableRegion where TableCode = '#TableCode#'))	
	BEGIN
		BEGIN TRY
			INSERT INTO [dbo].[TableRegion]
				([TableCode],[TableDescription] ,[RegionCode],[RegionDescription],[IsDistributor],[OSC_RegionCode],[BIL_SDR_Region],[AddressValidationRequired])
			VALUES ('#TableCode#' ,'#TableDescription#','#RegionCode#','#RegionDescription#',#IsDistributor#,'#OSC_RegionCode#','#BIL_SDR_Region#',#AddressValidationRequired#)						
			PRINT 'SUCCESS: New Table Region #TableCode# was successfully inserted into TableRegion table'
		END TRY
		BEGIN CATCH
			PRINT 'FAILED:  New Table Region #TableCode# was NOT inserted into TableRegion table due to: ' + ERROR_MESSAGE()
		END CATCH
	END
ELSE
	PRINT 'SUCCESS: New Table Region #TableCode# was ALREADY inserted into TableRegion table'
GO
";
        }

    }
}

