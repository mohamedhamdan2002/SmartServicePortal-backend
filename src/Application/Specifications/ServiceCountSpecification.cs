using Domain.Specifications;

namespace Application.Specifications;

public class ServiceCountSpecification : Specification<Domain.Entities.Service>
{
    public ServiceCountSpecification(SpecificationParameter specParams)
        : base(service =>
                        (string.IsNullOrEmpty(specParams.Search) || service.Name.ToLower().Contains(specParams.Search!)) &&
                        (!specParams.CategoryId.HasValue || service.CategoryId == specParams.CategoryId.Value)
        )
    {

    }

}
