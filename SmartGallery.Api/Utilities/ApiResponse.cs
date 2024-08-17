namespace SmartGallery.Api.Utilities
{
    public class ApiResponse<TResult>
    {
        public TResult? Data { get; set; }
        public bool Succeeded { get; set; }
        public string[]? Errors { get; set; }
        public ApiResponse() { }
        public ApiResponse(TResult data)
        {
            Succeeded = true;
            this.Data = data;
            Errors = null;
        }
        public static implicit operator ApiResponse<TResult>(TResult data) => new(data);
        
    }
}
