using System.Windows;
using TransportApp.Models;
using TransportApp.ViewModels;

namespace TransportApp.Dialogs;

public partial class KosztDialog : Window
{
    public Koszty Koszt =>
        ((KosztDialogViewModel)DataContext)
        .Koszt;

    public KosztDialog()
    {
        InitializeComponent();

        DataContext =
            new KosztDialogViewModel(
                new Koszty(),
                this);
    }

    public KosztDialog(Koszty koszt)
    {
        InitializeComponent();

        DataContext =
            new KosztDialogViewModel(
                koszt,
                this);
    }
}