using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graal_774_Client.Base.Cef;
using Graal_774_Client.Base.Client;

namespace Graal_774_Client.Base.UI
{
    public class FrmHandler
    {

        public class Form
        {

            public static void Drag()
            {
                GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                {
                    if (GlobalVar._clientFm.headerPnl.ContainsFocus)
                    {
                        GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                        {
                            if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                            {
                                Win32.ReleaseCapture();
                                Win32.SendMessage(GlobalVar._clientFm.Handle, Win32.WM_NCLBUTTONDOWN, Win32.HT_CAPTION, 0);
                            }
                        }));
                    }
                    else if (Cef.Cef.Browser.gameBrowser.Focused)
                    {
                        GlobalVar._clientFm.Invoke(new MethodInvoker(delegate
                        {
                            if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                            {
                                Win32.ReleaseCapture();
                                Win32.SendMessage(GlobalVar._clientFm.Handle, Win32.WM_NCLBUTTONDOWN, Win32.HT_CAPTION, 0);
                            }
                        }));
                    }
                }));
            }

            public const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

            const int _ = 10;
            public static Rectangle Top { get { return new Rectangle(0, 0, GlobalVar._clientFm.ClientSize.Width, _); } }
            public static Rectangle Left { get { return new Rectangle(0, 0, _, GlobalVar._clientFm.ClientSize.Height); } }
            public static Rectangle Bottom { get { return new Rectangle(0, GlobalVar._clientFm.ClientSize.Height - _, GlobalVar._clientFm.ClientSize.Width, _); } }
            public static Rectangle Right { get { return new Rectangle(GlobalVar._clientFm.ClientSize.Width - _, 0, _, GlobalVar._clientFm.ClientSize.Height); } }

            public static Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
            public static Rectangle TopRight { get { return new Rectangle(GlobalVar._clientFm.ClientSize.Width - _, 0, _, _); } }
            public static Rectangle BottomLeft { get { return new Rectangle(0, GlobalVar._clientFm.ClientSize.Height - _, _, _); } }
            public static Rectangle BottomRight { get { return new Rectangle(GlobalVar._clientFm.ClientSize.Width - _, GlobalVar._clientFm.ClientSize.Height - _, _, _); } }

        }


    }

}
