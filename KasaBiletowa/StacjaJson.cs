namespace KasaBiletowa;

public class StacjaJson
{
    public string? name { get; init; }
    public int? id { get; init; }
    public string? name_slug { get; init; }
    public float? latitude { get; init; }
    public float? longitude { get; init; }
    public int? hits { get; init; }
    public string? ibnr { get; init; }
    public string? city { get; init; }
    public string? region { get; init; }
    public string? country { get; init; }
    public string? localised_name { get; init; }
    public bool? is_group { get; init; }
    public bool? has_announcements { get; init; }

    public override string ToString()
    {
        return $"{name}, {country}, {city}";
    }
}