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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using ClassLibrary1;

namespace WpfBank_App
{
    /// <summary>
    /// Логика взаимодействия
    /// </summary>
    public partial class ManagerChangingClientsData : Window
    {
        private static Manager manager = new Manager();
        private static Client BaseClientData = new Client(); //базовые данные клиента

        /// <summary>
        /// Окно кнопки редактирования
        /// </summary>
        public ManagerChangingClientsData()
        {
            InitializeComponent();
            ListEditingClients.ItemsSource = WorkWithFiles.AddDetailsToCollection<Client>
                (WorkWithFiles.VerificationVariable = "Client");
            ListDeletingClient.ItemsSource = WorkWithFiles.AddDetailsToCollection<Client>
                (WorkWithFiles.VerificationVariable = "Client");
        }

        /// <summary>
        /// Кнопка для редактирования данных 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            BaseClientData = (Client)ListEditingClients.SelectedItem;
            Client NewClientData = new Client();

            if (BaseClientData is null)
            {
                MessageBox.Show($"Выберите клиента");
            }
            else
            {
                try
                {
                    if (passportDetails.Text.Length == 9)
                    {
                        //новые данные клиента
                        NewClientData = new Client(lastname.Text, firstname.Text, patronymic.Text, phoneNumber.Text,
                            passportDetails.Text);

                        manager.EditClientDetails(BaseClientData, NewClientData); 
                    }
                    else
                    {
                        throw new NotEnoughSignsException($"Неверное кол-во символов в поле 'Passport Details' " +
                            $"- {passportDetails.Text.Length}. Должно быть 9 символов.");
                    }
                }
                catch (NotEnoughSignsException exc)
                {
                    MessageBox.Show($"{exc.Message}");
                }  
            }  
        }

        /// <summary>
        /// Кнопка для удаления данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            BaseClientData = (Client)ListDeletingClient.SelectedItem;
            manager.DeleteDetails(BaseClientData);
        }
    }
}
