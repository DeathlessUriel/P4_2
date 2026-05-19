using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TransportApp.Models;
using TransportApp.Services;

namespace TransportApp.ViewModels;

public partial class PojazdZdjeciaViewModel
    : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ZdjeciaPojazdu>
        zdjecia = new();

    public PojazdZdjeciaViewModel(
        List<ZdjeciaPojazdu> data)
    {
        Zdjecia =
            new ObservableCollection<ZdjeciaPojazdu>(
                data);
    }
}