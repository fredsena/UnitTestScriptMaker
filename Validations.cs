using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace UnitTestProject
{
    public class Validations
    {
        [Fact]
        public void Email_Slice()
        {
            var userEmail = "fredsena@fred.com";
            var userNameAny = userEmail.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var userId = @"BRAZIL\FredSena";
            var user = userId.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
        }

        //var userName = userEmail.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

        public enum UserRights : ushort
        {
            Access = 166,
            UpdateItSettings = 169,
            UpdateSettings = 168
        }

        public static IEnumerable<string> GetUserRights()
        {
            var list = new List<string>();

            foreach (var value in Enum.GetNames(typeof(UserRights)))
            {
                var rightValue = (int)(UserRights)Enum.Parse(typeof(UserRights), value);
                list.Add(rightValue.ToString());
            }
            return list;
        }

        public string UserRightsToStringValue(UserRights userRights)
        {
            return Convert.ToString((int)userRights);
        }

        [Fact]
        public void Enum_Test()
        {
            var userRights = GetUserRights();
            var value = UserRightsToStringValue(UserRights.Access);
        }

        //^(BU+:\d+:COMP:([A-Z0-9]+))
        [Theory]
        [InlineData("BU:11:COMP:A34B2")]
        [InlineData("BU:11:COMP:123456")]
        [InlineData("BU:11:COMP:ABDC")]
        public void Validate_COMP_Regex(string input)
        {
            Regex regex = new Regex("^(BU+:\\d+:COMP:([A-Z0-9]+))$");
            regex.Match(input);
            var isValid = regex.IsMatch(input);

            Assert.Equal(input, regex.Match(input).ToString());
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("BU:11:COMP:A34b2")]
        [InlineData("BU:11:COmP:A34B2")]
        [InlineData("BU:11:COMP")]
        [InlineData("BU:11:comp:A34B2")]
        [InlineData("BU:11:COMP:a3f4")]
        [InlineData("BU:11:COMP:")]
        public void Validate_COMP_InvalidRegex(string input)
        {
            Regex regex = new Regex("^(BU+:\\d+:COMP:([A-Z0-9]+))$");
            regex.Match(input);
            var isValid = regex.IsMatch(input);

            Assert.NotEqual(input, regex.Match(input).ToString());
            Assert.False(isValid);
        }

        [Fact]
        public void Validate_BU()
        {
            var key = "BU:202:COMP:01";

            var value = key.Split(':');

            var buKey = string.Empty;

            if (value.Length > 0)
            {
                buKey = $"{value[0]}:{value[1]}";
            }

            Assert.Equal("BU:202", buKey);
        }


        [Theory]
        [InlineData("BU:11")]
        [InlineData("BU:01")]
        [InlineData("BU:123456789")]
        public void Validate_BU_Regex(string input)
        {
            Regex regex = new Regex("^(BU+:\\d+)$");
            regex.Match(input);
            var isValid = regex.IsMatch(input);

            Assert.Equal(input, regex.Match(input).ToString());
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("BU:")]
        [InlineData("BU:ABC")]
        [InlineData("BU:A8c")]
        [InlineData("bu:A8c")]
        [InlineData("BU:11:COMP:01")]
        [InlineData("BU:11:COMP")]
        public void Validate_BU_InvalidRegex(string input)
        {
            Regex regex = new Regex("^(BU+:\\d+)$");
            regex.Match(input);
            var isValid = regex.IsMatch(input);

            Assert.NotEqual(input, regex.Match(input).ToString());
            Assert.False(isValid);
        }


        [Fact]
        public void Validate_Data_For_DB()
        {
            string sentence1 =
                @" ' UNION SELECT name, type, id FROM sysobjects;--  '; DELETE Orders; -- realdatavalue ''; DELETE Orders; --a(c1 int); SHuTdOWn WITH NOWAIT; --'; Drop Table Test; -- ;";

            string[] reservedWordsToTrim = {"sysobjects", "NOWAIT", "SHUTDOWN", "delete", "alter", "begin", "break", "checkpoint", "commit", "create", "cursor", "dbcc",
                "deny", "drop", "escape", "exec", "execute", "insert", "go", "grant", "opendatasource", "openquery",
                "openrowset", "shutdown", "sp_", "tran", "transaction", "update", "while", "xp_", ";", "--", "'" };


            string sentence = null;

            foreach (var excludedWords in reservedWordsToTrim)
            {
                sentence = Regex.Replace(sentence, excludedWords, "", RegexOptions.IgnoreCase).Trim();
            }

            //Assert.NotEmpty(sentence);
        }
    }

    static class StringTrimExtension
    {
        public static string TrimStart(this string value, string toTrim)
        {
            if (value.StartsWith(toTrim))
            {
                int startIndex = toTrim.Length;
                return value.Substring(startIndex);
            }
            return value;
        }

        public static string TrimEnd(this string value, string toTrim)
        {
            if (value.EndsWith(toTrim))
            {
                int startIndex = toTrim.Length;
                return value.Substring(0, value.Length - startIndex);
            }
            return value;
        }
    }

}
