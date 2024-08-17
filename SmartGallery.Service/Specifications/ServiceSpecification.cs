using SmartGallery.Core.Specifications;
using SmartGallery.Service.Dtos.ServiceDtos;

namespace SmartGallery.Service.Specifications
{
    public class ServiceSpecification : Specification<Core.Entities.Service, ServiceDto>
    {
        public ServiceSpecification(SpecificationParameter specParams)
            : base(
                service =>
                            (string.IsNullOrEmpty(specParams.Search) || service.Name.ToLower().Contains(specParams.Search!)) &&
                            (!specParams.CategoryId.HasValue || service.CategoryId == specParams.CategoryId.Value)

            )
        {
            AddInclude(service => service.Category);
            ApplySort(specParams.Sort);
            AddSelector();
            AddPagination(specParams.PageSize, specParams.PageIndex);
        }
        private void ApplySort(string? sort)
        {
            if (sort is null)
                OrderBy = service => service.Name;
            switch (sort)
            {
                case "cost":
                    OrderBy = service => service.Cost;
                    break;
                case "costDesc":
                    OrderByDescending = service => service.Cost;
                    break;
                default:
                    OrderBy = service => service.Name;
                    break;
            }
        }
        private void AddSelector()
        {
            Selector = service => new ServiceDto
            {
                Id = service.Id,
                Category = service.Category.Name,
                Name = service.Name,
                Cost = service.Cost,
                Description = service.Description,
                PictureUrl = service.PictureUrl
            };
        }
        public ServiceSpecification(int id)
            : base(service => service.Id == id)
        {
            AddInclude(service => service.Category);
            AddSelector();
        }

    }
}
