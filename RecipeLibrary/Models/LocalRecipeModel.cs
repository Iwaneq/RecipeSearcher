
using System.Drawing;

namespace RecipeLibrary.Models
{
    public class LocalRecipeModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }
        public Image Photo { get; set; }
    }
}
