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

namespace WpfMailDBManager
{
    public partial class MainWindow : Window  //Главная таблица
    {
        private Mail[] mailArr;
        private Mail[] mailArrNow;
        ShowOrDeleteInMainTable ShowMail;
        ShowOrDeleteInMainTable DeleteMail;
        InsertOrUpdateToMainTable InsertMail;
        InsertOrUpdateToMainTable UpdateMail;

        //Метод для установки значений в Главную таблицу
        private void SetDataInGridMainTable()
        {
            using (MailContext context = new MailContext())
            {
                IQueryable<Mail> mails = context.Mail.Select(c => c);

                foreach (Mail mail in mails)
                {
                    mail.id_Mailing.ToString();
                    mail.id_Operation.ToString();
                    if (mail.id_Subscription != null)
                    {
                        mail.id_Subscription.ToString();
                    }
                    mail.date_Operation.ToLongDateString();
                }

                mailArr = mails.ToArray<Mail>();

                dataGrid_MainTable.ItemsSource = mails.ToList<Mail>();

                mailArrNow = mailArr;
            }
        }

        //Метод для очистки Главной таблицы
        private void CleanDataMainTable()
        {
            mailArr = null;
            dataGrid_MainTable.ItemsSource = mailArr;
        }

        private List<ShowOrDeleteInMainTable> ShowMailList = new List<ShowOrDeleteInMainTable>();
        //Событие двойного нажатия на запись в Главной таблице. 
        private void dataGrid_MainTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowInMainTable();
        }

        //Метод вызова окна просмотра записи, а также контроля за ними
        private void ShowInMainTable()
        {
            int rowIndex = dataGrid_MainTable.SelectedIndex;

            if (rowIndex >= 0)
            {
                int index = mailArrNow[rowIndex].mail_Id;
                if (ShowMailList.Count != 0)
                {
                    //Очистка листа ShowMailList от закрытых окон
                    ShowOrDeleteInMainTable[] tempArr = ShowMailList.ToArray();
                    foreach (ShowOrDeleteInMainTable SODIMT in tempArr)
                    {
                        if (SODIMT.IsVisible == false)
                        {
                            MessageBox.Show("Удаление");
                            ShowMailList.Remove(SODIMT);
                        }
                    }

                    //Вывод окна на передний план если оно уже было открыто
                    foreach (ShowOrDeleteInMainTable SODIMT in ShowMailList)
                    {
                        if (SODIMT.id == index)
                        {
                            SODIMT.Activate();
                            return;
                        }
                    }
                }
                ShowMail = new ShowOrDeleteInMainTable();

                ShowMail.Owner = this;
                ShowMail.Show();
                ShowMail.SetData(index, ShowOrDeleteInMainTable.ModeWindow.Show);
                ShowMailList.Add(ShowMail);
            }
        }

        //Событие кнопки "Обновить" для Главной таблицы
        private void button_Click_RefreshMainTable(object sender, RoutedEventArgs e)
        {
            RefreshMainTable();
        }

        //Вызов набора методов для обновление блока Главной таблицы
        public void RefreshMainTable()
        {
            SetDataInGridMainTable();
            SetDataSearchInMainTable();

            CleanSortMainVariable();

            InitializeSearchDataInMainTable();
            SearchInMainTable();
        }

        //Событие кнопки "Добавить запись" для Главной таблицы
        private void button_InsertToMainTable_Click(object sender, RoutedEventArgs e)
        {
            InserToMainTable();
        }

        //Вызов окна добавления записи для Главной таблицы, а также контроль за ним 
        private void InserToMainTable()
        {
            if (InsertMail != null)
            {
                if (InsertMail.IsVisible == true && InsertMail.IsActive == false)
                {
                    InsertMail.Activate();
                    return;
                }
            }
            InsertMail = null;
            InsertMail = new InsertOrUpdateToMainTable();
            InsertMail.SetMode(InsertOrUpdateToMainTable.ModeWindow.Insert);
            InsertMail.mainWindow = this;

            InsertMail.Owner = this;
            InsertMail.Show();
        }

        //Событие кнопки "Редактировать" для Главной таблицы
        private void button_UpdateInMainTable_Click(object sender, RoutedEventArgs e)
        {
            UpdateInMainTable();
        }

        //Вызов окна редактирования записи для Главной таблицы, а также контроль за ним 
        private void UpdateInMainTable()
        {
            if (UpdateMail != null)
            {
                if (UpdateMail.IsVisible == true && UpdateMail.IsActive == false)
                {
                    UpdateMail.Activate();
                    return;
                }
            }
            UpdateMail = null;
            UpdateMail = new InsertOrUpdateToMainTable();
            UpdateMail.SetMode(InsertOrUpdateToMainTable.ModeWindow.Update);
            if (dataGrid_MainTable.SelectedIndex >= 0)
            {
                UpdateMail.addData(mailArr[dataGrid_MainTable.SelectedIndex]);
                UpdateMail.Owner = this;
                UpdateMail.mainWindow = this;

                UpdateMail.Show();
            }
            else
            {
                MessageBox.Show("Выделите запись которую желаете изменить", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Событие кнопки "Удалить запись" для Главной таблицы
        private void button_DeleteInMainTable_Click(object sender, RoutedEventArgs e)
        {
            DeleteInMainTable();
        }

        //Вызов окна подтверждения удаления записи для Главной таблицы, а также удаление записи 
        private void DeleteInMainTable()
        {
            DeleteMail = new ShowOrDeleteInMainTable();
            int selectedIndex;
            int selectedRow = dataGrid_MainTable.SelectedIndex;
            if (selectedRow >= 0)
            {
                DeleteMail.SetData(mailArr[selectedRow], ShowOrDeleteInMainTable.ModeWindow.DeleteConfirm);
                selectedIndex = mailArr[selectedRow].mail_Id;
            }
            else
            {
                MessageBox.Show("Выделите запись которую желаете удалить", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DeleteMail.Owner = this;

            bool? result = DeleteMail.ShowDialog();
            if (result == true)
            {
                using (MailContext context = new MailContext())
                {
                    Mail mail = context.Mail.FirstOrDefault<Mail>(c => c.mail_Id == selectedIndex);
                    context.Mail.Remove(mail);
                    context.SaveChanges();

                    MessageBox.Show("Запись №" + selectedIndex + " в Главной таблице удалена успешно!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                    RefreshMainTable();
                }
            }
        }

        Operation[] searchMainTable_operationsArr;
        Mailing[] searchMainTable_mailingsArr;
        Subscription[] searchMainTable_subscriptionsArr;
        //Метод установки значений ссылкой на другую таблицу в ComboBox в блоке поиска значений в Главной таблице
        private void SetDataSearchInMainTable()
        {
            using (MailContext context = new MailContext())
            {
                IQueryable<Operation> operations = context.Operations.Select(c => c);
                IQueryable<Mailing> mailings = context.Mailings.Select(c => c);
                IQueryable<Subscription> subscriptions = context.Subscriptions.Select(c => c);

                searchMainTable_operationsArr = operations.ToArray<Operation>();
                searchMainTable_mailingsArr = mailings.ToArray<Mailing>();
                searchMainTable_subscriptionsArr = subscriptions.ToArray<Subscription>();

                comboBox_Search_IDOperation.Items.Clear();
                comboBox_Search_IDMailing.Items.Clear();
                comboBox_Search_IDSubscription.Items.Clear();

                comboBox_Search_IDOperation.Items.Add("Нет");
                comboBox_Search_IDMailing.Items.Add("Нет");
                comboBox_Search_IDSubscription.Items.Add("Нет");
                comboBox_Search_IDSubscription.Items.Add("Без подписки"); //Т.к. подписка можент быть null

                foreach (Operation operation in operations)
                {
                    comboBox_Search_IDOperation.Items.Add(operation.ToString());
                }
                foreach (Mailing mailing in mailings)
                {
                    comboBox_Search_IDMailing.Items.Add(mailing.ToString());
                }
                foreach (Subscription subscription in subscriptions)
                {
                    comboBox_Search_IDSubscription.Items.Add(subscription.LongToString());
                }

                comboBox_Search_IDOperation.SelectedIndex = 0;
                comboBox_Search_IDMailing.SelectedIndex = 0;
                comboBox_Search_IDSubscription.SelectedIndex = 0;
            }
        }

        //Вызов набора методов для полной очистки полей и переменных поиска для Главной таблицы
        private void CleanDataSearchInMainTable()
        {
            CleanMainSearchVariables();
            CleanMainSearch();
            comboBox_Search_IDOperation.Items.Clear();
            comboBox_Search_IDMailing.Items.Clear();
            comboBox_Search_IDSubscription.Items.Clear();
        }

        //Событие кнопки "Поиск" для Главной таблицы
        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            InitializeSearchDataInMainTable();
            SearchInMainTable();
        }

        int searchMainTable_minId = -1;
        int searchMainTable_maxId = -1;

        Operation searchMainTable_operation = null;

        Mailing searchMainTable_mailing = null;

        Subscription searchMainTable_subscription = null;
        bool searchMainTable_canSubNull = false; //Т.к. поиск может происходить по отсутствию подписки
        //, необходимо разделять не инициализированную переменную, от параметра поиска

        string searchMainTable_senderAddress = "";
        string searchMainTable_recipientAddress = "";

        float searchMainTable_minWeight = -1;
        float searchMainTable_maxWeight = -1;

        DateTime searchMainTable_minDateOperation = DateTime.MinValue;
        DateTime searchMainTable_maxDateOperation = DateTime.MinValue;

        decimal searchMainTable_minPrice = -1;
        decimal searchMainTable_maxPrice = -1;

        const int cbSearchMainTableStep = 1; //Делаем один шаг, т.к. Первая запись в comboBox пустая
        const int cbSubscriptionSearchMainTableStep = 2; //Делаем два шага, т.к. Первая запись в comboBox пустая, а также id_Subscription может быть Null
       
        //Метод инициализации переменных для поиска в Главной таблице
        private void InitializeSearchDataInMainTable()
        {
            //Предварительно очищаем переменные
            CleanMainSearchVariables();

            using (MailContext context = new MailContext())
            {
                textBox_Search_MinID.Text.Trim();
                if (textBox_Search_MinID.Text != null && textBox_Search_MinID.Text != "")
                {
                    if (Int32.TryParse(textBox_Search_MinID.Text, out searchMainTable_minId) == true)
                    {
                        searchMainTable_minId = Int32.Parse(textBox_Search_MinID.Text);
                    }
                }
                textBox_Search_MaxID.Text.Trim();
                if (textBox_Search_MaxID.Text != null && textBox_Search_MaxID.Text != "")
                {
                    if (Int32.TryParse(textBox_Search_MaxID.Text, out searchMainTable_maxId) == true)
                    {
                        searchMainTable_maxId = Int32.Parse(textBox_Search_MaxID.Text);
                    }
                }

                if (comboBox_Search_IDOperation.SelectedIndex > 0)
                {
                    int operationId = searchMainTable_operationsArr[comboBox_Search_IDOperation.SelectedIndex - cbSearchMainTableStep].operation_Id;
                    searchMainTable_operation = context.Operations.FirstOrDefault<Operation>(c => c.operation_Id == operationId);
                    if (searchMainTable_operation == null)
                    {
                        MessageBox.Show("Ошибка (Операциция не найдена! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        SetDataSearchInMainTable();
                        return;
                    }
                }

                //Ссылки на другие таблицы перепроверяются на случай изменения в соответствующих таблицах
                if (comboBox_Search_IDMailing.SelectedIndex > 0)
                {
                    int mailingId = searchMainTable_mailingsArr[comboBox_Search_IDMailing.SelectedIndex - cbSearchMainTableStep].mailing_Id;
                    searchMainTable_mailing = context.Mailings.FirstOrDefault<Mailing>(c => c.mailing_Id == mailingId);
                    if (searchMainTable_mailing == null)
                    {
                        MessageBox.Show("Ошибка (Тип не найден! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        SetDataSearchInMainTable();
                        return;
                    }
                }

                if (comboBox_Search_IDSubscription.SelectedIndex > 1)
                {
                    int subscriptionId = searchMainTable_subscriptionsArr[comboBox_Search_IDSubscription.SelectedIndex - cbSubscriptionSearchMainTableStep].subscription_Id;
                    searchMainTable_subscription = context.Subscriptions.FirstOrDefault<Subscription>(c => c.subscription_Id == subscriptionId);
                    if (searchMainTable_subscription == null)
                    {
                        MessageBox.Show("Ошибка (Подписка не найдена! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        SetDataSearchInMainTable();
                        return;
                    }
                }
                else if (comboBox_Search_IDSubscription.SelectedIndex == 1)
                {
                    searchMainTable_canSubNull = true;
                }

                textBox_Search_Sender.Text.Trim();
                if (textBox_Search_Sender.Text != "" || textBox_Search_Sender.Text != null)
                {
                    searchMainTable_senderAddress = textBox_Search_Sender.Text;
                }

                textBox_Search_Recipient.Text.Trim();
                if (textBox_Search_Recipient.Text != "" || textBox_Search_Recipient.Text != null)
                {
                    searchMainTable_recipientAddress = textBox_Search_Recipient.Text;
                }

                textBox_Search_MinWeight.Text.Trim();
                if (textBox_Search_MinWeight.Text != null && textBox_Search_MinWeight.Text != "")
                {
                    if (float.TryParse(textBox_Search_MinWeight.Text.Replace(",", "."), NumberStyles.Float, new CultureInfo("en-US"), out searchMainTable_minWeight) == true)
                    {
                        searchMainTable_minWeight = float.Parse(textBox_Search_MinWeight.Text, NumberStyles.Float, new CultureInfo("en-US"));
                    }
                }
                textBox_Search_MaxWeight.Text.Trim();
                if (textBox_Search_MaxWeight.Text != null && textBox_Search_MaxWeight.Text != "")
                {
                    if (float.TryParse(textBox_Search_MaxWeight.Text.Replace(",", "."), NumberStyles.Float, new CultureInfo("en-US"), out searchMainTable_maxWeight) == true)
                    {
                        searchMainTable_maxWeight = float.Parse(textBox_Search_MaxWeight.Text, NumberStyles.Float, new CultureInfo("en-US"));
                    }
                }

                if (DateTime.TryParse(DatePicker_Search_MinDate.Text, out searchMainTable_minDateOperation) == true)
                {
                    searchMainTable_minDateOperation = DateTime.Parse(DatePicker_Search_MinDate.Text);
                }
                if (DateTime.TryParse(DatePicker_Search_MaxDate.Text, out searchMainTable_maxDateOperation) == true)
                {
                    searchMainTable_maxDateOperation = DateTime.Parse(DatePicker_Search_MaxDate.Text);
                }

                textBox_Search_MinPrice.Text.Trim();
                if (textBox_Search_MinPrice.Text != null && textBox_Search_MinPrice.Text != "")
                {
                    if (decimal.TryParse(textBox_Search_MinPrice.Text.Replace(",", "."), NumberStyles.Float, new CultureInfo("en-US"), out searchMainTable_minPrice) == true)
                    {
                        searchMainTable_minPrice = decimal.Parse(textBox_Search_MinPrice.Text.Replace(",", "."), NumberStyles.Float, new CultureInfo("en-US"));
                    }
                }
                textBox_Search_MaxPrice.Text.Trim();
                if (textBox_Search_MaxPrice.Text != null && textBox_Search_MaxPrice.Text != "")
                {
                    if (decimal.TryParse(textBox_Search_MaxPrice.Text.Replace(",", "."), NumberStyles.Float, new CultureInfo("en-US"), out searchMainTable_maxPrice) == true)
                    {
                        searchMainTable_maxPrice = decimal.Parse(textBox_Search_MaxPrice.Text.Replace(",", "."), NumberStyles.Float, new CultureInfo("en-US"));
                    }
                }
            }
        }

        //Метод поиска в Главной таблице
        private void SearchInMainTable()
        {
            Mail[] sortMailsArr = mailArr.ToArray();

            if (searchMainTable_minId >= 0)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.mail_Id >= searchMainTable_minId)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }
            if (searchMainTable_maxId >= 0)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.mail_Id <= searchMainTable_maxId)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }

            if (searchMainTable_operation != null)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.id_Operation.operation_Id == searchMainTable_operation.operation_Id)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }

            if (searchMainTable_mailing != null)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.id_Mailing.mailing_Id == searchMainTable_mailing.mailing_Id)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }

            if (searchMainTable_subscription != null || searchMainTable_canSubNull == true)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.id_Subscription.subscription_Id == searchMainTable_subscription.subscription_Id)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }

            if (searchMainTable_senderAddress != null && searchMainTable_senderAddress != "")
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.sender_Address.Contains(searchMainTable_senderAddress))
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }

            if (searchMainTable_recipientAddress != null && searchMainTable_recipientAddress != "")
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.recipient_Address.Contains(searchMainTable_recipientAddress))
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }

            if (searchMainTable_minWeight >= 0)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.weight_Package >= searchMainTable_minWeight)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }
            if (searchMainTable_maxWeight >= 0)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.weight_Package <= searchMainTable_maxWeight)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }

            if (searchMainTable_minWeight >= 0)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.weight_Package >= searchMainTable_minWeight)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }
            if (searchMainTable_maxWeight >= 0)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.weight_Package <= searchMainTable_maxWeight)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }

            if (searchMainTable_minDateOperation > DateTime.MinValue)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.date_Operation.CompareTo(searchMainTable_minDateOperation) >= 0)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }
            if (searchMainTable_maxDateOperation > DateTime.MinValue)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.date_Operation.CompareTo(searchMainTable_maxDateOperation) <= 0)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }

            if (searchMainTable_minPrice >= 0)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.price >= searchMainTable_minPrice)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }
            if (searchMainTable_maxPrice >= 0)
            {
                List<Mail> tempList = new List<Mail>();
                foreach (Mail mail in sortMailsArr)
                {
                    if (mail.price <= searchMainTable_maxPrice)
                    {
                        tempList.Add(mail);
                    }
                }
                sortMailsArr = tempList.ToArray();
            }
            if (sortMailsArr.Length == 0)
            {
                MessageBox.Show("Поиск не дал результатов!", "Нет результатов", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                return;
            }

            mailArrNow = sortMailsArr;

            dataGrid_MainTable.ItemsSource = sortMailsArr.ToList<Mail>();
        }

        //Событие кнопки "Очистить" в блоке поиска Главной таблицы
        private void button_Clean_Main_Search_Click(object sender, RoutedEventArgs e)
        {
            CleanMainSearch();
            CleanMainSearchVariables();
            RefreshMainTable();
        }

        //Метод очистки полей поиска в Главной таблице
        private void CleanMainSearch()
        {        
            textBox_Search_MinID.Text = null;
            textBox_Search_MaxID.Text = null;

            comboBox_Search_IDOperation.SelectedIndex = 0;
            comboBox_Search_IDMailing.SelectedIndex = 0;
            comboBox_Search_IDSubscription.SelectedIndex = 0;

            textBox_Search_Sender.Text = null;
            textBox_Search_Recipient.Text = null;

            textBox_Search_MinWeight.Text = null;
            textBox_Search_MaxWeight.Text = null;

            DatePicker_Search_MinDate.Text = null;
            DatePicker_Search_MaxDate.Text = null;

            textBox_Search_MinPrice.Text = null;
            textBox_Search_MaxPrice.Text = null;
        }

        //Очистка переменных поиска в Главной таблице
        private void CleanMainSearchVariables()
        {
            mailArrNow = mailArr;

            searchMainTable_minId = -1;
            searchMainTable_maxId = -1;

            searchMainTable_operation = null;

            searchMainTable_mailing = null;

            searchMainTable_subscription = null;
            searchMainTable_canSubNull = false;

            searchMainTable_senderAddress = "";
            searchMainTable_recipientAddress = "";

            searchMainTable_minWeight = -1;
            searchMainTable_maxWeight = -1;

            searchMainTable_minDateOperation = DateTime.MinValue;
            searchMainTable_maxDateOperation = DateTime.MinValue;

            searchMainTable_minPrice = -1;
            searchMainTable_maxPrice = -1;
        }

        //Событие нажатия на заголовок таблицы
        private void dataGrid_MainTable_MouseColumnHeaderClick(object sender, MouseColumnHeaderEventArgs e)
        {          
            switch (e.ColumnIndex)
            {
                case 0:
                    SortMainTableByID();
                    break;

                case 1:
                    SortMainTableByOperationStr();
                    break;

                case 2:
                    SortMainTableByMailingStr();
                    break;

                case 3:
                    SortMainTableBySubscriptionID();
                    break;

                case 4:
                    SortMainTableBySenderAddress();
                    break;

                case 5:
                    SortMainTableByRecipientAddress();
                    break;

                case 6:
                    SortMainTableByWeightPackage();
                    break;

                case 7:
                    SortMainTableByDateOperation();
                    break;

                case 8:
                    SortMainTableByPrice();
                    break;
            }
        }

        bool isAscendingMainTableID = true;
        //Метод сортировка таблицы по ID
        private void SortMainTableByID()
        {
            Mail[] tempArr = mailArrNow;
            if (isAscendingMainTableID == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.mail_Id).ToArray();
                isAscendingMainTableID = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.mail_Id).ToArray();
                isAscendingMainTableID = true;
            }

            dataGrid_MainTable.SelectedIndex = -1;
            dataGrid_MainTable.ItemsSource = tempArr;
        }

        bool isAscendingOperation = false;
        //Метод сортировка таблицы по строковому значению ссылки на тип операции
        private void SortMainTableByOperationStr()
        {
            Mail[] tempArr = mailArrNow;
            if (isAscendingOperation == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.id_Operation.type_Operation).ToArray();
                isAscendingOperation = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.id_Operation.type_Operation).ToArray();
                isAscendingOperation = true;
            }

            dataGrid_MainTable.SelectedIndex = -1;
            dataGrid_MainTable.ItemsSource = tempArr;
        }

        bool isAscendingMailing = false;
        //Метод сортировка таблицы по строковому значению ссылки на тип почтового отправления
        private void SortMainTableByMailingStr()
        {
            Mail[] tempArr = mailArrNow;
            if (isAscendingMailing == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.id_Mailing.type_Mailing).ToArray();
                isAscendingMailing = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.id_Mailing.type_Mailing).ToArray();
                isAscendingMailing = true;
            }

            dataGrid_MainTable.SelectedIndex = -1;
            dataGrid_MainTable.ItemsSource = tempArr;
        }

        bool isAscendingSubscription = false;
        //Метод сортировка таблицы по числовому значению ссылки на подписку
        private void SortMainTableBySubscriptionID()
        {
            Mail[] tempArr = mailArrNow;
            if (isAscendingSubscription == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.id_Subscription.subscription_Id).ToArray();
                isAscendingSubscription = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.id_Subscription.subscription_Id).ToArray();
                isAscendingSubscription = true;
            }

            dataGrid_MainTable.SelectedIndex = -1;
            dataGrid_MainTable.ItemsSource = tempArr;
        }

        bool isAscendingSenderAddress = false;
        //Метод сортировка таблицы по адресу отправителя
        private void SortMainTableBySenderAddress()
        {
            Mail[] tempArr = mailArrNow;
            if (isAscendingSenderAddress == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.sender_Address).ToArray();
                isAscendingSenderAddress = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.sender_Address).ToArray();
                isAscendingSenderAddress = true;
            }

            dataGrid_MainTable.SelectedIndex = -1;
            dataGrid_MainTable.ItemsSource = tempArr;
        }

        bool isAscendingRecipientAddress = false;
        //Метод сортировка таблицы по адресу получателя
        private void SortMainTableByRecipientAddress()
        {
            Mail[] tempArr = mailArrNow;
            if (isAscendingRecipientAddress == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.recipient_Address).ToArray();
                isAscendingRecipientAddress = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.recipient_Address).ToArray();
                isAscendingRecipientAddress = true;
            }

            dataGrid_MainTable.SelectedIndex = -1;
            dataGrid_MainTable.ItemsSource = tempArr;
        }

        bool isAscendingWeightPackage = false;
        //Метод сортировка таблицы по весу
        private void SortMainTableByWeightPackage()
        {
            Mail[] tempArr = mailArrNow;
            if (isAscendingWeightPackage == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.weight_Package).ToArray();
                isAscendingWeightPackage = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.weight_Package).ToArray();
                isAscendingWeightPackage = true;
            }

            dataGrid_MainTable.SelectedIndex = -1;
            dataGrid_MainTable.ItemsSource = tempArr;
        }

        bool isAscendingDateOperation = false;
        //Метод сортировка таблицы по дате
        private void SortMainTableByDateOperation()
        {
            Mail[] tempArr = mailArrNow;
            if (isAscendingDateOperation == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.date_Operation).ToArray();
                isAscendingDateOperation = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.date_Operation).ToArray();
                isAscendingDateOperation = true;
            }

            dataGrid_MainTable.SelectedIndex = -1;
            dataGrid_MainTable.ItemsSource = tempArr;
        }

        bool isAscendingPrice = false;
        //Метод сортировка таблицы по цене
        private void SortMainTableByPrice()
        {
            Mail[] tempArr = mailArrNow;
            if (isAscendingPrice == true)
            {
                tempArr = tempArr.OrderByDescending(x => x.price).ToArray();
                isAscendingPrice = false;
            }
            else
            {
                tempArr = tempArr.OrderBy(x => x.price).ToArray();
                isAscendingPrice = true;
            }

            dataGrid_MainTable.SelectedIndex = -1;
            dataGrid_MainTable.ItemsSource = tempArr;
        }

        //Метод очистки переменных сортировки
        private void CleanSortMainVariable()
        {
            isAscendingMainTableID = true;
            isAscendingOperation = false;
            isAscendingMailing = false;
            isAscendingSubscription = false;
            isAscendingSenderAddress = false;
            isAscendingRecipientAddress = false;
            isAscendingWeightPackage = false;
            isAscendingDateOperation = false;
            isAscendingPrice = false;
        }
    }
    public class MouseColumnHeaderEventArgs : EventArgs
    {
        public int ColumnIndex { get; }

        public MouseColumnHeaderEventArgs(int columnIndex)
        {
            ColumnIndex = columnIndex;
        }
    }


}
