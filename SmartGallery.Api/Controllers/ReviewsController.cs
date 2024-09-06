using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Api.Filters;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.ReviewDtos;

namespace SmartGallery.Api.Controllers
{
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
}
