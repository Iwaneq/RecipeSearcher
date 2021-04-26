using System.Drawing;

namespace RecipeLibrary.Models
{
    /// <summary>
    /// Lite version of Recipe Model
    /// </summary>
    public class RecipeModelLite
    {
        /// <summary>
        /// Name of the meal
        /// </summary>
        public string StrMeal { get; set; }

        /// <summary>
        /// String path to picture of meal
        /// </summary>
        public string StrMealThumb { get; set; }

        /// <summary>
        /// ID number for meal
        /// </summary>
        public int IdMeal { get; set; }

        /// <summary>
        /// Local path of this recipe
        /// </summary>
        public string LocalPath { get; set; }

        public Image Photo { get; set; }
    }
}
