using static UiTests.Helpers.Enums;

namespace UiTests.Helpers
{
    public class TestData
    {
        public List<User> Users { get; set; }
        public List<SearchFormData> SearchFormData { get; set; }
        public List<FiltersData> FiltersData { get; set; }
        public List<MessageCaptainData> MessageCaptainData { get; set; }
        public List<ResultsData> ResultsData { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EncryptedPassword { get; set; }
        public string KeyFilePath { get; set; }
    }

    public class SearchFormData
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public DateOption? DateOption { get; set; }
        public DateTime? Date { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
    }

    public class MessageCaptainData
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DayInTheMonth? DayInTheMonth { get; set; }
        public GroupSize? GroupSize { get; set; }
        public TripOptions? TripOptions { get; set; }
    }

    public class ResultsData
    {
        public int Id { get; set; }
        public Stickers? Sticker { get; set; }
        public Option? Option { get; set; }
    }

    public class FiltersData
    {
        public int Id { get; set; }
        public ReviewScore? ReviewScore { get; set; }
        public FishingType? FishingType { get; set; }
        public TargetedSpecies? TargetedSpecies { get; set; }
    }
}
