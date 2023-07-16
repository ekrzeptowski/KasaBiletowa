using System.Linq;
using KasaBiletowa.Model;

namespace KasaBiletowa.ViewModel;

public class MainViewModel : ViewModelBase
{
    private static readonly KasaBiletowaContext Context = new();

    public MainViewModel()
    {
        if (LoggedUser == null)
        {
            return;
        }

        NumberOfTickets = LoggedUser.Bilety.Count;
    }

    public Klient? LoggedUser { get; } = Global.LoggedUser;
    public int NumberOfTickets { get; private set; }
    public int NumberOfConnections { get; } = Context.Polaczenia.Count();

    public void UpdateNumberOfTickets()
    {
        if (LoggedUser == null)
        {
            return;
        }

        NumberOfTickets = LoggedUser.Bilety.Count;
        OnPropertyChanged(nameof(NumberOfTickets));
    }
}