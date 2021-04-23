using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecipeLibrary.Models
{
    public class RecipeProcessor
    {
        public static async Task<RecipeItemsModelLite> LoadRecipes(string searchTerms)
        {
            string url = $"https://www.themealdb.com/api/json/v1/1/search.php?s={searchTerms}";
            //string url = $"https://www.googleapis.com/books/v1/volumes/{id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    RecipeItemsModelLite meals = await response.Content.ReadAsAsync<RecipeItemsModelLite>();

                    return meals;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<RecipeModel> LoadRecipe(string id)
        {
            string url = $"https://www.themealdb.com/api/json/v1/1/lookup.php?i={id}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    RecipeItemsModel recipe = await response.Content.ReadAsAsync<RecipeItemsModel>();

                    return recipe.Meals.First();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
