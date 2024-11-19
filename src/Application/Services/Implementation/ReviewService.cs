using Application.Dtos.ReviewDtos;
using Application.Services.Contracts;
using Application.Specifications;
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
    public async Task<Result<ReviewDto>> CreateReviewAsync(string customerId, ReviewForCreateDto reviewForCreate)
    {
        var service = await _repositoryManager.ServiceRepository.GetByIdAsync(reviewForCreate.ServiceId);
        if (customerId is null || service is null)
            return Result.Fail<ReviewDto>(ApplicationErrors.BadRequestError);
        var review = reviewForCreate.ToEntity(customerId);
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
        return Result.Success(reviewDto);
    }

    public async Task<Result<IEnumerable<ReviewDto>>> GetAllReviewsAsync(int? serviceId)
    {
        var spec = new ReviewSpecification(serviceId);
        var reviews = await _repositoryManager.ReviewRepository.GetAllAsync(spec);
        return Result.Success(reviews);
    }
}
