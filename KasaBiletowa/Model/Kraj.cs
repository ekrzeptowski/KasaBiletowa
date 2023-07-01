using System.Collections.Generic;

namespace KasaBiletowa.Model;

public class Kraj
{
    public int KrajId { get; init; }
    public string Country { get; init; } = string.Empty;
    public virtual ICollection<Region>? Regions { get; init; }
}