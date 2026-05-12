using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using TransportApp.Helpers;
using TransportApp.Models;

namespace TransportApp.ViewModels;

public partial class KierowcaDialogViewModel
    : ObservableObject
{
    private readonly Window _window;

    [ObservableProperty]
    private Kierowca kierowca;

    public DateTime? DataZatrudnienia
    {
        get => Kierowca.DataZatrudnienia
            .ToDateTime(TimeOnly.MinValue);

        set
        {
            if (value != null)
            {
                Kierowca.DataZatrudnienia =
                    DateOnly.FromDateTime(
                        value.Value);
            }
        }
    }

    public KierowcaDialogViewModel(
        Kierowca kierowca,
        Window window)
    {
        Kierowca = kierowca;
        _window = window;
    }

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(
                Kierowca.Imie))
        {
            DialogHelper.Error(
                "Imię jest wymagane.");

            return;
        }

        if (string.IsNullOrWhiteSpace(
                Kierowca.Nazwisko))
        {
            DialogHelper.Error(
                "Nazwisko jest wymagane.");

            return;
        }

        if (string.IsNullOrWhiteSpace(
                Kierowca.NumerPrawaJazdy))
        {
            DialogHelper.Error(
                "Numer prawa jazdy jest wymagany.");

            return;
        }

        _window.DialogResult = true;
        _window.Close();
    }
}