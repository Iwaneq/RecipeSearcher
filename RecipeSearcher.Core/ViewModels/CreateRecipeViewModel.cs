using MvvmCross.Commands;
using MvvmCross.ViewModels;
using PlatformServices.Services;
using RecipeLibrary.Models;
using RecipeSearcher.Core.Services;
using System;
using System.Threading.Tasks;

namespace RecipeSearcher.Core.ViewModels
{
    public class CreateRecipeViewModel : MvxViewModel
    {
        /*   PROPFULLS   */

        private LocalRecipeModel _recipe = new LocalRecipeModel();

        public LocalRecipeModel Recipe
        {
            get { return _recipe; }
            set
            {
                _recipe = value;
                RaisePropertyChanged(() => Recipe);
            }
        }

        /*   SERVICES AND VIEW MODELS   */

        private MainViewModel _mainViewModel;
        private readonly IMessageBoxService _messageBox;
        private readonly ISaveDataService _saveDataService;

        /*   COMMANDS   */

        public IMvxCommand SaveRecipeCommand { get; set; }


        /*   CONSTRUCTOR   */

        public CreateRecipeViewModel(IMessageBoxService messageBoxService, ISaveDataService saveDataService, MainViewModel mainViewModel)
        {
            //Models and services
            _messageBox = messageBoxService;
            _saveDataService = saveDataService;
            _mainViewModel = mainViewModel;

            //Commands
            SaveRecipeCommand = new MvxCommand(SaveRecipe);
        }

        private void SaveRecipe()
        {
            Progress<string> progress = new Progress<string>();
            progress.ProgressChanged += ReportProgress;

            if (ValidateRecipe())
            {
                _saveDataService.SaveRecipe(Recipe, progress);
                Recipe = new LocalRecipeModel();
            }

            Task.Run(() => _mainViewModel.ClearProgressText());
        }

        private void ReportProgress(object sender, string e)
        {
            _mainViewModel.ProgressText = e;
        }

        //Checking if user completed every field
        private bool ValidateRecipe()
        {
            if (Recipe.Name.Length == 0)
            {
                _messageBox.ShowWarningMessageBox("Please complete the Name field.");
                return false;
            }
            if (Recipe.Category.Length == 0)
            {
                _messageBox.ShowWarningMessageBox("Please complete the Category field.");
                return false;
            }
            if (Recipe.Instructions.Length == 0)
            {
                _messageBox.ShowWarningMessageBox("Please complete the Instructions field.");
                return false;
            }
            if (Recipe.Ingredients.Length == 0)
            {
                _messageBox.ShowWarningMessageBox("Please complete the Ingredients field.");
                return false;
            }

            return true;
        }

    }
}
