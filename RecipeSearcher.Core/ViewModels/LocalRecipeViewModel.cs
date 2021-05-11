using MvvmCross.ViewModels;
using RecipeLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeSearcher.Core.ViewModels
{
    public class LocalRecipeViewModel : MvxViewModel
    {
        /*   PROPFULL'S   */
        private LocalRecipeModel _recipe;

        public LocalRecipeModel Recipe
        {
            get { return _recipe; }
            set { _recipe = value; }
        }

        /*   CONSTRUCTOR   */
        public LocalRecipeViewModel(LocalRecipeModel recipe)
        {
            Recipe = recipe;
        }
    }
}
