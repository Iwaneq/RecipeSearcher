using MvvmCross.Commands;
using MvvmCross.ViewModels;
using RecipeLibrary.Models;
using RecipeSearcher.Core.Services;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace RecipeSearcher.Core.ViewModels
{
    public class RecipeViewModel : MvxViewModel
    {
        private RecipeModel _recipe;

        public RecipeModel Recipe
        {
            get { return _recipe; }
            set
            {
                _recipe = value;
                RaisePropertyChanged(() => Recipe);
            }
        }

        private string _ingredients;

        public string Ingredients
        {
            get
            {
                return _ingredients;
            }
            set { _ingredients = value; }
        }

        private Image _photo;

        public Image Photo
        {
            get { return _photo; }
            set { _photo = value; }
        }


        private ISaveDataService _saveDataService;
        private MainViewModel _mainViewModel;
        public IMvxCommand SaveRecipeCommand { get; set; }

        public RecipeViewModel(RecipeModel recipe, ISaveDataService saveDataService, MainViewModel mainViewModel)
        {
            Recipe = recipe;
            _saveDataService = saveDataService;
            SaveRecipeCommand = new MvxCommand(SaveRecipe);

            _mainViewModel = mainViewModel;

            Ingredients = Recipe.CreateIngredientsList();
        }

        private async void SaveRecipe()
        {
            Progress<string> progress = new Progress<string>();
            progress.ProgressChanged += ReportProgress;

            LocalRecipeModel recipe = new LocalRecipeModel();

            recipe.Name = Recipe.StrMeal;
            recipe.Category = Recipe.StrCategory;
            recipe.Ingredients = Ingredients;
            recipe.Instructions = Recipe.StrInstructions;
            recipe.Photo = new Bitmap(await RecipeProcessor.getBitmapStream(Recipe.StrMealThumb));

            await Task.Run(()=> _saveDataService.SaveRecipe(recipe, progress));
        }

        private void ReportProgress(object sender, string e)
        {
            _mainViewModel.ProgressText = e;
        }
    }
}
