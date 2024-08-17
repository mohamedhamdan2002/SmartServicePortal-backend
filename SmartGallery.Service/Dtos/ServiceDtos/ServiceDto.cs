namespace SmartGallery.Service.Dtos.ServiceDtos
{
    public record ServiceDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string PictureUrl { get; init; }
        public decimal Cost { get; init; }
        public string Category { get; init; }
    }
}
