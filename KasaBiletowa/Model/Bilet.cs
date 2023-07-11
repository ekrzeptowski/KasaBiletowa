using System;

namespace KasaBiletowa.Model;

public class Bilet
{
    public int BiletId { get; init; }
    public int KlientId { get; init; }
    public virtual Klient Klient { get; init; } = new();
    public DateTime DataSprzedazy { get; init; }
    public virtual Polaczenie Polaczenie { get; init; } = new();
    public int PolaczenieId { get; init; }
}