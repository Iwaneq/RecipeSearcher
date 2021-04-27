using MvvmCross.Commands;
using MvvmCross.ViewModels;
using RecipeLibrary.Models;
using RecipeSearcher.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSearcher.Core.ViewModels
{
    public class LocalRecipesListViewModel : MvxViewModel
    {
        private readonly ISaveDataService _saveDataService;
        private readonly MainViewModel _mainViewModel;

        private List<RecipeModelLite> _recipes;

        public List<RecipeModelLite> Recipes
        {
            get { return _recipes; }
            set
            {
                _recipes = value;
                RaisePropertyChanged(() => Recipes);
            }
        }

        public IMvxCommand LoadRecipeCommand { get; set; }

        public LocalRecipesListViewModel(ISaveDataService saveDataService, MainViewModel mainViewModel)
        {
            _saveDataService = saveDataService;
            _mainViewModel = mainViewModel;

            LoadRecipeCommand = new MvxAsyncCommand<RecipeModelLite>(LoadRecipe);
        }

        public override async Task Initialize()
        {
            Recipes = await _saveDataService.LoadRecipes();
        }

        public async Task LoadRecipe(RecipeModelLite r)
        {
            var recipe = await _saveDataService.LoadRecipe(r.LocalPath);

            _mainViewModel.OpenRecipe(recipe);
        }
    }
}
