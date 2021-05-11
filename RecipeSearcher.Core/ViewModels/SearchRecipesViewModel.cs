using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using RecipeLibrary.Models;
using System;
using PlatformServices.Services;

namespace RecipeSearcher.Core.ViewModels
{
    public class SearchRecipesViewModel : MvxViewModel
    {
        /*   PROPFULLS   */

        private RecipeItemsModelLite _recipes;

        public RecipeItemsModelLite Recipes
        {
            get { return _recipes; }
            set
            {
                _recipes = value;
                RaisePropertyChanged(() => Recipes);
            }
        }

        private string _searchTerms;

        public string SearchTerms
        {
            get { return _searchTerms; }
            set
            {
                _searchTerms = value;
                RaisePropertyChanged(() => SearchTerms);
            }
        }

        /*   SERVICES AND VIEW MODELS   */

        private readonly MainViewModel _mainViewModel;
        private readonly IMessageBoxService _messageBoxService;

        /*   COMMANDS   */

        public IMvxAsyncCommand LoadRecipesCommand { get; set; }
        public IMvxAsyncCommand<RecipeModelLite> LoadRecipeCommand { get; set; }

        /*   CONSTRUCTOR   */
        public SearchRecipesViewModel(MainViewModel main, IMessageBoxService messageBoxService)
        {
            //ViewModels and Services
            _mainViewModel = main;
            _messageBoxService = messageBoxService;

            //Commands
            LoadRecipesCommand = new MvxAsyncCommand(LoadRecipes);
            LoadRecipeCommand = new MvxAsyncCommand<RecipeModelLite>(LoadRecipe);

            //Setting up an ApiHelper from RecipeLibrary
            ApiHelper.InitializeClient();
        }

        private async Task LoadRecipes()
        {
            Progress<string> progress = new Progress<string>();
            progress.ProgressChanged += ReportProgress;

            //Waiting for recipes
            Recipes = await Task.Run(() => RecipeProcessor.LoadRecipes(SearchTerms, progress));
            if(Recipes.Meals == null)
            {
                _messageBoxService.ShowInformationMessageBox($"Sorry, we can't found any recipes that match your search terms: {SearchTerms}");
            }

            await Task.Run(() => _mainViewModel.ClearProgressText());
        }

        private void ReportProgress(object sender, string e)
        {
            _mainViewModel.ProgressText = e;
        }
        public async Task LoadRecipe(RecipeModelLite r)
        {
            Progress<string> progress = new Progress<string>();
            progress.ProgressChanged += ReportProgress;

            var recipe = await RecipeProcessor.LoadRecipe(r.IdMeal.ToString(), progress);

            _mainViewModel.OpenRecipe(recipe);

            await Task.Run(() => _mainViewModel.ClearProgressText());
        }
    }
}
