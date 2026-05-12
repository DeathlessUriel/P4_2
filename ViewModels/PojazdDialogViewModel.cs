using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using TransportApp.Helpers;
using TransportApp.Models;

namespace TransportApp.ViewModels;

public partial class PojazdDialogViewModel : ObservableObject
{
    private readonly Window _window;

    [ObservableProperty]
    private Pojazd pojazd;

    [ObservableProperty]
    private ObservableCollection<ModelePojazdow> modele = new();

    [ObservableProperty]
    private ModelePojazdow? selectedModel;

    public DateTime? DataPrzegladu
    {
        get => Pojazd.DataPrzegladu?.ToDateTime(TimeOnly.MinValue);
        set
        {
            if (value != null)
            {
                Pojazd.DataPrzegladu =
                    DateOnly.FromDateTime(value.Value);
            }
        }
    }

    public DateTime? DataUbezpieczenia
    {
        get => Pojazd.DataUbezpieczenia?.ToDateTime(TimeOnly.MinValue);
        set
        {
            if (value != null)
            {
                Pojazd.DataUbezpieczenia =
                    DateOnly.FromDateTime(value.Value);
            }
        }
    }

    public PojazdDialogViewModel(
        Pojazd pojazd,
        Window window)
    {
        Pojazd = pojazd;
        _window = window;

        LoadModels();
    }

    private async void LoadModels()
    {
        try
        {
            using var context = new AppDbContext();

            var data = await context.ModelePojazdow
                .ToListAsync();

            Modele = new ObservableCollection<ModelePojazdow>(data);

            SelectedModel = data
                .FirstOrDefault(x => x.IdModelu == Pojazd.IdModelu);
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
                Pojazd.NumerRejestracyjny))
        {
            DialogHelper.Error(
                "Numer rejestracyjny jest wymagany.");

            return;
        }

        if (Pojazd.RokProdukcji > DateTime.Now.Year)
        {
            DialogHelper.Error(
                "Niepoprawny rok produkcji.");

            return;
        }

        if (Pojazd.DopuszczalnaLadownosc <= 0)
        {
            DialogHelper.Error(
                "Ładowność musi być większa od 0.");

            return;
        }

        if (Pojazd.PojemnoscLdm <= 0)
        {
            DialogHelper.Error(
                "LDM musi być większe od 0.");

            return;
        }

        if (SelectedModel == null)
        {
            DialogHelper.Error(
                "Wybierz model pojazdu.");

            return;
        }

        Pojazd.IdModelu = SelectedModel.IdModelu;

        _window.DialogResult = true;
        _window.Close();
    }
}