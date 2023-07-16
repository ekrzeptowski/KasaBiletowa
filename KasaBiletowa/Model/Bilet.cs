using System;

namespace KasaBiletowa.Model;

public class Bilet
{
    public int BiletId { get; init; }
    public virtual Klient Klient { get; init; } = null!;
    public int KlientId { get; init; }
    public DateTime DataSprzedazy { get; init; }
    public virtual Polaczenie Polaczenie { get; init; } = new();
}