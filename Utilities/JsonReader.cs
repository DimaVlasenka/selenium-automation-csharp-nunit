using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelenFramework.Utilities
{
    public class JsonReader
    {
        public JsonReader() 
        { 
        }

        public string extractData(String tokenName) 
        {
            var myJsonString = File.ReadAllText("TestData/testData.json");

            var jsonObject = JToken.Parse(myJsonString);

            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public string[] extractDataArray(String tokenName)
        {
            var myJsonString = File.ReadAllText("TestData/testData.json");

            var jsonObject = JToken.Parse(myJsonString);

            return jsonObject.SelectTokens(tokenName).Values<string>().ToArray<string>();
        }
    }
}
