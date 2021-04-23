using MvvmCross.Commands;
using MvvmCross.ViewModels;
using RecipeLibrary.Models;
using RecipeSearcher.Core.Services;
using WPF_Services.Services;

namespace RecipeSearcher.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        public IMvxCommand<string> OpenViewModelCommand { get; set; }


        private MvxViewModel _searchRecipesViewModel;
        private MvxViewModel _createRecipeViewModel;
        private MvxViewModel _childViewModel;

        public MvxViewModel ChildViewModel
        {
            get { return _childViewModel; }
            set
            {
                _childViewModel = value;
                RaisePropertyChanged(() => ChildViewModel);
            }
        }


        public MainViewModel(IMessageBoxService messageBoxService, ISaveDataService saveDataService)
        {
            _searchRecipesViewModel = new SearchRecipesViewModel(this);
            _createRecipeViewModel = new CreateRecipeViewModel(messageBoxService, saveDataService);

            OpenViewModelCommand = new MvxCommand<string>(OpenViewModel);
        }

        private void OpenViewModel(string parameter)
        {
            if (parameter == "SearchRecipes")
            {
                ChildViewModel = _searchRecipesViewModel;
            }
            else if (parameter == "CreateRecipe")
            {
                ChildViewModel = _createRecipeViewModel;
            }
        }

        public void OpenRecipe(RecipeModel model)
        {
            ChildViewModel = new RecipeViewModel(model);
        }
    }
}
