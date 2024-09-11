using Api.Filters;
using Application.Dtos.ReviewDtos;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ReviewsController : BaseApiController
{
    private readonly IReviewService _reviewService;
    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    [Authorize]
    [BindFromClaims(Claim = "uid", ParameterName = nameof(customerId))]
    public async Task<ActionResult<ReviewDto>> CreateReview(string? customerId, ReviewForCreateDto createDto)
    {
        var result = await _reviewService.CreateReviewAsync(customerId!, createDto);
        return HandleResult<ReviewDto>(result);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int? serviceId)
    {
        var result = await _reviewService.GetAllReviewsAsync(serviceId);
        return HandleResult<IEnumerable<ReviewDto>>(result);
    }
}
