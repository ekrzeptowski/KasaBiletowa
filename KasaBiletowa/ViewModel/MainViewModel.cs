using System.Linq;
using KasaBiletowa.Model;

namespace KasaBiletowa.ViewModel;

public class MainViewModel
{
    private static readonly KasaBiletowaContext Context = new();

    public MainViewModel()
    {
        if (LoggedUser == null)
        {
            return;
        }
    }

    public Klient? LoggedUser { get; } = Global.LoggedUser;
    public int NumberOfTickets { get; } = Global.LoggedUser?.Bilety.Count ?? 0;
    public int NumberOfConnections { get; } = Context.Polaczenia.Count();
}