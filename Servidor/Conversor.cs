using System;
using System.Threading;

namespace Conversor
{
    public class Conversor : MarshalByRefObject, IConversor
    {
        private const double eurToUsdRate = 1.15;
        private const double usdToEurRate = 0.87;
        public double ConvertEurToUsd(double euros)
        {
            double resultado = 0;

            Thread hilo = new Thread(() =>
            {
                resultado = euros * eurToUsdRate;
                Console.WriteLine($"Convertido {euros} EUR a {resultado} USD");
            });

            hilo.Start();
            hilo.Join();

            return resultado;

        }
        public double ConvertUsdToEur(double dolares)
        {
            double resultado = 0;

            Thread hilo = new Thread(() =>
            {
                resultado = dolares * usdToEurRate;
                Console.WriteLine($"Convertido {dolares} USD a {resultado} EUR");
            });

            hilo.Start();
            hilo.Join();

            return resultado;
        }
    }
}
