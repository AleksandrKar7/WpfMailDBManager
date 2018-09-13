using WpfMailDBManager.DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Data;

namespace WpfMailDBManager
{
    /// <summary>
    /// Логика взаимодействия для Connect.xaml
    /// </summary>
    public partial class Connect : Window
    {
        public static string ConnectionString = "";
        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
        public static bool itsOk = false;
        enum Authentication
        {
            WindowsAuthentication,
            SQLServerAuthentication
        }
        public Connect()
        {
            InitializeComponent();
            ConnectionString = "";
        }

        //Событие кнопки "ОК"
        private void button_Click_OK(object sender, RoutedEventArgs e)
        {
            itsOk = false;
            if (CreateConnectionStirngToServer() == false)
            {
                return;
            }
            if (CheckConnectionToServer() == false)
            {
                ConnectionString = null;
                MessageBox.Show("Не удалось подключиться к указанному серверу!"
                    + " Возможно неверно введенны данные или отсутствует связь с указанным сервером."
                    , "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(CheckExistDataBaseAndCreateIfNot() == false)
            {
                ConnectionString = null;               
                return;
            }
            itsOk = true;
            this.Close();
        }

        //Инициализация строки подключения
        private bool CreateConnectionStirngToServer()
        {
            string dataSource = "";
            string login = "";
            string password = "";

            dataSource = Connect_TBox_dataSource.Text;
            login = Connect_TBox_UserName.Text;
            password = Connect_PassBox_Password.Password;

            dataSource.Trim();
            login.Trim();
            password.Trim();

            if (dataSource == null || dataSource == "")
            {
                MessageBox.Show("Имя сервера не введен", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (Connect_ComboBox_Authentication.SelectedIndex == (int)Authentication.WindowsAuthentication)
            {
                scsb = new SqlConnectionStringBuilder()
                {
                    DataSource = dataSource,
                    MultipleActiveResultSets = true,
                    ConnectTimeout = 1,
                    IntegratedSecurity = true

                };
                ConnectionString = scsb.ConnectionString;
            }

            if (Connect_ComboBox_Authentication.SelectedIndex == (int)Authentication.SQLServerAuthentication)
            {
                if (login == null || login == "")
                {
                    MessageBox.Show("Логин не введен", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                if (password == null || password == "")
                {
                    MessageBox.Show("Пароль не введен", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                scsb = new SqlConnectionStringBuilder()
                {
                    DataSource = dataSource,
                    MultipleActiveResultSets = true,
                    ConnectTimeout = 1,
                    UserID = login,
                    Password = password
                };

                ConnectionString = scsb.ConnectionString;
            }
            return true;
        }

        //Проверка подключения
        private bool CheckConnectionToServer()
        {
            try
            {
                using (MailContext context = new MailContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
            }
            catch
            {             
                return false;
            }
            return true;
        }

        //Создание БД в случае её отсутсвия
        private bool CheckExistDataBaseAndCreateIfNot()
        {
            scsb.InitialCatalog = "MailDB";
            ConnectionString = scsb.ConnectionString;

            using (MailContext context = new MailContext())
            {
                if (context.Database.Exists() == false)
                {
                    MessageBoxResult mbr = MessageBox.Show("База данных MailDB не обнаружена. Создать её?", "MailDB не обнаружена", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
                    if (mbr == MessageBoxResult.Yes)
                    {
                        CreateTables();
                        mbr = MessageBox.Show("MailDB пуста! Заполнить вспомогательные таблицы?" 
                            + "\n В случае отказа вы сможете сделать это позже на панели Действие => Заполнить вспомогательные таблицы"
                            , "MailDB пуста", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (mbr == MessageBoxResult.Yes)
                        {
                            AutoInsert ai = new AutoInsert();
                            ai.AutoInsertSecondaryTable();

                            mbr = MessageBox.Show("Добавить тестовые значения в основные таблицы?"
                           + "\n В случае отказа вы сможете сделать это позже на панели Действие => Добавить тестовые значения"
                           , "MailDB пуста", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                            if (mbr == MessageBoxResult.Yes && ai.CanAutoInsertPrimaryTable())
                            {
                                ai.AutoInsertPrimaryTable();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Без существующей базы данных MailDB робота не может быть продолжено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        //Событие на изменение окна в зависимости от выбраной Authentication
        private void Connect_ComboBox_Authentication_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Connect_TBox_UserName != null && Connect_PassBox_Password != null)
            {
                if (Connect_ComboBox_Authentication.SelectedIndex == (int)Authentication.WindowsAuthentication)
                {
                    Connect_TBox_UserName.IsEnabled = false;
                    Connect_PassBox_Password.IsEnabled = false;
                }
                else
                {
                    Connect_TBox_UserName.IsEnabled = true;
                    Connect_PassBox_Password.IsEnabled = true;
                }
            }
        }

        //Создание БД
        private static void CreateTables()
        {
            using (MailContext context = new MailContext())
            {
                context.Database.Create();

                context.SaveChanges();

                Console.WriteLine("Все прошло удачно!");
            }
        }

        //Событие кнопки "Отмена"
        private void Connect_button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
