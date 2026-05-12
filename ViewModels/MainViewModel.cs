using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TransportApp.Services;

namespace TransportApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private object currentView;

    public PojazdyViewModel PojazdyVM { get; }
    public KierowcyViewModel KierowcyVM { get; }
    public ZleceniaViewModel ZleceniaVM { get; }
    public PrzejazdyViewModel PrzejazdyVM { get; }
    public KosztyViewModel KosztyVM { get; }
    public DashboardViewModel DashboardVM { get; }
    public MainViewModel(
        PojazdService pojazdService,
        KierowcaService kierowcaService,
        ZlecenieService zlecenieService,
        PrzejazdService przejazdService,
        KosztService kosztService,
        DashboardViewModel dashboardViewModel)
    {
        PojazdyVM = new PojazdyViewModel(pojazdService);
        KierowcyVM = new KierowcyViewModel(kierowcaService);
        ZleceniaVM = new ZleceniaViewModel(zlecenieService);
        PrzejazdyVM = new PrzejazdyViewModel(przejazdService);
        KosztyVM = new KosztyViewModel(kosztService);
        DashboardVM = dashboardViewModel;

        CurrentView = PojazdyVM;
    }

    [RelayCommand]
    private void ShowPojazdy()
    {
        CurrentView = PojazdyVM;
    }

    [RelayCommand]
    private void ShowKierowcy()
    {
        CurrentView = KierowcyVM;
    }

    [RelayCommand]
    private void ShowZlecenia()
    {
        CurrentView = ZleceniaVM;
    }
    [RelayCommand]
    private void ShowPrzejazdy()
    {
        CurrentView = PrzejazdyVM;
    }
    [RelayCommand]
    private void ShowKoszty()
    {
        CurrentView = KosztyVM;
    }
    [RelayCommand]
        private void ShowDashboard()
    {
        CurrentView = DashboardVM;
    }

}