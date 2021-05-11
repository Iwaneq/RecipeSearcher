namespace PlatformServices.Services
{
    public interface IMessageBoxService
    {
        void ShowWarningMessageBox(string message);
        void ShowInformationMessageBox(string message);
        void ShowErrorMessageBox(string message);
    }
}
