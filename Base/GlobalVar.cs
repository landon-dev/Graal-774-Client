using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graal_774_Client.Base
{
    public class GlobalVar
    {
        public static clientFm _clientFm = null;
        public GlobalVar(clientFm __clientFm)
        {
            _clientFm = __clientFm;
        }


        public class Web
        {
            public class Discord
            {
                public static string state;
                public static string id = "685272741095669797";
                public static string user = "";
                public static string avatar = "";
                public static string largeImg = "logo";
                public static string smallImg = "";
            }

            public class Client
            {
                public static string homeUrl = "http://client.team774.rf.gd";
                public static string headerUrl = "http://client.team774.rf.gd/pages/header/index.html";
            }
        }

        public class Client
        {
            public static string mainThread = "Cefsharp.BrowserSubprocess.exe";
            public class Settings
            {
                public class UI
                {
                    public static int headerHeight = 40;
                    public static bool Overlay = false;
                    public class Header
                    {
                        public const int maxHeight = 400;
                        public const int minHeight = 25;
                    }
                }

                public class Lua
                {
                    public static string server = "G774";
                }
            
            }
        }
    }
}
