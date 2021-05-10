using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using RecipeSearcher.Core.Services;
using WPF_Services.Services;

namespace RecipeSearcher.Core.ViewModels
{
    public class SettingsViewModel : MvxViewModel
    {
        private ISaveDataService _saveDataService;
        private IColorThemeChanger _colorThemeChanger;

        public IMvxCommand SaveSettingsCommand { get; set; }
        public IMvxCommand<string> ChangeThemeColorCommand { get; set; }

        private readonly MainViewModel _mainViewModel;

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


        public SettingsViewModel(ISaveDataService saveDataService, MainViewModel mainViewModel, IColorThemeChanger colorThemeChanger)
        {
            _mainViewModel = mainViewModel;
            _saveDataService = saveDataService;
            _colorThemeChanger = colorThemeChanger;

            SaveSettingsCommand = new MvxCommand(SaveSettings);
            ChangeThemeColorCommand = new MvxCommand<string>(ChangeColorTheme);
            IsManuallyReloadingRecipes = AppSettings.Default.IsManuallyReloadingRecipes;
        }

        private void ChangeColorTheme(string color)
        {
            _colorThemeChanger.ChangeTheme(color);
        }

        private void SaveSettings()
        {
            AppSettings.Default.Save();
            AppSettings.Default.Reload();

            //Updating setting variables in ViewModels
            UpdateSettingsInViewModels();
        }

        private void UpdateSettingsInViewModels()
        {
            _saveDataService.UpdatePath();
            _mainViewModel.UpdateReloadType();
        }
    }
}
