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
    /// Логика взаимодействия
    /// </summary>
    public partial class ConsultantChangingClientsData : Window
    {
        private static Consultant consultant = new Consultant();
        private static Client BaseClientData = new Client();

        public ConsultantChangingClientsData()
        {
            InitializeComponent();
            ListEditingClients.ItemsSource = WorkWithFiles.AddDetailsToCollection<Client>
                (WorkWithFiles.VerificationVariable = "Client(Consultant)");
        }

        /// <summary>
        /// Кнопка изменения номера телефона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            //клиент, выбранный из списка
            BaseClientData = (Client)ListEditingClients.SelectedItem;

            //новые данные номера телефона
            Client NewClientData = new Client();

            NewClientData = new Client(phoneNumber.Text, phoneNumber.Text, phoneNumber.Text,
                phoneNumber.Text, phoneNumber.Text);
           
            if (BaseClientData is null)//проверка на null
            {
                MessageBox.Show($"Выберите клиента");
            }
            else 
            {
                NewClientData = new Client(phoneNumber.Text, phoneNumber.Text, phoneNumber.Text,
                    phoneNumber.Text, phoneNumber.Text);

                consultant.EditClientDetails(BaseClientData, NewClientData);
            }  
        }
    }
}
