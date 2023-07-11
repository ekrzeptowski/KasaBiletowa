using System;

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

    public long Odleglosc(Stacja stacja)
    {
        double? dLat = (Latitude - stacja.Latitude) * (Math.PI / 180.0);
        double? dLon = (Longitude - stacja.Longitude) * (Math.PI / 180.0);
        double? lat1 = stacja.Latitude * (Math.PI / 180.0);
        double? lat2 = Latitude * (Math.PI / 180.0);
        if (dLat == null || dLon == null || lat1 == null || lat2 == null) return 0;
        double a = Math.Sin((double)(dLat / 2)) * Math.Sin((double)(dLat / 2)) +
                   Math.Sin((double)(dLon / 2)) * Math.Sin((double)(dLon / 2)) * Math.Cos((double)lat1) *
                   Math.Cos((double)lat2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return (long)(6371 * c);
    }
}