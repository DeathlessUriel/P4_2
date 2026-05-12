using System.Windows;
using TransportApp.Models;
using TransportApp.ViewModels;

namespace TransportApp.Dialogs;

public partial class KierowcaDialog : Window
{
    public Kierowca Kierowca =>
        ((KierowcaDialogViewModel)DataContext)
        .Kierowca;

    public KierowcaDialog()
    {
        InitializeComponent();

        DataContext =
            new KierowcaDialogViewModel(
                new Kierowca(),
                this);
    }

    public KierowcaDialog(Kierowca kierowca)
    {
        InitializeComponent();

        DataContext =
            new KierowcaDialogViewModel(
                kierowca,
                this);
    }
}