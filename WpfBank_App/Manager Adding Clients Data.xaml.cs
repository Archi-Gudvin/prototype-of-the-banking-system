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
    public partial class ManagerAddingClientsData : Window
    {
        public ManagerAddingClientsData()
        {
            InitializeComponent();
            ListAddingClients.ItemsSource = WorkWithFiles.AddDetailsToCollection<Client>
                (WorkWithFiles.VerificationVariable = "Client");
        }

        private void Button_AddToFile(object sender, RoutedEventArgs e)
        {
            try
            {
                if (passportDetails.Text.Length == 9)
                {
                    Manager.AddClient(lastname.Text, firstname.Text, patronymic.Text, phoneNumber.Text, passportDetails.Text);
                    ListAddingClients.Items.Refresh();
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
}
