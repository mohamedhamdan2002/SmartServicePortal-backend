using System.Diagnostics.CodeAnalysis;

namespace Application.Utilities;

public record CollectionOfIds : IParsable<CollectionOfIds>
{
    public IEnumerable<int> Values { get; init; } = Enumerable.Empty<int>();

    public static CollectionOfIds Parse(string value, IFormatProvider? provider)
    {
        if (!TryParse(value, provider, out var result))
        {
            throw new ArgumentException("Could not parse this value", nameof(value));
        }
        return result;
    }

    public static bool TryParse([NotNullWhen(true)] string? value, IFormatProvider? provider, [MaybeNullWhen(false)] out CollectionOfIds result)
    {
        var segments = value?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (segments is null || !segments.Any())
        {
            result = null;
            return false;
        }
        List<int> ids = new List<int>();
        foreach (var segment in segments)
        {
            if (int.TryParse(segment, out var id))
            {
                ids.Add(id);
            }
        }
        result = new CollectionOfIds
        {
            Values = ids
        };
        //segments.Select(segmant => int.TryParse(segmant, out int id) ? (int?)id : null)
        //                     .Where(id => id.HasValue)
        //                     .Select(id => id.Value)
        return true;

    }
}