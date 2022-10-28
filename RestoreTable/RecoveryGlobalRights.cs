
using Xunit;

namespace UnitTestProject
{
	public class RecoveryGlobalRights
	{
		static string path = @"C:\DATA-SCRIPTS\RecoveryGlobalRights\";
		string fileToSearch = path + "recoveryGlobalRights.csv";
		string[] list = { "#id#", "#Name#", "#Id#", "#Value#", "#DisplayName#", "#Display#", "#GlobalTestTypeId#" };
		

		[Fact]
		public void Script_DEPLOY_RecoveryGlobalRights()
		{
			string filename = "DEPLOY_RecoveryGlobalRights";

			new ScriptHelper().GenerateFile(path, filename,
				fileToSearch, "", list,
				Get_RecoveryGlobalRights_INSERT(), ";", "", "INSERT into RightGlobalTestHierarchy (RightId,GlobalTestHierarchyId)", "UNION", "");
		}

		public string Get_RecoveryGlobalRights_INSERT()
		{
			return @"select (select id from Rights where name = '#Name#') as RightId , (select id from GlobalTestHierarchy where [Value] = '#Value#' and GlobalTestTypeId = 3) as GlobalTestHierarchyId
";
		}
	}
}


