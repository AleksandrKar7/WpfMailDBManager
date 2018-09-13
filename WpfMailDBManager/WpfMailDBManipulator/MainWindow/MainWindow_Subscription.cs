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
using WpfMailDBManager.SubscriptionWindows;

namespace WpfMailDBManager
{
    public partial class MainWindow : Window //таблица Подписок
    {
        private Subscription[] subscriptionArr;
        private Subscription[] subscriptionArrNow;
        ShowOrDeleteInSubscription ShowSubscription;
        ShowOrDeleteInSubscription DeleteSubscription;
        InsertOrUpdateToSubscription InsertSubscription;
        InsertOrUpdateToSubscription UpdateSubscription;

        //Метод для установки значений в таблицу Подписок
        private void SetDataInGridSubscription()
        {
            using (MailContext context = new MailContext())
            {
                IQueryable<Subscription> subscriptions = context.Subscriptions.Select(c => c);

                foreach (Subscription subscription in subscriptions)
                {
                    subscription.id_Subscriber.ToString();
                    subscription.id_Edition.ToString();
                }

                subscriptionArr = subscriptions.ToArray<Subscription>();

                dataGrid_Subscription.ItemsSource = subscriptions.ToList<Subscription>();

                subscriptionArrNow = subscriptionArr;
            }
        }

        //Метод для очистки таблицы Подписок
        private void CleanDataSubscription()
        {
            subscriptionArr = null;
            dataGrid_Subscription.ItemsSource = subscriptionArr;
        }

        private List<ShowOrDeleteInSubscription> ShowSubscriptionList = new List<ShowOrDeleteInSubscription>();
        //Событие двойного нажатия на запись в таблице Подписок. 
        private void dataGrid_Subscription_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowInSubscription();
        }

        //Метод вызова окна просмотра записи, а также контроля за ними
        private void ShowInSubscription()
        {
            int rowIndex = dataGrid_Subscription.SelectedIndex;

            if (rowIndex >= 0)
            {
                int index = subscriberArrNow[rowIndex].subscriber_Id;
                if (ShowSubscriptionList.Count != 0)
                {
                    //Очистка листа ShowSubscriptionList от закрытых окон
                    ShowOrDeleteInSubscription[] tempArr = ShowSubscriptionList.ToArray();
                    foreach (ShowOrDeleteInSubscription SODISubion in tempArr)
                    {
                        if (SODISubion.IsVisible == false)
                        {
                            ShowSubscriptionList.Remove(SODISubion);
                        }
                    }

                    //Вывод окна на передний план если оно уже было открыто
                    foreach (ShowOrDeleteInSubscription SODISubion in ShowSubscriptionList)
                    {
                        //MessageBox.Show(SODISubion.id.ToString());
                        if (SODISubion.id == index)
                        {
                            SODISubion.Activate();
                            return;
                        }
                    }
                }
                ShowSubscription = new ShowOrDeleteInSubscription();

                ShowSubscription.Owner = this;
                ShowSubscription.Show();
                ShowSubscription.SetData(index, ShowOrDeleteInSubscription.ModeWindow.Show);
                ShowSubscriptionList.Add(ShowSubscription);
            }
        }

        //Событие кнопки "Обновить" для таблицы Подписок
        private void button_Click_RefresSubscriptionTable(object sender, RoutedEventArgs e)
        {
            RefreshSubscriptionTable();
        }

        //Вызов набора методов для обновление блока таблицы Подписок
        public void RefreshSubscriptionTable()
        {
            SetDataInGridSubscription();
            SetDataSearchInSubscription();

            CleanSortSubscriptionVariable();

            InitializeSearchDataInSubscription();
            SearchInSubscription();
        }

        //Событие кнопки "Добавить запись" для таблицы Подписок
        private void button_InsertToSubscription_Click(object sender, RoutedEventArgs e)
        {
            InsertToSubscription();
        }

        //Вызов окна добавления записи для таблицы Подписок, а также контроль за ним 
        private void InsertToSubscription()
        {
            if (InsertSubscription != null)
            {
                if (InsertSubscription.IsVisible == true && InsertSubscription.IsActive == false)
                {
                    InsertSubscription.Activate();
                    return;
                }
            }
            InsertSubscription = null;
            InsertSubscription = new InsertOrUpdateToSubscription();
            InsertSubscription.SetMode(InsertOrUpdateToSubscription.ModeWindow.Insert);
            InsertSubscription.mainWindow = this;

            InsertSubscription.Owner = this;
            InsertSubscription.Show();
        }

        //Событие кнопки "Редактировать" для таблицы Подписок
        private void button_UpdateInSubscription_Click(object sender, RoutedEventArgs e)
        {
            UpdateInSubscription();
        }

        //Вызов окна редактирования записи для таблицы Подписок, а также контроль за ним 
        private void UpdateInSubscription()
        {
            if (UpdateSubscription != null)
            {
                if (UpdateSubscription.IsVisible == true && UpdateSubscription.IsActive == false)
                {
                    UpdateSubscription.Activate();
                    return;
                }
            }
            UpdateSubscription = null;
            UpdateSubscription = new InsertOrUpdateToSubscription();
            UpdateSubscription.SetMode(InsertOrUpdateToSubscription.ModeWindow.Update);
            if (dataGrid_Subscription.SelectedIndex >= 0)
            {
                UpdateSubscription.chooseData(subscriptionArr[dataGrid_Subscription.SelectedIndex]);
                UpdateSubscription.Owner = this;
                UpdateSubscription.mainWindow = this;
                UpdateSubscription.Show();
            }
            else
            {
                MessageBox.Show("Выделите запись которую желаете изменить", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Событие кнопки "Удалить запись" для таблицы Подписок
        private void button_DeleteInSubscription_Click(object sender, RoutedEventArgs e)
        {
            DeleteInSubscription();
        }

        //Вызов окна подтверждения удаления записи для таблицы Подписок, а также удаление записи 
        private void DeleteInSubscription()
        {
            DeleteSubscription = new ShowOrDeleteInSubscription();
            int selectedIndex;
            int selectedRow = dataGrid_Subscription.SelectedIndex;
            if (selectedRow >= 0)
            {
                DeleteSubscription.SetData(subscriptionArr[selectedRow], ShowOrDeleteInSubscription.ModeWindow.DeleteConfirm);
                selectedIndex = subscriptionArr[selectedRow].subscription_Id;
            }
            else
            {
                MessageBox.Show("Выделите запись которую желаете удалить", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DeleteSubscription.Owner = this;

            bool? result = DeleteSubscription.ShowDialog();
            if (result == true)
            {
                using (MailContext context = new MailContext())
                {
                    Subscription subscription = (Subscription)context.Subscriptions.FirstOrDefault(c => c.subscription_Id == selectedIndex);
                    List<Mail> mails = context.Mail.Where(c => c.id_Subscription.subscription_Id == subscription.subscription_Id).ToList();
                    if (mails.Count > 0)
                    {
                        if (MessageBox.Show("Внимание! Удаление " + selectedIndex
                            + " записи таблице приведет к обнулению " + mails.Count
                            + " ссылок на подписчика в Главной таблице!"
                            + "\n Вы уверены что хотите удалить " + selectedIndex + " запись в таблице Подписок?"
                            , "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    context.Subscriptions.Remove(subscription);
                    context.SaveChanges();

                    MessageBox.Show("Запись №" + selectedIndex + " в таблице Подписок удалена успешно!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                    RefreshSubscriptionTable();
                    RefreshMainTable();
                }
            }
        }

        Subscriber[] searchSubscription_SubscriberArr;
        Edition[] searchSubscription_EditionArr;
        //Метод установки значений ссылкой на другую таблицу в ComboBox в блоке поиска значений в таблице Подписок
        private void SetDataSearchInSubscription()
        {
            using (MailContext context = new MailContext())
            {
                IQueryable<Subscriber> subscribers = context.Subscribers.Select(c => c);
                IQueryable<Edition> editions = context.Editions.Select(c => c);

                searchSubscription_SubscriberArr = subscribers.ToArray<Subscriber>();
                searchSubscription_EditionArr = editions.ToArray<Edition>();

                comboBox_SearchSubscription_IDSubscriber.Items.Clear();
                comboBox_SearchSubscription_IDEdition.Items.Clear();

                comboBox_SearchSubscription_IDSubscriber.Items.Add("Нет");
                comboBox_SearchSubscription_IDEdition.Items.Add("Нет");

                foreach (Subscriber subscriber in subscribers)
                {
                    comboBox_SearchSubscription_IDSubscriber.Items.Add(subscriber.ToString());
                }
                foreach (Edition edition in editions)
                {
                    comboBox_SearchSubscription_IDEdition.Items.Add(edition.ToString());
                }

                comboBox_SearchSubscription_IDSubscriber.SelectedIndex = 0;
                comboBox_SearchSubscription_IDEdition.SelectedIndex = 0;
            }
        }

        //Вызов набора методов для полной очистки полей и переменных поиска для таблицы Подписок
        private void CleanDataSearchInSubscription()
        {
            CleanSearchSubscriptionVariables();
            CleanSearchSubscriptionVariables();
            comboBox_SearchSubscription_IDSubscriber.Items.Clear();
            comboBox_SearchSubscription_IDEdition.Items.Clear();
        }       

        //Событие кнопки "Поиск" для таблицы Подписок
        private void button_SearchSubscription_Click(object sender, RoutedEventArgs e)
        {
            InitializeSearchDataInSubscription();
            SearchInSubscription();
        }

        int searchSubscription_minId = -1;
        int searchSubscription_maxId = -1;

        Subscriber searchSubscription_Subscriber = null;

        Edition searchSubscription_Edition = null;

        DateTime searchSubscription_minDateCreate = DateTime.MinValue;
        DateTime searchSubscription_maxDateCreate = DateTime.MinValue;

        DateTime searchSubscription_minDateExpiration = DateTime.MinValue;
        DateTime searchSubscription_maxDateExpiration = DateTime.MinValue;

        const int cbSearchSubscriptionStep = 1; //Делаем один шаг, т.к. Первая запись в comboBox пустая

        //Метод инициализации переменных для поиска в таблице Подписок
        private void InitializeSearchDataInSubscription()
        {
            //Предварительно очищаем переменные
            CleanSearchSubscriptionVariables();

            using (MailContext context = new MailContext())
            {
                textBox_SearchSubscription_MinID.Text.Trim();
                if (textBox_SearchSubscription_MinID.Text != null && textBox_SearchSubscription_MinID.Text != "")
                {
                    if (Int32.TryParse(textBox_SearchSubscription_MinID.Text, out searchSubscription_minId) == true)
                    {
                        searchSubscription_minId = Int32.Parse(textBox_SearchSubscription_MinID.Text);
                    }
                }
                textBox_SearchSubscription_MaxID.Text.Trim();
                if (textBox_SearchSubscription_MaxID.Text != null && textBox_SearchSubscription_MaxID.Text != "")
                {
                    if (Int32.TryParse(textBox_SearchSubscription_MaxID.Text, out searchSubscription_maxId) == true)
                    {
                        searchSubscription_maxId = Int32.Parse(textBox_SearchSubscription_MaxID.Text);
                    }
                }

                if (comboBox_SearchSubscription_IDSubscriber.SelectedIndex > 0)
                {
                    int subscriberId = searchSubscription_SubscriberArr[comboBox_SearchSubscription_IDSubscriber.SelectedIndex - cbSearchSubscriptionStep].subscriber_Id;
                    searchSubscription_Subscriber = context.Subscribers.FirstOrDefault<Subscriber>(c => c.subscriber_Id == subscriberId);
                    if (searchSubscription_Subscriber == null)
                    {
                        MessageBox.Show("Ошибка (Операциция не найдена! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        SetDataSearchInSubscription();
                        return;
                    }
                }

                if (comboBox_SearchSubscription_IDEdition.SelectedIndex > 0)
                {
                    int editionId = searchSubscription_EditionArr[comboBox_SearchSubscription_IDEdition.SelectedIndex - cbSearchSubscriptionStep].edition_Id;
                    searchSubscription_Edition = context.Editions.FirstOrDefault<Edition>(c => c.edition_Id == editionId);
                    if (searchSubscription_Edition == null)
                    {
                        MessageBox.Show("Ошибка (Тип не найден! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        SetDataSearchInSubscription();
                        return;
                    }
                }

                if (DateTime.TryParse(DatePicker_SearchSubscription_MinDateCreate.Text, out searchSubscription_minDateCreate) == true)
                {
                    searchSubscription_minDateCreate = DateTime.Parse(DatePicker_SearchSubscription_MinDateCreate.Text);
                }
                if (DateTime.TryParse(DatePicker_SearchSubscription_MaxDateCreate.Text, out searchSubscription_maxDateCreate) == true)
                {
                    searchSubscription_maxDateCreate = DateTime.Parse(DatePicker_SearchSubscription_MaxDateCreate.Text);
                }

                if (DateTime.TryParse(DatePicker_SearchSubscription_MinDateExpiration.Text, out searchSubscription_minDateExpiration) == true)
                {
                    searchSubscription_minDateExpiration = DateTime.Parse(DatePicker_SearchSubscription_MinDateExpiration.Text);
                }
                if (DateTime.TryParse(DatePicker_SearchSubscription_MaxDateExpiration.Text, out searchSubscription_maxDateExpiration) == true)
                {
                    searchSubscription_maxDateExpiration = DateTime.Parse(DatePicker_SearchSubscription_MaxDateExpiration.Text);
                }

            }
        }

        //Метод поиска в таблице Подписок
        private void SearchInSubscription()
        {
            Subscription[] sortSubscriptionsArr = subscriptionArr.ToArray();

            if (searchSubscription_minId >= 0)
            {
                List<Subscription> tempList = new List<Subscription>();
                foreach (Subscription subscription in sortSubscriptionsArr)
                {
                    if (subscription.subscription_Id >= searchSubscription_minId)
                    {
                        tempList.Add(subscription);
                    }
                }
                sortSubscriptionsArr = tempList.ToArray();
            }
            if (searchSubscription_maxId >= 0)
            {
                List<Subscription> tempList = new List<Subscription>();
                foreach (Subscription subscription in sortSubscriptionsArr)
                {
                    if (subscription.subscription_Id <= searchSubscription_maxId)
                    {
                        tempList.Add(subscription);
                    }
                }
                sortSubscriptionsArr = tempList.ToArray();
            }

            if (searchSubscription_Subscriber != null)
            {
                List<Subscription> tempList = new List<Subscription>();
                foreach (Subscription subscription in sortSubscriptionsArr)
                {
                    if (subscription.id_Subscriber.subscriber_Id == searchSubscription_Subscriber.subscriber_Id)
                    {
                        tempList.Add(subscription);
                    }
                }
                sortSubscriptionsArr = tempList.ToArray();
            }

            if (searchSubscription_Edition != null)
            {
                List<Subscription> tempList = new List<Subscription>();
                foreach (Subscription subscription in sortSubscriptionsArr)
                {
                    if (subscription.id_Edition.edition_Id == searchSubscription_Edition.edition_Id)
                    {
                        tempList.Add(subscription);
                    }
                }
                sortSubscriptionsArr = tempList.ToArray();
            }

            if (searchSubscription_minDateCreate > DateTime.MinValue)
            {
                List<Subscription> tempList = new List<Subscription>();
                foreach (Subscription subscription in sortSubscriptionsArr)
                {
                    if (subscription.date_Сreation.CompareTo(searchSubscription_minDateCreate) >= 0)
                    {
                        tempList.Add(subscription);
                    }
                }
                sortSubscriptionsArr = tempList.ToArray();
            }
            if (searchSubscription_maxDateCreate > DateTime.MinValue)
            {
                List<Subscription> tempList = new List<Subscription>();
                foreach (Subscription subscription in sortSubscriptionsArr)
                {
                    if (subscription.date_Сreation.CompareTo(searchSubscription_maxDateCreate) <= 0)
                    {
                        tempList.Add(subscription);
                    }
                }
                sortSubscriptionsArr = tempList.ToArray();
            }

            if (searchSubscription_minDateExpiration > DateTime.MinValue)
            {
                List<Subscription> tempList = new List<Subscription>();
                foreach (Subscription subscription in sortSubscriptionsArr)
                {
                    if (subscription.date_Expiration.CompareTo(searchSubscription_minDateExpiration) >= 0)
                    {
                        tempList.Add(subscription);
                    }
                }
                sortSubscriptionsArr = tempList.ToArray();
            }
            if (searchSubscription_maxDateExpiration > DateTime.MinValue)
            {
                List<Subscription> tempList = new List<Subscription>();
                foreach (Subscription subscription in sortSubscriptionsArr)
                {
                    if (subscription.date_Expiration.CompareTo(searchSubscription_maxDateExpiration) <= 0)
                    {
                        tempList.Add(subscription);
                    }
                }
                sortSubscriptionsArr = tempList.ToArray();
            }

            if (sortSubscriptionsArr.Length == 0)
            {
                MessageBox.Show("Поиск не дал результатов!", "Нет результатов", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            subscriptionArrNow = sortSubscriptionsArr;

            dataGrid_Subscription.ItemsSource = subscriptionArrNow.ToList<Subscription>();
        }

        //Событие кнопки "Очистить" в блоке поиска таблицы Подписок
        private void button_Clean_SearchSubscription_Click(object sender, RoutedEventArgs e)
        {
            CleanSearchSubscriptionVariables();
            CleanSearchSubscription();
            RefreshSubscriptionTable();
        }

        //Метод очистки полей поиска в таблице Подписок
        private void CleanSearchSubscription()
        {
            textBox_SearchSubscription_MinID.Text = null;
            textBox_SearchSubscription_MaxID.Text = null;

            comboBox_SearchSubscription_IDSubscriber.SelectedIndex = 0;
            comboBox_SearchSubscription_IDEdition.SelectedIndex = 0;

            DatePicker_SearchSubscription_MinDateCreate.Text = null;
            DatePicker_SearchSubscription_MaxDateCreate.Text = null;

            DatePicker_SearchSubscription_MinDateExpiration.Text = null;
            DatePicker_SearchSubscription_MaxDateExpiration.Text = null;
        }

        //Очистка переменных поиска в таблице Подписок
        private void CleanSearchSubscriptionVariables()
        {
            subscriptionArrNow = subscriptionArr;

            searchSubscription_minId = -1;
            searchSubscription_maxId = -1;

            searchSubscription_Subscriber = null;

            searchSubscription_Edition = null;

            searchSubscription_minDateCreate = DateTime.MinValue;
            searchSubscription_maxDateCreate = DateTime.MinValue;

            searchSubscription_minDateExpiration = DateTime.MinValue;
            searchSubscription_maxDateExpiration = DateTime.MinValue;
        }

        //Событие нажатия на заголовок таблицы
        private void dataGrid_Subscription_MouseColumnHeaderClick(object sender, MouseColumnHeaderEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    SortSubscriptionByID();
                    break;

                case 1:
                    SortSubscriptionBySubscriberID();
                    break;

                case 2:
                    SortSubscriptionByEditionStr();
                    break;

                case 3:
                    SortSubscriptionByDateCreate();
                    break;

                case 4:
                    SortSubscriptionByDateExpiration();
                    break;
            }
        }

        bool isAscendingSubscriptionID = true;
        //Метод сортировка таблицы по ID
        private void SortSubscriptionByID()
        {
            Subscription[] tempArr = subscriptionArrNow;
            if (isAscendingSubscriptionID == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.subscription_Id).ToArray();
                isAscendingSubscriptionID = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.subscription_Id).ToArray();
                isAscendingSubscriptionID = true;
            }

            dataGrid_Subscription.SelectedIndex = -1;
            dataGrid_Subscription.ItemsSource = tempArr;
        }
   
        bool isAscendingSubscriber = false;
        //Метод сортировка таблицы по ссылку на подписчика
        private void SortSubscriptionBySubscriberID()
        {
            Subscription[] tempArr = subscriptionArrNow;
            if (isAscendingSubscriber == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.id_Subscriber.subscriber_Id).ToArray();
                isAscendingSubscriber = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.id_Subscriber.subscriber_Id).ToArray();
                isAscendingSubscriber = true;
            }

            dataGrid_Subscription.SelectedIndex = -1;
            dataGrid_Subscription.ItemsSource = tempArr;
        }
        
        bool isAscendingEdition = false;
        //Метод сортировка таблицы по строковому значению ссылки на издание
        private void SortSubscriptionByEditionStr()
        {
            Subscription[] tempArr = subscriptionArrNow;
            if (isAscendingEdition == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.id_Edition.ToString()).ToArray();
                isAscendingEdition = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.id_Edition.ToString()).ToArray();
                isAscendingEdition = true;
            }

            dataGrid_Subscription.SelectedIndex = -1;
            dataGrid_Subscription.ItemsSource = tempArr;
        }

        bool isAscendingDateCreate = false;
        //Метод сортировка таблицы по дате начала
        private void SortSubscriptionByDateCreate()
        {
            Subscription[] tempArr = subscriptionArrNow;
            if (isAscendingDateCreate == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.date_Сreation).ToArray();
                isAscendingDateCreate = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.date_Сreation).ToArray();
                isAscendingDateCreate = true;
            }

            dataGrid_Subscription.SelectedIndex = -1;
            dataGrid_Subscription.ItemsSource = tempArr;
        }

        bool isAscendingDateExpiration = false;
        //Метод сортировка таблицы по дате завершения
        private void SortSubscriptionByDateExpiration()
        {
            Subscription[] tempArr = subscriptionArrNow;
            if (isAscendingDateExpiration == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.date_Expiration).ToArray();
                isAscendingDateExpiration = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.date_Expiration).ToArray();
                isAscendingDateExpiration = true;
            }

            dataGrid_Subscription.SelectedIndex = -1;
            dataGrid_Subscription.ItemsSource = tempArr;
        }

        //Метод очистки переменных сортировки
        private void CleanSortSubscriptionVariable()
        {
            isAscendingSubscriptionID = true;
            isAscendingSubscriber = false;
            isAscendingEdition = false;
            isAscendingDateCreate = false;
            isAscendingDateExpiration = false;
        }
    }
}
