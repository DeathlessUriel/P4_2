using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

public partial class AppDbContext : DbContext
{
    

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adresy> Adresy { get; set; }

    public virtual DbSet<Kierowca> Kierowca { get; set; }

    public virtual DbSet<Koszty> Koszty { get; set; }

    public virtual DbSet<KursyWalut> KursyWalut { get; set; }

    public virtual DbSet<ModelePojazdow> ModelePojazdow { get; set; }

    public virtual DbSet<Pojazd> Pojazd { get; set; }

    public virtual DbSet<Przejazd> Przejazd { get; set; }

    public virtual DbSet<Waluty> Waluty { get; set; }

    public virtual DbSet<WidokHistoriaKierowcy> WidokHistoriaKierowcy { get; set; }

    public virtual DbSet<WidokKosztPrzejazdu> WidokKosztPrzejazdu { get; set; }

    public virtual DbSet<WidokKosztZlecenium> WidokKosztZlecenium { get; set; }

    public virtual DbSet<Zlecenie> Zlecenie { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adresy>(entity =>
        {
            entity.HasKey(e => e.IdAdres).HasName("PK__Adresy__6C3F73B1F1D2E132");
        });

        modelBuilder.Entity<Kierowca>(entity =>
        {
            entity.HasKey(e => e.IdKierowca).HasName("PK__Kierowca__0AD814562EB9DF35");
        });

        modelBuilder.Entity<Koszty>(entity =>
        {
            entity.HasKey(e => e.IdKoszty).HasName("PK__Koszty__D54FB7ABC29B3129");

            entity.HasOne(d => d.IdKursuNavigation).WithMany(p => p.Koszties).HasConstraintName("FK_Koszt_Kurs");

            entity.HasOne(d => d.IdPrzejazdNavigation).WithMany(p => p.Koszties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Koszt_Przejazd");
        });

        modelBuilder.Entity<KursyWalut>(entity =>
        {
            entity.HasKey(e => e.IdKursu).HasName("PK__Kursy_Wa__D6642112D1BB602B");

            entity.HasOne(d => d.IdValutyNavigation).WithMany(p => p.KursyWaluts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kurs_Valuta");
        });

        modelBuilder.Entity<ModelePojazdow>(entity =>
        {
            entity.HasKey(e => e.IdModelu).HasName("PK__Modele_P__813C237CD15252D8");
        });

        modelBuilder.Entity<Pojazd>(entity =>
        {
            entity.HasKey(e => e.IdPojazd).HasName("PK__Pojazd__322D742F382ED778");

            entity.HasOne(d => d.IdModeluNavigation).WithMany(p => p.Pojazds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pojazd_Model");
        });

        modelBuilder.Entity<Przejazd>(entity =>
        {
            entity.HasKey(e => e.IdPrzejazd).HasName("PK__Przejazd__589BA8F4B766FFA5");

            entity.HasOne(d => d.AdresDocelowyNavigation).WithMany(p => p.PrzejazdAdresDocelowyNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Przejazd_Adres_Stop");

            entity.HasOne(d => d.AdresStartuNavigation).WithMany(p => p.PrzejazdAdresStartuNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Przejazd_Adres_Start");

            entity.HasOne(d => d.IdKierowcaNavigation).WithMany(p => p.Przejazds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Przejazd_Kierowca");

            entity.HasOne(d => d.IdPojazdNavigation).WithMany(p => p.Przejazds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Przejazd_Pojazd");
        });

        modelBuilder.Entity<Waluty>(entity =>
        {
            entity.HasKey(e => e.IdValuty).HasName("PK__Waluty__6E14EB4DF82BF093");

            entity.Property(e => e.KodIso).IsFixedLength();
        });

        modelBuilder.Entity<WidokHistoriaKierowcy>(entity =>
        {
            entity.ToView("Widok_Historia_Kierowcy");
        });

        modelBuilder.Entity<WidokKosztPrzejazdu>(entity =>
        {
            entity.ToView("Widok_Koszt_Przejazdu");
        });

        modelBuilder.Entity<WidokKosztZlecenium>(entity =>
        {
            entity.ToView("Widok_Koszt_Zlecenia");
        });

        modelBuilder.Entity<Zlecenie>(entity =>
        {
            entity.HasKey(e => e.IdZlecenie).HasName("PK__Zlecenie__99AAD0906B9DDD8C");

            entity.Property(e => e.NipKlienta).IsFixedLength();

            entity.HasOne(d => d.AdresRozladunkuNavigation).WithMany(p => p.ZlecenieAdresRozladunkuNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zlecenie_Adres_R");

            entity.HasOne(d => d.AdresZaladunkuNavigation).WithMany(p => p.ZlecenieAdresZaladunkuNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zlecenie_Adres_Z");

            entity.HasOne(d => d.IdKursuNavigation).WithMany(p => p.Zlecenies).HasConstraintName("FK_Zlecenie_Kurs");

            entity.HasMany(d => d.IdPrzejazds).WithMany(p => p.IdZlecenies)
                .UsingEntity<Dictionary<string, object>>(
                    "ZleceniePrzejazd",
                    r => r.HasOne<Przejazd>().WithMany()
                        .HasForeignKey("IdPrzejazd")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ZP_Przejazd"),
                    l => l.HasOne<Zlecenie>().WithMany()
                        .HasForeignKey("IdZlecenie")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ZP_Zlecenie"),
                    j =>
                    {
                        j.HasKey("IdZlecenie", "IdPrzejazd").HasName("PK_ZP");
                        j.ToTable("Zlecenie_Przejazd");
                        j.IndexerProperty<int>("IdZlecenie").HasColumnName("ID_Zlecenie");
                        j.IndexerProperty<int>("IdPrzejazd").HasColumnName("ID_Przejazd");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
