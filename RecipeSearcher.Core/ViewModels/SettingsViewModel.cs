using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using RecipeSearcher.Core.Services;
using System.Threading.Tasks;

namespace RecipeSearcher.Core.ViewModels
{
    public class SettingsViewModel : MvxViewModel
    {
        /*   PROPFULLS   */

        private bool _isManuallyReloadingRecipes;

        public bool IsManuallyReloadingRecipes
        {
            get { return _isManuallyReloadingRecipes; }
            set
            {
                _isManuallyReloadingRecipes = value;
                //Set settings
                AppSettings.Default.IsManuallyReloadingRecipes = value;
                IsAutomaticallyReloadingRecipes = !value;
            }
        }

        public bool IsAutomaticallyReloadingRecipes { get; set; }

        /*   SERVICES AND VIEW MODELS   */

        private readonly MainViewModel _mainViewModel;

        private ISaveDataService _saveDataService;
        private IColorThemeChanger _colorThemeChanger;

        /*   COMMANDS   */
        public IMvxAsyncCommand SaveSettingsCommand { get; set; }
        public IMvxCommand<string> ChangeThemeColorCommand { get; set; }

        /*   CONSTRUCTOR   */
        public SettingsViewModel(ISaveDataService saveDataService, MainViewModel mainViewModel, IColorThemeChanger colorThemeChanger)
        {
            //ViewModels and Services
            _mainViewModel = mainViewModel;
            _saveDataService = saveDataService;
            _colorThemeChanger = colorThemeChanger;

            //Settings Variables
            IsManuallyReloadingRecipes = AppSettings.Default.IsManuallyReloadingRecipes;

            //Commands
            SaveSettingsCommand = new MvxAsyncCommand(SaveSettings);
            ChangeThemeColorCommand = new MvxCommand<string>(ChangeColorTheme);
        }

        private void ChangeColorTheme(string color)
        {
            _colorThemeChanger.ChangeTheme(color);
        }

        private async Task SaveSettings()
        {
            AppSettings.Default.Save();
            AppSettings.Default.Reload();

            //Updating setting variables in ViewModels
            UpdateSettingsInViewModels();

            _mainViewModel.ProgressText = "Settings have been saved";

            //Clear ProgressText
            Task.Run(() => _mainViewModel.ClearProgressText());
        }

        private void UpdateSettingsInViewModels()
        {
            _saveDataService.UpdatePath();
            _mainViewModel.UpdateReloadType();
        }
    }
}
