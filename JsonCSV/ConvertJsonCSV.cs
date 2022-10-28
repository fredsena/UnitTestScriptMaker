using ChoETL;
using System.Linq;
using Xunit;

namespace UnitTestProject.JsonCSV
{
	public class ConvertJsonCSV
	{
        // https://www.codeproject.com/Tips/1193650/Cinchoo-ETL-Quick-Start-Converting-JSON-to-CSV-Fil
        // Install-Package ChoETL
        // Install-Package ChoETL.JSON

        static string path = @"C:\DATA-SCRIPTS\";
        string fileToWrite = path + "LOCAL_PROD_db_export_profiles.csv";
        string fileToSearch = path + "LOCAL_PROD_db_export_profiles.json";	

		[Fact]
		public void Script_DEPLOY_RecoveryGlobalRights()
		{
            using (var csv = new ChoCSVWriter(fileToWrite).WithFirstLineHeader())
            {
                using (var json = new ChoJSONReader(fileToSearch))
                {
                    csv.Write(json.Select(i => new
                    {
                        EmployeeStatus = i.employeeStatus,
                        ProfileName = i.profileName,
                        FirstName = i.firstName,
                        LastName = i.lastName,
                        Email = i.email,
                        SalesRepId = i.salesRepId,
                        EmployeeId = i.employeeId,                                        
                        NtLogin = i.ntLogin,
                        Domain = i.domain
                    }));
                }
            }
        }
	}
}


