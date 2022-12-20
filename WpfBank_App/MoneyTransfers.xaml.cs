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
    public partial class MoneyTransfers : Window
    {
        public MoneyTransfers()
        {
            InitializeComponent();
            ListClients.ItemsSource = WorkWithFiles.AddDetailsToCollection<Client>
               (WorkWithFiles.VerificationVariable = "Client");
            ListClients1.ItemsSource = WorkWithFiles.AddDetailsToCollection<Client>
               (WorkWithFiles.VerificationVariable = "Client");
        }

        private void Button_ShowAccounts(object sender, RoutedEventArgs e)
        {
            Client BaseClientData = (Client)ListClients.SelectedItem;
            if (BaseClientData != null)
            {
                ListAccounts.ItemsSource = BankA.DataSampling(BaseClientData.PassportDetails);
            }
            else MessageBox.Show("Выберите клиента для просмотра счета");
        }

        /// <summary>
        /// Кнопка для перевода средств на свой счет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_TransferToYourAccount(object sender, RoutedEventArgs e)
        {  
            if (AmountOfMoney.Text != "" && NumberTheFirstAccount.Text != "" && NumberTheSecondAccount.Text != "")
            {
                if (double.TryParse(AmountOfMoney.Text, out double number) &&
                       Convert.ToDouble(AmountOfMoney.Text) > 0)  
                {
                    BankA.TransferMoney(Convert.ToDouble(AmountOfMoney.Text),NumberTheFirstAccount.Text,
                        NumberTheSecondAccount.Text);
                    ListClients.Items.Refresh();
                }
                else MessageBox.Show("Значения в полях введены неверно");
                }
            else MessageBox.Show("Заполните все поля");
        }

        private void Button_TransferToOtherAccount(object sender, RoutedEventArgs e)
        {
            BankA bankA = new BankA();

            if (AmountOfMoney2.Text != "" && NumberTheFirstAccount2.Text != "" && NumberTheSecondAccount2.Text != "")
            {
                if (double.TryParse(AmountOfMoney2.Text, out double number) &&
                       Convert.ToDouble(AmountOfMoney2.Text) > 0)
                {
                    bankA.LogicOfTransferring(Convert.ToDouble(AmountOfMoney2.Text), NumberTheFirstAccount2.Text,
                        NumberTheSecondAccount2.Text);

                    ListClients.Items.Refresh();
                }
                else MessageBox.Show("Значения в полях введены неверно");
            }
            else MessageBox.Show("Заполните все поля");
        }

        private void Button_ShowAccounts1(object sender, RoutedEventArgs e)
        {
            Client BaseClientData = (Client)ListClients1.SelectedItem;
            if (BaseClientData != null)
            {
                ListAccounts1.ItemsSource = BankA.DataSampling(BaseClientData.PassportDetails);
            }
            else MessageBox.Show("Выберите клиента для просмотра счета");
        }
    }
}
