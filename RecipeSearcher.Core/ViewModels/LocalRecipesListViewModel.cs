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
        /*   PROPFULLS   */

        private List<RecipeModelLite> _recipes = new List<RecipeModelLite>();

        public List<RecipeModelLite> Recipes
        {
            get { return _recipes; }
            set
            {
                _recipes = value;
                RaisePropertyChanged(() => Recipes);
            }
        }


        /*   SERVICES AND VIEW MODELS   */

        private readonly ISaveDataService _saveDataService;
        private readonly MainViewModel _mainViewModel;

        /*   COMMANDS   */

        public IMvxCommand LoadRecipeCommand { get; set; }

        /*   CONSTRUCTOR   */

        public LocalRecipesListViewModel(ISaveDataService saveDataService, MainViewModel mainViewModel)
        {
            //Services and ViewModels
            _saveDataService = saveDataService;
            _mainViewModel = mainViewModel;

            //Commands
            LoadRecipeCommand = new MvxAsyncCommand<RecipeModelLite>(LoadRecipe);
        }

        public async Task ReloadRecipes()
        {
            Recipes.Clear();

            Progress<string> progress = new Progress<string>();
            progress.ProgressChanged += ProgressReport;

            Recipes = await Task.Run(() =>_saveDataService.LoadRecipes(progress));

            await Task.Run(() => _mainViewModel.ClearProgressText());
        }

        private void ProgressReport(object sender, string e)
        {
            _mainViewModel.ProgressText = e;
        }

        public async Task LoadRecipe(RecipeModelLite r)
        {
            var recipe = await _saveDataService.LoadRecipe(r.LocalPath);

            _mainViewModel.OpenRecipe(recipe);
        }
    }
}
