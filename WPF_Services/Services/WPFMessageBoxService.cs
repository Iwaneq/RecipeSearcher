using System.Windows;

namespace WPF_Services.Services
{
    public class WPFMessageBoxService : IMessageBoxService
    {
        public void ShowInformationMessageBox(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowWarningMessageBox(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
