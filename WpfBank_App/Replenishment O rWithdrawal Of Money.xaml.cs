using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClassLibrary1;

namespace WpfBank_App
{
    /// <summary>
    /// Логика взаимодействия для MoneyTransfers.xaml
    /// </summary>
    public partial class ReplenishmentOrWithdrawalOfMoney : Window
    {
        public ReplenishmentOrWithdrawalOfMoney()
        {
            InitializeComponent();
            ListClients_replenishment.ItemsSource = WorkWithFiles.AddDetailsToCollection<Client>
                (WorkWithFiles.VerificationVariable = "Client");
            ListClients_withdrawal.ItemsSource = WorkWithFiles.AddDetailsToCollection<Client>
                (WorkWithFiles.VerificationVariable = "Client");
        }

        /// <summary>
        /// Логика кнопки вывода существующих счетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ShowAccountsReplenishment(object sender, RoutedEventArgs e)
        {
            Client BaseClientData = (Client)ListClients_replenishment.SelectedItem;

            if (BaseClientData != null)
            {
                ListAccounts_replenishment.ItemsSource = BankA.DataSampling(BaseClientData.PassportDetails);
            }
            else MessageBox.Show("Выберите клиента для просмотра счета");
        }

        /// <summary>
        /// Логика кнопки вывода существующих счетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ShowAccountsWithdrawal(object sender, RoutedEventArgs e)
        {
            Client BaseClientData = (Client)ListClients_withdrawal.SelectedItem;

            if (BaseClientData != null)
            {
                ListAccounts_withdrawal.ItemsSource = BankA.DataSampling(BaseClientData.PassportDetails);
            }
            else MessageBox.Show("Выберите клиента для просмотра счета");
        }

        /// <summary>
        /// Логика кнопки пополнения счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Refill(object sender, RoutedEventArgs e)
        {
            string value = "Refill";

            Account BaseAccountData = (Account)ListAccounts_replenishment.SelectedItem;

            if (BaseAccountData != null)
            {
                if (amountOfMoney_replenishment.Text != "")
                {
                    if (double.TryParse(amountOfMoney_replenishment.Text, out double number) &&
                        Convert.ToDouble(amountOfMoney_replenishment.Text) > 0)
                    {
                        BankA.ReplenishmentOrWithdrawalOfMoney(BaseAccountData,
                            Convert.ToDouble(amountOfMoney_replenishment.Text), value);

                        ListClients_replenishment.Items.Refresh();
                    }
                    else MessageBox.Show("Значение в поле 'Amount of money' введено неверно");              
                }
                else MessageBox.Show("Заполните все поля");    
            }
            else MessageBox.Show("Выберите счет");
        }

        /// <summary>
        /// Логика кнопки вывода средств со счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Withdrawal(object sender, RoutedEventArgs e)
        {
            string value = "Withdrawal";

            Account BaseAccountData = (Account)ListAccounts_withdrawal.SelectedItem;

            if (BaseAccountData != null)
            {
                if (amountOfMoney_withdrawal.Text != "")
                {
                    if (double.TryParse(amountOfMoney_withdrawal.Text, out double number) &&
                        Convert.ToDouble(amountOfMoney_withdrawal.Text) > 0)
                    {
                        BankA.ReplenishmentOrWithdrawalOfMoney(BaseAccountData,
                            Convert.ToDouble(amountOfMoney_withdrawal.Text), value);

                        ListClients_withdrawal.Items.Refresh();
                    }
                    else MessageBox.Show("Значение в поле 'Amount of money' введено неверно");
                }
                else MessageBox.Show("Заполните все поля");
            }
            else MessageBox.Show("Выберите счет");
        }
    }
}
