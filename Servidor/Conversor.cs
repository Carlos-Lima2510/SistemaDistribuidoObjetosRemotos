using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Conversor
{
    public class Conversor : MarshalByRefObject, IConversor
    {
        private const double eurToUsdRate = 1.1;
        private const double usdToEurRate = 0.9;
        public double ConvertEurToUsd(double euros)
        {
            Console.WriteLine($"Convirtiendo {euros} EUR → USD");
            return euros * eurToUsdRate;
        }
        public double ConvertUsdToEur(double dolares)
        {
            Console.WriteLine($"Convirtiendo {dolares} USD → EUR");
            return dolares * usdToEurRate;
        }
    }
}
