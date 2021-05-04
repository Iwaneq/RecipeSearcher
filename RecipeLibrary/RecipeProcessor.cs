using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecipeLibrary.Models
{
    public class RecipeProcessor
    {
        public static async Task<RecipeItemsModelLite> LoadRecipes(string searchTerms, IProgress<string> progress)
        {
            string url = $"https://www.themealdb.com/api/json/v1/1/search.php?s={searchTerms}";
            //string url = $"https://www.googleapis.com/books/v1/volumes/{id}";

            progress.Report("Searching...");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    RecipeItemsModelLite meals = await response.Content.ReadAsAsync<RecipeItemsModelLite>();

                    if(meals.Meals != null)
                    {
                        progress.Report("Recipes loaded.");
                    }
                    else
                    {
                        progress.Report("Recipes not found.");
                    }

                    return meals;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<RecipeModel> LoadRecipe(string id, IProgress<string> progress)
        {
            string url = $"https://www.themealdb.com/api/json/v1/1/lookup.php?i={id}";

            progress.Report("Loading recipe...");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    RecipeItemsModel recipes = await response.Content.ReadAsAsync<RecipeItemsModel>();

                    var recipe = recipes.Meals.First();

                    progress.Report("Recipe loaded.");

                    return recipe;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<MemoryStream> getBitmapStream(string imgUrl)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(imgUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    return new MemoryStream(await response.Content.ReadAsByteArrayAsync());
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
