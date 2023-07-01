namespace KasaBiletowa.Model;

public class Stacja
{
    public string? Name { get; init; }
    public int? Id { get; init; }
    public string? NameSlug { get; init; }
    public float? Latitude { get; init; }
    public float? Longitude { get; init; }
    public int? Hits { get; init; }
    public string? Ibnr { get; init; }
    public string? City { get; init; }
    public virtual Region Region { get; init; } = new();
    public virtual Kraj? Country { get; init; }
    public string? LocalisedName { get; init; }
    public bool? IsGroup { get; init; }
    public bool? HasAnnouncements { get; init; }

    public override string ToString()
    {
        return $"{Name}, {Country}, {City}";
    }
}