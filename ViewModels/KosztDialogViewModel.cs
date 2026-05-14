using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using TransportApp.Helpers;
using TransportApp.Models;

namespace TransportApp.ViewModels;

public partial class KosztDialogViewModel
    : ObservableObject
{
    private readonly Window _window;

    [ObservableProperty]
    private Koszty koszt;

    [ObservableProperty]
    private ObservableCollection<Przejazd> przejazdy
        = new();

    [ObservableProperty]
    private ObservableCollection<KursyWalut> kursy
        = new();

    [ObservableProperty]
    private Przejazd? selectedPrzejazd;

    [ObservableProperty]
    private KursyWalut? selectedKurs;

    public KosztDialogViewModel(
        Koszty koszt,
        Window window)
    {
        Koszt = koszt;
        _window = window;

        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            using var context = new AppDbContext();

            var przejazdyDb =
                await context.Przejazd
                    .ToListAsync();

            var kursyDb =
                await context.KursyWalut
                    .Include(x => x.IdValutyNavigation)
                    .ToListAsync();

            Przejazdy =
                new ObservableCollection<Przejazd>(
                    przejazdyDb);

            Kursy =
                new ObservableCollection<KursyWalut>(
                    kursyDb);

            SelectedPrzejazd =
                przejazdyDb.FirstOrDefault(
                    x => x.IdPrzejazd ==
                    Koszt.IdPrzejazd);

            SelectedKurs =
                kursyDb.FirstOrDefault(
                    x => x.IdKursu ==
                    Koszt.IdKursu);
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
                Koszt.RodzajKosztu))
        {
            DialogHelper.Error(
                "Podaj rodzaj kosztu.");

            return;
        }

        if (Koszt.WartoscKosztu <= 0)
        {
            DialogHelper.Error(
                "Wartość musi być większa od 0.");

            return;
        }

        if (SelectedPrzejazd == null)
        {
            DialogHelper.Error(
                "Wybierz przejazd.");

            return;
        }

        Koszt.IdPrzejazd =
            SelectedPrzejazd.IdPrzejazd;

        if (SelectedKurs != null)
        {
            Koszt.IdKursu =
                SelectedKurs.IdKursu;
        }

        _window.DialogResult = true;
        _window.Close();
    }
}