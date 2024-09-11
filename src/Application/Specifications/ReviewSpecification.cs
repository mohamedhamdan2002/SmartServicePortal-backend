using Application.Dtos.ReviewDtos;
using Domain.Entities;
using Domain.Specifications;

namespace Application.Specifications;

public class ReviewSpecification : SpecificationWithResultType<Review, ReviewDto>
{
    public ReviewSpecification(int? serviceId)
        : base(review => !serviceId.HasValue || review.ServiceId == serviceId)
    {
        AddInclude(review => review.Customer);
        OrderByDescending = review => review.CreatedAt;
        Selector = review => new ReviewDto
        {
            Rate = review.Rate,
            AsAnonymous = review.AsAnonymous,
            Comment = review.Comment,
            Id = review.Id,
            Title = review.Title,
            CustomerName = review.Customer.UserName!,
            CreatedAt = review.CreatedAt
        };
    }
}
