using WpfMailDBManager.DataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WpfMailDBManager.MainTableWindows
{
    /// <summary>
    /// Логика взаимодействия для AddToMainTable.xaml
    /// </summary>
    public partial class InsertOrUpdateToMainTable : Window
    {
        public enum ModeWindow
        {
            Insert,
            Update
        }
        public MainWindow mainWindow { get; set; }

        private ModeWindow currentMW { get; set; } = 0;
        Mail updateMail = null;

        Operation[] operationsArr;
        Mailing[] mailingsArr;
        Subscription[] subscriptionsArr;
        public InsertOrUpdateToMainTable()
        {
            InitializeComponent();
            SetData();
        }

        //Установка режима роботы окна
        public void SetMode(ModeWindow MW)
        {
            currentMW = MW;
            if(currentMW == ModeWindow.Insert)
            {
                this.Title = "Добавление записи в Главную таблицу";
            }
            if (currentMW == ModeWindow.Update)
            {
                this.Title = "Редактирование записи в Главной таблице";
            }
        }

        //Установка значений в ComboBox и в строку даты
        private void SetData()
        {
            using (MailContext context = new MailContext())
            {
                IQueryable<Operation> operations = context.Operations.Select(c => c);
                IQueryable<Mailing> mailings = context.Mailings.Select(c => c);
                IQueryable<Subscription> subscriptions = context.Subscriptions.Select(c => c);

                operationsArr = operations.ToArray<Operation>();
                mailingsArr = mailings.ToArray<Mailing>();
                subscriptionsArr = subscriptions.ToArray<Subscription>();

                IOUMainTable_ComboBox_id_Subscription.Items.Add("Без подписки");
                foreach (Operation operation in operations)
                {
                    IOUMainTable_ComboBox_id_Operation.Items.Add(operation.ToString());
                }
                foreach (Mailing mailing in mailings)
                {
                    IOUMainTable_ComboBox_id_Mailing.Items.Add(mailing.ToString());
                }
                foreach (Subscription subscription in subscriptions)
                {
                    IOUMainTable_ComboBox_id_Subscription.Items.Add(subscription.LongToString());
                }

                IOUMainTable_TBox_date_Operation.Text = DateTime.UtcNow.ToLocalTime().ToString();
            }
        }

        //Установка значений в соответствующие поля для редактирования
        // mail - запись которая редактируется
        public void addData(Mail mail)
        {
            updateMail = mail;

            int operationArrIndex = -1;
            foreach (Operation operation in operationsArr)
            {
                operationArrIndex++;
                if (operation.operation_Id == mail.id_Operation.operation_Id)
                {
                    break;
                }
            }
            IOUMainTable_ComboBox_id_Operation.SelectedIndex = operationArrIndex;

            int mailingArrIndex = -1;
            foreach (Mailing mailing in mailingsArr)
            {
                mailingArrIndex++;
                if (mailing.mailing_Id == mail.id_Mailing.mailing_Id)
                {
                    break;
                }
            }
            IOUMainTable_ComboBox_id_Mailing.SelectedIndex = mailingArrIndex;

            int subscriptionArrIndex = -1;
            if (mail.id_Subscription != null)
            {
                foreach (Subscription subscription in subscriptionsArr)
                {
                    subscriptionArrIndex++;
                    if (subscription.subscription_Id == mail.id_Subscription.subscription_Id)
                    {
                        break;
                    }
                }
                IOUMainTable_ComboBox_id_Subscription.SelectedIndex = subscriptionArrIndex;
            }

            IOUMainTable_TBox_sender_Address.Text = mail.sender_Address;
            IOUMainTable_TBox_recipient_Address.Text = mail.recipient_Address;
            IOUMainTable_TBox_weight_Package.Text = mail.weight_Package.ToString();
            IOUMainTable_TBox_price.Text = mail.price.ToString();
        }

        const int cbId_SubStep = 1; //Делаем один шага, т.к. id_Subscription может быть Null
        bool canSubNull = false;
        //Событие кнопки "ОК"
        private void button_Click_OK(object sender, RoutedEventArgs e)
        {
            Operation operation = null;
            Mailing mailing = null;
            Subscription subscription = null;
            string senderAddress = null;
            string recipientAddress = null;
            float weight;
            DateTime dateOperation;
            decimal price;

            using (MailContext context = new MailContext())
            {
                Mail mail = new Mail();

                if (currentMW == ModeWindow.Update)
                {
                    mail = context.Mail.FirstOrDefault(c => c.mail_Id == updateMail.mail_Id);
                }

                if (IOUMainTable_ComboBox_id_Operation.SelectedIndex < 0)
                {
                    MessageBox.Show("Ошибка (Операциция не выбрана)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int operationId = operationsArr[IOUMainTable_ComboBox_id_Operation.SelectedIndex].operation_Id;
                operation = context.Operations.FirstOrDefault<Operation>(c => c.operation_Id == operationId);
                if (operation == null)
                {
                    MessageBox.Show("Ошибка (Операциция не найдена! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    SetData();
                    return;
                }

                if (IOUMainTable_ComboBox_id_Mailing.SelectedIndex < 0)
                {
                    MessageBox.Show("Ошибка (Тип не выбран)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int mailingId = mailingsArr[IOUMainTable_ComboBox_id_Mailing.SelectedIndex].mailing_Id;
                mailing = context.Mailings.FirstOrDefault<Mailing>(c => c.mailing_Id == mailingId);
                if (mailing == null)
                {
                    MessageBox.Show("Ошибка (Тип не найден! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    SetData();
                    return;
                }

                if (IOUMainTable_ComboBox_id_Subscription.SelectedIndex > 0)
                {
                    int subscriptionId = subscriptionsArr[IOUMainTable_ComboBox_id_Subscription.SelectedIndex - cbId_SubStep].subscription_Id;
                    subscription = context.Subscriptions.FirstOrDefault<Subscription>(c => c.subscription_Id == subscriptionId);
                   
                }
                else if (IOUMainTable_ComboBox_id_Subscription.SelectedIndex == 0)
                {
                    canSubNull = true;
                }
                if (subscription == null && canSubNull == false)
                {
                    MessageBox.Show("Ошибка (Подписка не найдена! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    SetData();
                    return;
                }

                senderAddress = IOUMainTable_TBox_sender_Address.Text;
                if (isValidAddress(senderAddress) == false)
                {
                    MessageBox.Show("Ошибка! (Адрес отправителя введен неправильно или не введен вовсе)"
                        + "\n" + "Внимание! Максимальная длинна адреса " + maxLenghtStringAddress + " символов"
                        + "\n" + "Форма записи(Страна, город, 'район', улица, дом, 'картира'"
                        + "\n" + "' ' - необязательны для ввода в случае их отсутствия"
                        , "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    senderAddress = null;
                    return;
                }

                recipientAddress = IOUMainTable_TBox_recipient_Address.Text;
                if (isValidAddress(recipientAddress) == false)
                {
                    MessageBox.Show("Ошибка! (Адрес получателя введен неправильно или не введен вовсе)"
                        + "\n" + "Внимание! Максимальная длинна адреса " + maxLenghtStringAddress + " символов"
                        + "\n" + "Форма записи(Страна, город, 'район', улица, дом, 'картира'"
                        + "\n" + "' ' - необязательны для ввода в случае их отсутствия"
                        , "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    recipientAddress = null;
                    return;
                }

                string weightStr = IOUMainTable_TBox_weight_Package.Text;
                weightStr.Trim();
                if (weightStr.IndexOf(".") == -1 && weightStr.IndexOf(",") == -1)
                {
                    weightStr += ".0";
                }
                else
                {
                    weightStr = weightStr.Replace(",", ".");
                }
                if (float.TryParse(weightStr, NumberStyles.Float, new CultureInfo("en-US"), out weight))
                {
                    weight = float.Parse(weightStr, NumberStyles.Float, new CultureInfo("en-US"));
                }
                else
                {
                    MessageBox.Show("Ошибка (Вес пакета введен не правильно)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (weight <= 0)
                {
                    MessageBox.Show("Ошибка (Вес пакета не может быть меньше нуля или равняться ему)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string dateStr = IOUMainTable_TBox_date_Operation.Text;
                dateOperation = DateTime.Parse(dateStr);

                string priceStr = IOUMainTable_TBox_price.Text;
                priceStr.Trim();
                if (priceStr.IndexOf(".") == -1 && priceStr.IndexOf(",") == -1)
                {
                    priceStr += ".0";
                }
                else
                {
                    priceStr = priceStr.Replace(",", ".");
                }   
                if (decimal.TryParse(priceStr, NumberStyles.Float, new CultureInfo("en-US"), out price))
                {
                    price = decimal.Parse(priceStr, NumberStyles.Float, new CultureInfo("en-US"));
                }
                else
                {
                    MessageBox.Show("Ошибка (Цена введена не правильно)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (price <= 0)
                {
                    MessageBox.Show("Ошибка (Вес пакета не может быть меньше нуля или равняться ему)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (operation != null && mailing != null
                    && senderAddress != null && recipientAddress != null
                    && weight != 0 && dateOperation != null
                    && price != 0)
                {
                    mail.id_Operation = operation;
                    mail.id_Mailing = mailing;
                    mail.id_Subscription = subscription;
                    mail.sender_Address = senderAddress;
                    mail.recipient_Address = recipientAddress;
                    mail.weight_Package = weight;
                    mail.date_Operation = dateOperation;
                    mail.price = price;

                    if (currentMW == ModeWindow.Insert)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы уверенны что хотите добавить новую запись в главную таблицу?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.None);

                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                        context.Mail.Add(mail);
                        context.SaveChanges();
                        MessageBox.Show("Добавление записи в главную таблицу прошло успешно", "Успешно", MessageBoxButton.OK, MessageBoxImage.None);
                    }

                    if (currentMW == ModeWindow.Update)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы уверенны что хотите изменить " + mail.mail_Id + " запись в главной таблице?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.None);

                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                        context.SaveChanges();
                        MessageBox.Show("Изменение записи в главной таблице прошло успешно", "Успешно", MessageBoxButton.OK, MessageBoxImage.None);
                    }
                }
                else
                {
                    MessageBox.Show("Невозможно продолжить операцию: (Данные потеряны)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            mainWindow.RefreshMainTable();

            this.Close();
        }

        //Событие кнопки "Отмена"
        private void button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Собыите для блокировки ввода сторонних символов при вводе числа
        private void ItFloatNumberInTbox(object sender, KeyEventArgs e)
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

        int maxLenghtStringAddress = 100;

        //Проверка на валиность адреса
        public bool isValidAddress(string address)
        {
            if (address.Length > maxLenghtStringAddress)
            {
                return false;
            }
            if (address.Split(',').Length < 4)
            {
                return false;
            }
            return true;
        }
    }
}
