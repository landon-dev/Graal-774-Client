using CefSharp;
using _Cef = CefSharp.Cef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graal_774_Client.Base.CE;

namespace Graal_774_Client.Base.Client
{
    public class Functions
    {
        public static void NewAct()
        {
            // Deletes the browser cookie containing your accounts creation time, Note: this doesn't clear data such as images, and gani!
            _Cef.GetGlobalCookieManager().DeleteCookies("", "");
            Cef.Cef.Browser.gameBrowser.Reload();
        }

        public static void Reload()
        {
            Cef.Cef.Browser.gameBrowser.Reload();
        }

        public static void Resize()
        {
            if (Cef.Cef.Browser.gameBrowser.IsBrowserInitialized)
            {
                Cef.Cef.Browser.gameBrowser.GetMainFrame().ExecuteJavaScriptAsync($"document.getElementById('unityContainer').style='width: 100%; height: {Cef.Cef.Browser.gameBrowser.Height + 1}px;';");
            }
        }
        public static void Enabled()
        {
            GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
            {
                GlobalVar._clientFm.bootsCheck.Enabled = true;
                GlobalVar._clientFm.jesusCheck.Enabled = true;
            }));
        }

        public static void RepString(string ostring, string nstring)
        {
            UnityBypass.Suspender.Suspend();
            Lua.Execute("OpenProcess('CefSharp.BrowserSubprocess.exe')\r\nlocal memsc = createMemScan()\r\nmemsc.firstScan(soExactValue, vtString, nil, '" + ostring + "', nil, 0, 0x7fffffffffffffff, \" + W - C\", fsmNotAligned, nil, nil, nil, false, true)\r\nmemsc.waitTillDone()\r\nlocal foundlist = createFoundList(memsc)\r\nfoundlist.initialize()\r\nfor i = 0, foundlist.Count - 1 do\r\n                        writeString(foundlist.Address[i], '" + nstring + "')\r\nend\r\nfoundlist.deinitialize()\r\n\r\nfoundlist.destroy()\r\nmemsc.destroy()\r\n");
            UnityBypass.Suspender.Resume();
        }

        public static void RemoveControls()
        {
            Cef.Cef.Browser.gameBrowser.GetMainFrame().ExecuteJavaScriptAsync("document.getElementById('tabs').remove();");
            Cef.Cef.Browser.gameBrowser.GetMainFrame().ExecuteJavaScriptAsync("document.getElementById('temp').remove();");
            Cef.Cef.Browser.gameBrowser.GetMainFrame().ExecuteJavaScriptAsync("$('iframe').remove();");
        }
    }
}
