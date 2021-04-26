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
    public class LocalRecipesViewModel : MvxViewModel
    {
        private readonly ISaveDataService _saveDataService;

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

        public IMvxCommand LoadRecipePanelsCommand { get; set; }

        public LocalRecipesViewModel(ISaveDataService saveDataService)
        {
            _saveDataService = saveDataService;

            Initialize().Wait();
        }

        public override async Task Initialize()
        {
            Recipes = await _saveDataService.LoadRecipes();
        }
    }
}
