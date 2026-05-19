using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

public partial class TransportContext : DbContext
{
    public TransportContext()
    {
    }

    public TransportContext(DbContextOptions<TransportContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adresy> Adresies { get; set; }

    public virtual DbSet<Kierowca> Kierowcas { get; set; }

    public virtual DbSet<Koszty> Koszties { get; set; }

    public virtual DbSet<KursyWalut> KursyWaluts { get; set; }

    public virtual DbSet<ModelePojazdow> ModelePojazdows { get; set; }

    public virtual DbSet<Pojazd> Pojazds { get; set; }

    public virtual DbSet<Przejazd> Przejazds { get; set; }

    public virtual DbSet<Waluty> Waluties { get; set; }

    public virtual DbSet<WidokHistoriaKierowcy> WidokHistoriaKierowcies { get; set; }

    public virtual DbSet<WidokKosztPrzejazdu> WidokKosztPrzejazdus { get; set; }

    public virtual DbSet<WidokKosztZlecenium> WidokKosztZlecenia { get; set; }

    public virtual DbSet<WidokZyskPrzejazdu> WidokZyskPrzejazdus { get; set; }

    public virtual DbSet<ZdjeciaPojazdu> ZdjeciaPojazdus { get; set; }

    public virtual DbSet<Zlecenie> Zlecenies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=Transport;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adresy>(entity =>
        {
            entity.HasKey(e => e.IdAdres).HasName("PK__Adresy__6C3F73B1F1D2E132");

            entity.ToTable("Adresy");

            entity.Property(e => e.IdAdres).HasColumnName("ID_Adres");
            entity.Property(e => e.Kraj)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Miasto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumerDomu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Numer_domu");
            entity.Property(e => e.Ulica)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Kierowca>(entity =>
        {
            entity.HasKey(e => e.IdKierowca).HasName("PK__Kierowca__0AD814562EB9DF35");

            entity.ToTable("Kierowca");

            entity.HasIndex(e => e.NumerPrawaJazdy, "UQ__Kierowca__84392B56FFA0FE22").IsUnique();

            entity.HasIndex(e => e.NumerKartyKierowcy, "UQ__Kierowca__FB107E3CCF12D1B6").IsUnique();

            entity.Property(e => e.IdKierowca).HasColumnName("ID_Kierowca");
            entity.Property(e => e.DataZatrudnienia).HasColumnName("Data_zatrudnienia");
            entity.Property(e => e.Imie)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nazwisko)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumerKartyKierowcy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Numer_karty_kierowcy");
            entity.Property(e => e.NumerPrawaJazdy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Numer_prawa_jazdy");
            entity.Property(e => e.TelefonSluzbowy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Telefon_sluzbowy");
        });

        modelBuilder.Entity<Koszty>(entity =>
        {
            entity.HasKey(e => e.IdKoszty).HasName("PK__Koszty__D54FB7ABC29B3129");

            entity.ToTable("Koszty");

            entity.Property(e => e.IdKoszty).HasColumnName("ID_Koszty");
            entity.Property(e => e.IdKursu).HasColumnName("ID_Kursu");
            entity.Property(e => e.IdPrzejazd).HasColumnName("ID_Przejazd");
            entity.Property(e => e.OpisKosztu)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Opis_kosztu");
            entity.Property(e => e.RodzajKosztu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Rodzaj_kosztu");
            entity.Property(e => e.WartoscKosztu)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Wartosc_kosztu");

            entity.HasOne(d => d.IdKursuNavigation).WithMany(p => p.Koszties)
                .HasForeignKey(d => d.IdKursu)
                .HasConstraintName("FK_Koszt_Kurs");

            entity.HasOne(d => d.IdPrzejazdNavigation).WithMany(p => p.Koszties)
                .HasForeignKey(d => d.IdPrzejazd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Koszt_Przejazd");
        });

        modelBuilder.Entity<KursyWalut>(entity =>
        {
            entity.HasKey(e => e.IdKursu).HasName("PK__Kursy_Wa__D6642112D1BB602B");

            entity.ToTable("Kursy_Walut");

            entity.HasIndex(e => new { e.IdValuty, e.Data }, "UQ_Kurs").IsUnique();

            entity.Property(e => e.IdKursu).HasColumnName("ID_Kursu");
            entity.Property(e => e.IdValuty).HasColumnName("ID_Valuty");
            entity.Property(e => e.WartoscKursu)
                .HasColumnType("decimal(10, 4)")
                .HasColumnName("Wartosc_kursu");

            entity.HasOne(d => d.IdValutyNavigation).WithMany(p => p.KursyWaluts)
                .HasForeignKey(d => d.IdValuty)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kurs_Valuta");
        });

        modelBuilder.Entity<ModelePojazdow>(entity =>
        {
            entity.HasKey(e => e.IdModelu).HasName("PK__Modele_P__813C237CD15252D8");

            entity.ToTable("Modele_Pojazdow");

            entity.HasIndex(e => new { e.Marka, e.Model }, "UQ_Marka_Model").IsUnique();

            entity.Property(e => e.IdModelu).HasColumnName("ID_Modelu");
            entity.Property(e => e.Marka)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pojazd>(entity =>
        {
            entity.HasKey(e => e.IdPojazd).HasName("PK__Pojazd__322D742F382ED778");

            entity.ToTable("Pojazd");

            entity.HasIndex(e => e.NumerRejestracyjny, "UQ__Pojazd__581C138883747DF5").IsUnique();

            entity.Property(e => e.IdPojazd).HasColumnName("ID_Pojazd");
            entity.Property(e => e.DataPrzegladu).HasColumnName("Data_przegladu");
            entity.Property(e => e.DataUbezpieczenia).HasColumnName("Data_ubezpieczenia");
            entity.Property(e => e.DopuszczalnaLadownosc)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Dopuszczalna_ladownosc");
            entity.Property(e => e.IdModelu).HasColumnName("ID_Modelu");
            entity.Property(e => e.NumerRejestracyjny)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Numer_rejestracyjny");
            entity.Property(e => e.PojemnoscLdm)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Pojemnosc_LDM");
            entity.Property(e => e.RokProdukcji).HasColumnName("Rok_produkcji");

            entity.HasOne(d => d.IdModeluNavigation).WithMany(p => p.Pojazds)
                .HasForeignKey(d => d.IdModelu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pojazd_Model");
        });

        modelBuilder.Entity<Przejazd>(entity =>
        {
            entity.HasKey(e => e.IdPrzejazd).HasName("PK__Przejazd__589BA8F4B766FFA5");

            entity.ToTable("Przejazd");

            entity.Property(e => e.IdPrzejazd).HasColumnName("ID_Przejazd");
            entity.Property(e => e.AdresDocelowy).HasColumnName("Adres_docelowy");
            entity.Property(e => e.AdresStartu).HasColumnName("Adres_startu");
            entity.Property(e => e.DataRozpoczecia)
                .HasColumnType("datetime")
                .HasColumnName("Data_rozpoczecia");
            entity.Property(e => e.DataZakonczenia)
                .HasColumnType("datetime")
                .HasColumnName("Data_zakonczenia");
            entity.Property(e => e.IdKierowca).HasColumnName("ID_Kierowca");
            entity.Property(e => e.IdPojazd).HasColumnName("ID_Pojazd");
            entity.Property(e => e.StanLicznikaStart).HasColumnName("Stan_licznika_start");
            entity.Property(e => e.StanLicznikaStop).HasColumnName("Stan_licznika_stop");

            entity.HasOne(d => d.AdresDocelowyNavigation).WithMany(p => p.PrzejazdAdresDocelowyNavigations)
                .HasForeignKey(d => d.AdresDocelowy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Przejazd_Adres_Stop");

            entity.HasOne(d => d.AdresStartuNavigation).WithMany(p => p.PrzejazdAdresStartuNavigations)
                .HasForeignKey(d => d.AdresStartu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Przejazd_Adres_Start");

            entity.HasOne(d => d.IdKierowcaNavigation).WithMany(p => p.Przejazds)
                .HasForeignKey(d => d.IdKierowca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Przejazd_Kierowca");

            entity.HasOne(d => d.IdPojazdNavigation).WithMany(p => p.Przejazds)
                .HasForeignKey(d => d.IdPojazd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Przejazd_Pojazd");
        });

        modelBuilder.Entity<Waluty>(entity =>
        {
            entity.HasKey(e => e.IdValuty).HasName("PK__Waluty__6E14EB4DF82BF093");

            entity.ToTable("Waluty");

            entity.HasIndex(e => e.KodIso, "UQ__Waluty__1F4DE9F36C74FBFC").IsUnique();

            entity.Property(e => e.IdValuty).HasColumnName("ID_Valuty");
            entity.Property(e => e.KodIso)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Kod_ISO");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WidokHistoriaKierowcy>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Widok_Historia_Kierowcy");

            entity.Property(e => e.DataRozpoczecia)
                .HasColumnType("datetime")
                .HasColumnName("Data_rozpoczecia");
            entity.Property(e => e.DataZakonczenia)
                .HasColumnType("datetime")
                .HasColumnName("Data_zakonczenia");
            entity.Property(e => e.IdKierowca).HasColumnName("ID_Kierowca");
            entity.Property(e => e.IdPrzejazd).HasColumnName("ID_Przejazd");
            entity.Property(e => e.Imie)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nazwisko)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WidokKosztPrzejazdu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Widok_Koszt_Przejazdu");

            entity.Property(e => e.IdPrzejazd).HasColumnName("ID_Przejazd");
            entity.Property(e => e.SumaKosztow)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Suma_kosztow");
        });

        modelBuilder.Entity<WidokKosztZlecenium>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Widok_Koszt_Zlecenia");

            entity.Property(e => e.IdZlecenie).HasColumnName("ID_Zlecenie");
            entity.Property(e => e.SumaKosztow)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Suma_kosztow");
        });

        modelBuilder.Entity<WidokZyskPrzejazdu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Widok_Zysk_Przejazdu");

            entity.Property(e => e.IdPrzejazd).HasColumnName("ID_Przejazd");
            entity.Property(e => e.Zysk).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<ZdjeciaPojazdu>(entity =>
        {
            entity.HasKey(e => e.IdZdjecia).HasName("PK__ZdjeciaP__B06BCE3B494BC141");

            entity.ToTable("ZdjeciaPojazdu");

            entity.Property(e => e.IdZdjecia).HasColumnName("ID_Zdjecia");
            entity.Property(e => e.IdPojazd).HasColumnName("ID_Pojazd");
            entity.Property(e => e.Sciezka).HasMaxLength(500);

            entity.HasOne(d => d.IdPojazdNavigation).WithMany(p => p.ZdjeciaPojazdus)
                .HasForeignKey(d => d.IdPojazd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zdjecie_Pojazd");
        });

        modelBuilder.Entity<Zlecenie>(entity =>
        {
            entity.HasKey(e => e.IdZlecenie).HasName("PK__Zlecenie__99AAD0906B9DDD8C");

            entity.ToTable("Zlecenie");

            entity.HasIndex(e => e.NumerZlecenia, "UQ__Zlecenie__73693E0EE731BF07").IsUnique();

            entity.Property(e => e.IdZlecenie).HasColumnName("ID_Zlecenie");
            entity.Property(e => e.AdresRozladunku).HasColumnName("Adres_Rozladunku");
            entity.Property(e => e.AdresZaladunku).HasColumnName("Adres_Zaladunku");
            entity.Property(e => e.DataPrzyjecia).HasColumnName("Data_przyjecia");
            entity.Property(e => e.IdKursu).HasColumnName("ID_Kursu");
            entity.Property(e => e.Kwota).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Ldm)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("LDM");
            entity.Property(e => e.NipKlienta)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NIP_Klienta");
            entity.Property(e => e.NumerZlecenia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Numer_zlecenia");
            entity.Property(e => e.OpisTransportu)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Opis_transportu");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TerminRozladunku).HasColumnName("Termin_rozladunku");
            entity.Property(e => e.TerminZaladunku).HasColumnName("Termin_zaladunku");
            entity.Property(e => e.Waga).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.AdresRozladunkuNavigation).WithMany(p => p.ZlecenieAdresRozladunkuNavigations)
                .HasForeignKey(d => d.AdresRozladunku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zlecenie_Adres_R");

            entity.HasOne(d => d.AdresZaladunkuNavigation).WithMany(p => p.ZlecenieAdresZaladunkuNavigations)
                .HasForeignKey(d => d.AdresZaladunku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zlecenie_Adres_Z");

            entity.HasOne(d => d.IdKursuNavigation).WithMany(p => p.Zlecenies)
                .HasForeignKey(d => d.IdKursu)
                .HasConstraintName("FK_Zlecenie_Kurs");

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
