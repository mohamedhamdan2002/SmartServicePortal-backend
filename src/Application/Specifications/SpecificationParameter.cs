namespace Application.Specifications;

public class SpecificationParameter
{
    private const int _maxPageSize = 20;
    private int _pageSize = 10;

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value > _maxPageSize ? _maxPageSize : value;
        }
    }

    private int _pageIndex = 1;
    public int PageIndex
    {
        get
        {
            return _pageIndex;
        }
        set
        {
            _pageIndex = value;
        }
    }
    public string? Sort { get; set; }
    public int? CategoryId { get; set; }
    private string? search;

    public string? Search
    {
        get { return search; }
        set { search = value?.ToLower(); }
    }


}
