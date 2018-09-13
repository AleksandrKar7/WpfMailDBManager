using WpfMailDBManager.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfMailDBManager.MainTableWindows;
using System.Globalization;
using System.Reflection;
using System.IO;

namespace WpfMailDBManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static string ConnectionString;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Start();
        }

        //Метод вызова окна подключения и окна загрузки
        private void Start()
        {
            Connect connect = new Connect();
            connect.Owner = this;
            connect.ShowDialog();
            if (Connect.ConnectionString == null || Connect.ConnectionString == "")
            {
                this.Close();
                return;
            }
            if(Connect.itsOk == false)
            {
                this.Close();
                return;
            }
            Load load = new Load();
            load.Owner = this;
            load.Show();

            SetDataInGridMainTable();
            SetDataInGridSubscriber();
            SetDataInGridSubscription();
            load.Close();

            SetDataSearchInMainTable();
            SetDataSearchInSubscription();
        }

        //Метод для очиски всех блоков таблиц
        private void ClearAll()
        {
            CleanDataMainTable();
            CleanDataSubscriber();
            CleanDataSubscription();

            CleanDataSearchInMainTable();
            CleanDataSearchInSubscriber();
            CleanDataSearchInSubscription();
        }

        //Событие нажатия на пунк меню "Сменить пользователя"
        private void MenuItem_ChangeUser_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            Start();
        }

        //Событие нажатия на пунк меню "Выход"
        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Событие нажатия на пунк меню "Добавить запись" в подменю "Главная таблица"
        private void MenuItem_InsertToMainTable_Click(object sender, RoutedEventArgs e)
        {
            InserToMainTable();
        }
        //Событие нажатия на пунк меню "Редактировать запись" в подменю "Главная таблица"
        private void MenuItem_UpdateInMainTable_Click(object sender, RoutedEventArgs e)
        {
            UpdateInMainTable();
        }
        //Событие нажатия на пунк меню "Удалить запись" в подменю "Главная таблица"
        private void MenuItem_DeleteInMainTable_Click(object sender, RoutedEventArgs e)
        {
            DeleteInMainTable();
        }



        //Событие нажатия на пунк меню "Добавить запись" в подменю "Таблица Подписок"
        private void MenuItem_InsertToSubsciber_Click(object sender, RoutedEventArgs e)
        {
            InsertToSubscriber();
        }
        //Событие нажатия на пунк меню "Редактировать запись" в подменю "Таблица Подписок"
        private void MenuItem_UpdateInSubsciber_Click(object sender, RoutedEventArgs e)
        {
            UpdateInSubscriber();
        }
        //Событие нажатия на пунк меню "Удалить запись" в подменю "Таблица Подписок"
        private void MenuItem_DeleteInSubsciber_Click(object sender, RoutedEventArgs e)
        {
            DeleteInSubscriber();
        }



        //Событие нажатия на пунк меню "Добавить запись" в подменю "Таблица Подписчиков"
        private void MenuItem_InsertToSubscription_Click(object sender, RoutedEventArgs e)
        {
            InsertToSubscription();
        }
        //Событие нажатия на пунк меню "Редактировать запись" в подменю "Таблица Подписчиков"
        private void MenuItem_UpdateInSubscription_Click(object sender, RoutedEventArgs e)
        {
            UpdateInSubscription();
        }
        //Событие нажатия на пунк меню "Удалить запись" в подменю "Таблица Подписчиков"
        private void MenuItem_DeleteInSubscription_Click(object sender, RoutedEventArgs e)
        {
            DeleteInSubscription();
        }

        //Событие нажатия на пунк меню "Обновить все таблицы"
        private void MenuItem_RefreshAll_Click(object sender, RoutedEventArgs e)
        {
            RefreshMainTable();
            RefreshSubscriberTable();
            RefreshSubscriptionTable();
        }

        //Событие нажатия на пунк меню "Заполнить вспомогательные таблицы"
        private void MenuItem_InsertTestSecondaryData_Click(object sender, RoutedEventArgs e)
        {
            AutoInsert ai = new AutoInsert();
            MessageBoxResult mbr = MessageBox.Show("Заполнить вспомогательные таблицы?", "Заполнить вспомогательные таблицы", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
            if (mbr == MessageBoxResult.Yes)
            {
                ai.AutoInsertSecondaryTable();

                RefreshMainTable();
                RefreshSubscriberTable();
                RefreshSubscriptionTable();
            }
        }

        //Событие нажатия на пунк меню "Добавить тестовые значения"
        private void MenuItem_InsertTestPrimaryData_Click(object sender, RoutedEventArgs e)
        {

            AutoInsert ai = new AutoInsert();
            if (ai.CanAutoInsertPrimaryTable())
            {
                MessageBoxResult mbr = MessageBox.Show("Добавить тестовые значения в основные таблицы?", "Добавить тестовые значения", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
                if (mbr == MessageBoxResult.Yes)
                {
                    ai.AutoInsertPrimaryTable();

                    RefreshMainTable();
                    RefreshSubscriberTable();
                    RefreshSubscriptionTable();
                }
            }
            else
            {
                MessageBox.Show("Для добаления тестовых значений должны быть заполненны второстепенные таблицы", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Событие предотвращающее ввод сторонних символов при вводе числа с плавающей точкой
        private void ItValidFloatKeyInTbox(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
          || (e.Key == Key.Decimal
          && (((TextBox)sender).Text.IndexOf(".") == -1 && ((TextBox)sender).Text.IndexOf(",") == -1) && ((TextBox)sender).Text.Length > 0))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        //Событие предотвращающее ввод сторонних символов при вводе целочисленного числа
        private void ItValidIntKeyInTbox(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

    

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Process.Start(dir + "Help.pdf");
        }
    }

    public class MyDataGrid : DataGrid
    {
        public event EventHandler<MouseColumnHeaderEventArgs> MouseColumnHeaderClick = delegate { };

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            var obj = (DependencyObject)e.OriginalSource;

            while ((obj != null) && !(obj is DataGridColumnHeader))
            {
                obj = VisualTreeHelper.GetParent(obj);
            }

            if (obj == null)
            {
                return;
            }

            if (obj is DataGridColumnHeader)
            {
                var columnHeader = (DataGridColumnHeader)obj;
                MouseColumnHeaderClick(this, new MouseColumnHeaderEventArgs(columnHeader.DisplayIndex));
            }
            base.OnPreviewMouseLeftButtonUp(e);
        }

        protected override void OnPreviewMouseDoubleClick(MouseButtonEventArgs e)
        {
            var obj = (DependencyObject)e.OriginalSource;

            while ((obj != null) && !(obj is DataGridColumnHeader))
            {
                obj = VisualTreeHelper.GetParent(obj);
            }

            if (obj == null)
            {
                return;
            }

            if (obj is DataGridColumnHeader)
            {
                return;
            }
            base.OnPreviewMouseDoubleClick(e);
        }
    }
}


