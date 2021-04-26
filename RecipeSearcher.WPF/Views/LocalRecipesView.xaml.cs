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
    public partial class LocalRecipesView : MvxWpfView
    {
        public LocalRecipesView()
        {
            InitializeComponent();
        }
    }
}
