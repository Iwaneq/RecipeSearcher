
using System.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RecipeLibrary.Models;

namespace RecipeSearcher.Core.Services
{
    public class SaveDataService : ISaveDataService
    {
        public string Path { get; set; } = $"C:\\data\\Recipes";

        public async void SaveRecipe(LocalRecipeModel recipe)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            string fullDirPath = Path + $"\\{recipe.Name}";

            Directory.CreateDirectory(fullDirPath);

            string lines = $"{recipe.Name}^{recipe.Category}^{recipe.Ingredients}^{recipe.Instructions}";
            await File.WriteAllTextAsync(fullDirPath + $"\\{recipe.Name}.txt", lines);

            recipe.Photo.Save(fullDirPath + "\\photo.png");
        }
    }
}
