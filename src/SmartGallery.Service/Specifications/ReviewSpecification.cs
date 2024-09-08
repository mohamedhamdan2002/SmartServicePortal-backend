using SmartGallery.Core.Entities;
using SmartGallery.Core.Specifications;
using SmartGallery.Service.Dtos.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Service.Specifications
{
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
                CustomerName = review.Customer.UserName!
            };
        }
    }
}
