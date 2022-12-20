using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public class BankA : IAccountTypes<ObservableCollection<Account>>, ILogicOfTransferring<string>
    {
        private static string Property;
        private static string ChoosingActions;

        private static IAccountTypes<ObservableCollection<Account>> bankA = new BankA();
        public static string PathAccountsFile = "AccountsDetails.json";
        public static ObservableCollection<Account> AccountsList = new ObservableCollection<Account>();
        private static Account account1 = new Account();
        private static Account account2 = new Account();

        #region События и их логика
        //событие вызова оповещений во время различных операций
        public static event Action<string> Alerts = Msg;

        /// <summary>
        /// Метод окошка оповещения при различных операциях
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static void Msg(string msg)
        {
            MessageBox.Show($"{msg}");
        }

        //событие ведения журнала действий
        private static event Action<string, string> ActionLog = actionLog;

        /// <summary>
        /// Метод логики ведения журнала действий
        /// </summary>
        private static void actionLog(string Property, string ChoosingActions)
        {
            AccountsList = new ObservableCollection<Account>
                (AccountsList.OrderBy(account => account));

            WorkWithFiles.Serializer(PathAccountsFile, AccountsList, Property, ChoosingActions);
        }
        #endregion

        #region Методы
        /// <summary>
        /// Метод создания номера счета
        /// </summary>
        /// <returns></returns>
        private static string CreatingBankAccountNumber()
        {
            string s = "", symb = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rnd = new Random();

            for (int i = 0; i < 5; i++)
                s += symb[rnd.Next(0, symb.Length)];
            return s;
        }

        /// <summary>
        /// Метод создания банковского счета клиента
        /// </summary>
        /// <param name="PassportDetailsClient"></param>
        /// <param name="AccounTypeClient"></param>
        /// <param name="AmountOfMoneyClient"></param>
        /// <returns></returns>
        public static void OpeningNewAccount(string PassportDetails, string AccountType, double AmountOfMoney)
        {
            if (PassportDetails != "" && PassportDetails != " " && AccountType != "" && AccountType != " ")
            {
                AccountsList = WorkWithFiles.Deserializer(PathAccountsFile, AccountsList);

                string BankAccountNumber = CreatingBankAccountNumber();

                uint value = 0;

                foreach (var item in AccountsList)
                {
                    if (item.PassportDetailsClient == PassportDetails && item.AccountType == "Credit account")
                    {
                        value ++;
                    }     
                }

                if (value == 0 || AccountType == "Debit account")
                {
                   LogicOpeningAccount(PassportDetails, AccountType, AmountOfMoney, BankAccountNumber);
                }
                else Alerts?.Invoke("Кредитный счет уже существует.");  
            }
            else Alerts?.Invoke($"Заполните все поля");
        }

        /// <summary>
        /// Метод логики открытия счета
        /// </summary>
        /// <param name="PassportDetails"></param>
        /// <param name="AccounType"></param>
        /// <param name="AmountOfMoney"></param>
        /// <param name="BankAccountNumber"></param>
        /// <returns></returns>
        private static void LogicOpeningAccount(string PassportDetails, string AccountType,
            double AmountOfMoney, string BankAccountNumber)
        {
            AccountsList.Add(new Account(AccountType, BankAccountNumber, PassportDetails, Math.Round(AmountOfMoney, 2),
                DateTime.Now));

            ActionLog?.Invoke("Account", "Открытие счета"); Alerts?.Invoke("Счет открыт");    
        }

        /// <summary>
        /// Метод удаления счета конкретного клиента
        /// </summary>
        /// <param name="BaseAccountData"></param>
        /// <returns></returns>
        public static void ClosingAccount(Account BaseAccountData)
        {
            AccountsList = WorkWithFiles.Deserializer(PathAccountsFile, AccountsList);

            LogicClosingAccount(BaseAccountData);
        }

        /// <summary>
        /// Метод логики закрытия счета
        /// </summary>
        /// <param name="BaseAccountData"></param>
        /// <returns></returns>
        private static void LogicClosingAccount(Account BaseAccountData)
        {
            foreach (var item in AccountsList)
            {
                //проверка на соответствие данных
                if (item.AccountType == BaseAccountData.AccountType &&
                    item.BankAccountNumber == BaseAccountData.BankAccountNumber &&
                    item.PassportDetailsClient == BaseAccountData.PassportDetailsClient &&
                    item.AmountOfMoney == BaseAccountData.AmountOfMoney)
                {
                    //проверка типа и остатка счета
                    if (item.AccountType == "Credit account" && item.AmountOfMoney < 0)
                    {
                        Alerts?.Invoke("Погасите долг перед закрытием счета"); break;   
                    }
                    else
                    {
                        AccountsList.Remove(item);

                        ActionLog?.Invoke("Account", "Закрытие счета"); Alerts?.Invoke("Данные удалены"); break;     
                    }
                }
            }
        }

        /// <summary>
        /// Метод отбора данных счетов конкретного клиента
        /// </summary>
        /// <param name="PassportDetails"></param>
        /// <returns></returns>
        public static ObservableCollection<Account> DataSampling(string PassportDetails)
        {
            //временный список счетов
            ObservableCollection<Account> TemporaryListAccounts = new ObservableCollection<Account>();
            AccountsList = WorkWithFiles.Deserializer(PathAccountsFile, AccountsList);

            foreach (var item in AccountsList)
            {
                if (item.PassportDetailsClient == PassportDetails)
                {
                    TemporaryListAccounts.Add(item);
                }
            }
            return TemporaryListAccounts;
        }

        /// <summary>
        /// Метод пополнения счета и вывода средств со счета
        /// </summary>
        public static void ReplenishmentOrWithdrawalOfMoney(Account BaseAccountData, double amountOfMoney,
            string value)
        {
            AccountsList = WorkWithFiles.Deserializer(PathAccountsFile, AccountsList);

            foreach (var item in AccountsList)
            {
                if (item.AmountOfMoney == BaseAccountData.AmountOfMoney &&
                    item.PassportDetailsClient == BaseAccountData.PassportDetailsClient)
                {
                    if (value == "Refill")
                    {
                        Math.Round(item.AmountOfMoney += amountOfMoney, 2);

                        ActionLog?.Invoke("Account", "Пополнение счета");

                        Alerts?.Invoke("Счет пополнен"); break;
                    }
                    else if (value == "Withdrawal")
                    {
                        if (BaseAccountData.AccountType == "Credit account" &&
                            (item.AmountOfMoney - amountOfMoney) >= -5000 ||
                            BaseAccountData.AccountType == "Debit account" && (item.AmountOfMoney - amountOfMoney) >= 0)
                        {
                            Math.Round(item.AmountOfMoney -= amountOfMoney, 2);

                            ActionLog?.Invoke("Account", "Вывод средств со счета");
  
                            Alerts?.Invoke("Вывод совершен"); break;
                        }
                        else Alerts?.Invoke("Недостаточно средств"); break;
                    }
                }
            }
        }

        /// <summary>
        /// Метод перевода средств между счетами клиента
        /// </summary>
        /// <param name="AmountOfMoney"></param>
        /// <param name="NumberTheFirstAccount"></param>
        /// <param name="NumberTheSecondAccount"></param>
        /// <returns></returns>
        public static void TransferMoney(double AmountOfMoney,
            string NumberAccount1, string NumberAccount2)
        {
            AccountsList = WorkWithFiles.Deserializer(PathAccountsFile, AccountsList);

            //перебор коллекции на существование счетов
            account1 = DataSearch(NumberAccount1, account1);
            account2 = DataSearch(NumberAccount2, account2);

            //обработка условий
            if (account1.PassportDetailsClient == account2.PassportDetailsClient)
            { 
                LogicTransferMoney(ref AmountOfMoney, ref account1, ref account2);               
            }
            else Alerts?.Invoke("Здесь есть не ваш счет");
        }

        /// <summary>
        /// Метод логики перевода средств между счетами
        /// </summary>
        /// <param name="AmountOfMoney"></param>
        /// <param name="PassportDetailsTheFirstAccount"></param>
        /// <param name="AmountOfMoneyTheFirstAccount"></param>
        /// <param name="OldAmount1"></param>
        /// <param name="PassportDetailsTheSecondAccount"></param>
        /// <param name="AmountOfMoneyTheSecondAccount"></param>
        /// <param name="OldAmount2"></param>
        /// <returns></returns>
        private static void LogicTransferMoney(ref double AmountOfMoney, ref Account account1, ref Account account2)
        {
            foreach (var item in AccountsList)
            {
                //при переводе с кредитного счета
                if (account1.AccountType == "Credit account" && account1.AmountOfMoney > -5000 &&
                        account1.AmountOfMoney - AmountOfMoney >= -5000 &&
                        item.BankAccountNumber == account1.BankAccountNumber)
                {
                    Math.Round(account2.AmountOfMoney += AmountOfMoney, 2);

                    AccountsList = bankA.LogicCreditAccount(ref AmountOfMoney, ref account1);

                    //обновление нынешнего количества денег на счетах
                    AccountsList = DataSearch(account2);

                    ActionLog?.Invoke("Account", "Перевод средств");

                    Alerts?.Invoke("Перевод удался"); break;
                    
                }

                //при переводе с дебитового счета
                else if (account1.AccountType == "Debit account" && account1.AmountOfMoney > 0 &&
                        account1.AmountOfMoney - AmountOfMoney >= 0 &&
                        item.BankAccountNumber == account1.BankAccountNumber)
                {
                    Math.Round(account1.AmountOfMoney -= AmountOfMoney, 2);
                    Math.Round(account2.AmountOfMoney += AmountOfMoney, 2);

                    AccountsList = DataSearch(account1);
                    AccountsList = DataSearch(account2);

                    ActionLog?.Invoke("Account", "Перевод средств");

                    Alerts?.Invoke("Перевод удался"); break;  
                }
                else Alerts?.Invoke("Недостаточно средств на счету");
            }
        }

        /// <summary>
        /// Метод перебора коллекции
        /// </summary>
        /// <param name="NumberTheFirstAccount"></param>
        /// <param name="NumberTheSecondAccount"></param>
        /// <param name="PassportDetailsTheFirstAccount"></param>
        /// <param name="AmountOfMoneyTheFirstAccount"></param>
        /// <param name="PassportDetailsTheSecondAccount"></param>
        /// <param name="AmountOfMoneyTheSecondAccount"></param>
        private static Account DataSearch(string NumberAccount, Account account)
        {
            foreach (var item in AccountsList)
            {
                if (NumberAccount == item.BankAccountNumber)
                {
                    account = new Account(item.AccountType, item.BankAccountNumber, item.PassportDetailsClient,
                        item.AmountOfMoney, item.AccountOpeningDate);
                }
            }

            return account;
        }

        /// <summary>
        /// Перегрузка метода перебора коллекции
        /// </summary>
        /// <param name="OldAmount"></param>
        /// <param name="PassportDetails"></param>
        /// <param name="NewAmountOfMoney"></param>
        private static ObservableCollection<Account> DataSearch(Account account)
        {
            foreach (var item in AccountsList)
            {
                if (item.PassportDetailsClient == account.PassportDetailsClient &&
                    item.BankAccountNumber == account.BankAccountNumber)
                {
                    item.AmountOfMoney = account.AmountOfMoney;
                    break;  
                }
            }
            return AccountsList;
        }

        public ObservableCollection<Account> LogicDepossitAccount()
        {
            return AccountsList;
        }

        public ObservableCollection<Account> LogicCreditAccount()
        {
            AccountsList = WorkWithFiles.Deserializer(PathAccountsFile, AccountsList);

            foreach (var item in AccountsList)
            {
                if (item.AccountType == "Credit account" && item.AmountOfMoney < 0 && ChoosingActions is null)
                {          
                    item.AmountOfMoney = Math.Round(item.AmountOfMoney + item.AmountOfMoney * 0.15, 2);
                    break;
                } 
            }

            WorkWithFiles.Serializer(PathAccountsFile, AccountsList);

            return AccountsList;
        }

        public ObservableCollection<Account> LogicCreditAccount(ref double AmountOfMoney, ref Account account)
        {
            AccountsList = WorkWithFiles.Deserializer(PathAccountsFile, AccountsList);

            foreach (var item in AccountsList)
            {
                if (item.AccountType == "Credit account" && item.BankAccountNumber == account.BankAccountNumber)
                {
                    item.AmountOfMoney = account.AmountOfMoney + Math.Round(-AmountOfMoney - AmountOfMoney * 0.03, 2);
                    break; 
                }
            }

            return AccountsList;
        }

        public void LogicOfTransferring(double AmountOfMoney, string NumberAccount1, string NumberAccount2)
        {
            AccountsList = WorkWithFiles.Deserializer(PathAccountsFile, AccountsList);

            //перебор коллекции на существование счетов
            account1 = DataSearch(NumberAccount1, account1);
            account2 = DataSearch(NumberAccount2, account2);

            LogicTransferMoney(ref AmountOfMoney, ref account1, ref account2);
        }
        #endregion
    }
}
