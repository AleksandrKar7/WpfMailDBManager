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
using WpfMailDBManager.SubscriberWindows;

namespace WpfMailDBManager
{
    public partial class MainWindow : Window //таблица Подписчиков
    {
        private Subscriber[] subscriberArr;
        private Subscriber[] subscriberArrNow;
        ShowOrDeleteInSubscriber ShowSubscriber;
        ShowOrDeleteInSubscriber DeleteSubscriber;
        InsertOrUpdateToSubscriber InsertSubscriber;
        InsertOrUpdateToSubscriber UpdateSubscriber;

        //Метод для установки значений в таблице Подписчиков
        private void SetDataInGridSubscriber()
        {
            using (MailContext context = new MailContext())
            {
                IQueryable<Subscriber> subscribers = context.Subscribers.Select(c => c);

                subscriberArr = subscribers.ToArray<Subscriber>();

                dataGrid_Subscriber.ItemsSource = subscribers.ToList<Subscriber>();

                subscriberArrNow = subscriberArr;
            }
        }

        //Метод для очистки таблицы Подписчиков
        private void CleanDataSubscriber()
        {
            subscriberArr = null;
            dataGrid_Subscriber.ItemsSource = subscriberArr;
        }

        private List<ShowOrDeleteInSubscriber> ShowSubscriberList = new List<ShowOrDeleteInSubscriber>();
        //Событие двойного нажатия на запись в таблице Подписчиков. 
        private void dataGrid_Subscriber_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowInSubscriber();
        }

        //Метод вызова окна просмотра записи, а также контроля за ними
        private void ShowInSubscriber()
        {
            int rowIndex = dataGrid_Subscriber.SelectedIndex;
            if (rowIndex >= 0)
            {
                int index = subscriberArrNow[rowIndex].subscriber_Id;
                if (ShowSubscriberList.Count != 0)
                {
                    //Очистка листа ShowSubscriberList от закрытых окон
                    ShowOrDeleteInSubscriber[] tempArr = ShowSubscriberList.ToArray();
                    foreach (ShowOrDeleteInSubscriber SODISuber in tempArr)
                    {
                        if (SODISuber.IsVisible == false)
                        {
                            ShowSubscriberList.Remove(SODISuber);
                        }
                    }

                    //Вывод окна на передний план если оно уже было открыто
                    foreach (ShowOrDeleteInSubscriber SODISubion in ShowSubscriberList)
                    {
                        //MessageBox.Show(SODISubion.id.ToString());
                        if (SODISubion.id == index)
                        {
                            SODISubion.Activate();
                            return;
                        }
                    }
                }
                ShowSubscriber = new ShowOrDeleteInSubscriber();

                ShowSubscriber.Owner = this;
                ShowSubscriber.Show();
                ShowSubscriber.SetData(index, ShowOrDeleteInSubscriber.ModeWindow.Show);
                ShowSubscriberList.Add(ShowSubscriber);

            }
        }

        //Событие кнопки "Обновить" для таблицы Подписчиков
        private void button_Click_RefreshSubscriberTable(object sender, RoutedEventArgs e)
        {
            RefreshSubscriberTable();
        }

        //Вызов набора методов для обновление блока Таблицы подписчиков
        public void RefreshSubscriberTable()
        {
            SetDataInGridSubscriber();
            SetDataSearchInSubscription();

            CleanSortSubscriberVariable();

            InitializeSearchDataInSubscriber();
            SearchInSubscriber();
        }

        //Событие кнопки "Добавить запись" для таблицы Подписчиков
        private void button_InsertToSubscriber_Click(object sender, RoutedEventArgs e)
        {
            InsertToSubscriber();
        }

        //Вызов окна добавления записи для таблицы Подписчиков, а также контроль за ним 
        private void InsertToSubscriber()
        {
            if (InsertSubscriber != null)
            {
                if (InsertSubscriber.IsVisible == true && InsertSubscriber.IsActive == false)
                {
                    InsertSubscriber.Activate();
                    return;
                }
            }
            InsertSubscriber = null;
            InsertSubscriber = new InsertOrUpdateToSubscriber();
            InsertSubscriber.SetMode(InsertOrUpdateToSubscriber.ModeWindow.Insert);
            InsertSubscriber.mainWindow = this;

            InsertSubscriber.Owner = this;
            InsertSubscriber.Show();
        }

        //Событие кнопки "Редактировать" для таблицы Подписчиков
        private void button_UpdateInSubscriber_Click(object sender, RoutedEventArgs e)
        {
            UpdateInSubscriber();
        }

        //Вызов окна редактирования записи для таблицы Подписок, а также контроль за ним 
        private void UpdateInSubscriber()
        {
            if (UpdateSubscriber != null)
            {

                if (UpdateSubscriber.IsVisible == true && UpdateSubscriber.IsActive == false)
                {
                    UpdateSubscriber.Activate();
                    return;
                }
            }
            UpdateSubscriber = null;
            UpdateSubscriber = new InsertOrUpdateToSubscriber();
            UpdateSubscriber.SetMode(InsertOrUpdateToSubscriber.ModeWindow.Update);
            if (dataGrid_Subscriber.SelectedIndex >= 0)
            {
                UpdateSubscriber.ChooseData(subscriberArr[dataGrid_Subscriber.SelectedIndex]);
                UpdateSubscriber.Owner = this;
                UpdateSubscriber.mainWindow = this;
                UpdateSubscriber.Show();
            }
            else
            {
                MessageBox.Show("Выделите запись которую желаете изменить", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Событие кнопки "Удалить запись" для таблицы Подписок
        private void button_DeleteInSubscriber_Click(object sender, RoutedEventArgs e)
        {
            DeleteInSubscriber();
        }

        //Вызов окна подтверждения удаления записи для таблицы Подписок, а также удаление записи 
        private void DeleteInSubscriber()
        {
            DeleteSubscriber = new ShowOrDeleteInSubscriber();
            int selectedIndex;
            int selectedRow = dataGrid_Subscriber.SelectedIndex;
            if (selectedRow >= 0)
            {
                DeleteSubscriber.SetData(subscriberArr[selectedRow], ShowOrDeleteInSubscriber.ModeWindow.DeleteConfirm);
                selectedIndex = subscriberArr[selectedRow].subscriber_Id;
            }
            else
            {
                MessageBox.Show("Выделите запись которую желаете удалить", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DeleteSubscriber.Owner = this;

            bool? result = DeleteSubscriber.ShowDialog();
            if (result == true)
            {
                using (MailContext context = new MailContext())
                {
                    Subscriber subscriber = (Subscriber)context.Subscribers.FirstOrDefault(c => c.subscriber_Id == selectedIndex);

                    List<Subscription> subscriptions = context.Subscriptions.Where(c => c.id_Subscriber.subscriber_Id == subscriber.subscriber_Id).ToList();
                    List<int> subscriptionsId = new List<int>();
                    foreach (Subscription subscription in subscriptions)
                    {
                        subscriptionsId.Add(subscription.subscription_Id);
                    }

                    //Проверка на наличие ссылки на удаляемую запись
                    List<Mail> mails = context.Mail.Where(c => subscriptionsId.Contains(c.id_Subscription.subscription_Id)).ToList();
                    if (mails.Count > 0)
                    {
                        if (MessageBox.Show("Внимание! Удаление " + selectedIndex
                            + " записи таблице приведет к удалению " + subscriptions.Count
                            + " подписок в таблице Подписок! \n"
                            + " А также к обнулению " + mails.Count
                            + " ссылок на подписку в Главной таблице!"
                            + "\n Вы уверены что хотите удалить " + selectedIndex + " запись в таблице Подписок?"
                            , "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    context.Subscribers.Remove(subscriber);
                    context.SaveChanges();

                    MessageBox.Show("Запись №" + selectedIndex + " в таблице Подписчиков удалена успешно!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                    RefreshSubscriberTable();
                    RefreshSubscriptionTable();
                    RefreshMainTable();
                }
            }
        }

        //Событие кнопки "Поиск" для таблицы Подписок

        private void button_SearchSubscriber_Click(object sender, RoutedEventArgs e)
        {
            InitializeSearchDataInSubscriber();
            SearchInSubscriber();
        }

        int searchSubscriber_minId = -1;
        int searchSubscriber_maxId = -1;

        string searchSubscriber_FirstName = null;
        string searchSubscriber_LastName = null;
        string searchSubscriber_MiddleName = null;
        string searchSubscriber_Address = null;

        //Метод инициализации переменных для поиска в таблице Подписчиков
        private void InitializeSearchDataInSubscriber()
        {
            //Предварительно очищаем переменные
            CleanSearchSubscriberVariables();

            using (MailContext context = new MailContext())
            {
                textBox_SearchSubscriber_MinID.Text.Trim();
                if (textBox_SearchSubscriber_MinID.Text != null && textBox_SearchSubscriber_MinID.Text != "")
                {
                    if (Int32.TryParse(textBox_SearchSubscriber_MinID.Text, out searchSubscriber_minId) == true)
                    {
                        searchSubscriber_minId = Int32.Parse(textBox_SearchSubscriber_MinID.Text);
                    }
                }
                textBox_SearchSubscriber_MaxID.Text.Trim();
                if (textBox_SearchSubscriber_MaxID.Text != null && textBox_SearchSubscriber_MaxID.Text != "")
                {
                    if (Int32.TryParse(textBox_SearchSubscriber_MaxID.Text, out searchSubscriber_maxId) == true)
                    {
                        searchSubscriber_maxId = Int32.Parse(textBox_SearchSubscriber_MaxID.Text);
                    }
                }

                textBox_SearchSubscriber_FirstName.Text.Trim();
                if (textBox_SearchSubscriber_FirstName.Text != "" || textBox_SearchSubscriber_FirstName.Text != null)
                {
                    searchSubscriber_FirstName = textBox_SearchSubscriber_FirstName.Text;
                }

                textBox_SearchSubscriber_LastName.Text.Trim();
                if (textBox_SearchSubscriber_LastName.Text != "" || textBox_SearchSubscriber_LastName.Text != null)
                {
                    searchSubscriber_LastName = textBox_SearchSubscriber_LastName.Text;
                }

                textBox_SearchSubscriber_MiddleName.Text.Trim();
                if (textBox_SearchSubscriber_MiddleName.Text != "" || textBox_SearchSubscriber_MiddleName.Text != null)
                {
                    searchSubscriber_MiddleName = textBox_SearchSubscriber_MiddleName.Text;
                }

                textBox_SearchSubscriber_Address.Text.Trim();
                if (textBox_SearchSubscriber_Address.Text != "" || textBox_SearchSubscriber_Address.Text != null)
                {
                    searchSubscriber_Address = textBox_SearchSubscriber_Address.Text;
                }

            }
        }

        //Вызов набора методов для полной очистки полей и переменных поиска для таблицы Подписок
        private void CleanDataSearchInSubscriber()
        {
            CleanSearchSubscriberVariables();
            CleanSearchSubscriberVariables();
        }

        //Метод поиска в таблицы Подписок
        private void SearchInSubscriber()
        {
            Subscriber[] sortSubscriberArr = subscriberArr.ToArray();

            if (searchSubscriber_minId >= 0)
            {
                List<Subscriber> tempList = new List<Subscriber>();
                foreach (Subscriber subscriber in sortSubscriberArr)
                {
                    if (subscriber.subscriber_Id >= searchSubscriber_minId)
                    {
                        tempList.Add(subscriber);
                    }
                }
                sortSubscriberArr = tempList.ToArray();
            }
            if (searchSubscriber_maxId >= 0)
            {
                List<Subscriber> tempList = new List<Subscriber>();
                foreach (Subscriber subscriber in sortSubscriberArr)
                {
                    if (subscriber.subscriber_Id <= searchSubscriber_maxId)
                    {
                        tempList.Add(subscriber);
                    }
                }
                sortSubscriberArr = tempList.ToArray();
            }

            if (searchSubscriber_FirstName != null && searchSubscriber_FirstName != "")
            {
                List<Subscriber> tempList = new List<Subscriber>();
                foreach (Subscriber subscriber in sortSubscriberArr)
                {
                    if (subscriber.first_Name.Contains(searchSubscriber_FirstName))
                    {
                        tempList.Add(subscriber);
                    }
                }
                sortSubscriberArr = tempList.ToArray();
            }

            if (searchSubscriber_LastName != null && searchSubscriber_LastName != "")
            {
                List<Subscriber> tempList = new List<Subscriber>();
                foreach (Subscriber subscriber in sortSubscriberArr)
                {
                    if (subscriber.last_Name.Contains(searchSubscriber_LastName))
                    {
                        tempList.Add(subscriber);
                    }
                }
                sortSubscriberArr = tempList.ToArray();
            }

            if (searchSubscriber_MiddleName != null && searchSubscriber_MiddleName != "")
            {
                List<Subscriber> tempList = new List<Subscriber>();
                foreach (Subscriber subscriber in sortSubscriberArr)
                {
                    if (subscriber.middle_Name.Contains(searchSubscriber_MiddleName))
                    {
                        tempList.Add(subscriber);
                    }
                }
                sortSubscriberArr = tempList.ToArray();
            }

            if (searchSubscriber_Address != null && searchSubscriber_Address != "")
            {
                List<Subscriber> tempList = new List<Subscriber>();
                foreach (Subscriber subscriber in sortSubscriberArr)
                {
                    if (subscriber.sub_Address.Contains(searchSubscriber_Address))
                    {
                        tempList.Add(subscriber);
                    }
                }
                sortSubscriberArr = tempList.ToArray();
            }

            if (sortSubscriberArr.Length == 0)
            {
                MessageBox.Show("Поиск не дал результатов!", "Нет результатов", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            subscriberArrNow = sortSubscriberArr; 

            dataGrid_Subscriber.ItemsSource = sortSubscriberArr.ToList<Subscriber>();
        }

        //Событие кнопки "Очистить" в блоке поиска таблицы Подписок
        private void button_SearchSubscriber_Clean_Click(object sender, RoutedEventArgs e)
        {
            CleanSearchSubscriberVariables();
            CleanSearchSubscriber();
            RefreshSubscriberTable();
        }

        //Метод очистки полей поиска в таблице Подписчиков
        private void CleanSearchSubscriber()
        {
            textBox_SearchSubscriber_MinID.Text = null;
            textBox_SearchSubscriber_MaxID.Text = null;

            textBox_SearchSubscriber_FirstName.Text = null;
            textBox_SearchSubscriber_LastName.Text = null;
            textBox_SearchSubscriber_MiddleName.Text = null;

            textBox_SearchSubscriber_Address.Text = null;
        }

        //Очистка переменных поиска в таблице Подписчиков
        private void CleanSearchSubscriberVariables()
        {
            subscriberArrNow = subscriberArr;

            searchSubscriber_minId = -1;
            searchSubscriber_maxId = -1;

            searchSubscriber_LastName = null;
            searchSubscriber_FirstName = null;
            searchSubscriber_MiddleName = null;
            searchSubscriber_Address = null;
        }

        //Событие нажатия на заголовок таблицы
        private void dataGrid_Subscriber_MouseColumnHeaderClick(object sender, MouseColumnHeaderEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    SortSubscriberByID();
                    break;

                case 1:
                    SortSubscriberByLastName();
                    break;

                case 2:
                    SortSubscriberByFirstName();
                    break;

                case 3:
                    SortSubscriberByMiddleName();
                    break;

                case 4:
                    SortSubscriberByAddress();
                    break;
            }
        }

        bool isAscendingSubscriberID = true;
        //Метод сортировка таблицы по ID
        public void SortSubscriberByID()
        {
            Subscriber[] tempArr = subscriberArrNow;
            if (isAscendingSubscriberID == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.subscriber_Id).ToArray();
                isAscendingSubscriberID = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.subscriber_Id).ToArray();
                isAscendingSubscriberID = true;
            }

            dataGrid_Subscriber.SelectedIndex = -1;
            dataGrid_Subscriber.ItemsSource = tempArr;
        }

        bool isAscendingLastName = false;
        //Метод сортировка таблицы по фамилии
        public void SortSubscriberByLastName()
        {
            Subscriber[] tempArr = subscriberArrNow;
            if (isAscendingLastName == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.last_Name).ToArray();
                isAscendingLastName = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.last_Name).ToArray();
                isAscendingLastName = true;
            }

            dataGrid_Subscriber.SelectedIndex = -1;
            dataGrid_Subscriber.ItemsSource = tempArr;
        }

        bool isAscendingFirstName = false;
        //Метод сортировка таблицы по имени
        public void SortSubscriberByFirstName()
        {
            Subscriber[] tempArr = subscriberArrNow;
            if (isAscendingFirstName == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.first_Name).ToArray();
                isAscendingFirstName = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.first_Name).ToArray();
                isAscendingFirstName = true;
            }

            dataGrid_Subscriber.SelectedIndex = -1;
            dataGrid_Subscriber.ItemsSource = tempArr;
        }

        bool isAscendingMiddleName = false;
        //Метод сортировка таблицы по отчеству
        public void SortSubscriberByMiddleName()
        {
            Subscriber[] tempArr = subscriberArrNow;
            if (isAscendingMiddleName == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.middle_Name).ToArray();
                isAscendingMiddleName = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.middle_Name).ToArray();
                isAscendingMiddleName = true;
            }

            dataGrid_Subscriber.SelectedIndex = -1;
            dataGrid_Subscriber.ItemsSource = tempArr;
        }

        bool isAscendingAddress = false;
        //Метод сортировка таблицы по адресу
        public void SortSubscriberByAddress()
        {
            Subscriber[] tempArr = subscriberArrNow;
            if (isAscendingAddress == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.sub_Address).ToArray();
                isAscendingAddress = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.sub_Address).ToArray();
                isAscendingAddress = true;
            }

            dataGrid_Subscriber.SelectedIndex = -1;
            dataGrid_Subscriber.ItemsSource = tempArr;
        }

        //Метод очистки переменных сортировки
        private void CleanSortSubscriberVariable()
        {
            isAscendingSubscriberID = true;
            isAscendingLastName = false;
            isAscendingFirstName = false;
            isAscendingMiddleName = false;
            isAscendingAddress = false;
        }
    }
}
