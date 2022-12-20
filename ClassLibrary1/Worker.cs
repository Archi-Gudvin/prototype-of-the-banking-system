using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace ClassLibrary1
{
    public class Worker
    {
        //основной лист для храния данных во время работы программы
        public static ObservableCollection<Client> ClientsList = new ObservableCollection<Client>();
        //имя файла для хранения данных
        public static string PathClientsFile = "ClientDetails.json";

        /// <summary>
        /// Метод вывода данных
        /// </summary>
        /// <param name="PathFile"></param>
        /// <param name="list"></param>
        public static object PrintData<T>()
        {
            string printList = "";

            //десериализация во временную коллекцию
            ObservableCollection<Client> newList = WorkWithFiles.Deserializer(PathClientsFile, ClientsList);

            //логика метода
            if (newList != null)
            {
                foreach (var item in newList)
                {
                    //обработка вывода паспортных данных
                    if (item.PassportDetails == " " || item.PassportDetails == "" || item.PassportDetails is null)
                    {
                        item.PassportDetails = "";
                    }
                    else { item.PassportDetails = "******************"; }

                    printList += item.ClientInformation();
                    printList +="\n\n";
                }
            }
            else printList = "Файл пуст";

            return printList;
        }
    }
}
