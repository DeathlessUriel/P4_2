using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TransportApp.Dialogs;
using TransportApp.Helpers;
using TransportApp.Models;
using TransportApp.Services;

namespace TransportApp.ViewModels;

public partial class KosztyViewModel
    : ObservableObject
{
    private readonly KosztService _service;

    [ObservableProperty]
    private ObservableCollection<Koszty> koszty
        = new();

    [ObservableProperty]
    private Koszty? selectedKoszt;

    public KosztyViewModel(
        KosztService service)
    {
        _service = service;

        _ = LoadData();
    }

    public async Task LoadData()
    {
        var data = await _service.GetAllAsync();

        Koszty =
            new ObservableCollection<Koszty>(data);
    }

    [RelayCommand]
    private async Task Add()
    {
        var dialog = new KosztDialog();

        if (dialog.ShowDialog() == true)
        {
            var success =
                await _service.AddAsync(dialog.Koszt);

            if (success)
            {
                DialogHelper.Info(
                    "Dodano koszt.");

                await LoadData();
            }
        }
    }

    [RelayCommand]
    private async Task Edit()
    {
        if (SelectedKoszt == null)
        {
            DialogHelper.Info(
                "Wybierz koszt.");

            return;
        }

        var copy = new Koszty
        {
            IdKoszty = SelectedKoszt.IdKoszty,
            RodzajKosztu =
                SelectedKoszt.RodzajKosztu,
            OpisKosztu =
                SelectedKoszt.OpisKosztu,
            WartoscKosztu =
                SelectedKoszt.WartoscKosztu,
            IdPrzejazd =
                SelectedKoszt.IdPrzejazd,
            IdKursu =
                SelectedKoszt.IdKursu
        };

        var dialog = new KosztDialog(copy);

        if (dialog.ShowDialog() == true)
        {
            var success =
                await _service.UpdateAsync(
                    dialog.Koszt);

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
        if (SelectedKoszt == null)
        {
            DialogHelper.Info(
                "Wybierz koszt.");

            return;
        }

        if (!DialogHelper.Confirm(
                "Usunąć koszt?"))
            return;

        var success =
            await _service.DeleteAsync(
                SelectedKoszt);

        if (success)
        {
            DialogHelper.Info(
                "Usunięto koszt.");

            await LoadData();
        }
    }
}