
namespace WebApi.Domain.src.Entities
{

    public class SearchQueryOptions
    {
        public string SearchQuery { get; set; } = string.Empty;
        public string SortBy { get; set; } = "CreatedAt"; // Default sorting by Id
        public bool SortAscending { get; set; } = true; // Default sorting in ascending order
        public int PageNumber { get; set; } = 1; // Default page number is 1
        public int PageSize { get; set; } = 10; // Default page size is 10
    }
}
