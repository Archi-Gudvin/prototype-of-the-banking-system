using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface ILogicOfTransferring<in T>
    {
        void LogicOfTransferring(double AmountOfMoney, T NumberAccount1, T NumberAccount2);
    }
}
