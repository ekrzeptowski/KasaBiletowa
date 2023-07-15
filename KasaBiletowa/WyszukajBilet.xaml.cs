using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KasaBiletowa.Model;
using KasaBiletowa.ViewModel;

namespace KasaBiletowa;

public partial class WyszukajBilet : Window
{
    private readonly KasaBiletowaContext _context = new();

    public WyszukajBilet()
    {
        InitializeComponent();
        DataOdjazdu.SelectedDate = DateTime.Now;

        var kupBiletViewModel = new WyszukajBiletViewModel();
        DataContext = kupBiletViewModel;
    }

    private void SearchButton_OnClick(object sender, RoutedEventArgs e)
    {
        var kupBiletViewModel = (WyszukajBiletViewModel)DataContext;
        if (kupBiletViewModel.StacjaPoczatkowa == null || kupBiletViewModel.StacjaKoncowa == null) return;
        kupBiletViewModel.GetPolaczenia(kupBiletViewModel.StacjaPoczatkowa, kupBiletViewModel.StacjaKoncowa);
    }

    private void KupBiletButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (Global.LoggedUser == null) return;
        _context.Bilety.Add(new Bilet
        {
            PolaczenieId = (int)((Button)sender).CommandParameter,
            KlientId = Global.LoggedUser.KlientId,
            DataSprzedazy = DateTime.Now
        });
        var relacja = _context.Polaczenia.FirstOrDefault(polaczenie =>
            polaczenie.PolaczenieId == (int)((Button)sender).CommandParameter);
        MessageBox.Show($"Kupiono bilet relacji {relacja?.StacjaPoczatkowa.Name} - {relacja?.StacjaKoncowa.Name}");
        _context.SaveChanges();
        Close();
    }
}