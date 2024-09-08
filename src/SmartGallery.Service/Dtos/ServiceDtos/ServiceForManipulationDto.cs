using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SmartGallery.Service.Dtos.ServiceDtos
{
    public abstract record ServiceForManipulationDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; }
        public IFormFile Picture { get; init; }
        [Required]
        public decimal Cost { get; init; }
        [Required]
        public int CategoryId { get; init; }
    }
}
