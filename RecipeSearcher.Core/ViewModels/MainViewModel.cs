using MvvmCross.Commands;
using MvvmCross.ViewModels;
using RecipeLibrary.Models;
using RecipeSearcher.Core.Services;
using WPF_Services.Services;

namespace RecipeSearcher.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {

        private string _progressText;

        public string ProgressText
        {
            get { return _progressText; }
            set 
            {
                _progressText = value;
                RaisePropertyChanged(() => ProgressText);
            }
        }


        public IMvxCommand<string> OpenViewModelCommand { get; set; }


        private readonly MvxViewModel _searchRecipesViewModel;
        private readonly MvxViewModel _createRecipeViewModel;
        private readonly MvxViewModel _localRecipesViewModel;
        private MvxViewModel _childViewModel;

        private ISaveDataService _saveDataService;

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
            _searchRecipesViewModel = new SearchRecipesViewModel(this, messageBoxService);
            _createRecipeViewModel = new CreateRecipeViewModel(messageBoxService, saveDataService, this);
            _localRecipesViewModel = new LocalRecipesListViewModel(saveDataService, this);
            _localRecipesViewModel.Initialize();

            OpenViewModelCommand = new MvxCommand<string>(OpenViewModel);
            _saveDataService = saveDataService;
        }

        private void OpenViewModel(string parameter)
        {
            if (parameter == "SearchRecipes")
            {
                ChildViewModel = _searchRecipesViewModel;
                UpdateDownButton(DownButton.Null);
            }
            else if (parameter == "CreateRecipe")
            {
                ChildViewModel = _createRecipeViewModel;
                UpdateDownButton(DownButton.Null);
            }
            else if(parameter == "LocalRecipes")
            {
                ChildViewModel = _localRecipesViewModel;
                UpdateDownButton(DownButton.ReloadRecipes);
            }
        }

        public void OpenRecipe(RecipeModel model)
        {
            UpdateDownButton(DownButton.Null);
            ChildViewModel = new RecipeViewModel(model, _saveDataService, this);
        }

        public void OpenRecipe(LocalRecipeModel model)
        {
            UpdateDownButton(DownButton.Null);
            ChildViewModel = new LocalRecipeViewModel(model);
        }


        /* DOWN BUTTON PROPFULL'S */

        enum DownButton
        {
            Null = 0,
            ReloadRecipes = 1
        }

        private string _downButtonText;

        public string DownButtonText
        {
            get { return _downButtonText; }
            set
            {
                _downButtonText = value;
                RaisePropertyChanged(() => DownButtonText);
            }
        }

        private IMvxCommand _downButtonCommand;

        public IMvxCommand DownButtonCommand
        {
            get { return _downButtonCommand; }
            set 
            {
                _downButtonCommand = value;
                RaisePropertyChanged(() => DownButtonCommand);
            }
        }

        private void UpdateDownButton(DownButton type)
        {
            switch (type)
            {
                case DownButton.Null:
                    DownButtonText = "";
                    break;
                case DownButton.ReloadRecipes:
                    DownButtonText = "Reload recipes";
                    DownButtonCommand = new MvxCommand(ReloadRecipes);
                    break;
            }
        }

        private void ReloadRecipes()
        {
            _localRecipesViewModel.Initialize();
        }
    }
}
