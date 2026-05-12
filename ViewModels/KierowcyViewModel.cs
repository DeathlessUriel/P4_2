using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TransportApp.Dialogs;
using TransportApp.Helpers;
using TransportApp.Models;
using TransportApp.Services;

namespace TransportApp.ViewModels;

public partial class KierowcyViewModel : ObservableObject
{
    private readonly KierowcaService _service;

    [ObservableProperty]
    private ObservableCollection<Kierowca> kierowcy = new();

    [ObservableProperty]
    private Kierowca? selectedKierowca;

    public KierowcyViewModel(
        KierowcaService service)
    {
        _service = service;

        _ = LoadData();
    }

    public async Task LoadData()
    {
        var data = await _service.GetAllAsync();

        Kierowcy =
            new ObservableCollection<Kierowca>(data);
    }

    [RelayCommand]
    private async Task Add()
    {
        var dialog = new KierowcaDialog();

        if (dialog.ShowDialog() == true)
        {
            var success =
                await _service.AddAsync(dialog.Kierowca);

            if (success)
            {
                DialogHelper.Info(
                    "Dodano kierowcę.");

                await LoadData();
            }
        }
    }

    [RelayCommand]
    private async Task Edit()
    {
        if (SelectedKierowca == null)
        {
            DialogHelper.Info(
                "Wybierz kierowcę.");

            return;
        }

        var copy = new Kierowca
        {
            IdKierowca = SelectedKierowca.IdKierowca,
            Imie = SelectedKierowca.Imie,
            Nazwisko = SelectedKierowca.Nazwisko,
            NumerPrawaJazdy =
                SelectedKierowca.NumerPrawaJazdy,
            TelefonSluzbowy =
                SelectedKierowca.TelefonSluzbowy,
            NumerKartyKierowcy =
                SelectedKierowca.NumerKartyKierowcy,
            DataZatrudnienia =
                SelectedKierowca.DataZatrudnienia
        };

        var dialog = new KierowcaDialog(copy);

        if (dialog.ShowDialog() == true)
        {
            var success =
                await _service.UpdateAsync(dialog.Kierowca);

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
        if (SelectedKierowca == null)
        {
            DialogHelper.Info(
                "Wybierz kierowcę.");

            return;
        }

        if (!DialogHelper.Confirm(
                "Czy usunąć kierowcę?"))
            return;

        var success =
            await _service.DeleteAsync(
                SelectedKierowca);

        if (success)
        {
            DialogHelper.Info(
                "Usunięto kierowcę.");

            await LoadData();
        }
    }
}