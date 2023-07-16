using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using KasaBiletowa.Model;
using KasaBiletowa.Utils;
using KasaBiletowa.ViewModel;

namespace KasaBiletowa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly KasaBiletowaContext _context = new();

        public MainWindow()
        {
            var mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
            InitializeComponent();
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _context.Dispose();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var kupBilet = new WyszukajBilet();
            kupBilet.Show();
            Close();
        }

        private void WylogujButton_OnClick(object sender, RoutedEventArgs e)
        {
            Global.UnsetLoggedUser();
            var login = new Login();
            login.Show();
            Close();
        }
    }
}