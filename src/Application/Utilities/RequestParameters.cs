namespace Application.Utilities;

public abstract class RequestParameters
{
    private const int _maxPageSize = 20;
    private const int _defaultPageIndex = 1;
    private int _pageSize = 10;
    private int _pageIndex = _defaultPageIndex;
    private string? search;
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
    public int PageIndex
    {
        get
        {
            return _pageIndex;
        }
        set
        {
            _pageIndex = value < 1 ? _defaultPageIndex : value;
        }
    }
    public string? Sort { get; set; }
    public string? Search
    {
        get { return search; }
        set { search = value?.ToLower(); }
    }
}
