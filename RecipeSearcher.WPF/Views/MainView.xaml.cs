using MvvmCross.Platforms.Wpf.Views;
using System.Windows;
using System.Windows.Controls.Primitives;
using MvvmCross.Commands;

namespace WvxStarter.Wpf.Views
{
    public partial class MainView : MvxWpfView
    {
        ToggleButton currButton;

        public MainView()
        {
            InitializeComponent();
        }

        //Setting Navigation Button's look
        //If first NavigationButton is checked and another NavigationButton was clicked, then uncheck first button
        private void navigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (currButton != null && currButton != sender)
            {
                currButton.IsChecked = false;
            }
            else if (currButton != null)
            {
                currButton.IsChecked = true;
            }

            currButton = (ToggleButton)sender;
        }

        //Unchecking Current NavigationButton after clicking SettingsButton
        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            if(currButton != null)
            {
                currButton.IsChecked = false;
            }

            currButton = null;
        }
    }
}
