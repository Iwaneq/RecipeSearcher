using MvvmCross.ViewModels;
using RecipeLibrary.Models;
using System.Drawing;

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

        public RecipeViewModel(RecipeModel recipe)
        {
            Recipe = recipe;

            Ingredients = Recipe.CreateIngredientsList();
        }
    }
}
