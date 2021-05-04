using System.Collections.Generic;
using System.Drawing;

namespace RecipeLibrary.Models
{
    public class RecipeModel
    {
        /*"idMeal": "52771",
      "strMeal": "Spicy Arrabiata Penne",
      "strDrinkAlternate": null,
      "strCategory": "Vegetarian",
      "strArea": "Italian",
      "strInstructions": "Bring a large pot of water to a boil. Add kosher salt to the boiling water, then add the pasta. Cook according to the package instructions, about 9 minutes.\r\nIn a large skillet over medium-high heat, add the olive oil and heat until the oil starts to shimmer. Add the garlic and cook, stirring, until fragrant, 1 to 2 minutes. Add the chopped tomatoes, red chile flakes, Italian seasoning and salt and pepper to taste. Bring to a boil and cook for 5 minutes. Remove from the heat and add the chopped basil.\r\nDrain the pasta and add it to the sauce. Garnish with Parmigiano-Reggiano flakes and more basil and serve warm.",
      "strMealThumb": "https://www.themealdb.com/images/media/meals/ustsqw1468250014.jpg",
      "strTags": "Pasta,Curry",
      "strYoutube": "https://www.youtube.com/watch?v=1IszT_guI08",
      "strIngredient1": "penne rigate",
      "strIngredient2": "olive oil",
      "strIngredient3": "garlic",
      "strIngredient4": "chopped tomatoes",
      "strIngredient5": "red chile flakes",
      "strIngredient6": "italian seasoning",
      "strIngredient7": "basil",
      "strIngredient8": "Parmigiano-Reggiano",
      "strIngredient9": "",
      "strIngredient10": "",
      "strIngredient11": "",
      "strIngredient12": "",
      "strIngredient13": "",
      "strIngredient14": "",
      "strIngredient15": "",
      "strIngredient16": null,
      "strIngredient17": null,
      "strIngredient18": null,
      "strIngredient19": null,
      "strIngredient20": null,
      "strMeasure1": "1 pound",
      "strMeasure2": "1/4 cup",
      "strMeasure3": "3 cloves",
      "strMeasure4": "1 tin ",
      "strMeasure5": "1/2 teaspoon",
      "strMeasure6": "1/2 teaspoon",
      "strMeasure7": "6 leaves",
      "strMeasure8": "spinkling",
      "strMeasure9": "",
      "strMeasure10": "",
      "strMeasure11": "",
      "strMeasure12": "",
      "strMeasure13": "",
      "strMeasure14": "",
      "strMeasure15": "",
      "strMeasure16": null,
      "strMeasure17": null,
      "strMeasure18": null,
      "strMeasure19": null,
      "strMeasure20": null,
      "strSource": null,
      "dateModified": null*/

        /// <summary>
        /// Name of the meal
        /// </summary>
        public string StrMeal { get; set; }

        /// <summary>
        /// String path to picture of meal
        /// </summary>
        public string StrMealThumb { get; set; }

        /// <summary>
        /// Link to Youtube Video  
        /// </summary>
        public string StrYoutube { get; set; }

        /// <summary>
        /// ID number for meal
        /// </summary>
        public int IdMeal { get; set; }

        /// <summary>
        /// Category of meal
        /// </summary>
        public string StrCategory { get; set; }

        /// <summary>
        /// Instructions for preparing meal
        /// </summary>
        public string StrInstructions { get; set; }

        /// <summary>
        /// Ingredients
        /// </summary>
        public string StrIngredient1 { get; set; }
        public string StrIngredient2 { get; set; }
        public string StrIngredient3 { get; set; }
        public string StrIngredient4 { get; set; }
        public string StrIngredient5 { get; set; }
        public string StrIngredient6 { get; set; }
        public string StrIngredient7 { get; set; }
        public string StrIngredient8 { get; set; }
        public string StrIngredient9 { get; set; }
        public string StrIngredient10 { get; set; }
        public string StrIngredient11 { get; set; }
        public string StrIngredient12 { get; set; }
        public string StrIngredient13 { get; set; }
        public string StrIngredient14 { get; set; }
        public string StrIngredient15 { get; set; }
        public string StrIngredient16 { get; set; }
        public string StrIngredient17 { get; set; }
        public string StrIngredient18 { get; set; }
        public string StrIngredient19 { get; set; }
        public string StrIngredient20 { get; set; }

        public string StrMeasure1 { get; set; }
        public string StrMeasure2 { get; set; }
        public string StrMeasure3 { get; set; }
        public string StrMeasure4 { get; set; }
        public string StrMeasure5 { get; set; }
        public string StrMeasure6 { get; set; }
        public string StrMeasure7 { get; set; }
        public string StrMeasure8 { get; set; }
        public string StrMeasure9 { get; set; }
        public string StrMeasure10 { get; set; }
        public string StrMeasure11 { get; set; }
        public string StrMeasure12 { get; set; }
        public string StrMeasure13 { get; set; }
        public string StrMeasure14 { get; set; }
        public string StrMeasure15 { get; set; }
        public string StrMeasure16 { get; set; }
        public string StrMeasure17 { get; set; }
        public string StrMeasure18 { get; set; }
        public string StrMeasure19 { get; set; }
        public string StrMeasure20 { get; set; }

        public string CreateIngredientsList()
        {
            string output = "";

            List<string> Ingredients = new List<string>
            {
            StrIngredient1,StrIngredient2,StrIngredient3,StrIngredient4,StrIngredient5,StrIngredient6,StrIngredient7,StrIngredient8,StrIngredient9,StrIngredient10,StrIngredient11,StrIngredient12,StrIngredient13,StrIngredient14,StrIngredient15,StrIngredient16,StrIngredient17,StrIngredient18,StrIngredient19,StrIngredient20,
            };
            List<string> Measures = new List<string>
            {
            StrMeasure1,StrMeasure2,StrMeasure3,StrMeasure4,StrMeasure5,StrMeasure6,StrMeasure7,StrMeasure8,StrMeasure9,StrMeasure10,StrMeasure11,StrMeasure12,StrMeasure13,StrMeasure14,StrMeasure15,StrMeasure16,StrMeasure17,StrMeasure18,StrMeasure19,StrMeasure20,
            };

            for (int i = 0; i < 20; i++)
            {
                if (Ingredients[i] != "")
                {
                    output += $"{Ingredients[i]} - {Measures[i]}, ";
                }
            }
            output = output.Substring(0, output.Length - 1);

            return output;
        }
    }
}
