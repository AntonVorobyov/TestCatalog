namespace TestCatalog.Models.Custom
{
    public class Filter
    {
        public Filter()
        {
            Page = 1;
            Take = 10;
        }

        public string Query { get; set; }
        public SortBy Sorting { get; set; }
        public int Page { get; set; }
        public int Take { get; set; }
    }
}