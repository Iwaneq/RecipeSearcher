
using System.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RecipeLibrary.Models;
using System.Threading.Tasks;

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
            await File.WriteAllTextAsync(fullDirPath + "\\recipe.txt", lines);

            recipe.Photo.Save(fullDirPath + "\\photo.png");
        }

        public async Task<List<RecipeModelLite>> LoadRecipes()
        {
            List<RecipeModelLite> output = new List<RecipeModelLite>();

            var folders = Directory.GetDirectories(Path);

            foreach (string folder in folders)
            {
                if(File.Exists(folder + "\\recipe.txt"))
                {
                    RecipeModelLite recipe = new RecipeModelLite();

                    var recipeFile = await File.ReadAllTextAsync(folder + "\\recipe.txt");
                    var lines = recipeFile.Split('^');

                    recipe.StrMeal = lines[0];
                    recipe.LocalPath = folder;
                    recipe.Photo = new Bitmap(folder+"\\photo.png");

                    output.Add(recipe);
                }
            }

            return output;
        }
    }
}
