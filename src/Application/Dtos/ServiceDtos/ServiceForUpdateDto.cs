using Domain.Entities;

namespace Application.Dtos.ServiceDtos;

public record ServiceForUpdateDto : ServiceForManipulationDto
{
    public void UpdateEntity(Service service, string? pictureUrl = null)
    {
        service.Name = this.Name;
        service.Cost = this.Cost;
        service.Description = this.Description;
        if (pictureUrl != null) 
            service.PictureUrl = pictureUrl;
    }
}
