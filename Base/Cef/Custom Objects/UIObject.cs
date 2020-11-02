using CefSharp.WinForms;
using Graal_774_Client.Base.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal_774_Client.Base.Cef.Custom_Objects
{
    public class UIObject
    {
        // Declare a local instance of chromium and the main form in order to execute things from here in the main thread
        // I invoked the func so everything is called from the main thread but all the work is done on a cef's thread to prevent illegal thread exceptions
        private static ChromiumWebBrowser _instanceBrowser = null;
        public static clientFm _instanceMainForm = null;

        public UIObject(ChromiumWebBrowser originalBrowser, clientFm mainForm)
        {
            _instanceBrowser = originalBrowser;
            _instanceMainForm = mainForm;
        }

        private void Drag() { FrmHandler.Form.Drag(); }
     
    }
}
