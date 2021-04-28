
using System.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RecipeLibrary.Models;
using System.Threading.Tasks;
using MvvmCross;
using WPF_Services.Services;

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

            if(recipe.Photo != null)
            {
                recipe.Photo.Save(fullDirPath + "\\photo.png");
            }
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

                    if (File.Exists(folder + "\\photo.png"))
                    {
                        recipe.Photo = new Bitmap(folder + "\\photo.png");
                    }

                    output.Add(recipe);
                }
            }
            
            return output;
        }

        public async Task<LocalRecipeModel> LoadRecipe(string folderPath)
        {
            if (File.Exists(folderPath + "\\recipe.txt"))
            {
                LocalRecipeModel recipe = new LocalRecipeModel();

                var recipeFile = await File.ReadAllTextAsync(folderPath + "\\recipe.txt");
                var lines = recipeFile.Split('^');

                recipe.Name = lines[0];
                recipe.Category = lines[1];
                recipe.Ingredients = lines[2];
                recipe.Instructions = lines[3];
                recipe.Photo = new Bitmap(folderPath+"\\photo.png");

                return recipe;
            }
            else
            {
                Mvx.IoCProvider.Resolve<IMessageBoxService>().ShowErrorMessageBox("We found a problem with your recipe: 'recipe.txt' file has been deleted or we can't find it.");
                return null;
            }
        }
    }
}
