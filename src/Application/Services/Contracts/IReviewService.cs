using Application.Dtos.ReviewDtos;
using Domain.Errors;

namespace Application.Services.Contracts;

public interface IReviewService
{
    Task<Result> GetAllReviewsAsync(int? serviceId);
    Task<Result> GetReviewById(int Id);
    Task<Result> CreateReviewAsync(string customerId, ReviewForCreateDto ReviewForCreate);
    Task<Result> UpdateReviewAsync(int id, ReviewForUpdateDto ReviewForUpdate);
    Task<Result> DeleteReviewAsync(int id);
}
