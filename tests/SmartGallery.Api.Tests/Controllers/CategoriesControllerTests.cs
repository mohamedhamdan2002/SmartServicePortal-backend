using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartGallery.Api.Controllers;
using SmartGallery.Core.Errors;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.CategoryDtos;

namespace SmartGallery.Api.Tests.Controllers
{
    public class CategoriesControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<ICategoryService> _serviceMock;
        private readonly CategoriesController _sut; // sut => system under test
        public CategoriesControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<ICategoryService>>();
            _sut = new CategoriesController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllCategories_WhenThereAreCategoriesInDataBase_ShouldReturnOKResponseWithNotEmptyListOfCategories()
        {
            // Arrange
            var categoriesMock = _fixture.Create<IReadOnlyList<CategoryDto>>();
            var resultMock = Result<IReadOnlyList<CategoryDto>>.Success(categoriesMock);
            _serviceMock.Setup(x => x.GetAllCategoriesAsync()).ReturnsAsync(resultMock);

            // Act
            var response = await _sut.GetAllCategories();

            // Assert 
            response.Result
                    .Should()
                    .BeOfType<OkObjectResult>()
                    .Which.StatusCode
                    .Should().Be(StatusCodes.Status200OK);

            response.Result
                    .As<OkObjectResult>()
                    .Value.Should().NotBeNull()
                    .And.BeAssignableTo<IReadOnlyList<CategoryDto>>()
                    .Which.Should().NotBeEmpty();

            _serviceMock.Verify(x => x.GetAllCategoriesAsync(), Times.Once());
        }

        [Fact]
        public async Task GetAllCategories_WhenThereAreNotCategoriesInDataBase_ShouldReturnOKResponseWithEmptyListOfCategories()
        {
            // Arrange
            var empytCategoriesMock = new List<CategoryDto>().AsReadOnly();
            var resultMock = Result<IReadOnlyList<CategoryDto>>.Success(empytCategoriesMock);
            _serviceMock.Setup(x => x.GetAllCategoriesAsync()).ReturnsAsync(resultMock);

            // Act
            var response = await _sut.GetAllCategories();

            // Assert 
            response.Result
                    .Should()
                    .BeOfType<OkObjectResult>()
                    .Which.StatusCode
                    .Should().Be(StatusCodes.Status200OK);

            response.Result
                    .As<OkObjectResult>()
                    .Value.Should().NotBeNull()
                    .And.BeAssignableTo<IReadOnlyList<CategoryDto>>()
                    .Which.Should().BeEmpty();
            _serviceMock.Verify(x => x.GetAllCategoriesAsync(), Times.Once());
        }

        [Fact]
        public async Task GetCategoryById_WhenCategoryExists_ShouldReturnOkResponseWithCategoryDto()
        {
            // arrange
            var categoryDto = _fixture.Create<CategoryDto>();
            var id = _fixture.Create<int>();
            var resultMock = Result<CategoryDto>.Success(categoryDto);
            _serviceMock.Setup(x => x.GetCategoryByIdAsync(id)).ReturnsAsync(resultMock);

            // act
            var response = await _sut.GetCategoryById(id);

            // assert
            response.Result
                    .Should()
                    .BeOfType<OkObjectResult>()
                    .Which.StatusCode
                    .Should().Be(StatusCodes.Status200OK);

            response.Result
                    .As<OkObjectResult>()
                    .Value.Should().NotBeNull()
                    .And.BeOfType<CategoryDto>();

            _serviceMock.Verify(x => x.GetCategoryByIdAsync(id), Times.Once());
        }

        [Fact]
        public async Task GetCategoryById_WhenCategoryDoesNotExist_ShouldReturnNotFoundResponseWithNotFondError()
        {
            // arrange
            var id = _fixture.Create<int>();
            var resultMock = Result.Failure(ApplicationErrors.NotFoundError);
            _serviceMock.Setup(x => x.GetCategoryByIdAsync(id)).ReturnsAsync(resultMock);

            // act
            var response = await _sut.GetCategoryById(id);

            // assert
            response.Result
                    .Should()
                    .BeOfType<NotFoundObjectResult>()
                    .Which.StatusCode
                    .Should().Be(StatusCodes.Status404NotFound);

            response.Result
                    .As<NotFoundObjectResult>()
                    .Value.Should().NotBeNull()
                    .And.BeOfType<Error>();

            _serviceMock.Verify(x => x.GetCategoryByIdAsync(id), Times.Once());
        }

        [Fact]
        public async Task CreateCategory_WhenValidCategoryForCreateDto_ShouldReturnOkResponseWithCategoryDto()
        {
            // arrange
            var categoryForCreateDto = _fixture.Create<CategoryForCreateDto>();
            var categoryDto = _fixture.Create<CategoryDto>();
            var resultMock = Result<CategoryDto>.Success(categoryDto);
            _serviceMock.Setup(x => x.CreateCategoryAsync(categoryForCreateDto)).ReturnsAsync(resultMock);

            // act 
            var response = await _sut.CreateCategory(categoryForCreateDto);

            // assert
            response.Result
                    .Should()
                    .BeOfType<OkObjectResult>()
                    .Which.StatusCode
                    .Should().Be(StatusCodes.Status200OK);

            response.Result
                    .As<OkObjectResult>()
                    .Value.Should().NotBeNull()
                    .And.BeOfType<CategoryDto>();
            _serviceMock.Verify(x => x.CreateCategoryAsync(categoryForCreateDto), Times.Once());
        }

        [Fact]
        public async Task UpdateCategory_WhenValidIdAndValidCategoryForUpdateDto_ShouldReturnNoContentResponse()
        {
            // arrange
            var categoryForUpdateDto = _fixture.Create<CategoryForUpdateDto>();
            var id = _fixture.Create<int>();
            var resultMock = Result.Success();
            _serviceMock.Setup(x => x.UpdateCategoryAsync(id, categoryForUpdateDto)).ReturnsAsync(resultMock);

            // act 
            var response = await _sut.UpdateCategory(id, categoryForUpdateDto);

            // assert
            response.Should()
                    .BeOfType<NoContentResult>()
                    .Which.StatusCode
                    .Should().Be(StatusCodes.Status204NoContent);

            _serviceMock.Verify(x => x.UpdateCategoryAsync(id, categoryForUpdateDto), Times.Once());
        }

        [Fact]
        public async Task UpdateCategory_WhenCategoryDoesNotExist_ShouldReturnBadRequestWithBadRequestError()
        {
            // arrange
            var categoryForUpdateDto = _fixture.Create<CategoryForUpdateDto>();
            var id = _fixture.Create<int>();
            var resultMock = Result.Failure(ApplicationErrors.BadRequestError);
            _serviceMock.Setup(x => x.UpdateCategoryAsync(id, categoryForUpdateDto)).ReturnsAsync(resultMock);

            // act 
            var response = await _sut.UpdateCategory(id, categoryForUpdateDto);

            // assert
            response.Should()
                    .BeOfType<BadRequestObjectResult>()
                    .Which.StatusCode
                    .Should().Be(StatusCodes.Status400BadRequest);

            response.As<BadRequestObjectResult>()
                    .Value.Should().NotBeNull()
                    .And.BeOfType<Error>();

            _serviceMock.Verify(x => x.UpdateCategoryAsync(id, categoryForUpdateDto), Times.Once());
        }

        [Fact]
        public async Task DeleteCategory_WhenCategoryDoesExist_ShouldReturnNoContent()
        {
            // arrange
            var id = _fixture.Create<int>();
            var resultMock = Result.Success();
            _serviceMock.Setup(x => x.DeleteCategoryAsync(id)).ReturnsAsync(resultMock);

            // act 
            var response = await _sut.DeleteCategory(id);

            // assert
            response.Should()
                    .BeOfType<NoContentResult>()
                    .Which.StatusCode
                    .Should().Be(StatusCodes.Status204NoContent);

            _serviceMock.Verify(x => x.DeleteCategoryAsync(id), Times.Once());
        }

        [Fact]
        public async Task DeleteCategory_WhenCategoryDoesNotExist_ShouldReturnBadRequestWithError()
        {
            // arrange
            var id = _fixture.Create<int>();
            var resultMock = Result.Failure(ApplicationErrors.BadRequestError);
            _serviceMock.Setup(x => x.DeleteCategoryAsync(id)).ReturnsAsync(resultMock);

            // act 
            var response = await _sut.DeleteCategory(id);

            // assert
            response.Should()
                    .BeOfType<BadRequestObjectResult>()
                    .Which.StatusCode
                    .Should().Be(StatusCodes.Status400BadRequest);

            response.As<BadRequestObjectResult>()
                    .Value.Should().NotBeNull()
                    .And.BeOfType<Error>();

            _serviceMock.Verify(x => x.DeleteCategoryAsync(id), Times.Once());
        }
    }
}
