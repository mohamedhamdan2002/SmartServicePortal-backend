using SmartGallery.Core.Specifications;

namespace SmartGallery.Service.Specifications
{
    public class ServiceCountSpecification : BaseSpecification<Core.Entities.Service>
    {
        public ServiceCountSpecification(SpecificationParameter specParams)
            : base(service =>
                            (string.IsNullOrEmpty(specParams.Search) || service.Name.ToLower().Contains(specParams.Search!)) &&
                            (!specParams.CategoryId.HasValue || service.CategoryId == specParams.CategoryId.Value)
            )
        {

        }

    }
}
