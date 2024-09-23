using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Application.Specifications;

public abstract class RequestParameters
{
    private const int _maxPageSize = 20;
    private const int _defaultPageIndex = 1;
    private int _pageSize = 10;
    private int _pageIndex = _defaultPageIndex;
    private string? search;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value > _maxPageSize ? _maxPageSize : value;
        }
    }
    public int PageIndex
    {
        get
        {
            return _pageIndex;
        }
        set
        {
            _pageIndex = value < 1 ? _defaultPageIndex : value;
        }
    }
    public string? Sort { get; set; }
    public string? Search
    {
        get { return search; }
        set { search = value?.ToLower(); }
    }
}

public class SpecificationParameters : RequestParameters
{
    public CategoriesIds? CategoriesIds { get; set; }

}
public record CategoriesIds : IParsable<CategoriesIds>
{
    public IEnumerable<int> Values { get; init; } = Enumerable.Empty<int>();

    public static CategoriesIds Parse(string value, IFormatProvider? provider)
    {
        if(!TryParse(value, provider, out var result))
        {
            throw new ArgumentException("Could not parse this value", nameof(value));
        }
        return result;
    }

    public static bool TryParse([NotNullWhen(true)] string? value, IFormatProvider? provider, [MaybeNullWhen(false)] out CategoriesIds result)
    {
        var segments = value?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        
        if(segments is null || !segments.Any())
        {
            result = null;
            return false;
        }
        List<int> ids = new List<int>();
        foreach (var segment in segments)
        {
            if(int.TryParse(segment, out var id))
            {
                ids.Add(id);
            }
        }
        result = new CategoriesIds
        {
            Values = ids
        };
        //segments.Select(segmant => int.TryParse(segmant, out int id) ? (int?)id : null)
        //                     .Where(id => id.HasValue)
        //                     .Select(id => id.Value)
        return true;
 
    }
}