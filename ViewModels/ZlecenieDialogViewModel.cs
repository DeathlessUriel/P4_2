using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using TransportApp.Helpers;
using TransportApp.Models;

namespace TransportApp.ViewModels;

public partial class ZlecenieDialogViewModel
    : ObservableObject
{
    private readonly Window _window;

    [ObservableProperty]
    private Zlecenie zlecenie;

    [ObservableProperty]
    private ObservableCollection<Adresy> adresy
        = new();
    [ObservableProperty]
    private ObservableCollection<KursyWalut> kursy
    = new();

    [ObservableProperty]
    private KursyWalut? selectedKurs;

    [ObservableProperty]
    private Adresy? selectedAdresZaladunku;

    [ObservableProperty]
    private Adresy? selectedAdresRozladunku;

    [ObservableProperty]
    private ObservableCollection<string> statusy
        = new()
        {
            "NOWE",
            "W_TRAKCIE",
            "ZAKONCZONE"
        };

    [ObservableProperty]
    private string? selectedStatus;

    public DateTime? DataPrzyjecia
    {
        get => Zlecenie.DataPrzyjecia
            .ToDateTime(TimeOnly.MinValue);

        set
        {
            if (value != null)
            {
                Zlecenie.DataPrzyjecia =
                    DateOnly.FromDateTime(
                        value.Value);
            }
        }
    }

    public DateTime? TerminZaladunku
    {
        get => Zlecenie.TerminZaladunku?
            .ToDateTime(TimeOnly.MinValue);

        set
        {
            if (value != null)
            {
                Zlecenie.TerminZaladunku =
                    DateOnly.FromDateTime(
                        value.Value);
            }
        }
    }

    public DateTime? TerminRozladunku
    {
        get => Zlecenie.TerminRozladunku?
            .ToDateTime(TimeOnly.MinValue);

        set
        {
            if (value != null)
            {
                Zlecenie.TerminRozladunku =
                    DateOnly.FromDateTime(
                        value.Value);
            }
        }
    }

    public ZlecenieDialogViewModel(
        Zlecenie zlecenie,
        Window window)
    {
        Zlecenie = zlecenie;
        _window = window;

        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            using var context = new AppDbContext();

            var adresyDb =
                await context.Adresy
                    .OrderBy(x => x.Miasto)
                    .ToListAsync();

                  var kursyDb =
                  await context.KursyWalut
                 .Include(x => x.IdValutyNavigation)
                 .OrderByDescending(x => x.Data)
                 .ToListAsync();

            Kursy =
                new ObservableCollection<KursyWalut>(
                    kursyDb);

            SelectedKurs =
                kursyDb.FirstOrDefault(
                    x => x.IdKursu ==
                    Zlecenie.IdKursu);

            Adresy =
                new ObservableCollection<Adresy>(
                    adresyDb);

            SelectedAdresZaladunku =
                adresyDb.FirstOrDefault(
                    x => x.IdAdres ==
                    Zlecenie.AdresZaladunku);

            SelectedAdresRozladunku =
                adresyDb.FirstOrDefault(
                    x => x.IdAdres ==
                    Zlecenie.AdresRozladunku);

            SelectedStatus = Zlecenie.Status;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);
        }
    }

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(
                Zlecenie.NumerZlecenia))
        {
            DialogHelper.Error(
                "Numer zlecenia jest wymagany.");

            return;
        }

        if (string.IsNullOrWhiteSpace(
                Zlecenie.NipKlienta))
        {
            DialogHelper.Error(
                "NIP jest wymagany.");

            return;
        }

        if (Zlecenie.NipKlienta.Length != 10)
        {
            DialogHelper.Error(
                "NIP musi mieć 10 cyfr.");

            return;
        }

        if (Zlecenie.Waga <= 0)
        {
            DialogHelper.Error(
                "Waga musi być większa od 0.");

            return;
        }

        if (Zlecenie.Kwota < 0)
        {
            DialogHelper.Error(
                "Kwota nie może być ujemna.");

            return;
        }

        if (SelectedAdresZaladunku == null)
        {
            DialogHelper.Error(
                "Wybierz adres załadunku.");

            return;
        }

        if (SelectedAdresRozladunku == null)
        {
            DialogHelper.Error(
                "Wybierz adres rozładunku.");

            return;
        }

        if (SelectedStatus == null)
        {
            DialogHelper.Error(
                "Wybierz status.");

            return;
        }

        Zlecenie.AdresZaladunku =
            SelectedAdresZaladunku.IdAdres;

        Zlecenie.AdresRozladunku =
            SelectedAdresRozladunku.IdAdres;

        Zlecenie.Status = SelectedStatus;

        if (SelectedKurs == null)
        {
            DialogHelper.Error(
                "Wybierz kurs waluty.");

            return;
        }

        Zlecenie.IdKursu =
            SelectedKurs.IdKursu;

        _window.DialogResult = true;
        _window.Close();
    }
}