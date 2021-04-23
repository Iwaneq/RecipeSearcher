using MvvmCross;
using MvvmCross.ViewModels;
using RecipeSearcher.Core.Services;
using RecipeSearcher.Core.ViewModels;
using WPF_Services.Services;

namespace RecipeSearcher.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MainViewModel>();

            Mvx.IoCProvider.RegisterType<IMessageBoxService, WPFMessageBoxService>();
            Mvx.IoCProvider.RegisterType<ISaveDataService, SaveDataService>();
        }
    }
}
