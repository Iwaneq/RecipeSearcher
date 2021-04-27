using MvvmCross.ViewModels;
using RecipeLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeSearcher.Core.ViewModels
{
    public class LocalRecipeViewModel : MvxViewModel
    {
        private LocalRecipeModel _recipe;

        public LocalRecipeModel Recipe
        {
            get { return _recipe; }
            set { _recipe = value; }
        }


        public LocalRecipeViewModel(LocalRecipeModel recipe)
        {
            Recipe = recipe;
        }
    }
}
