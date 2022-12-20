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
    public interface IEditDetails
    {
        /// <summary>
        /// Метод изменения записи
        /// </summary>
        void EditClientDetails(Client BaseClientData, Client NewClientData);
    }
}
