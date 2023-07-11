using System;
using System.IO;
using KasaBiletowa.Model;
using Microsoft.EntityFrameworkCore;

namespace KasaBiletowa;

public class KasaBiletowaContext : DbContext
{
    public DbSet<Klient> Klienci { get; set; } = null!;
    public DbSet<Bilet> Bilety { get; set; } = null!;
    public DbSet<Polaczenie> Polaczenia { get; set; } = null!;
    public DbSet<Stacja> Stacje { get; set; } = null!;
    public DbSet<Kraj> Kraje { get; set; } = null!;
    public DbSet<Region> Regiony { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={Path.Combine(Directory.GetCurrentDirectory(), "KasaBiletowa.db")}");
        optionsBuilder.UseLazyLoadingProxies();
    }
}