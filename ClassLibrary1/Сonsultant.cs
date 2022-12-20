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
    public class Consultant : Worker, IEditDetails
    {
        private static string consultant = "Консультант";
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

            WorkWithFiles.Serializer(PathClientsFile, ClientsList, Property, ChoosingActions, consultant);
        }
        #endregion

        #region Методы
        public void EditClientDetails(Client BaseClientData, Client NewClientData)
        {
            ClientsList = WorkWithFiles.Deserializer(PathClientsFile, ClientsList); //десериализация в основной лист

            foreach (var clientsList in ClientsList)
            {
                if (NewClientData.PhoneNumber == "" || NewClientData.PhoneNumber == " " || 
                    NewClientData.PhoneNumber == null)
                {
                    Alerts?.Invoke("Заполните данные");
                    break;
                }
                //проверка на соответствие условию выбора данных
                else if (clientsList.PhoneNumber == BaseClientData.PhoneNumber &&
                    clientsList.Lastname == BaseClientData.Lastname)
                {   
                    clientsList.PhoneNumber = NewClientData.PhoneNumber;

                    ActionLog?.Invoke("PhoneNumber", "Изменение данных");
                    Alerts?.Invoke("Данные изменены"); break; 
                }  
            }  
        }
        #endregion
    }
}
