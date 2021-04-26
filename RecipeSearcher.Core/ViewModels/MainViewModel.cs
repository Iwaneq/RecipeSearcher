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


        private readonly MvxViewModel _searchRecipesViewModel;
        private readonly MvxViewModel _createRecipeViewModel;
        private readonly MvxViewModel _localRecipesViewModel;
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
            _localRecipesViewModel = new LocalRecipesViewModel(saveDataService);

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
            else if(parameter == "LocalRecipes")
            {
                ChildViewModel = _localRecipesViewModel;
            }
        }

        public void OpenRecipe(RecipeModel model)
        {
            ChildViewModel = new RecipeViewModel(model);
        }
    }
}
