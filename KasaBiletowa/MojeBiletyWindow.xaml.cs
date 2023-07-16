using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using KasaBiletowa.Model;
using KasaBiletowa.ViewModel;

namespace KasaBiletowa;

public partial class MojeBiletyWindow : Window
{
    public MojeBiletyWindow()
    {
        var mojeBiletyViewModel = new MojeBiletyViewModel();
        DataContext = mojeBiletyViewModel;
        InitializeComponent();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        var main = new MainWindow(true);
        main.Show();
        base.OnClosing(e);
    }
}