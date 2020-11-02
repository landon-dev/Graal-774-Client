using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.Event;
using CefSharp.WinForms;
using Graal_774_Client.Base.Cef.Custom_Objects;
using Graal_774_Client.Base.Client;
using Graal_774_Client.Base.UI;
using _Cef = CefSharp.Cef;

namespace Graal_774_Client.Base.Cef
{
    class Cef
    {
        public class Browser
        {
            /*
            public static void RegisterJsObjects()
            {
                BindingOptions options = new BindingOptions() { CamelCaseJavascriptNames = false };
                headerBrowser.JavascriptObjectRepository.Register("uiObject", new UIObject(headerBrowser, clientFm), true, options);
            }*/

           /* public static void ShowDevTools(int Browser)
            {
                switch(Browser)
                {
                    case 0:
                        GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                        {
                            headerBrowser.ShowDevTools();
                        }));
                        break;
                    case 1:
                        GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                        {
                            gameBrowser.ShowDevTools();
                        }));
                        break;
                }
            } */

            private static clientFm clientFm = GlobalVar._clientFm;
            public static ChromiumWebBrowser gameBrowser;
            // public static ChromiumWebBrowser headerBrowser;
            public static void Init()
            {
                //CefSharpSettings.Proxy = new ProxyOptions(ip: "13.72.87.132", port: "1080");

                using (CefSettings settings = new CefSettings()
                {
                    // Cef Settings
                    CachePath = Application.StartupPath + @"\G774 Cache",
                    LogFile = "G774-Cef.log",
                    BrowserSubprocessPath = Application.StartupPath + @"\CefSharp.BrowserSubprocess.exe",
                    IgnoreCertificateErrors = true
                    // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.2.1; en-us; Nexus One Build/FRG83) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1"
                })
                {
                    // For Windows 7 and above, best to include relevant app.manifest entries as well
                    _Cef.EnableHighDPISupport();

                    CefSharpSettings.ShutdownOnExit = false;

                    // Cef Frame Settings
                    //settings.CefCommandLineArgs.Add("proxy-server", "13.72.87.132:1080");
                    settings.CefCommandLineArgs.Add("enable-gpu", "1");
                    settings.CefCommandLineArgs.Add("enable-webgl", "1");
                    //settings.CefCommandLineArgs.Add("proxy-server", "1.10.188.140:43327");

                    // Don't use a proxy server, always make direct connections. Overrides any other proxy server flags that are passed.
                    // Slightly improves Cef initialize time as it won't attempt to resolve a proxy
                    settings.CefCommandLineArgs.Add("no-proxy-server", "1");


                    // Perform dependency check to make sure all relevant resources are in our output directory & initialize cef with the provided settings
                    if (!_Cef.IsInitialized) _Cef.Initialize(settings); 



                }

                // create browser Objects & Components
                gameBrowser = new ChromiumWebBrowser(GlobalVar.Web.Client.homeUrl);
                clientFm.bodyPnl.Controls.Add(gameBrowser);
                gameBrowser.Dock = DockStyle.Fill;

                /*headerBrowser = new ChromiumWebBrowser(GlobalVar.Data.Web.Client.headerUrl);
                clientFm.headerPnl.Controls.Add(headerBrowser);
                headerBrowser.Dock = DockStyle.Fill;*/


                RequestHandler RequestHandler = new RequestHandler();
                gameBrowser.RequestHandler = RequestHandler;

                /* Rehook JsObj's
                headerBrowser.JavascriptObjectRepository.ResolveObject += (sender, e) =>
                {
                    var repo = e.ObjectRepository;
                    if (e.ObjectName == "uiObject")
                    {
                        BindingOptions bindingOptions = new BindingOptions { CamelCaseJavascriptNames = false };
                        repo.Register("uiObject", new UIObject(headerBrowser, GlobalVar._clientFm), isAsync: true, options: bindingOptions);
                    }
                }; */

                // Wait for the header browser to finish loading
                gameBrowser.FrameLoadEnd += (sender, args) =>
                {

                    // Wait for the MainFrame to finish loading
                    if (args.Frame.IsMain)
                    {
                        if (gameBrowser.Address == Theme.Game.Classic.Url || gameBrowser.Address == Theme.Game.Era.Url || gameBrowser.Address == Theme.Game.Zone.Url)
                        {
                            Functions.RemoveControls();
                            Functions.Resize();
                            Functions.Enabled();
                            gameBrowser.ShowDevTools();
                        }
                    }
                }; 

                // Check if the Obj's are being hooked/registered
                gameBrowser.JavascriptObjectRepository.ObjectBoundInJavascript += (sender, e) =>
                {
                    var name = e.ObjectName;

                    MessageBox.Show($"Object {e.ObjectName} was bound successfully.");
                };




            }

            // suppress js dialogs
            public bool OnJSDialog(IWebBrowser browserControl, IBrowser browser, string originUrl, CefJsDialogType dialogType, string messageText, string defaultPromptText, IJsDialogCallback callback, ref bool suppressMessage)
            {
                callback.Continue(true);
                return true;
            }

        }
    }
}
