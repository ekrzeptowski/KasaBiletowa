using System;

namespace KasaBiletowa.Model;

public class Polaczenie
{
    public int PolaczenieId { get; init; }
    public virtual Stacja StacjaPoczatkowa { get; init; } = new();
    public virtual Stacja StacjaKoncowa { get; init; } = new();
    public int IdStacjiPoczatkowej { get; init; }
    public int IdStacjiKoncowej { get; init; }
    public DateTime DataOdjazdu { get; init; }
    public DateTime DataPrzyjazdu { get; init; }
}