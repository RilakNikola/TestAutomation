using Newtonsoft.Json;
using System.Reflection;
using TestFramework.Extensions;

namespace UiTests.Helpers
{
    public static class TestDataMethods
    {
        private static readonly TestData _testData;

        static TestDataMethods()
        {
            var json = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/testData.json");
            _testData = JsonConvert.DeserializeObject<TestData>(json)!;
        }

        public static User GetUserById(int id)
        {
            return _testData.Users.FirstOrDefault(u => u.Id == id)!;
        }

        public static SearchFormData GetSearchFormDataById(int id)
        {
            return _testData.SearchFormData.FirstOrDefault(s => s.Id == id)!;
        }

        public static FiltersData GetFiltersDataById(int id)
        {
            return _testData.FiltersData.FirstOrDefault(f => f.Id == id)!;
        }

        public static MessageCaptainData GetMessageCaptainDataById(int id)
        {
            return _testData.MessageCaptainData.FirstOrDefault(m => m.Id == id)!;
        }

        public static ResultsData GetResultsDataById(int id)
        {
            return _testData.ResultsData.FirstOrDefault(m => m.Id == id)!;
        }

        public static User GetRandomUser()
        {
            return _testData.Users.RandomItem();
        }

        public static SearchFormData GetRandomSearchFormData()
        {
            return _testData.SearchFormData.RandomItem();
        }

        public static FiltersData GetRandomFiltersData()
        {
            return _testData.FiltersData.RandomItem();
        }

        public static MessageCaptainData GetRandomMessageCaptainDataById()
        {
            return _testData.MessageCaptainData.RandomItem();
        }
        public static ResultsData GetRandomResultsData()
        {
            return _testData.ResultsData.RandomItem();
        }
    }
}
