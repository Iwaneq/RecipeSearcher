using Microsoft.Win32;
using MvvmCross.Platforms.Wpf.Views;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WvxStarter.Wpf.Views
{
    public partial class CreateRecipeView : MvxWpfView
    {
        public CreateRecipeView()
        {
            InitializeComponent();
        }

        private void OpenFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                RecipePhoto.Source = bitmap;
            }
        }

        //Browse photo method
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog();
        }
    }
}
