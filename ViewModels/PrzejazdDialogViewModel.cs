using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using TransportApp.Helpers;
using TransportApp.Models;
using TransportApp.Services;

namespace TransportApp.ViewModels;

public partial class PrzejazdDialogViewModel
    : ObservableObject
{
    private readonly Window _window;

    [ObservableProperty]
    private Przejazd przejazd;

    [ObservableProperty]
    private ObservableCollection<Kierowca> kierowcy
        = new();

    [ObservableProperty]
    private ObservableCollection<Pojazd> pojazdy
        = new();

    [ObservableProperty]
    private ObservableCollection<Adresy> adresy
        = new();

    [ObservableProperty]
    private Kierowca? selectedKierowca;

    [ObservableProperty]
    private Pojazd? selectedPojazd;

    [ObservableProperty]
    private Adresy? selectedAdresStart;

    [ObservableProperty]
    private Adresy? selectedAdresStop;

    public DateTime DataRozpoczecia
    {
        get => Przejazd.DataRozpoczecia;
        set => Przejazd.DataRozpoczecia = value;
    }

    public DateTime DataZakonczenia
    {
        get => Przejazd.DataZakonczenia;
        set => Przejazd.DataZakonczenia = value;
    }

    public PrzejazdDialogViewModel(
        Przejazd przejazd,
        Window window)
    {
        Przejazd = przejazd;
        _window = window;

        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            using var context = new AppDbContext();

            Kierowcy =
                new ObservableCollection<Kierowca>(
                    await context.Kierowca.ToListAsync());

            Pojazdy =
                new ObservableCollection<Pojazd>(
                    await context.Pojazd.ToListAsync());

            Adresy =
                new ObservableCollection<Adresy>(
                    await context.Adresy.ToListAsync());
            SelectedKierowca =
                Kierowcy.FirstOrDefault(
                    x => x.IdKierowca ==
                    Przejazd.IdKierowca);

            SelectedPojazd =
                Pojazdy.FirstOrDefault(
                    x => x.IdPojazd ==
                    Przejazd.IdPojazd);

            SelectedAdresStart =
                Adresy.FirstOrDefault(
                    x => x.IdAdres ==
                    Przejazd.AdresStartu);

            SelectedAdresStop =
                Adresy.FirstOrDefault(
                    x => x.IdAdres ==
                    Przejazd.AdresDocelowy);
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        if (SelectedKierowca == null)
        {
            DialogHelper.Error(
                "Wybierz kierowcę.");

            return;
        }

        if (SelectedPojazd == null)
        {
            DialogHelper.Error(
                "Wybierz pojazd.");

            return;
        }

        if (SelectedAdresStart == null)
        {
            DialogHelper.Error(
                "Wybierz adres startu.");

            return;
        }

        if (SelectedAdresStop == null)
        {
            DialogHelper.Error(
                "Wybierz adres docelowy.");

            return;
        }

        if (Przejazd.StanLicznikaStop <
            Przejazd.StanLicznikaStart)
        {
            DialogHelper.Error(
                "Stan licznika stop nie może być mniejszy.");

            return;
        }

        var service = App.ServiceProvider
            .GetRequiredService<PrzejazdService>();

        var dostepny =
            await service.CzyPojazdDostepny(
                SelectedPojazd.IdPojazd,
                DataRozpoczecia,
                DataZakonczenia);

        if (!dostepny &&
            Przejazd.IdPrzejazd == 0)
        {
            DialogHelper.Error(
                "Pojazd jest zajęty w tym terminie.");

            return;
        }

        Przejazd.IdKierowca =
            SelectedKierowca.IdKierowca;

        Przejazd.IdPojazd =
            SelectedPojazd.IdPojazd;

        Przejazd.AdresStartu =
            SelectedAdresStart.IdAdres;

        Przejazd.AdresDocelowy =
            SelectedAdresStop.IdAdres;

        _window.DialogResult = true;
        _window.Close();
    }
}