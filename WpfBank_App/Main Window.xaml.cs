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
    public partial class Window4 : Window
    {
        private static BankA bankA = new BankA();

        public Window4()
        {          
            InitializeComponent();
            bankA.LogicCreditAccount();
        }

        #region Кнопки общие для всех окон
        /// <summary>
        /// Кнопка вывода данных клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ViewingClientsData(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{Manager.PrintData<Client>()}");
        }

        /// <summary>
        /// Кнопка просмотра истории изменений данных клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ViewingTheHistoryOfChanges(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{WorkWithFiles.PrintHistory()}");
        }

        /// <summary>
        /// Кнопка выхода из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
        
        #region Кнопки менеджера
        /// <summary>
        /// Кнопка менеджера для добавления новых клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ManagerAddingClientsData(object sender, RoutedEventArgs e)
        {
            ManagerAddingClientsData managerAddingClientsData = new ManagerAddingClientsData();
            managerAddingClientsData.Show();
        }

        /// <summary>
        /// Кнопка менеджера для изменения данных клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ManagerChangingClientsData(object sender, RoutedEventArgs e)
        {
            ManagerChangingClientsData managerChangingClientsData = new ManagerChangingClientsData();
            managerChangingClientsData.Show();
        }
        #endregion

        #region Кнопки консультанта
        /// <summary>
        /// Кнопка изменения данных для консультанта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ConsultantChangingClientsData(object sender, RoutedEventArgs e)
        {
            ConsultantChangingClientsData consultantChangingClientsData = new ConsultantChangingClientsData();
            consultantChangingClientsData.Show();
        }
        #endregion

        #region Кнопки для работы со счетом
        /// <summary>
        /// Кнопка для работы с денежными переводами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ReplenishmentOrWithdrawalOfMoney(object sender, RoutedEventArgs e)
        {
            ReplenishmentOrWithdrawalOfMoney replenishmentOrWithdrawalOfMoney = new ReplenishmentOrWithdrawalOfMoney();
            replenishmentOrWithdrawalOfMoney.Show();
        }

        /// <summary>
        /// Кнопка для открытия/закрытия счетов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OpeningOrClosingAnAccount(object sender, RoutedEventArgs e)
        {
            OpeningOrClosingAnAccount openingWindow = new OpeningOrClosingAnAccount();
            openingWindow.Show();
        }

        /// <summary>
        /// Кнопка для просмотра информации о счетах клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ViewingAccountsData(object sender, RoutedEventArgs e)
        {
            ViewingAccountsData viewingAccountsData = new ViewingAccountsData();
            viewingAccountsData.Show();
        }

        /// <summary>
        /// Кнопка для перевода средств
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MoneyTransfers(object sender, RoutedEventArgs e)
        {
            MoneyTransfers moneyTransfers = new MoneyTransfers();
            moneyTransfers.Show();
        }
        #endregion


    }
}
