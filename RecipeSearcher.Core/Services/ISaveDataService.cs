
using RecipeLibrary.Models;
using RecipeSearcher.Core.ReportModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeSearcher.Core.Services
{
    public interface ISaveDataService
    {
        void SaveRecipe(LocalRecipeModel recipe, IProgress<string> progress);
        public Task<List<RecipeModelLite>> LoadRecipes(IProgress<LocalRecipesReportModel> progress);
        public Task<LocalRecipeModel> LoadRecipe(string folderPath);

        public void UpdatePath();
    }
}
