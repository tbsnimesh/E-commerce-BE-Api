namespace ShopApi.Api.QueryParameters
{
    public class QueryParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        private int _page = 1;
        public int Page
        {
            get => _page;
            set => _page = value < 1 ? 1 : value;
        } 

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value switch
            {
                < 1 => 10,
                > MaxPageSize => MaxPageSize,
                _ => value
            };
        }

        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string SortDirection { get; set; } = "asc";

        // Computed helper — used in LINQ
        public bool IsDescending =>
            SortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase);

        // How many records to skip — used in .Skip()
        public int Skip => (Page - 1) * PageSize;
    }
}
