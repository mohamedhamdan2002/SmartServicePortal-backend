using Domain.Entities;

namespace Application.Dtos.ReviewDtos;

public record ReviewForCreateDto : ReviewForManipulationDto
{
    public Review ToEntity(string customerId)
    {
        return new Review
        {
            Title = this.Title,
            AsAnonymous = this.AsAnonymous,
            Comment = this.Comment,
            CustomerId = customerId,
            Rate = this.Rate,
            ServiceId = this.ServiceId
        };
    }
}
