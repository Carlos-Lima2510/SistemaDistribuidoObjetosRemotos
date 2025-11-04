using System;

namespace Conversor
{
    public class Conversor : MarshalByRefObject, IConversor
    {
        private const double eurToUsdRate = 1.1;
        private const double usdToEurRate = 0.9;
        public double ConvertEurToUsd(double euros)
        {
            Console.WriteLine($"Convirtiendo {euros} EUR a USD");
            return euros * eurToUsdRate;
        }
        public double ConvertUsdToEur(double dolares)
        {
            Console.WriteLine($"Convirtiendo {dolares} USD a EUR");
            return dolares * usdToEurRate;
        }
    }
}
