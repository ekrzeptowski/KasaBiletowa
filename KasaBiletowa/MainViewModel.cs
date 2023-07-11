using System.Windows.Markup;
using KasaBiletowa.Model;

namespace KasaBiletowa;

public class MainViewModel
{
    private readonly KasaBiletowaContext _context = new();
    public Klient? LoggedUser { get; } = Global.LoggedUser;
}