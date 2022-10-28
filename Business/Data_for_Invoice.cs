using Xunit;

namespace UnitTestProject
{
	public class Data_for_Invoice
	{
		static string path = @"C:\DATA-SCRIPTS\LOV-Data-for-CFOInvoiceType\";
		string fileToSearch = path + "LOV-Data-for-CFOInvoiceType.csv";
		string[] list = { "#Region#", "#BU_ID#", "#BU_NAME#", "#LOOKUP_TYPE#", "#Lookup_Code#", "#MEANING#" };

		[Fact]
		public void Script_DEPLOY_DateTimeFormatAdd()
		{
			string filename = "DEPLOY_LOV-Data-for-CFOInvoiceType_15-MAR-2022";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "", list,
				Get_LOV_Data_for_CFOInvoiceType_INSERT(), ";", "", "","UNION", GetFinalScript());
		}

		public string Get_LOV_Data_for_CFOInvoiceType_INSERT()
		{
			return @"-- #number#.  #Region#, #BU_ID#, #BU_NAME#, #LOOKUP_TYPE#, #Lookup_Code#, #MEANING# 
SELECT 
	'#Region#' as 'region',
	'#BU_ID#' as 'bu_id',
	'#BU_NAME#' as 'bu_name',
	'#LOOKUP_TYPE#' as 'lookup_type', 
	'#Lookup_Code#' as 'lookup_code',
	 N'#MEANING#' as 'meaning' 
";
		}

		public string GetFinalScript()
		{
			return @"FOR JSON PATH";
		}

	}
}

