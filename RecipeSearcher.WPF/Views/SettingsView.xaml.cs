using Microsoft.Win32;
using MvvmCross.Platforms.Wpf.Views;
using RecipeSearcher.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MvvmCross.Commands;

namespace WvxStarter.Wpf.Views
{
    public partial class SettingsView : MvxWpfView
    {
        /*   COMMANDS   */

        public IMvxCommand<string> ChangeThemeColorCommand
        {
            get { return (IMvxCommand<string>)GetValue(ChangeThemeColorCommandProperty); }
            set { SetValue(ChangeThemeColorCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeThemeColorCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeThemeColorCommandProperty =
            DependencyProperty.Register("ChangeThemeColorCommand", typeof(IMvxCommand<string>), typeof(SettingsView), new PropertyMetadata(null));



        public SettingsView()
        {
            InitializeComponent();
        }

        private void ChangePathButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPathFileDialog();
        }

        private void OpenPathFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = AppSettings.Default.SavingPath;

            dlg.ValidateNames = false;
            dlg.CheckFileExists = false;
            dlg.CheckPathExists = true;

            dlg.FileName = "Folder Selection.";

            if (dlg.ShowDialog() == true)
            {
                if (Directory.Exists(Path.GetDirectoryName(dlg.FileName)))
                {
                    AppSettings.Default.SavingPath = Path.GetDirectoryName(dlg.FileName);
                }
            }
        }

        private void Green_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeThemeColorCommand.Execute("green");
        }

        private void Red_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeThemeColorCommand.Execute("red");
        }

        private void Gray_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeThemeColorCommand.Execute("gray");
        }

        private void Blue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeThemeColorCommand.Execute("blue");
        }

        private void Purple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeThemeColorCommand.Execute("purple");
        }
    }
}
