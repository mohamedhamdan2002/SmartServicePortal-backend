using SmartGallery.Core.Entities;

namespace SmartGallery.Service.Dtos.ReviewDtos
{
    public record ReviewForManipulationDto
    {
        public string Title { get; init; }
        public string Comment { get; init; }
        public int Rate { get; init; }
        public bool AsAnonymous { get; init; }
        public int ServiceId { get; init; }
    }
}
