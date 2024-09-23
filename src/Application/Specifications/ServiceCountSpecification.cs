using Domain.Specifications;

namespace Application.Specifications;

public class ServiceCountSpecification : Specification<Domain.Entities.Service>
{
    public ServiceCountSpecification(SpecificationParameters specParams)
        : base(service =>
                        (string.IsNullOrEmpty(specParams.Search) || service.Name.ToLower().Contains(specParams.Search!)) &&
                        (!(specParams.CategoriesIds != null) || specParams.CategoriesIds.Values.Any(value => value == service.CategoryId))
        )
    {

    }

}
