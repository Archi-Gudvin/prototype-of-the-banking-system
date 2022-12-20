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
    public class WorkWithFiles
    {
        public static string PathHistory = "History_of_changes.txt";
        public static string VerificationVariable = "";

        /// <summary>
        /// Проверка файла на существование
        /// </summary>
        public static void ExistFile(string Path)
        {
            if (File.Exists(Path))
            {
                return;
            }
            else
            {
                FileStream fileStream = new FileStream(Path, FileMode.OpenOrCreate);
            }
        }

        /// <summary>
        /// Метод сериализации JSON-файла
        /// </summary>
        public static void Serializer<T>(string PathFile, ObservableCollection<T> list,
            string Property, string ChoosingActions, string Worker)
        {
            ExistFile(PathFile);
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(PathFile, json);
            HistoryOfChangingData(Property, ChoosingActions, Worker);
        }

        public static void Serializer<T>(string PathFile, ObservableCollection<T> list,
            string Property, string ChoosingActions)
        {
            ExistFile(PathFile);
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(PathFile, json);
            HistoryOfChangingData(Property, ChoosingActions);
        }

        public static void Serializer<T>(string PathFile, ObservableCollection<T> list)
        {
            ExistFile(PathFile);
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(PathFile, json);
        }

        /// <summary>
        /// Метод десериализации JSON-файла
        /// </summary>
        public static ObservableCollection<T> Deserializer<T>(string PathFile, ObservableCollection<T> list)
        {
            ExistFile(PathFile);

            string json = File.ReadAllText(PathFile);

            list = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);

            return list;
        }

        /// <summary>
        /// Метод обработки истории изменений в данных клиентов
        /// </summary>
        /// <param name="property"></param>
        /// <param name="baseValue"></param>
        /// <param name="newValue"></param>
        /// <param name="choosingActions"></param>
        /// <param name="worker"></param>
        public static void HistoryOfChangingData(string Property, string ChoosingActions, string Worker)
        {
            ExistFile(PathHistory);

            List<string> history = new List<string>();

            history.Add($"{DateTime.Now} - {Property}  - {ChoosingActions} - {Worker}");

            File.AppendAllLines(PathHistory, history);
        }

        public static void HistoryOfChangingData(string Property, string ChoosingActions)
        {
            ExistFile(PathHistory);

            List<string> history = new List<string>();

            history.Add($"{DateTime.Now} - {Property}  - {ChoosingActions}");

            File.AppendAllLines(PathHistory, history);
        }

        /// <summary>
        /// Метод вывода файла "История изменений"
        /// </summary>
        /// <returns></returns>
        public static object PrintHistory()
        {
            string printList = "";

            printList = File.ReadAllText(PathHistory);

            if (printList == "" || printList == " ")
            {
                printList = "Файл пуст";
            }

            return printList;
        }

        /// <summary>
        /// Метод добавления данных в коллекцию
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<T> AddDetailsToCollection<T>(string VerificationVariable)
            where T : Client
        {
            ObservableCollection<T> ListData = new ObservableCollection<T>();

            if (VerificationVariable == "Client")
            {
                ListData = WorkWithFiles.Deserializer(Worker.PathClientsFile, ListData);
            }
            else if (VerificationVariable == "Account")
            {
                ListData = WorkWithFiles.Deserializer(BankA.PathAccountsFile, ListData);
            }
            else if (VerificationVariable == "Client(Consultant)")
            {
                ListData = WorkWithFiles.Deserializer(Worker.PathClientsFile, ListData);
                
                foreach (var item in ListData)
                {
                    item.PassportDetails = "***********";      
                }
            }
            return ListData;
        }
    }
}
