using System.Collections.Generic;

namespace KasaBiletowa.Model;

public class Region
{
    public int RegionId { get; init; }
    public string RegionName { get; init; } = string.Empty;
    public int KrajId { get; init; }
    public virtual Kraj Kraj { get; init; } = new();
    public virtual ICollection<Stacja> Stacje { get; init; } = new List<Stacja>();
}