using System.ComponentModel.DataAnnotations;

namespace SmartGallery.Service.Dtos.CategoryDtos
{
    public abstract record CategoryForManipulationDto
    {
        [Required]
        public string Name { get; init; }
    }
   

}
