using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using RecipeLibrary.Models;


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

        private readonly MainViewModel mainViewModel;


        public IMvxAsyncCommand LoadRecipesCommand { get; set; }
        public IMvxAsyncCommand LoadRecipeCommand { get; set; }
        public SearchRecipesViewModel(MainViewModel main)
        {
            mainViewModel = main;

            LoadRecipesCommand = new MvxAsyncCommand(LoadRecipes);
        }

        public SearchRecipesViewModel()
        {

        }

        private async Task LoadRecipes()
        {
            Recipes = await RecipeProcessor.LoadRecipes(SearchTerms);
        }

        public async Task LoadRecipe(string id)
        {
            var recipe = await RecipeProcessor.LoadRecipe(id);

            mainViewModel.OpenRecipe(recipe);
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
