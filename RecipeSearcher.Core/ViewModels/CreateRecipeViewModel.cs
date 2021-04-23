using MvvmCross.Commands;
using MvvmCross.ViewModels;
using RecipeLibrary.Models;
using RecipeSearcher.Core.Services;
using WPF_Services.Services;

namespace RecipeSearcher.Core.ViewModels
{
    public class CreateRecipeViewModel : MvxViewModel
    {
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

        private readonly IMessageBoxService _messageBox;
        private readonly ISaveDataService _saveDataService;
        public IMvxCommand SaveRecipeCommand { get; set; }

        public CreateRecipeViewModel(IMessageBoxService messageBoxService, ISaveDataService saveDataService)
        {
            _messageBox = messageBoxService;
            _saveDataService = saveDataService;

            SaveRecipeCommand = new MvxCommand(SaveRecipe);
        }

        private void SaveRecipe()
        {
            if (ValidateRecipe())
            {
                _saveDataService.SaveRecipe(Recipe);
            }
        }

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
