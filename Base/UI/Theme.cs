using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graal_774_Client.Base.UI
{
    class Theme
    {
        public class Client
        {
            public static string Name { get; } = Application.ProductName;
            public static string Version { get; } = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public class Game
        {
            public class Classic
            {
                public const string Url = "https://classic.graalonline.com/";
                public static string Name { get; set; } = "iClassic";
                public static Color Color { get; set; } = Color.FromArgb(128, 255, 128);
                public static string State { get; set; } = "Playing " + Name;
            }


            public static class Era
            {
                public const string Url = "https://era.graalonline.com";
                public static string Name { get; set; } = "iEra";
                public static Color Color { get; set; } = Color.FromArgb(255, 128, 128);
                public static string State { get; set; } = "Playing " + Name;
            }

            public static class Zone
            {
                public static string Url { get; set; } = "https://zone.graalonline.com";
                public static string Name { get; set; } = "iZone";
                public static Color Color { get; set; } = Color.FromArgb(128, 255, 255);
                public static string State { get; set; } = "iPlaying " + Name;
            }

            public static class OlWest
            {
                public static string Url { get; set; } = "https://olwest.graalonline.com";
                public static string Name { get; set; } = "O'l West";
                public static Color Color { get; set; } = Color.FromArgb(255, 255, 128);
                public static string State { get; set; } = "Playing " + Name;
            }

            public static class Delteria
            {
                public static string Url { get; set; } = "https://delteria.graalonline.com";
                public static string Name { get; set; } = "Delteria";
                public static Color Color { get; set; } = Color.Orange;
                public static string State { get; set; } = "Playing " + Name;
            }
        }
    }
}
