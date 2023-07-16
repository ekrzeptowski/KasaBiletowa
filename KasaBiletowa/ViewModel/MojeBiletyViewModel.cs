using System.Collections.Generic;
using System.Linq;
using KasaBiletowa.Model;

namespace KasaBiletowa.ViewModel;

public class MojeBiletyViewModel : ViewModelBase
{
    private static readonly KasaBiletowaContext Context = new();

    public MojeBiletyViewModel()
    {
        if (LoggedUser == null)
        {
            return;
        }
    }

    public Klient? LoggedUser { get; } = Global.LoggedUser;

    public List<Bilet>? Bilety { get; } = Context.Bilety
        .Where(bilet => Global.LoggedUser != null && bilet.KlientId == Global.LoggedUser.KlientId).ToList();
}