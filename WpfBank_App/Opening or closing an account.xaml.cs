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
    /// Логика взаимодействия для OpeningOrClosingAnAccount.xaml
    /// </summary>
    public partial class OpeningOrClosingAnAccount : Window
    {
        private static Client BaseClientData = new Client(); //базовые данные клиента

        public OpeningOrClosingAnAccount()
        {
            InitializeComponent();
            ListClients_Opening.ItemsSource = WorkWithFiles.AddDetailsToCollection<Client>
                (WorkWithFiles.VerificationVariable = "Client");
            ListClients_Closing.ItemsSource = WorkWithFiles.AddDetailsToCollection<Client>
                (WorkWithFiles.VerificationVariable = "Client");
        }

        /// <summary>
        /// Кнопка открытия счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OpenNewAccount(object sender, RoutedEventArgs e)
        {
            BaseClientData = (Client)ListClients_Opening.SelectedItem;

            if (BaseClientData != null)
            {
                if (double.TryParse(amountOfMoney.Text, out double number) && Convert.ToDouble(amountOfMoney.Text) >= 0)
                {
                    BankA.OpeningNewAccount(BaseClientData.PassportDetails, accountType.Text,
                        Convert.ToDouble(amountOfMoney.Text));

                    ListClients_Opening.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Значение в поле 'Amount of money' введено неверно");
                }       
            }
            else MessageBox.Show("Выберите клиента для открытия счета");
        }

        /// <summary>
        /// Кнопка закрытия счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClosingAccount(object sender, RoutedEventArgs e)
        {
            Account BaseAccountData = (Account)ListAccount.SelectedItem;

            if (BaseAccountData != null)
            {
                BankA.ClosingAccount(BaseAccountData);
            }
            else MessageBox.Show("Выберите счет");
        }

        /// <summary>
        /// Кнопка показа счетов конкретного клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ShowAccounts(object sender, RoutedEventArgs e)
        {
            BaseClientData = (Client)ListClients_Closing.SelectedItem;

            if (BaseClientData != null)
            {
                ListAccount.ItemsSource = BankA.DataSampling(BaseClientData.PassportDetails);
            }
            else MessageBox.Show("Выберите клиента для открытия счета");   
        }
    }
}
