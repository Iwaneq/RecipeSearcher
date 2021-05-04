
using System.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RecipeLibrary.Models;
using System.Threading.Tasks;
using MvvmCross;
using WPF_Services.Services;
using System.Drawing.Imaging;
using RecipeSearcher.Core.ReportModels;

namespace RecipeSearcher.Core.Services
{
    public class SaveDataService : ISaveDataService
    {
        public string Path { get; set; } = $"C:\\data\\Recipes";

        public async void SaveRecipe(LocalRecipeModel recipe, IProgress<string> progress)
        {
            progress.Report("Started saving...");
            if (!Directory.Exists(Path))
            {
                progress.Report("Creating directory for recipe...");
                Directory.CreateDirectory(Path);
            }

            progress.Report("Writing paths and files...");
            string fullDirPath = Path + $"\\{recipe.Name}";

            Directory.CreateDirectory(fullDirPath);

            string lines = $"{recipe.Name}^{recipe.Category}^{recipe.Ingredients}^{recipe.Instructions}";

            progress.Report("Saving recipe file...");
            await File.WriteAllTextAsync(fullDirPath + "\\recipe.txt", lines);

            if(recipe.Photo != null)
            {
                progress.Report("Saving photo...");
                recipe.Photo.Save(fullDirPath + "\\photo.png");
            }

            progress.Report("Recipe saved.");
        }

        public async Task<List<RecipeModelLite>> LoadRecipes(IProgress<LocalRecipesReportModel> progress)
        {
            List<RecipeModelLite> output = new List<RecipeModelLite>();
            LocalRecipesReportModel report = new LocalRecipesReportModel();

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
                    else if(File.Exists(folder + "\\photo.jpg"))
                    {
                        recipe.Photo = new Bitmap(folder + "\\photo.jpg");
                    }

                    output.Add(recipe);
                    report.LoadingPrecentage = (output.Count * 100) / folders.Length;
                    progress.Report(report);
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

                if (File.Exists(folderPath + "\\photo.png"))
                {
                    recipe.Photo = new Bitmap(folderPath + "\\photo.png");
                }
                else if (File.Exists(folderPath + "\\photo.jpg"))
                {
                    recipe.Photo = new Bitmap(folderPath + "\\photo.jpg");
                }

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
