using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Account : IComparable
    {
        #region Поля

        private string accountType;

        private string bankAccountNumber;

        private double amountOfMoney;

        private DateTime accountOpeningDate;

        private string passportDetailsClient;

        #endregion

        #region Свойства
        /// <summary>
        /// Тип банковского счета
        /// </summary>
        public string AccountType { get { return this.accountType; } set { this.accountType = value; } }

        /// <summary>
        /// Номер банковского счета
        /// </summary>
        public string BankAccountNumber { get { return this.bankAccountNumber; } set { this.bankAccountNumber = value; } }

        /// <summary>
        /// Состояние счета
        /// </summary>
        public double AmountOfMoney { get { return this.amountOfMoney; } set { this.amountOfMoney = value; } }

        public DateTime AccountOpeningDate { get { return this.accountOpeningDate; } set { this.accountOpeningDate = value; } }

        /// <summary>
        /// Серия, номер паспорта
        /// </summary>
        public string PassportDetailsClient { get { return this.passportDetailsClient; } set { this.passportDetailsClient = value; } }
        #endregion

        #region Конструкторы
        public Account(string AccountType, string BankAccountNumber, string PassportDetailsClient, double AmountOfMoney, DateTime AccountOpeningDate)
        {
            this.accountType = AccountType;
            this.bankAccountNumber = BankAccountNumber;
            this.passportDetailsClient = PassportDetailsClient;
            this.amountOfMoney = AmountOfMoney;
            this.accountOpeningDate = AccountOpeningDate;
        }

        public Account() : this("", "", "", 0, new DateTime())
        {
        }

        #endregion

        #region Методы
        /// <summary>
        /// Информация о счете
        /// </summary>
        /// <returns></returns>
        public string AccountInformation()
        {
            return String.Format("AccountType:{0,10} | BankAccountNumber:{1,7} | PassportDetailsClient:{2,7} | " +
                "AmountOfMoney:{3,5} | AccountOpeningDate:{4,10} |",
                this.AccountType,
                this.BankAccountNumber,
                this.PassportDetailsClient,
                this.AmountOfMoney,
                this.AccountOpeningDate); ;
        }

        public int CompareTo(object obj)
        {
            Account account = obj as Account;
            if (account == null)
            {
                throw new ArgumentException("Object is not Account");
            }
            return this.PassportDetailsClient.CompareTo(account.PassportDetailsClient);
        }
        #endregion
    }
}
