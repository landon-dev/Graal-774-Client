using CefSharp;
using _RequestHandler = CefSharp.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal_774_Client.Base.Cef
{
    public class RequestHandler : _RequestHandler.RequestHandler
    {
        protected override bool OnCertificateError(IWebBrowser chromiumWebBrowser, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
        {
            Task.Run(() =>
            {
                if (!callback.IsDisposed)
                {
                    using (callback)
                    {
                        // Allow the expired certificate from graalonline
                        if (requestUrl.ToLower().Contains("https://classic.graalonline.com")) callback.Continue(true);
                        else if (requestUrl.ToLower().Contains("https://era.graalonline.com")) callback.Continue(true);
                        else if (requestUrl.ToLower().Contains("https://zone.graalonline.com")) callback.Continue(true);
                        else if (requestUrl.ToLower().Contains("https://olwest.graalonline.com")) callback.Continue(true);
                        else callback.Continue(false);
                    }
                }
            });

            return true;
        }
    }
}
