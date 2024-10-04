namespace TestFramework.Extensions
{
    public static class GenericExtensions
    {
        private static readonly Random _rand = new();

        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_rand.Next(s.Length)]).ToArray());
        }

        public static int GetRandomInt(int min = 0, int max = 100000)
        {
            return _rand.Next(min, max);
        }

        public static string GetRandomInt(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_rand.Next(s.Length)]).ToArray());
        }

        public static T RandomItem<T>(this IEnumerable<T> items)
        {
            IEnumerable<T> enumerable = items.ToList();
            return enumerable.ElementAt(_rand.Next(0, enumerable.Count()));
        }

        public static IEnumerable<T> GetRandomElements<T>(this IEnumerable<T> list, int elementsCount)
        {
            return list.OrderBy(x => Guid.NewGuid()).Take(elementsCount).ToList();
        }

        public static string ReadTextFromFile(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            return File.ReadAllText(filePath);
        }
    }
}
