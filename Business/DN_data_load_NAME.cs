using Xunit;

namespace UnitTestProject
{
	public class TEST_data_load_Test_LEVEL_NAME
	{
		static string path = @"C:\DATA-SCRIPTS\TEST_data_load\";
		string fileToSearch = path + "TEST_Japan_26_Sep_Test_LEVEL_NAME.csv";
		string[] list = { "#SALES_CHANNEL#", "#CAM_ID#" };	

		[Fact]
		public void Script_DEPLOY_Table_data_loadTest_LEVEL_NAME()
		{
			string filename = "DEPLOY_Table_data_load_Test_LEVEL_NAME_03-OCT-2022";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "", list,
				Get_Table_data_load_INSERT(), ";", "", "", "UNION", GetFinalScript());
		}

		public string Get_Table_data_load_INSERT()
		{
			//from: "TEST_Enabled",
			//to: $"{row.XYZ}:{row.CompanyNumber}:{row.Test}",
			//value: row.InvoiceValue.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ? "TRUE" : "FALSE",
			//type: "assigned_value"

			return @"-- #number#.  #SALES_CHANNEL#, #CAM_ID#
SELECT 	'TEST_Enabled' as 'from', '3535:#SALES_CHANNEL#:#CAM_ID#' as 'to', 'TRUE' as 'value', 'assigned_value' as 'type' ";
		}

		public string GetFinalScript()
		{
			return @"FOR JSON PATH";
		}

	}
}


