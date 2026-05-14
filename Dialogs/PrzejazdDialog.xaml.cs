using System.Windows;
using TransportApp.Models;
using TransportApp.ViewModels;

namespace TransportApp.Dialogs;

public partial class PrzejazdDialog : Window
{
    public Przejazd Przejazd =>
        ((PrzejazdDialogViewModel)DataContext)
        .Przejazd;

    public PrzejazdDialog()
    {
        InitializeComponent();

        DataContext =
            new PrzejazdDialogViewModel(
                new Przejazd(),
                this);
    }

    public PrzejazdDialog(Przejazd przejazd)
    {
        InitializeComponent();

        DataContext =
            new PrzejazdDialogViewModel(
                przejazd,
                this);
    }
}