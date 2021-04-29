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
        public IMvxCommand SaveRecipeCommand { get; set; }

        public RecipeViewModel(RecipeModel recipe, ISaveDataService saveDataService)
        {
            Recipe = recipe;
            _saveDataService = saveDataService;
            SaveRecipeCommand = new MvxCommand(SaveRecipe);

            Ingredients = Recipe.CreateIngredientsList();
        }

        private async void SaveRecipe()
        {
            LocalRecipeModel recipe = new LocalRecipeModel();

            recipe.Name = Recipe.StrMeal;
            recipe.Category = Recipe.StrCategory;
            recipe.Ingredients = Ingredients;
            recipe.Instructions = Recipe.StrInstructions;
            recipe.Photo = new Bitmap(await RecipeProcessor.getBitmapStream(Recipe.StrMealThumb));

            await Task.Run(()=> _saveDataService.SaveRecipe(recipe));
        }
    }
}
