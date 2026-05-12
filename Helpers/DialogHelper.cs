using System.Windows;

namespace TransportApp.Helpers;

public static class DialogHelper
{
    public static void Error(string message)
    {
        MessageBox.Show(
            message,
            "Błąd",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }

    public static void Info(string message)
    {
        MessageBox.Show(
            message,
            "Informacja",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    public static bool Confirm(string message)
    {
        return MessageBox.Show(
            message,
            "Potwierdzenie",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question) == MessageBoxResult.Yes;
    }
}