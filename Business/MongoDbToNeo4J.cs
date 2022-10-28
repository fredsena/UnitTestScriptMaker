using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTestProject.BusinessConfig
{
    public class MongoDbToNeo4J
    {
        static string path = @"C:\DATA-SCRIPTS\MongoDbToNeo4J\";
        string filename = "DEPLOY_Nodes_Neo4J_09-09-2022";
        string fileToSearch = path + "Nodes.csv";

        [Fact]
        public void CREATE_NODES_Script()
        {
            var builder = new StringBuilder();
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                var cleanupRow = row[i].Replace("\"[", "[").Replace("]\"", "]").Replace("\"\"", "'");
                cleanupRow = cleanupRow.Replace("','", "'|'");

                string[] data = cleanupRow.Split(new[] { "," }, StringSplitOptions.None);


                var keyAlias = data[1].Replace(":", "");
                var key = data[1];
                var group = data[0];
                var label = data[2];
                var type = data[3];
                var valueLabel = data[4];


                var valueList = data[5].Replace("'|'", "','");
                var valueType = data[6].Replace("'|'", "','");

                //  0    1    2     3      4          5        6 
                //group,key,label,type,valueLabel,valueList,valueType

                builder.Append(GetCREATE_Template()
                    .Replace("#type#", type.Trim())
                    .Replace("#key#", keyAlias)
                );

                if (!string.IsNullOrWhiteSpace(group))
                {
                    builder.Append($" group: \"{group}\", ");
                }

                if (!string.IsNullOrWhiteSpace(label))
                {
                    builder.Append($" label: \"{label}\", ");
                }

                if (!string.IsNullOrWhiteSpace(type))
                {
                    builder.Append($" type: \"{type}\", ");
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    builder.Append($" name: \"{key} {type}\", key: \"{key}\", ");
                }

                if (!string.IsNullOrWhiteSpace(valueLabel))
                {
                    builder.Append($" valueLabel: \"{valueLabel}\", ");
                }

                if (!string.IsNullOrWhiteSpace(valueType))
                {
                    builder.Append($" valueType: \"{valueType}\", ");
                }

                if (!string.IsNullOrWhiteSpace(valueList))
                {
                    builder.Append($" valueList: \"{valueList}\", ");
                }

                var index = builder.ToString().LastIndexOf(',');

                if (index >= 0)
                {
                    builder.Remove(index, 1);
                }

                builder.Append(GetEndCREATEStatement());

                builder.AppendLine();
            }

            string filePath = path + "\\" + filename + ".txt";
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));
            builder = null;
        }

        public string GetCREATE_Template()
        {
            //group,key,label,type,valueLabel,valueList,valueType
            return @"CREATE (#type##key#:Node {";
        }

        public string GetEndCREATEStatement()
        {
            return @"});";
        }

        [Fact]
        public void CREATE_EDGES_Script()
        {
            filename = "DEPLOY_EDGES_Neo4J_09-09-2022.txt";
            fileToSearch = path + "G4_BusinessConfig_EDGES2.csv";

            var builder = new StringBuilder();
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string[] row =
                GetDataFromFile(fileToSearch)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim())) continue;

                //var cleanupRow = row[i].Replace("\"[", "[").Replace("]\"", "]").Replace("\"\"", "'");
                //cleanupRow = cleanupRow.Replace("','", "'|'");
                //string[] data = cleanupRow.Split(new[] { "," }, StringSplitOptions.None);

                string[] data = row[i].Split(new[] { ";" }, StringSplitOptions.None);

                //  0   1   2   3  
                //from,to,type,value

                var from = data[0];
                var to = data[1];
                var type = data[2];
                var value = data[3];

                if (string.IsNullOrWhiteSpace(value))
                {
                    builder.AppendLine(GET_MATCH_MERGE_Script()
                        .Replace("#from#", from.Trim())
                        .Replace("#to#", to)
                        .Replace("#type#", type)
                        .Replace("#number#", (i + 1).ToString())
                    );
                }
                else
                {
                    builder.AppendLine(GET_MATCH_MERGE_Script_WITHVALUE()
                        .Replace("#from#", from.Trim())
                        .Replace("#to#", to)
                        .Replace("#type#", type)
                        .Replace("#value#", value)
                        .Replace("#number#", (i + 1).ToString())
                    );
                }                
            }

            string filePath = path + "\\" + filename;
            File.WriteAllText(filePath, builder.ToString().Substring(0, builder.Length));            
        }

        public string GET_MATCH_MERGE_Script()
        {
            return @"MATCH(m#number#:Node) WHERE m#number#.key = '#from#' WITH m#number# MATCH (p#number#:Node) WHERE p#number#.key = '#to#' MERGE (m#number#)-[:EDGE {from: '#from#', to: '#to#', type: '#type#'}]->(p#number#);";
        }

        public string GET_MATCH_MERGE_Script_WITHVALUE()
        {
            //            return @"MATCH (m:Node {key:'#from#'}),(p:Node {key: '#to#'})
            //MERGE (m)-[:EDGE {from: '#from#', to: '#to#', type: '#type#', value: '#value#'}]->(p);";

            return @"MATCH(m#number#:Node) WHERE m#number#.key = '#from#' WITH m#number# MATCH (p#number#:Node) WHERE p#number#.key = '#to#' MERGE (m#number#)-[:EDGE {from: '#from#', to: '#to#', type: '#type#', value: '#value#'}]->(p#number#);";


        }

        public string GetDataFromFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath, Encoding.GetEncoding("UTF-8"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [Fact]
        public void ReplaceTest()
        {
            var test1 = $"\"[\"\"PDF\"\", \"\"HTML\"\", \"\"Excel\"\", \"\"XML\"\"]\"";
            var result = test1.Replace("\"[", "[").Replace("]\"", "]").Replace("\"\"", "'");


            IList<string> ValueList = result
                .Trim()
                .Replace("[", "")
                .Replace("]", "")
                .Split(new[] { "," }, StringSplitOptions.None);


            var list = new List<string> { "foo", "bar" };

            var tags = new { tags = list };


            var searchKey = "FRED";
            var result2 = JsonConvert.SerializeObject(list);


            var cursor = @"
            MATCH (node:Node)-[edge:EDGE]-(nodeSetting:Node)
            WHERE node.key = '$key'
            AND nodeSetting.key IN $settingsList
            RETURN nodeSetting.key AS setting, 
            nodeSetting.type AS nodeType, 
            edge.value AS value".Replace("$settingsList", result2).Replace("$key", searchKey);

            //, new { key = searchKey, settingsList = JsonConvert.SerializeObject(settingName) };

        }
    }
}


