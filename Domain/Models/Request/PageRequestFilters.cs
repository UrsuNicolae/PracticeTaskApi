namespace Domain.Models.Request
{
    public class PageRequestFilters
    {
        public int Page { get; set; } = 1;

        public string Language { get; set; } = "en-US";

        public bool Include_Adult { get; set; } = false;

        public bool Include_Video { get; set; } = false;

        public bool MostPopular { get; set; } = true;

        public bool Latest { get; set; } = true;

        public bool MostIncome { get; set; } = true;
    }
}
