
using RecipeLibrary.Models;

namespace RecipeSearcher.Core.Services
{
    public interface ISaveDataService
    {
        void SaveRecipe(LocalRecipeModel recipe);
    }
}
