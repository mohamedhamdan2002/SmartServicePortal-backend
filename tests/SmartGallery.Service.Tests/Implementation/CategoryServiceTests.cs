using AutoFixture;
using FluentAssertions;
using Moq;
using SmartGallery.Core.Entities;
using SmartGallery.Core.Errors;
using SmartGallery.Core.Repositories;
using SmartGallery.Service.Dtos.CategoryDtos;
using SmartGallery.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Service.Tests.Implementation
{
    public class CategoryServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IRepositoryManager> _repositoryMangerMock;
        private readonly CategoryService _sut;
        public CategoryServiceTests()
        {
            _fixture = new Fixture();
            _repositoryMangerMock = _fixture.Freeze<Mock<IRepositoryManager>>();
            _sut = new CategoryService(_repositoryMangerMock.Object);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_WhenCategoriesExist_ShouldReturnResultWithListOfCategoriesDto()
        {
            // arrange
            var categoresFromDb = _fixture.Create<IEnumerable<Category>>();
            var expectedCategoriesDtos = categoresFromDb.Select(c => new CategoryDto(c.Id, c.Name));
            _repositoryMangerMock.Setup(x => x.CategoryRepository.GetAllAsync()).ReturnsAsync(categoresFromDb);

            // act 
            var actualResult = await _sut.GetAllCategoriesAsync();
            
            // assert 
            actualResult.IsSuccess
                        .Should()
                        .BeTrue();

            actualResult.Should()
                        .BeOfType<Result<IReadOnlyList<CategoryDto>>>();

            actualResult.GetData<IReadOnlyList<CategoryDto>>()
                        .Should()
                        .NotBeNull()
                        .And.NotBeEmpty();

            actualResult.GetData<IReadOnlyList<CategoryDto>>()
                        .Should().HaveCount(categoresFromDb.Count());

            actualResult.GetData<IReadOnlyList<CategoryDto>>()
                        .Should()
                        .BeEquivalentTo(expectedCategoriesDtos);

            _repositoryMangerMock.Verify(x => x.CategoryRepository.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task GetAllCategoriesAsync_WhenCategoriesDoesNotExist_ShouldReturnResultWithEmptyListOfCategoriesDto()
        {
            // arrange
            var categoresFromDb = Enumerable.Empty<Category>();
            _repositoryMangerMock.Setup(x => x.CategoryRepository.GetAllAsync()).ReturnsAsync(categoresFromDb);

            // act 
            var actualResult = await _sut.GetAllCategoriesAsync();

            // assert 
            actualResult.IsSuccess
                        .Should()
                        .BeTrue();

            actualResult.Should()
                        .BeOfType<Result<IReadOnlyList<CategoryDto>>>();

            actualResult.GetData<IReadOnlyList<CategoryDto>>()
                        .Should()
                        .NotBeNull()
                        .And.BeEmpty();

            _repositoryMangerMock.Verify(x => x.CategoryRepository.GetAllAsync(), Times.Once());
        }
        [Fact]
        public async Task CreateCategoryAsync_WhenValidCategoryForCreateDto_ShouldReturnSuccessResultWithCategoryDto()
        {
            // arrange
            var categoryForCreateDto = _fixture.Create<CategoryForCreateDto>();
            var categoryEntity = new Category
            {
                Id = _fixture.Create<int>(),
                Name = categoryForCreateDto.Name
            };

            _repositoryMangerMock.Setup(x => x.CategoryRepository.CreateAsync(It.IsAny<Category>()))
                                 .Callback<Category>(c => c.Id = categoryEntity.Id);
            _repositoryMangerMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
            // act 
            var actualResult = await _sut.CreateCategoryAsync(categoryForCreateDto);
            var actualCategoryDto = actualResult.GetData<CategoryDto>();
            // assert 
            actualResult.IsSuccess
                        .Should()
                        .BeTrue();

            actualResult.Should()
                        .BeOfType<Result<CategoryDto>>();

            actualCategoryDto.Should()
                             .NotBeNull()
                             .And.BeOfType<CategoryDto>();
             

            actualCategoryDto.Id
                             .Should()
                             .Be(categoryEntity.Id);

            actualCategoryDto.Name
                             .Should()
                             .Be(categoryEntity.Name);

            _repositoryMangerMock.Verify(x => x.CategoryRepository.CreateAsync(It.IsAny<Category>()), Times.Once());
            _repositoryMangerMock.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async Task UpdateCategoryAsync_WhenCategoryExist_ShouldReturnSuccessResult()
        {
            // arrange
            var categoryForUpdateDto = _fixture.Create<CategoryForUpdateDto>();
            var category = _fixture.Create<Category>();
            var id = category.Id;
            _repositoryMangerMock.Setup(x => x.CategoryRepository.GetByIdAsync(id)).ReturnsAsync(category);

            _repositoryMangerMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);
  
            // act 
            var actualResult = await _sut.UpdateCategoryAsync(id, categoryForUpdateDto);
            // assert 
            actualResult.IsSuccess
                        .Should()
                        .BeTrue();

            actualResult.Should()
                        .BeOfType<Result>();

            category.Name.Should().Be(categoryForUpdateDto.Name);
            category.Id.Should().Be(id);

            _repositoryMangerMock.Verify(x => x.CategoryRepository.GetByIdAsync(category.Id), Times.Once());
            _repositoryMangerMock.Verify(x => x.SaveChangesAsync(), Times.Once());
        }
        [Fact]
        public async Task UpdateCategoryAsync_WhenCategoryDoesNotExist_ShouldReturnFailureResultWithError()
        {
            // arrange
            var categoryForUpdateDto = _fixture.Create<CategoryForUpdateDto>();
            var id = _fixture.Create<int>();
            _repositoryMangerMock.Setup(x => x.CategoryRepository.GetByIdAsync(id)).ReturnsAsync(null as Category);


            // act 
            var actualResult = await _sut.UpdateCategoryAsync(id, categoryForUpdateDto);
            // assert 
            actualResult.IsSuccess
                        .Should()
                        .BeFalse();

            actualResult.Should()
                        .BeOfType<Result>()
                        .Which.Error.StatusCode.Should().Be(ApplicationErrors.BadRequestError.StatusCode);

            actualResult.Error
                        .Message
                        .Should().Be(ApplicationErrors.BadRequestError.Message);

            _repositoryMangerMock.Verify(x => x.CategoryRepository.GetByIdAsync(id), Times.Once());
            _repositoryMangerMock.Verify(x => x.SaveChangesAsync(), Times.Never());
        }
    }
}
