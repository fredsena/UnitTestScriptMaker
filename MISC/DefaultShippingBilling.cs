
using Xunit;

namespace UnitTestProject
{
    public class DefaultTESTBilling
    {
        [Fact]
        public void DbScriptByTemplate_DefaultTESTBilling()
        {
            string path = @"C:\BillingTESTMappings\";
            string filename = "DEPLOY_DefaultTESTBilling_26-10-2021";
            string fileToSearch = @"C:\BillingTESTMappings\Script_Map-Default_TESTBilling_Countries.txt";

            string[] list = { "#TableCode#" };

            new ScriptHelper().GenerateFile(path, filename,
                fileToSearch, "USE [DcDb]", list,
                DefaultTESTBilling_UPDATE(), ";", "DEPLOY");
        }

        public string DefaultTESTBilling_UPDATE()
        {
            return @"-- #number#. TableCode: #TableCode#
BEGIN
	BEGIN TRY
		UPDATE BSCR SET BSCR.[Default] = 1
		FROM BusinessTableTableRegion BSCR
			INNER JOIN Table BSG on BSCR.BusinessTableId = BSG.Id
			INNER JOIN table BU on BU.id = BSG.BusinessUnitId
			INNER JOIN TableRegion CR on CR.Id = BSCR.TableRegionId
		WHERE bscr.AddressPurposeType IN ('Billing', 'TEST') AND CR.TableCode = '#TableCode#' AND RTRIM(LTRIM(BU.Name)) IN ('#TableCode#') AND BSCR.[Default] = 0

		PRINT 'SUCCESS: ##number#: Company numbers for TableCode: #TableCode# were successfully mapped with [Default] = 1 on BusinessTableTableRegion table'
	END TRY
	BEGIN CATCH
		PRINT 'FAILED: ##number#: Company numbers for TableCode: #TableCode# were NOT mapped with [Default] = 1 on BusinessTableTableRegion table due to: ' + ERROR_MESSAGE()
	END CATCH
END
GO";
        }

        [Fact]
        public void ScriptValidation_DefaultTESTBilling()
        {
            string path = @"C:\BillingTESTMappings\";
            string filename = "VALIDATE_DefaultTESTBilling_26-10-2021";
            string fileToSearch = @"C:\BillingTESTMappings\Script_Map-Default_TESTBilling_Countries.txt";

            string[] list = { "#TableCode#" };

            new ScriptHelper().GenerateFile(path, filename,
                fileToSearch, "USE [DcDb]", list,
                DefaultTESTBilling_VALIDATION(), ";", "VALIDATION");
        }

        public string DefaultTESTBilling_VALIDATION()
        {
            return @"-- #number#. TableCode: #TableCode#
IF (NOT EXISTS ( SELECT DISTINCT BSCR.[Default] FROM BusinessTableTableRegion BSCR
	INNER JOIN Table BSG on BSCR.BusinessTableId = BSG.Id
	INNER JOIN table BU on BU.id = BSG.BusinessUnitId
	INNER JOIN TableRegion CR on CR.Id = BSCR.TableRegionId
WHERE BSCR.AddressPurposeType IN ('Billing', 'TEST') AND CR.TableCode = '#TableCode#' AND RTRIM(LTRIM(BU.Name)) IN ('#TableCode#') AND BSCR.[Default] = 0 ))
	PRINT 'SUCCESS: ##number#: Company numbers for TableCode: #TableCode# were successfully mapped with [Default] = 1 on BusinessTableTableRegion table'
ELSE
	PRINT 'FAILED: ##number#: Company numbers for TableCode: #TableCode# were NOT mapped with [Default] = 1 on BusinessTableTableRegion table'
GO";
        }
    }
}


