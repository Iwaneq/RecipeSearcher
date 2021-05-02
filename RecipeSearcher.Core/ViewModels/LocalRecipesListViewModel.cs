using MvvmCross.Commands;
using MvvmCross.ViewModels;
using RecipeLibrary.Models;
using RecipeSearcher.Core.ReportModels;
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

        public IMvxCommand LoadRecipeCommand { get; set; }

        public LocalRecipesListViewModel(ISaveDataService saveDataService, MainViewModel mainViewModel)
        {
            _saveDataService = saveDataService;
            _mainViewModel = mainViewModel;

            LoadRecipeCommand = new MvxAsyncCommand<RecipeModelLite>(LoadRecipe);
        }

        public override async Task Initialize()
        {
            Progress<LocalRecipesReportModel> progress = new Progress<LocalRecipesReportModel>();
            progress.ProgressChanged += ReportProgress;
            Recipes = await _saveDataService.LoadRecipes(progress);

            await ClearProgressText();
        }

        private void ReportProgress(object sender, LocalRecipesReportModel e)
        {
            _mainViewModel.ProgressText = $"Loading recipes - {e.LoadingPrecentage}% completed.";
        }

        private async Task ClearProgressText()
        {
            await Task.Delay(8000);
            _mainViewModel.ProgressText = "";
        }
        public async Task LoadRecipe(RecipeModelLite r)
        {
            var recipe = await _saveDataService.LoadRecipe(r.LocalPath);

            _mainViewModel.OpenRecipe(recipe);
        }
    }
}
