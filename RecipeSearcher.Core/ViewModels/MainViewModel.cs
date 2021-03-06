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

        private bool reloadManually = AppSettings.Default.IsManuallyReloadingRecipes;

        private readonly MvxViewModel _searchRecipesViewModel;
        private readonly MvxViewModel _createRecipeViewModel;
        private readonly MvxViewModel _localRecipesViewModel;
        private readonly MvxViewModel _settingsViewModel;
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

        public MainViewModel(IMessageBoxService messageBoxService, ISaveDataService saveDataService, IColorThemeChanger colorThemeChanger)
        {
            _searchRecipesViewModel = new SearchRecipesViewModel(this, messageBoxService);
            _createRecipeViewModel = new CreateRecipeViewModel(messageBoxService, saveDataService, this);
            _localRecipesViewModel = new LocalRecipesListViewModel(saveDataService, this);
            _settingsViewModel = new SettingsViewModel(saveDataService, this, colorThemeChanger);
            _localRecipesViewModel.Initialize();

            OpenViewModelCommand = new MvxCommand<string>(OpenViewModel);
            _saveDataService = saveDataService;
        }

        public void UpdateReloadType()
        {
            reloadManually = AppSettings.Default.IsManuallyReloadingRecipes;
        }

        private void OpenViewModel(string parameter)
        {
            switch (parameter)
            {
                case "SearchRecipes":
                    ChildViewModel = _searchRecipesViewModel;
                    UpdateDownButton(DownButton.Null);
                    break;

                case "CreateRecipe":
                    ChildViewModel = _createRecipeViewModel;
                    UpdateDownButton(DownButton.Null);
                    break;

                case "LocalRecipes":
                    if (!reloadManually)
                    {
                        _localRecipesViewModel.Initialize();
                        UpdateDownButton(DownButton.Null);
                    }
                    else
                    {
                        UpdateDownButton(DownButton.ReloadRecipes);
                    }
                    ChildViewModel = _localRecipesViewModel;
                    break;

                case "Settings":
                    ChildViewModel = _settingsViewModel;
                    UpdateDownButton(DownButton.Null);
                    break;

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
