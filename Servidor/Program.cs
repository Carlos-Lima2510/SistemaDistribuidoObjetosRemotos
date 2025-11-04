using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Servidor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpChannel channel = new TcpChannel(8085);
            ChannelServices.RegisterChannel(channel, false);

            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Conversor.Conversor),
                "ConversorService",
                WellKnownObjectMode.SingleCall);

            Console.WriteLine("Servidor Remoting iniciado en puerto 8085...");
            Console.WriteLine("Esperando peticiones...");
            Console.ReadLine();
        }
    }
}
