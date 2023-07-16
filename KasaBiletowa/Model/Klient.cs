using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KasaBiletowa.Model;

public class Klient
{
    public int KlientId { get; init; }
    public string? Imie { get; init; }
    public string? Nazwisko { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Haslo { get; init; } = string.Empty;
    public string? Telefon { get; init; }
    public virtual ICollection<Bilet> Bilety { get; } = new List<Bilet>();
}