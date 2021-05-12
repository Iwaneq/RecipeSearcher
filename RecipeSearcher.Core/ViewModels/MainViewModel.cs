using MvvmCross.Commands;
using MvvmCross.ViewModels;
using PlatformServices.Services;
using RecipeLibrary.Models;
using RecipeSearcher.Core.Services;
using System.Threading.Tasks;

namespace RecipeSearcher.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        /*   PROPFULLS   */

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

        /*   SERVICES AND VIEW MODELS   */

        private readonly MvxViewModel _searchRecipesViewModel;
        private readonly MvxViewModel _createRecipeViewModel;
        private readonly MvxViewModel _settingsViewModel;
        private readonly LocalRecipesListViewModel _localRecipesViewModel;

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

        private ISaveDataService _saveDataService;

        /*   COMMANDS   */

        public IMvxCommand<string> OpenViewModelCommand { get; set; }

        /*   SETTINGS VARIABLES   */

        private bool reloadManually = AppSettings.Default.IsManuallyReloadingRecipes;


        /*   CONSTRUCTOR   */

        public MainViewModel(IMessageBoxService messageBoxService, ISaveDataService saveDataService, IColorThemeChanger colorThemeChanger)
        {
            //ViewModels and Services
            _searchRecipesViewModel = new SearchRecipesViewModel(this, messageBoxService);
            _createRecipeViewModel = new CreateRecipeViewModel(messageBoxService, saveDataService, this);
            _localRecipesViewModel = new LocalRecipesListViewModel(saveDataService, this);
            _settingsViewModel = new SettingsViewModel(saveDataService, this, colorThemeChanger);

            _saveDataService = saveDataService;

            //Loading local recipes at application start
            Task.Run(() => _localRecipesViewModel.ReloadRecipes());

            //Commands
            OpenViewModelCommand = new MvxCommand<string>(OpenViewModel);
        }

        public async Task ClearProgressText()
        {
            await Task.Delay(8000);
            ProgressText = "";
        }

        //Updates type of local recipes reload for app
        public void UpdateReloadType()
        {
            reloadManually = AppSettings.Default.IsManuallyReloadingRecipes;
        }

        private void OpenViewModel(string parameter)
        {
            switch (parameter)
            {
                //Every case updates ChildViewModel and DownButton
                case "SearchRecipes":
                    ChildViewModel = _searchRecipesViewModel;
                    UpdateDownButton(DownButton.Null);
                    break;

                case "CreateRecipe":
                    ChildViewModel = _createRecipeViewModel;
                    UpdateDownButton(DownButton.Null);
                    break;

                //If recipes reloading isn't manually, then reload every time user open a LocalRecipesListViewModel and don't show DownButton
                case "LocalRecipes":
                    if (!reloadManually)
                    {
                        Task.Run(() => _localRecipesViewModel.ReloadRecipes());
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


        /*   DOWN BUTTON   */

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
            Task.Run(() => _localRecipesViewModel.ReloadRecipes());
        }
    }
}
