using System.Windows;
using TransportApp.ViewModels;

namespace TransportApp.Views;

public partial class PojazdZdjeciaWindow : Window
{
    public PojazdZdjeciaWindow(
        PojazdZdjeciaViewModel vm)
    {
        InitializeComponent();

        DataContext = vm;
    }
}