using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using RecipeLibrary.Models;
using WPF_Services.Services;

namespace RecipeSearcher.Core.ViewModels
{
    public class SearchRecipesViewModel : MvxViewModel
    {
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

        private readonly MainViewModel _mainViewModel;
        private readonly IMessageBoxService _messageBoxService;


        public IMvxAsyncCommand LoadRecipesCommand { get; set; }
        public IMvxAsyncCommand<RecipeModelLite> LoadRecipeCommand { get; set; }
        public SearchRecipesViewModel(MainViewModel main, IMessageBoxService messageBoxService)
        {
            _mainViewModel = main;
            _messageBoxService = messageBoxService;

            LoadRecipesCommand = new MvxAsyncCommand(LoadRecipes);
            LoadRecipeCommand = new MvxAsyncCommand<RecipeModelLite>(LoadRecipe);
            ApiHelper.InitializeClient();
        }

        public SearchRecipesViewModel()
        {

        }

        private async Task LoadRecipes()
        {
            Recipes = await RecipeProcessor.LoadRecipes(SearchTerms);
            if(Recipes.Meals == null)
            {
                _messageBoxService.ShowInformationMessageBox($"Sorry, we can't found any recipes that match your search terms: {SearchTerms}");
            }
        }

        public async Task LoadRecipe(RecipeModelLite r)
        {
            var recipe = await RecipeProcessor.LoadRecipe(r.IdMeal.ToString());

            _mainViewModel.OpenRecipe(recipe);
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


    }
}
