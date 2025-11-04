using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversor
{
    public interface IConversor
    {
        double ConvertEurToUsd(double euros);
        double ConvertUsdToEur(double dolares);
    }
}
