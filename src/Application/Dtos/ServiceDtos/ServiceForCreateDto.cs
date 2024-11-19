using Domain.Entities;

namespace Application.Dtos.ServiceDtos;

public record ServiceForCreateDto : ServiceForManipulationDto 
{ 
    public Service ToEntity(string? pictureUrl = null)
    {
        var service = new Service()
        {
            CategoryId = this.CategoryId,
            Cost = this.Cost,
            Description = this.Description,
            Name = this.Name
        };
        if( pictureUrl != null ) service.PictureUrl = pictureUrl;
        return service;
    }
}
