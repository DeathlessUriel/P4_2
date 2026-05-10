using System.Windows;
using TransportApp.ViewModels;

namespace TransportApp;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}