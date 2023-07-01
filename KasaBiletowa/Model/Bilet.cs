using System;

namespace KasaBiletowa.Model;

public class Bilet
{
    public int BiletId { get; init; }
    public int KlientId { get; init; }
    public virtual Klient Klient { get; init; } = new();
    public DateTime DataSprzedazy { get; init; }
    public int IdStacjiPoczatkowej { get; init; }
    public virtual Stacja StacjaPoczatkowa { get; init; } = new();
    public int IdStacjiKoncowej { get; init; }
    public virtual Stacja StacjaKoncowa { get; init; } = new();
}