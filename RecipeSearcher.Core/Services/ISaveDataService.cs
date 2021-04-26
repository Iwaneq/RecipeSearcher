
using RecipeLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeSearcher.Core.Services
{
    public interface ISaveDataService
    {
        void SaveRecipe(LocalRecipeModel recipe);
        public Task<List<RecipeModelLite>> LoadRecipes();
    }
}
