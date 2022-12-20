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
    /// Логика взаимодействия для Window5.xaml
    /// </summary>
    public partial class ViewingAccountsData : Window
    {
        public ViewingAccountsData()
        {
            InitializeComponent();
            ListClients.ItemsSource = WorkWithFiles.AddDetailsToCollection<Client>(WorkWithFiles.VerificationVariable = "Client");
        }

        /// <summary>
        /// Кнопка показа счетов конкретного клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ShowAccounts(object sender, RoutedEventArgs e)
        {
            Client BaseClientData = (Client)ListClients.SelectedItem;

            if (BaseClientData != null)
            {
                ListAccounts.ItemsSource = BankA.DataSampling(BaseClientData.PassportDetails);
            }
            else MessageBox.Show("Выберите клиента для просмотра счета");
        }
    }
}
