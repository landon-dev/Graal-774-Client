using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal_774_Client.Base.CE
{
    public class Server
    {

        public static ulong Execute(string serverName, string cmdString)
        {
            NamedPipeClientStream s = new NamedPipeClientStream(serverName);
            s.Connect();

            if (s.IsConnected)
            {
                byte[] result = new byte[8];
                byte[] command = Encoding.ASCII.GetBytes(cmdString);
                byte[] size = BitConverter.GetBytes((int)command.Length);

                byte[] fullcommand = new byte[1];

                fullcommand[0] = 1; // execute string
                fullcommand = fullcommand.Concat(size).ToArray();
                fullcommand = fullcommand.Concat(command).ToArray();

                fullcommand = fullcommand.Concat(BitConverter.GetBytes((long)0)).ToArray(); //the 'parameter' value

                s.Write(fullcommand, 0, fullcommand.Length);

                s.Read(result, 0, 8);

                s.Close();

                return BitConverter.ToUInt64(result, 0);
            }
            else
                return 0;
        }
    }
}
