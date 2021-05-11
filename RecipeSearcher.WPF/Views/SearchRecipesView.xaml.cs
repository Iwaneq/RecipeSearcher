using MvvmCross.Commands;
using MvvmCross.Platforms.Wpf.Views;
using RecipeLibrary.Models;
using RecipeSearcher.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WvxStarter.Wpf.Views
{
    public partial class SearchRecipesView : MvxWpfView
    {

        public IMvxAsyncCommand<RecipeModelLite> LoadRecipeCommand
        {
            get { return (IMvxAsyncCommand<RecipeModelLite>)GetValue(LoadRecipeCommandProperty); }
            set { SetValue(LoadRecipeCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadRecipeCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadRecipeCommandProperty =
            DependencyProperty.Register("LoadRecipeCommand", typeof(IMvxAsyncCommand<RecipeModelLite>), typeof(SearchRecipesView), new PropertyMetadata(null));



        public IMvxAsyncCommand LoadRecipesListCommand
        {
            get { return (IMvxAsyncCommand)GetValue(LoadRecipesListCommandProperty); }
            set { SetValue(LoadRecipesListCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadRecipesListCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadRecipesListCommandProperty =
            DependencyProperty.Register("LoadRecipesListCommand", typeof(IMvxAsyncCommand), typeof(SearchRecipesView), new PropertyMetadata(null));



        public SearchRecipesView()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadRecipesListCommand.ExecuteAsync();
            }
        }

        private void Panel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && LoadRecipeCommand != null)
            {
                var stackPanel = (StackPanel)sender;
                var recipe = (RecipeModelLite)stackPanel.DataContext;

                LoadRecipeCommand.Execute(recipe);
            }
        }
    }
}
