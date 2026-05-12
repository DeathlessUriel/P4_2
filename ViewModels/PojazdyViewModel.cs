using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TransportApp.Dialogs;
using TransportApp.Helpers;
using TransportApp.Models;
using TransportApp.Services;

namespace TransportApp.ViewModels;

public partial class PojazdyViewModel : ObservableObject
{
    private readonly PojazdService _service;

    [ObservableProperty]
    private ObservableCollection<Pojazd> pojazdy = new();

    [ObservableProperty]
    private Pojazd? selectedPojazd;

    public PojazdyViewModel(PojazdService service)
    {
        _service = service;

        _ = LoadData();
    }

    public async Task LoadData()
    {
        var data = await _service.GetAllAsync();

        Pojazdy = new ObservableCollection<Pojazd>(data);
    }

    [RelayCommand]
    private async Task Add()
    {
        var dialog = new PojazdDialog();

        if (dialog.ShowDialog() == true)
        {
            var success = await _service.AddAsync(dialog.Pojazd);

            if (success)
            {
                DialogHelper.Info("Dodano pojazd.");

                await LoadData();
            }
        }
    }

    [RelayCommand]
    private async Task Edit()
    {
        if (SelectedPojazd == null)
        {
            DialogHelper.Info("Wybierz pojazd.");
            return;
        }

        var copy = new Pojazd
        {
            IdPojazd = SelectedPojazd.IdPojazd,
            NumerRejestracyjny = SelectedPojazd.NumerRejestracyjny,
            RokProdukcji = SelectedPojazd.RokProdukcji,
            DopuszczalnaLadownosc = SelectedPojazd.DopuszczalnaLadownosc,
            IdModelu = SelectedPojazd.IdModelu
        };

        var dialog = new PojazdDialog(copy);

        if (dialog.ShowDialog() == true)
        {
            var success = await _service.UpdateAsync(dialog.Pojazd);

            if (success)
            {
                DialogHelper.Info("Zapisano zmiany.");

                await LoadData();
            }
        }
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedPojazd == null)
        {
            DialogHelper.Info("Wybierz pojazd.");
            return;
        }

        if (!DialogHelper.Confirm(
                "Czy na pewno usunąć pojazd?"))
            return;

        var success = await _service.DeleteAsync(SelectedPojazd);

        if (success)
        {
            DialogHelper.Info("Usunięto pojazd.");

            await LoadData();
        }
    }
}