using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _Cef = CefSharp.Cef;
using __Cef = CefSharp;
using Graal_774_Client.Base;
using Graal_774_Client.Base.Cef;
using Graal_774_Client.Base.Discord;
using Graal_774_Client.Base.UI;
using Graal_774_Client.Base.Cef.Custom_Objects;
using Graal_774_Client.Base.Client;
using Graal_774_Client.Properties;

namespace Graal_774_Client
{
    public partial class clientFm : Form
    {
        public clientFm()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            
            new GlobalVar(this);
            InitializeComponent();
            Cef.Browser.Init();
            RPC.Init();
            // Cef.Browser.RegisterJsObjects(); // Registers js objects   
        }


        private void clientFm_Load(object sender, EventArgs e)
        {
            Name = Theme.Client.Name;
            headerPnl.Height = 25;
            bodyPnl.Height = 533;
            homePnl.BringToFront();
        }

        private void clientFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Cef.Shutdown();
            if (RPC.client.IsInitialized) RPC.client.Deinitialize();
        }


        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = PointToClient(Cursor.Position);

                if (FrmHandler.Form.TopLeft.Contains(cursor)) message.Result = (IntPtr)FrmHandler.Form.HTTOPLEFT;
                else if (FrmHandler.Form.TopRight.Contains(cursor)) message.Result = (IntPtr)FrmHandler.Form.HTTOPRIGHT;
                else if (FrmHandler.Form.BottomLeft.Contains(cursor)) message.Result = (IntPtr)FrmHandler.Form.HTBOTTOMLEFT;
                else if (FrmHandler.Form.BottomRight.Contains(cursor)) message.Result = (IntPtr)FrmHandler.Form.HTBOTTOMRIGHT;

                else if (FrmHandler.Form.Top.Contains(cursor)) message.Result = (IntPtr)FrmHandler.Form.HTTOP;
                else if (FrmHandler.Form.Left.Contains(cursor)) message.Result = (IntPtr)FrmHandler.Form.HTLEFT;
                else if (FrmHandler.Form.Right.Contains(cursor)) message.Result = (IntPtr)FrmHandler.Form.HTRIGHT;
                else if (FrmHandler.Form.Bottom.Contains(cursor)) message.Result = (IntPtr)FrmHandler.Form.HTBOTTOM;
            }
        }

        public class NoFocusCueButton : Button
        {
            protected override bool ShowFocusCues
            {
                get
                {
                    SetStyle(ControlStyles.Selectable, false);
                    return false;
                }
            }

        }

        private void clientFm_ResizeBegin(object sender, EventArgs e)
        {
            SuspendLayout();
        }

        private void clientFm_ResizeEnd(object sender, EventArgs e)
        {
            ResumeLayout();
        }

        private void headerPnl_MouseDown(object sender, MouseEventArgs e)
        {
            FrmHandler.Form.Drag();
        }

        private void clientFm_Resize(object sender, EventArgs e)
        {

            if (WindowState != FormWindowState.Minimized) // prevents resizing when application minimized
            {
                // Prevents the user from resizing the form to a possibly unviewable size
                if (Size.Width < 450) Size = new Size(450, Size.Height);
                else if (Size.Height < 250) Size = new Size(Size.Width, 250);

                var Browser = Cef.Browser.gameBrowser;

                headerPnl.Width = ClientSize.Width - 11;

                bodyPnl.Width = ClientSize.Width - 12;
                bodyPnl.Height = ClientSize.Height - GlobalVar.Client.Settings.UI.headerHeight - footerPnl.Height;

                controlPnl.Location = new Point(Browser.Right - 60, controlPnl.Location.Y);

                footerPnl.Location = new Point(footerPnl.Location.X, Browser.Bottom + 35);
                footerPnl.Size = new Size(Browser.Width, footerPnl.Height);

                expandBtn.Location = new Point(headerPnl.Width - 93, MiniBtn.Location.Y);

                //bodyPnl.Width = bodyPnl.Width + controlPnl.Width;

                Functions.Resize();
            }


        }

        private void MiniBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void expandBtn_Click(object sender, EventArgs e)
        {
            switch (headerPnl.Height)
            {
                case 25:
                    expandBtn.Image = Resources.icons8_collapse_arrow_15;
                    headerPnl.BringToFront();
                    controlPnl.BringToFront();
                    headerPnl.Height = 300;             
                    bodyPnl.Height = 255;
                    bodyPnl.Location = new Point(5, 307);                 
                    break;
                case 300:
                    expandBtn.Image = Resources.icons8_expand_arrow_15__1_;
                    headerPnl.SendToBack();
                    controlPnl.BringToFront();
                    headerPnl.Height = 25;            
                    bodyPnl.Height = 532;
                    bodyPnl.Location = new Point(5, 32);
                    break;
            }
        }

        private void logoImg_MouseDown(object sender, MouseEventArgs e)
        {
            FrmHandler.Form.Drag();
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            homePnl.BringToFront();

            homeBtn.BackColor = Color.FromArgb(32, 34, 37);
            homeBtn.ForeColor = Color.Gray;

            hacksBtn.BackColor = Color.FromArgb(64, 68, 75);
            hacksBtn.ForeColor = Color.White;


            toolsBtn.BackColor = Color.FromArgb(64, 68, 75);
            toolsBtn.ForeColor = Color.White;
        }

        private void hacksBtn_Click(object sender, EventArgs e)
        {
            hacksPnl.BringToFront();

            hacksBtn.BackColor = Color.FromArgb(32, 34, 37);
            hacksBtn.ForeColor = Color.Gray;

            homeBtn.BackColor = Color.FromArgb(64, 68, 75);
            homeBtn.ForeColor = Color.White;


            toolsBtn.BackColor = Color.FromArgb(64, 68, 75);
            toolsBtn.ForeColor = Color.White;
        }

        private void toolsBtn_Click(object sender, EventArgs e)
        {
            toolsPnl.BringToFront();

            toolsBtn.BackColor = Color.FromArgb(32, 34, 37);
            toolsBtn.ForeColor = Color.Gray;

            homeBtn.BackColor = Color.FromArgb(64, 68, 75);
            homeBtn.ForeColor = Color.White;


            hacksBtn.BackColor = Color.FromArgb(64, 68, 75);
            hacksBtn.ForeColor = Color.White;
        }

        private void serverDrop_onItemSelected(object sender, EventArgs e)
        {
            switch (serverDrop.selectedIndex)
            {
                case 0:
                    serverDrop.NomalColor = Theme.Game.Classic.Color;
                    serverDrop.onHoverColor = Theme.Game.Classic.Color;
                    break;
                case 1:
                    serverDrop.NomalColor = Theme.Game.Era.Color;
                    serverDrop.onHoverColor = Theme.Game.Era.Color;
                    break;
                case 2:
                    serverDrop.NomalColor = Theme.Game.Zone.Color;
                    serverDrop.onHoverColor = Theme.Game.Zone.Color;
                    break;
                case 3:
                    serverDrop.NomalColor = Theme.Game.OlWest.Color;
                    serverDrop.onHoverColor = Theme.Game.OlWest.Color;
                    break;
            }
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            switch (serverDrop.selectedIndex)
            {
                case 0:
                    Cef.Browser.gameBrowser.Load(Theme.Game.Classic.Url);
                    break;
                case 1:
                    Cef.Browser.gameBrowser.Load(Theme.Game.Era.Url);
                    break;
                case 2:
                    Cef.Browser.gameBrowser.Load(Theme.Game.Zone.Url);
                    break;
            }
            
        }

        private void newactBtn_Click(object sender, EventArgs e)
        {
            DialogResult Dialog = MessageBox.Show("Are you sure you want to create a new account? Doing so will delete un-identified accounts!", Application.ProductName, MessageBoxButtons.YesNo);
            if (Dialog == DialogResult.Yes) Functions.NewAct();
        }

        private void reloadBtn_Click(object sender, EventArgs e)
        {
            DialogResult Dialog = MessageBox.Show("Are you sure you want reload your account?", Application.ProductName, MessageBoxButtons.YesNo);
            if (Dialog == DialogResult.Yes) Functions.Reload();
        }

        private void bootsCheck_OnChange(object sender, EventArgs e)
        {
            if (bootsCheck.Checked)
            {

            }
            else
            {

            }
        }

        private void jesusBtn_OnChange(object sender, EventArgs e)
        {
            if (jesusCheck.Checked)
            {
                Exploits.Jesus();
            }
            else
            {

            }
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
