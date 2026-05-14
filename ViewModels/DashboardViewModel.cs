using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using TransportApp.Models;

namespace TransportApp.ViewModels;

public partial class DashboardViewModel
    : ObservableObject
{
    [ObservableProperty]
    private int liczbaPojazdow;

    [ObservableProperty]
    private int liczbaKierowcow;

    [ObservableProperty]
    private int liczbaZlecen;

    [ObservableProperty]
    private decimal sumaKosztow;

    [ObservableProperty]
    private decimal sumaPrzychodow;

    public DashboardViewModel()
    {
        _ = LoadData();
    }

    private async Task LoadData()
    {
        using var context = new AppDbContext();

        LiczbaPojazdow =
            await context.Pojazd.CountAsync();

        LiczbaKierowcow =
            await context.Kierowca.CountAsync();

        LiczbaZlecen =
            await context.Zlecenie.CountAsync();

        SumaKosztow =
            await context.Koszty
                .SumAsync(x =>
                    (decimal?)x.WartoscKosztu)
            ?? 0;

        SumaPrzychodow =
            await context.Zlecenie
                .SumAsync(x =>
                    (decimal?)x.Kwota)
            ?? 0;
    }
}