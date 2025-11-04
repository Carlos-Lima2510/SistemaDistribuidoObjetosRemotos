using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Configuration;

namespace Servidor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int port = Int32.Parse(ConfigurationManager.AppSettings["port"]);
            TcpChannel channel = new TcpChannel(port);
            ChannelServices.RegisterChannel(channel, false);

            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Conversor.Conversor),
                "ConversorService",
                WellKnownObjectMode.SingleCall);

            Console.WriteLine($"Servidor Remoting iniciado en puerto {port}...");
            Console.WriteLine("Esperando peticiones...");
            Console.ReadLine();
        }
    }
}
