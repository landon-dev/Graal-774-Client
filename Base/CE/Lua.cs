using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graal_774_Client.Base.CE
{
    public class Lua
    {

        public static void Execute(string Script) { ThreadStart Thread = delegate { _Execute(Script); }; new Thread(Thread).Start(); }
        private static void _Execute(string Script) { Server.Execute(GlobalVar.Client.Settings.Lua.server, $"{Script}"); }

        public static void Return(string Script) { ThreadStart Thread = delegate { _Return(Script); }; new Thread(Thread).Start(); }

        private static void _Return(string Script) { Server.Execute(GlobalVar.Client.Settings.Lua.server, $"return '{Script}'"); }
  
    }
}
