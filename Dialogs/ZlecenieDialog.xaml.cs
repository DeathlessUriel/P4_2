using System.Windows;
using TransportApp.Models;
using TransportApp.ViewModels;

namespace TransportApp.Dialogs;

public partial class ZlecenieDialog : Window
{
    public Zlecenie Zlecenie =>
        ((ZlecenieDialogViewModel)DataContext)
        .Zlecenie;

    public ZlecenieDialog()
    {
        InitializeComponent();

        DataContext =
            new ZlecenieDialogViewModel(
                new Zlecenie(),
                this);
    }

    public ZlecenieDialog(Zlecenie zlecenie)
    {
        InitializeComponent();

        DataContext =
            new ZlecenieDialogViewModel(
                zlecenie,
                this);
    }
}