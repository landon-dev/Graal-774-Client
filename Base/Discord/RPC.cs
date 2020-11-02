using DiscordRPC;
using DiscordRPC.Logging;
using Graal_774_Client.Base.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal_774_Client.Base.Discord
{
    public class RPC
    {
        public static DiscordRpcClient client;
        public async static void Init()
        {
            /*
            Create a discord client
            NOTE: 	If you are using Unity3D, you must use the full constructor and define
                     the pipe connection.
            */
            client = new DiscordRpcClient(GlobalVar.Web.Discord.id)
            {

                //Set the logger
               // Logger = new ConsoleLogger() { Level = LogLevel.Warning }
            };


            //Subscribe to events
            client.OnReady += (sender, e) =>
            {
                //  MessageBox.Show("Received Ready from user {0}", e.User.Username);
                // Passes data onto the globalvar string values so the client can utilize it
                GlobalVar.Web.Discord.user = e.User.Username;
                User.AvatarSize size = new User.AvatarSize();
                GlobalVar.Web.Discord.avatar = e.User.GetAvatarURL(User.AvatarFormat.PNG, size);
            };


            client.OnPresenceUpdate += (sender, e) =>
            {

            };

            //Connect to the RPC
            client.Initialize();
            //Set the rich presence
            //Call this as many times as you want and anywhere in your code.

            await Task.Delay(2000);
            if (client.IsInitialized) client.SetPresence(new RichPresence()
            {
                Details = $"User: {GlobalVar.Web.Discord.user}",
                State = GlobalVar.Web.Discord.state,
                Assets = new Assets()
                {
                    LargeImageKey = GlobalVar.Web.Discord.largeImg,
                    LargeImageText = Theme.Client.Name,
                    SmallImageKey = ""
                }
            });
        }
    }
}
