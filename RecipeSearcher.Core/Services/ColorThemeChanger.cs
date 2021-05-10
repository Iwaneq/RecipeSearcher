using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeSearcher.Core.Services
{
    public class ColorThemeChanger : IColorThemeChanger
    {
        public void ChangeTheme(string color)
        {
            switch (color)
            {
                case "red":
                    AppSettings.Default.HeaderColor = "#a83232";
                    AppSettings.Default.NavigationColor = "#bf2828";
                    AppSettings.Default.BottomPanelColor = "#541212";
                    AppSettings.Default.ChildViewColor = "#7d2323";
                    break;
                case "green":
                    AppSettings.Default.HeaderColor = "#085c1f";
                    AppSettings.Default.NavigationColor = "#0f6e29";
                    AppSettings.Default.BottomPanelColor = "#011f09";
                    AppSettings.Default.ChildViewColor = "#084519";
                    break;
                case "gray":
                    AppSettings.Default.HeaderColor = "#383333";
                    AppSettings.Default.NavigationColor = "#4f4f4f";
                    AppSettings.Default.BottomPanelColor = "#262626";
                    AppSettings.Default.ChildViewColor = "#403535";
                    break;
                case "blue":
                    AppSettings.Default.HeaderColor = "#003282";
                    AppSettings.Default.NavigationColor = "#0043ad";
                    AppSettings.Default.BottomPanelColor = "#001e4d";
                    AppSettings.Default.ChildViewColor = "#05275c";
                    break;
                case "purple":
                    AppSettings.Default.HeaderColor = "#3d207d";
                    AppSettings.Default.NavigationColor = "#522ea3";
                    AppSettings.Default.BottomPanelColor = "#381c75";
                    AppSettings.Default.ChildViewColor = "#4a21a3";
                    break;
            }
        }
    }
}
