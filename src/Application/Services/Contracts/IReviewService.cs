using Application.Dtos.ReviewDtos;
using Domain.Errors;

namespace Application.Services.Contracts;

public interface IReviewService
{
    Task<Result<IEnumerable<ReviewDto>>> GetAllReviewsAsync(int? serviceId);
    Task<Result<ReviewDto>> CreateReviewAsync(string customerId, ReviewForCreateDto ReviewForCreate);
}
