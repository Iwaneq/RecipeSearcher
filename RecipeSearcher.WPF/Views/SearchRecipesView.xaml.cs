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
    /// <summary>
    /// Logika interakcji dla klasy GuestBookView.xaml
    /// </summary>
    public partial class SearchRecipesView : MvxWpfView
    {
        private readonly Dictionary<object, int> panels = new Dictionary<object, int>();

        SearchRecipesViewModel _viewModel;
        public SearchRecipesView()
        {
            InitializeComponent();

            ApiHelper.InitializeClient();
        }

        private async void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel = (SearchRecipesViewModel)DataContext;
                await _viewModel.LoadRecipesCommand.ExecuteAsync();

                AddPanels();
            }
        }

        private void AddPanels()
        {
            RecipesWrapPanel.Children.Clear();

            if (_viewModel.Recipes.Meals == null)
            {
                MessageBox.Show("Recipes not found", "Not found error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                List<Task> tasks = new List<Task>();

                foreach (RecipeModelLite r in _viewModel.Recipes.Meals)
                {
                    Task.Run(() => Dispatcher.Invoke(() => CreatePanel(r)));
                }
            }
        }

        private void CreatePanel(RecipeModelLite r)
        {
            StackPanel panel = new StackPanel { Width = 200, Height = 250, Background = new SolidColorBrush(Color.FromRgb(1, 31, 9)), Margin = new Thickness(5, 5, 5, 5) };

            //adding an Image to the panel
            Image image = new Image { Width = 200, Height = 200, Cursor = Cursors.Hand };
            BitmapImage bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.UriSource = new Uri(r.StrMealThumb, UriKind.Absolute);
            bitmap.EndInit();

            image.Source = bitmap;
            panel.Children.Add(image);

            //adding a text to the panel
            Viewbox nameLabel = new Viewbox { Width = 200, Height = 50, Stretch = Stretch.Uniform };
            TextBlock name = new TextBlock { Foreground = Brushes.White, TextWrapping = TextWrapping.Wrap, Padding = new Thickness(3, 3, 3, 3) };
            name.Text = r.StrMeal;

            nameLabel.Child = name;
            panel.Children.Add(nameLabel);

            //Adding event handler and event data
            panels.Add(panel, r.IdMeal);
            panel.MouseDown += Panel_MouseDown;

            RecipesWrapPanel.Children.Add(panel);
        }

        private async void Panel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                panels.TryGetValue(sender, out int id);

                await _viewModel.LoadRecipe(id.ToString());
            }
        }
    }
}
