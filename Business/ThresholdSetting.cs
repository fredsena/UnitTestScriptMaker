
using Xunit;

namespace UnitTestProject
{
	public class ThresholdSetting
	{
		static string path = @"C:\DATA-SCRIPTS\Threshold_Setting\";
		string fileToSearch = path + "Threshold_Setting.csv";
		string[] list = { "#BUCode#", "#Name#", "#IsDefault#", "#RegionCode#", "#Desc#", "#SFDCThreshold#" };

		[Fact]
		public void Script_DEPLOY_SFDCThresholdSetting()
		{
			string filename = "DEPLOY_ThresholdSetting_5-APR-2022";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "", list,
				Get_SFDCThresholdSetting_INSERT(), ";", "", "", "UNION", GetFinalScript());
		}

		public string Get_SFDCThresholdSetting_INSERT()
		{
			return @"-- #number#.  #BUCode#  #Name# #IsDefault# #RegionCode# #Desc# #SFDCThreshold#
SELECT 
	'SFDCThreshold' as 'from',
	'#BUCode#' as 'to',
	'#SFDCThreshold#' as 'value',
	'assigned_value' as 'type' 
";
		}

		public string GetFinalScript()
		{
			return @"FOR JSON PATH";
		}

	}
}


