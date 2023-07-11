﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace KasaBiletowa.Utils;

public static class ImportJson
{
    public static List<StacjaJson>? ToList()
    {
        Microsoft.Win32.OpenFileDialog dlg = new()
        {
            DefaultExt = ".json",
            Filter = "JSON Files (*.json)|*.json"
        };

        bool? result = dlg.ShowDialog();
        if (result != true) return null;
        string filename = dlg.FileName;
        string json = System.IO.File.ReadAllText(filename);
        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<StacjaJson[]>(json);
        List<StacjaJson>? stacje = (data ?? Array.Empty<StacjaJson>()).Where(station => station.country is "Polska")
            .ToList();

        MessageBox.Show($"Liczba stacji w Polsce: {stacje.Count}");
        return stacje;
    }
}