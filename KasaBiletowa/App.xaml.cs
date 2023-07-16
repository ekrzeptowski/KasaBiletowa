using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using KasaBiletowa.Model;
using KasaBiletowa.Utils;

namespace KasaBiletowa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly KasaBiletowaContext _context = new();

        private Region GetRegion(string regionName, string country)
        {
            var region = _context.Regiony.FirstOrDefault(r => r.RegionName == regionName);
            if (region != null) return region;
            region = new()
            {
                RegionName = regionName,
                Kraj = _context.Kraje.FirstOrDefault(k => k.Country == country) ?? new Kraj { Country = country }
            };
            _context.Regiony.Add(region);
            _context.SaveChanges();

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

        private void InitDatabase()
        {
            _context.Database.EnsureCreated();
            _context.SaveChanges();
            int stationsCount = _context.Stacje.Count();
            if (stationsCount != 0) return;
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

        // add random connections for the next 14 days, 100-150 per day, calculate travel time from stacja a to stacja b and add it to dataodjazdu
        private void InitConnections()
        {
            var lastConnection = _context.Polaczenia.OrderByDescending(p => p.DataOdjazdu).FirstOrDefault();
            if (lastConnection != null && lastConnection.DataOdjazdu < DateTime.Now.AddDays(13)) return;

            var random = new Random();
            var stacje = _context.Stacje.ToList();
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < random.Next(100, 150); j++)
                {
                    var dataOdjazdu = (lastConnection?.DataOdjazdu.Date ?? DateTime.Now).AddDays(i)
                        .AddHours(random.Next(0, 24))
                        .AddMinutes(random.Next(0, 60));
                    var stacjaPoczatkowa = stacje[random.Next(0, stacje.Count)];
                    var stacjaKoncowa = stacje[random.Next(0, stacje.Count)];
                    var connection = new Polaczenie
                    {
                        DataOdjazdu = dataOdjazdu,
                        DataPrzyjazdu = dataOdjazdu.AddMinutes(stacjaPoczatkowa.Odleglosc(stacjaKoncowa) /
                            (double)random.Next(40, 100) * 60),
                        StacjaPoczatkowa = stacjaPoczatkowa,
                        StacjaKoncowa = stacjaKoncowa,
                        Cena = (int)stacjaPoczatkowa.Odleglosc(stacjaKoncowa) / 2
                    };
                    _context.Polaczenia.Add(connection);
                }
            }

            _context.SaveChanges();
        }

        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            InitDatabase();
            InitConnections();
            if (!_context.Klienci.Any())
            {
                Register register = new();
                register.Show();
            }
            else
            {
                Login login = new();
                login.Show();
            }
        }
    }
}