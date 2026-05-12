using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TransportApp.Dialogs;
using TransportApp.Helpers;
using TransportApp.Models;
using TransportApp.Services;

namespace TransportApp.ViewModels;

public partial class ZleceniaViewModel
    : ObservableObject
{
    private readonly ZlecenieService _service;

    [ObservableProperty]
    private ObservableCollection<Zlecenie> zlecenia
        = new();

    [ObservableProperty]
    private Zlecenie? selectedZlecenie;

    public ZleceniaViewModel(
        ZlecenieService service)
    {
        _service = service;

        _ = LoadData();
    }

    public async Task LoadData()
    {
        var data = await _service.GetAllAsync();

        Zlecenia =
            new ObservableCollection<Zlecenie>(data);
    }

    [RelayCommand]
    private async Task Add()
    {
        var dialog = new ZlecenieDialog();

        if (dialog.ShowDialog() == true)
        {
            var success =
                await _service.AddAsync(
                    dialog.Zlecenie);

            if (success)
            {
                DialogHelper.Info(
                    "Dodano zlecenie.");

                await LoadData();
            }
        }
    }

    [RelayCommand]
    private async Task Edit()
    {
        if (SelectedZlecenie == null)
        {
            DialogHelper.Info(
                "Wybierz zlecenie.");

            return;
        }

        var copy = new Zlecenie
        {
            IdZlecenie = SelectedZlecenie.IdZlecenie,
            NumerZlecenia =
                SelectedZlecenie.NumerZlecenia,
            NipKlienta =
                SelectedZlecenie.NipKlienta,
            OpisTransportu =
                SelectedZlecenie.OpisTransportu,
            Waga = SelectedZlecenie.Waga,
            Kwota = SelectedZlecenie.Kwota,
            Ldm = SelectedZlecenie.Ldm,
            Status = SelectedZlecenie.Status,
            DataPrzyjecia =
                SelectedZlecenie.DataPrzyjecia,
            TerminZaladunku =
                SelectedZlecenie.TerminZaladunku,
            TerminRozladunku =
                SelectedZlecenie.TerminRozladunku,
            AdresRozladunku =
                SelectedZlecenie.AdresRozladunku,
            AdresZaladunku =
                SelectedZlecenie.AdresZaladunku
        };

        var dialog = new ZlecenieDialog(copy);

        if (dialog.ShowDialog() == true)
        {
            var success =
                await _service.UpdateAsync(
                    dialog.Zlecenie);

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
        if (SelectedZlecenie == null)
        {
            DialogHelper.Info(
                "Wybierz zlecenie.");

            return;
        }

        if (!DialogHelper.Confirm(
                "Usunąć zlecenie?"))
            return;

        var success =
            await _service.DeleteAsync(
                SelectedZlecenie);

        if (success)
        {
            DialogHelper.Info(
                "Usunięto zlecenie.");

            await LoadData();
        }
    }

    [RelayCommand]
    private async Task PokazKoszt()
    {
        if (SelectedZlecenie == null)
        {
            DialogHelper.Info(
                "Wybierz zlecenie.");

            return;
        }

        var koszt =
            await _service.GetKosztAsync(
                SelectedZlecenie.IdZlecenie);

        DialogHelper.Info(
            $"Koszt zlecenia: {koszt:N2} PLN");
    }
}