using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace ClassLibrary1
{
    public interface IAccountTypes<out T>
    {
        /// <summary>
        /// Метод логики депозитного счета
        /// </summary>
        /// <param name="AmountOfMoney"></param>
        /// <returns></returns>
        T LogicDepossitAccount();

        /// <summary>
        /// Метод логики кредитного счета
        /// </summary>
        /// <param name="AmountOfMoney"></param>
        /// <returns></returns>
        T LogicCreditAccount();

        /// <summary>
        /// Метод логики кредитного счета
        /// </summary>
        /// <param name="AmountOfMoney"></param>
        /// <param name="NumberAccount"></param>
        /// <returns></returns>
        T LogicCreditAccount(ref double AmountOfMoney, ref Account account1);
    }
}
