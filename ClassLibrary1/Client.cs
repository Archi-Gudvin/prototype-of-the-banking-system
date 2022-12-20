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
     public class Client : IComparable
     {
        #region Поля

        private string lastname;

        private string firstname;

        private string patronymic;

        private string phoneNumber;

        private string passportDetails;
        #endregion

        #region Свойства
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Lastname { get { return this.lastname; } set { this.lastname = value; } }

        /// <summary>
        /// Имя
        /// </summary>
        public string Firstname { get { return this.firstname; } set { this.firstname = value; } }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get { return this.patronymic; } set { this.patronymic = value; } }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get { return this.phoneNumber; } set { this.phoneNumber = value; } }

        /// <summary>
        /// Серия, номер паспорта
        /// </summary>
        public string PassportDetails { get { return this.passportDetails; } set { this.passportDetails = value; } }
        #endregion

        #region Конструкторы

        /// <summary>
        /// Создание клиента
        /// </summary>
        /// <param name="Lastname"></param>
        /// <param name="Firstname"></param>
        /// <param name="Patronymic"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="PassportDetails"></param>
        public Client(string Lastname, string Firstname, string Patronymic, string PhoneNumber, string PassportDetails)
        {
            this.lastname = Lastname;
            this.firstname = Firstname;
            this.patronymic = Patronymic;
            this.phoneNumber = PhoneNumber;
            this.passportDetails = PassportDetails;
        }

        /// <summary>
        /// Создание клиента с автопараметрами
        /// </summary>
        public Client() : this("", "", "", "", "")
        {
        }

        #endregion

        #region Методы

        /// <summary>
        /// Информация о клиенте
        /// </summary>
        /// <returns></returns>
        public string ClientInformation()
        {   
            return String.Format("Lastname:{0,5} | Firstname:{1,5} | Patronymic:{2,5} |" +
                " PhoneNumber:{3,5} | PassportDetails:{4,5}",
                this.Lastname,
                this.Firstname,
                this.Patronymic,
                this.PhoneNumber,
                this.PassportDetails);  
        }

        public int CompareTo(object obj)
        {
            Client client = obj as Client;
            if (client == null)
            {
                throw new ArgumentException("Object is not Client");
            }
            return this.Lastname.CompareTo(client.Lastname);
        }
        #endregion
     } 
}
