using Application.Dtos.ReviewDtos;
using Application.Services.Contracts;
using Application.Specifications;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;

namespace Application.Services.Implementation;

public class ReviewService : IReviewService
{
    private readonly IRepositoryManager _repositoryManager;
    public ReviewService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public async Task<Result> CreateReviewAsync(string customerId, ReviewForCreateDto reviewForCreate)
    {
        var service = await _repositoryManager.ServiceRepository.GetByIdAsync(reviewForCreate.ServiceId);
        if (customerId is null || service is null)
        {
            return ApplicationErrors.BadRequestError;
        }
        var review = new Review
        {
            Title = reviewForCreate.Title,
            AsAnonymous = reviewForCreate.AsAnonymous,
            Comment = reviewForCreate.Comment,
            CustomerId = customerId,
            Rate = reviewForCreate.Rate,
            ServiceId = reviewForCreate.ServiceId
        };
        _repositoryManager.ReviewRepository.Create(review);
        await _repositoryManager.SaveChangesAsync();
        var reviewDto = new ReviewDto
        {
            Id = review.Id,
            AsAnonymous = review.AsAnonymous,
            Comment = review.Comment,
            Rate = review.Rate,
            Title = review.Title,
            CreatedAt = review.CreatedAt
        };
        return Result<ReviewDto>.Success(reviewDto);
    }

    public Task<Result> DeleteReviewAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> GetAllReviewsAsync(int? serviceId)
    {
        var spec = new ReviewSpecification(serviceId);
        var reviews = await _repositoryManager.ReviewRepository.GetAllAsync(spec);
        return Result<IEnumerable<ReviewDto>>.Success(reviews);
    }

    public Task<Result> GetReviewById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateReviewAsync(int id, ReviewForUpdateDto ReviewForUpdate)
    {
        throw new NotImplementedException();
    }
}
