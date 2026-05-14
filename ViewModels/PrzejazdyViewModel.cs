using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TransportApp.Dialogs;
using TransportApp.Helpers;
using TransportApp.Models;
using TransportApp.Services;

namespace TransportApp.ViewModels;

public partial class PrzejazdyViewModel
    : ObservableObject
{
    private readonly PrzejazdService _service;

    [ObservableProperty]
    private ObservableCollection<Przejazd> przejazdy
        = new();

    [ObservableProperty]
    private Przejazd? selectedPrzejazd;

    public PrzejazdyViewModel(
        PrzejazdService service)
    {
        _service = service;

        _ = LoadData();
    }

    public async Task LoadData()
    {
        var data = await _service.GetAllAsync();

        Przejazdy =
            new ObservableCollection<Przejazd>(data);
    }

    [RelayCommand]
    private async Task Add()
    {
        var dialog = new PrzejazdDialog();

        if (dialog.ShowDialog() == true)
        {
            var success =
                await _service.AddAsync(
                    dialog.Przejazd);

            if (success)
            {
                DialogHelper.Info(
                    "Dodano przejazd.");

                await LoadData();
            }
        }
    }

    [RelayCommand]
    private async Task Edit()
    {
        if (SelectedPrzejazd == null)
        {
            DialogHelper.Info(
                "Wybierz przejazd.");

            return;
        }

        var copy = new Przejazd
        {
            IdPrzejazd =
                SelectedPrzejazd.IdPrzejazd,

            DataRozpoczecia =
                SelectedPrzejazd.DataRozpoczecia,

            DataZakonczenia =
                SelectedPrzejazd.DataZakonczenia,

            AdresStartu =
                SelectedPrzejazd.AdresStartu,

            AdresDocelowy =
                SelectedPrzejazd.AdresDocelowy,

            StanLicznikaStart =
                SelectedPrzejazd.StanLicznikaStart,

            StanLicznikaStop =
                SelectedPrzejazd.StanLicznikaStop,

            IdPojazd =
                SelectedPrzejazd.IdPojazd,

            IdKierowca =
                SelectedPrzejazd.IdKierowca
        };

        var dialog = new PrzejazdDialog(copy);

        if (dialog.ShowDialog() == true)
        {
            var success =
                await _service.UpdateAsync(
                    dialog.Przejazd);

            if (success)
            {
                DialogHelper.Info(
                    "Zapisano zmiany.");

                await LoadData();
            }
        }
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedPrzejazd == null)
        {
            DialogHelper.Info(
                "Wybierz przejazd.");

            return;
        }

        if (!DialogHelper.Confirm(
                "Usunąć przejazd?"))
            return;

        var success =
            await _service.DeleteAsync(
                SelectedPrzejazd);

        if (success)
        {
            DialogHelper.Info(
                "Usunięto przejazd.");

            await LoadData();
        }
    }
    [RelayCommand]
    private async Task PokazZysk()
    {
        if (SelectedPrzejazd == null)
        {
            DialogHelper.Info(
                "Wybierz przejazd.");

            return;
        }

        var zysk =
            await _service.PobierzZyskAsync(
                SelectedPrzejazd.IdPrzejazd);

        DialogHelper.Info(
            $"Zysk przejazdu: {zysk:N2} PLN");
    }
}