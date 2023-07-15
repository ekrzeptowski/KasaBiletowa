using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using KasaBiletowa.Model;

namespace KasaBiletowa.ViewModel;

public class WyszukajBiletViewModel : ViewModelBase
{
    private readonly KasaBiletowaContext _context = new();
    private DateTime _dataOdjazdu;
    private string? _stacjaPoczatkowa;
    private List<string?>? _stacjePoczatkowe;

    public WyszukajBiletViewModel()
    {
        DataOdjazdu = DateTime.Now;
    }

    public DateTime DataOdjazdu
    {
        get => _dataOdjazdu;
        set
        {
            if (value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Nie można wybrać daty z przeszłości");
                return;
            }

            _dataOdjazdu = value;
            StacjePoczatkowe = _context.Polaczenia
                .Where(polaczenie => polaczenie.DataOdjazdu.Date == DataOdjazdu.Date)
                .Select(p => p.StacjaPoczatkowa.Name)
                .Distinct()
                .OrderBy(s => s)
                .ToList();
            OnPropertyChanged(nameof(StacjePoczatkowe));
        }
    }

    public List<Polaczenie> FilteredPolaczenia { get; set; } = new();

    public List<string?>? StacjePoczatkowe
    {
        get => _stacjePoczatkowe;
        set
        {
            _stacjePoczatkowe = value;
            OnPropertyChanged(nameof(StacjePoczatkowe));
        }
    }

    public string? StacjaPoczatkowa
    {
        get => _stacjaPoczatkowa;
        set
        {
            _stacjaPoczatkowa = value;
            StacjeKoncowe = _context.Polaczenia
                .Where(polaczenie => polaczenie.DataOdjazdu.Date == DataOdjazdu.Date &&
                                     polaczenie.StacjaPoczatkowa.Name == StacjaPoczatkowa)
                .Select(p => p.StacjaKoncowa.Name)
                .Distinct()
                .OrderBy(s => s)
                .ToList();
            OnPropertyChanged(nameof(StacjeKoncowe));
            OnPropertyChanged(nameof(StacjaPoczatkowa));
        }
    }

    public object? StacjeKoncowe { get; set; }

    public string? StacjaKoncowa { get; set; }

    public void GetPolaczenia(string stacjaPoczatkowa, string stacjaKoncowa)
    {
        FilteredPolaczenia = _context.Polaczenia.Where(p => p.StacjaPoczatkowa.Name == stacjaPoczatkowa &&
                                                            p.StacjaKoncowa.Name == stacjaKoncowa &&
                                                            p.DataOdjazdu.Date == DataOdjazdu.Date)
            .OrderBy(p => p.DataOdjazdu).ToList();
        OnPropertyChanged(nameof(FilteredPolaczenia));
    }
}