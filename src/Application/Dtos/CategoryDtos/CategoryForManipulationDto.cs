using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.CategoryDtos;

public abstract record CategoryForManipulationDto
{
    [Required]
    public string Name { get; init; }
}
