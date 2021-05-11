using MvvmCross;
using MvvmCross.ViewModels;
using PlatformServices.Services;
using RecipeSearcher.Core.Services;
using RecipeSearcher.Core.ViewModels;

namespace RecipeSearcher.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MainViewModel>();

            Mvx.IoCProvider.RegisterType<IMessageBoxService, WPFMessageBoxService>();
            Mvx.IoCProvider.RegisterType<ISaveDataService, SaveDataService>();
            Mvx.IoCProvider.RegisterType<IColorThemeChanger, ColorThemeChanger>();
        }
    }
}
