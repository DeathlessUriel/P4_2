using System.Windows;
using TransportApp.Models;
using TransportApp.ViewModels;

namespace TransportApp.Dialogs;

public partial class PojazdDialog : Window
{
    public Pojazd Pojazd =>
        ((PojazdDialogViewModel)DataContext).Pojazd;

    public PojazdDialog()
    {
        InitializeComponent();

        DataContext = new PojazdDialogViewModel(new Pojazd(), this);
    }

    public PojazdDialog(Pojazd pojazd)
    {
        InitializeComponent();

        DataContext = new PojazdDialogViewModel(pojazd, this);
    }
}