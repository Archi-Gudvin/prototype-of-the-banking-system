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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public class Manager : Worker, IEditDetails
    {
        private static string manager = "Менеджер";
        private static string Property;
        private static string ChoosingActions;

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
            ClientsList = new ObservableCollection<Client>
                    (ClientsList.OrderBy(client => client));

            WorkWithFiles.Serializer(PathClientsFile, ClientsList, Property, ChoosingActions, manager);
        }
        #endregion

        #region Методы
        /// <summary>
        /// Метод добавления клиента
        /// </summary>
        /// <param name="lastname"></param>
        /// <param name="firstname"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passportDetails"></param>
        /// <returns></returns>
        public static void AddClient(string lastname, string firstname, string patronymic, 
            string phoneNumber, string passportDetails)
        {
            if (lastname != "" && lastname != " " && firstname != "" && firstname != " " &&
                patronymic != "" && patronymic != " " && phoneNumber != "" && phoneNumber != " " &&
                passportDetails != "" && passportDetails != " ")
            {
                ClientsList = WorkWithFiles.Deserializer(PathClientsFile, ClientsList);

                ClientsList.Add(new Client(lastname, firstname, patronymic, phoneNumber, passportDetails));

                ActionLog?.Invoke("Client", "Добавление клиента"); Alerts?.Invoke("Данные добавились");
            }
            else Alerts?.Invoke("Заполните все поля");   
        } 

        public void EditClientDetails(Client BaseClientData, Client NewClientData)
        {
            ClientsList = WorkWithFiles.Deserializer(PathClientsFile, ClientsList);

            foreach (var clientsList in ClientsList)
            {
                //проверка на соответствие условию выбора данных
                if (clientsList.PhoneNumber == BaseClientData.PhoneNumber &&
                    clientsList.Lastname == BaseClientData.Lastname)
                {
                    if (NewClientData.Lastname != "" && NewClientData.Firstname != "" && NewClientData.Patronymic != "" &&
                        NewClientData.PhoneNumber != "" && NewClientData.PassportDetails != "")
                    {
                        clientsList.Lastname = NewClientData.Lastname;
                        clientsList.Firstname = NewClientData.Firstname;
                        clientsList.Patronymic = NewClientData.Patronymic;
                        clientsList.PhoneNumber = NewClientData.PhoneNumber;
                        clientsList.PassportDetails = NewClientData.PassportDetails;

                        ActionLog?.Invoke("Client", "Изменение данных");
                        Alerts?.Invoke("Данные изменены"); break;
                    }
                    else Alerts?.Invoke("Заполните данные"); break;              
                }         
            }
        }

        /// <summary>
        /// метод удаления данных клиентов
        /// </summary>
        /// <param name="value"></param>
        public void DeleteDetails(Client BaseClientData)
        {
            ClientsList = WorkWithFiles.Deserializer(PathClientsFile, ClientsList);

            foreach (var item in ClientsList)
            {
                //проверка на null
                if (BaseClientData is null)
                {
                    Alerts?.Invoke("Выберите клиента"); break;
                }
                //проверка на соответствие условию выбора данных
                else if (item.PhoneNumber == BaseClientData.PhoneNumber &&
                    item.Lastname == BaseClientData.Lastname && item.Firstname == BaseClientData.Firstname && 
                    item.Patronymic == BaseClientData.Patronymic)
                {
                    ClientsList.Remove(item);

                    ActionLog?.Invoke("Client", "Удаление данных");
                    Alerts?.Invoke("Данные удалены"); break; 
                }
            }
        }
        #endregion
    }
}
