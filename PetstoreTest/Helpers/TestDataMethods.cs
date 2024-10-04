using Newtonsoft.Json;
using System.Reflection;
using TestFramework.Extensions;

namespace PetstoreTest.Helpers
{
    public class TestDataMethods
    {
        private static readonly TestData _testData;

        static TestDataMethods()
        {
            var json = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/testData.json");
            _testData = JsonConvert.DeserializeObject<TestData>(json)!;
        }

        public static User GetRandomUser()
        {
            return _testData.Users.RandomItem();
        }
    }
}
