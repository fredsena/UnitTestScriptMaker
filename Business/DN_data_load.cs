using Xunit;

namespace UnitTestProject
{
	public class DN_data_load
	{
		static string path = @"C:\DATA-SCRIPTS\TEST_data_load_13197765\";		
		string fileToSearch = path + "TEST_Japan_26_Sep.csv";
		string[] list = { "#BU_ID#", "#SALES_CHANNEL#", "#CAM_ID#", "#ADDRESS_ID#", "#LEVEL_NAME#", "#GII_ID#", "#VALUE#", "#SRC_TRANS_TIMESTAMP#", "#IS_PROCESSED#", "#SETTINGNAME#" };

		//"#BU_ID#", "#SALES_CHANNEL#", "#CAM_ID", "#ADDRESS_ID#", "#LEVEL_NAME#", "#GII_ID#", "#VALUE#", "#SRC_TRANS_TIMESTAMP#", "#IS_PROCESSED#", "#SETTINGNAME#"
		//3535;SMB;748969147;22207662;Address;22207662;TRUE;26-SEP-22 11.17.36.388680000 AM;FALSE;TEST

		[Fact]
		public void Script_DEPLOY_Table_data_load_13197765()
		{
			string filename = "DEPLOY_Table_data_load_13197765_30-SEP-2022";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "", list,
				Get_Table_data_load_INSERT(), ";", "", "", "UNION", GetFinalScript());
		}

		public string Get_Table_data_load_INSERT()
		{
			//from: "TEST_Enabled",
			//to: $"{CAM_ID}:{ADDRESS_ID}",
			//value: row.InvoiceValue.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ? "TRUE" : "FALSE",
			//type: "assigned_value"

			return @"-- #number#.  #CAM_ID#, #ADDRESS_ID#, #VALUE# 
SELECT 
	'TEST_Enabled' as 'from',
	'#CAM_ID#:#ADDRESS_ID#' as 'to',
	'#VALUE#' as 'value',
	'assigned_value' as 'type'
";
		}

		public string GetFinalScript()
		{
			return @"FOR JSON PATH";
		}

	}
}

