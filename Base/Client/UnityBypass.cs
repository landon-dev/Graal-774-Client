using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Graal_774_Client.Base.Client
{
    public class UnityBypass
    {
        // prevents the game from self scanning while memory writing is executing
        public class Suspender
        {
            static List<string> susProc = new List<string>();
            private enum ThreadAccess : int
            {
                TERMINATE = (0x1),
                SUSPEND_RESUME = (0x2),
                GET_CONTEXT = (0x8),
                SET_CONTEXT = (0x10),
                SET_INFORMATION = (0x20),
                QUERY_INFORMATION = (0x40),
                SET_THREAD_TOKEN = (0x80),
                IMPERSONATE = (0x100),
                DIRECT_IMPERSONATION = (0x200)
            }

            [DllImport("kernel32.dll")]
            private static extern IntPtr OpenThread(ThreadAccess dwDesriedAccess, bool bInheritHandle, int dwThreadId);
            [DllImport("kernel32.dll")]
            private static extern uint SuspendThread(IntPtr hThread);
            [DllImport("kernel32.dll")]
            private static extern uint ResumeThread(IntPtr hThread);
            [DllImport("kernel32.dll")]
            public static extern bool CloseHandle(IntPtr hHandle);

            private static Process[] processesByName = Process.GetProcessesByName("CefSharp.BrowserSubprocess");
            public static void Suspend()
            {
               
                for (int i = 0; i < processesByName.Length; i++)
                {
                    _Suspend(processesByName[i]);
                }
            }

            private static void _Suspend(Process proc)
            {

                foreach (ProcessThread t in proc.Threads)
                {
                    IntPtr th;
                    th = OpenThread(ThreadAccess.SUSPEND_RESUME, false, t.Id);
                    if (th != IntPtr.Zero)
                    {
                        SuspendThread(th);
                        CloseHandle(th);
                    }
                }
            }

            public static void Resume()
            {
                for (int i = 0; i < processesByName.Length; i++)
                {
                    _Resume(processesByName[i]);
                }
            }

            private static void _Resume(Process proc)
            {
                foreach (ProcessThread t in proc.Threads)
                {
                    IntPtr th;
                    th = OpenThread(ThreadAccess.SUSPEND_RESUME, false, t.Id);
                    if (th != IntPtr.Zero)
                    {
                        ResumeThread(th);
                        CloseHandle(th);
                    }
                }
            }

        }
    }
}
