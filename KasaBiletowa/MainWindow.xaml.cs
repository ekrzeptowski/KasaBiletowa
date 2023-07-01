using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using KasaBiletowa.Model;
using KasaBiletowa.Utils;

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
            InitializeComponent();
        }

        private Region GetRegion(string regionName, string country)
        {
            var region = _context.Regiony.FirstOrDefault(r => r.RegionName == regionName);
            if (region == null)
            {
                region = new()
                {
                    RegionName = regionName,
                    Kraj = _context.Kraje.FirstOrDefault(k => k.Country == country) ?? new Kraj { Country = country }
                };
                _context.Regiony.Add(region);
                _context.SaveChanges();
            }

            return region;
        }

        private Kraj GetCountry(string countryName)
        {
            var country = _context.Kraje.FirstOrDefault(k => k.Country == countryName);
            if (country != null) return country;
            country = new()
            {
                Country = countryName
            };
            _context.Kraje.Add(country);
            _context.SaveChanges();

            return country;
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitDatabase(sender, e);
        }

        private void InitDatabase(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
            _context.SaveChanges();
            var stations = _context.Stacje.ToList();
            if (stations.Count == 0)
            {
                var stacje = ImportJson.ToList();
                if (stacje == null) return;
                foreach (var stacja in stacje)
                {
                    _context.Stacje.Add(new Stacja
                    {
                        City = stacja.city,
                        Country = GetCountry(stacja.country ?? string.Empty),
                        Region = GetRegion(stacja.region ?? string.Empty, stacja.country ?? string.Empty),
                        HasAnnouncements = stacja.has_announcements,
                        Ibnr = stacja.ibnr,
                        Id = stacja.id,
                        IsGroup = stacja.is_group,
                        Latitude = stacja.latitude,
                        LocalisedName = stacja.localised_name,
                        Longitude = stacja.longitude,
                        Name = stacja.name,
                        NameSlug = stacja.name_slug
                    });
                }

                _context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Baza danych jest już wypełniona");
            }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _context.Dispose();
        }
    }
}