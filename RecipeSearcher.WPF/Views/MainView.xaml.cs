using MvvmCross.Platforms.Wpf.Views;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace WvxStarter.Wpf.Views
{
    public partial class MainView : MvxWpfView
    {
        ToggleButton currButton;
        public MainView()
        {
            InitializeComponent();
        }

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
    }
}
