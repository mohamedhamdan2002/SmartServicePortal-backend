namespace SmartGallery.Service.Utilities
{
    public class Pagination<T>
    {

        public IReadOnlyList<T> Data { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
