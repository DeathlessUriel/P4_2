using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using TransportApp.Models;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
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

    [ObservableProperty]
    private PlotModel chartModel = new();


    private readonly IDbContextFactory<AppDbContext> _factory;
    public DashboardViewModel(
    IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;

        _ = LoadData();
    }
    public async Task LoadData()
    {

       

        using var context = await _factory.CreateDbContextAsync();
       
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

        var model = new PlotModel
        {
            Title = "Przejazdy miesięczne"
        };

        
        var columnSeries = new OxyPlot.Series.LinearBarSeries
        {
            Title = "Przejazdy",
            StrokeColor = OxyColors.Automatic,
            StrokeThickness = 1
        };

        var miesiace = new[]
        {
    "Sty", "Lut", "Mar", "Kwi", "Maj", "Cze",
    "Lip", "Sie", "Wrz", "Paź", "Lis", "Gru"
};

        var przejazdy = await context.Przejazd.ToListAsync();

        for (int i = 1; i <= 12; i++)
        {
            var count = przejazdy.Count(x => x.DataRozpoczecia.Month == i);

            
            columnSeries.Points.Add(new DataPoint(i - 1, count));
        }

        model.Series.Add(columnSeries);

       
        model.Axes.Add(new CategoryAxis
        {
            Position = AxisPosition.Bottom,
            ItemsSource = miesiace
        });

       
        model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left,
            Minimum = 0
        });

        ChartModel = model;
    }
}