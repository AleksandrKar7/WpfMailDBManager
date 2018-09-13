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

namespace WpfMailDBManager.SubscriptionWindows
{
    /// <summary>
    /// Логика взаимодействия для InsertOrUpdateToSubscription.xaml
    /// </summary>
    public partial class InsertOrUpdateToSubscription : Window
    {
        public enum ModeWindow
        {
            Insert,
            Update
        }

        public MainWindow mainWindow { get; set; }
        private ModeWindow currentMW { get; set; } = 0;
        Subscription updateSubscription = null;

        Subscriber[] subscribersArr;
        Edition[] editionsArr;

        public InsertOrUpdateToSubscription()
        {
            InitializeComponent();
            SetData();
        }

        //Установка режима роботы окна
        public void SetMode(ModeWindow MW)
        {
            currentMW = MW;
            if (currentMW == ModeWindow.Insert)
            {
                this.Title = "Добавление записи в таблицу Подписок";
            }
            if (currentMW == ModeWindow.Update)
            {
                this.Title = "Редактирование записи в таблице Подписок";
            }
        }

        //Установка значений в ComboBox и в строку даты
        private void SetData()
        {
            using (MailContext context = new MailContext())
            {
                IQueryable<Subscriber> subscribers = context.Subscribers.Select(c => c);
                IQueryable<Edition> editions = context.Editions.Select(c => c);

                subscribersArr = subscribers.ToArray();
                editionsArr = editions.ToArray();

                foreach (Subscriber subscriber in subscribers)
                {
                    IOUSubscription_ComboBox_id_Subscriber.Items.Add(subscriber.ToLongString());
                }
                foreach (Edition edition in editions)
                {
                    IOUSubscription_ComboBox_id_Edition.Items.Add(edition.ToString());
                }

                IOUSubscription_TBox_date_Create.Text = DateTime.UtcNow.ToLocalTime().ToString();
            }
        }

        //Установка значений в соответствующие поля для редактирования
        // subscription - запись которая редактируется
        public void chooseData(Subscription subscription)
        {
            updateSubscription = subscription;

            int subscriberArrIndex = -1;
            foreach (Subscriber subscriber in subscribersArr)
            {
                subscriberArrIndex++;
                if (subscriber.subscriber_Id == subscription.id_Subscriber.subscriber_Id)
                {
                    break;
                }
            }
            IOUSubscription_ComboBox_id_Subscriber.SelectedIndex = subscriberArrIndex;

            int editionArrIndex = -1;
            foreach (Edition edition in editionsArr)
            {
                editionArrIndex++;
                if (edition.edition_Id == subscription.id_Edition.edition_Id)
                {
                    break;
                }
            }
            IOUSubscription_ComboBox_id_Edition.SelectedIndex = editionArrIndex;       

            IOUSubscription_DP_date_Expiration.Text = subscription.date_Expiration.ToString();
        }

        //Событие кнопки "ОК"
        private void button_Click_OK(object sender, RoutedEventArgs e)
        {
            Subscriber subscriber = null;
            Edition edition = null;
            DateTime dateCreate;
            DateTime dateExpiration;

            using (MailContext context = new MailContext())
            {
                Subscription subscription = new Subscription();

                if (currentMW == ModeWindow.Update)
                {
                    subscription = context.Subscriptions.FirstOrDefault(c => c.subscription_Id == updateSubscription.subscription_Id);
                }

                if (IOUSubscription_ComboBox_id_Subscriber.SelectedIndex < 0)
                {
                    MessageBox.Show("Ошибка (Подписчик не выбран)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int subscriberId = subscribersArr[IOUSubscription_ComboBox_id_Subscriber.SelectedIndex].subscriber_Id;
                subscriber = context.Subscribers.FirstOrDefault<Subscriber>(c => c.subscriber_Id == subscriberId);
                if (subscriber == null)
                {
                    MessageBox.Show("Ошибка (Подписчик не найден! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    SetData();
                    return;
                }

                if (IOUSubscription_ComboBox_id_Edition.SelectedIndex < 0)
                {
                    MessageBox.Show("Ошибка (Издание не выбран)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int editionId = editionsArr[IOUSubscription_ComboBox_id_Edition.SelectedIndex].edition_Id;
                edition = context.Editions.FirstOrDefault<Edition>(c => c.edition_Id == editionId);
                if (edition == null)
                {
                    MessageBox.Show("Ошибка (Издание не найден! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    SetData();
                    return;
                }

                string dateCreateStr = IOUSubscription_TBox_date_Create.Text;
                if (DateTime.TryParse(dateCreateStr, out dateCreate) == true)
                {
                    dateCreate = DateTime.Parse(dateCreateStr);
                }

                string dateExpirationStr = IOUSubscription_DP_date_Expiration.Text;
                if (DateTime.TryParse(dateExpirationStr, out dateExpiration) == true)
                {
                    dateExpiration = DateTime.Parse(dateExpirationStr);
                }
                else
                {
                    MessageBox.Show("Ошибка (Дата введена неправильно! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                }

                if (dateCreate >= dateExpiration)
                {
                    MessageBox.Show("Ошибка (Дата начала подписки не может быть больше даты завершения! Попробуйте выбрать заново)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(subscriber != null && edition != null
                    && dateCreate != null && dateExpiration != null)             
                {
                    subscription.id_Subscriber = subscriber;
                    subscription.id_Edition = edition;
                    subscription.date_Сreation = dateCreate;
                    subscription.date_Expiration = dateExpiration;            

                    if (currentMW == ModeWindow.Insert)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы уверенны что хотите добавить новую запись в таблицу 'Подписки'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.None);

                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                        context.Subscriptions.Add(subscription);
                        context.SaveChanges();
                        MessageBox.Show("Добавление записи в таблицу 'Подписки' прошло успешно", "Успешно", MessageBoxButton.OK, MessageBoxImage.None);
                    }

                    if (currentMW == ModeWindow.Update)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы уверенны что хотите изменить " + subscription.subscription_Id + " запись в таблицу 'Подписки'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.None);

                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                        context.SaveChanges();
                        MessageBox.Show("Изменение записи в таблицу 'Подписки' прошло успешно", "Успешно", MessageBoxButton.OK, MessageBoxImage.None);
                    }
                }
                else
                {
                    MessageBox.Show("Невозможно продолжить операцию: (Данные потеряны)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            mainWindow.RefreshSubscriptionTable();
            this.Close();
        }

        //Событие кнопки "Отмена"
        private void button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
