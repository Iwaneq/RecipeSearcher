using MvvmCross.Commands;
using MvvmCross.Platforms.Wpf.Views;
using RecipeLibrary.Models;
using RecipeSearcher.Core.ViewModels;
using RecipeSearcher.WPF.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WvxStarter.Wpf.Views
{
    /// <summary>
    /// Logika interakcji dla klasy GuestBookView.xaml
    /// </summary>
    public partial class LocalRecipesListView : MvxWpfView
    {


        public IMvxCommand LoadRecipeCommand
        {
            get { return (IMvxCommand)GetValue(LoadRecipeCommandProperty); }
            set { SetValue(LoadRecipeCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadRecipeCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadRecipeCommandProperty =
            DependencyProperty.Register("LoadRecipeCommand", typeof(IMvxCommand), typeof(LocalRecipesListView), new PropertyMetadata(null));



        public LocalRecipesListView()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(LoadRecipeCommand != null && e.ChangedButton == MouseButton.Left)
            {
                var stackPanel = (StackPanel)sender;
                var recipe = (RecipeModelLite)stackPanel.DataContext;
                LoadRecipeCommand.Execute(recipe);
            }
        }
    }
}
