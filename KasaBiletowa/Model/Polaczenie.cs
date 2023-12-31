﻿using System;

namespace KasaBiletowa.Model;

public class Polaczenie
{
    public int PolaczenieId { get; init; }
    public virtual Stacja StacjaPoczatkowa { get; init; } = new();
    public virtual Stacja StacjaKoncowa { get; init; } = new();
    public DateTime DataOdjazdu { get; init; }
    public DateTime DataPrzyjazdu { get; init; }
    public int Cena { get; init; }

    public string DzienOdjazdu => DataOdjazdu.ToString("dd.MM.yyyy");
    public string GodzinaOdjazdu => DataOdjazdu.ToString("HH:mm");
    public string GodzinaPrzyjazdu => DataPrzyjazdu.ToString("HH:mm");
    public string CzasPodrozy => new TimeOnly(DataPrzyjazdu.Ticks - DataOdjazdu.Ticks).ToString("HH:mm");
    public string Odleglosc => $"{StacjaPoczatkowa.Odleglosc(StacjaKoncowa):F0} km";
}