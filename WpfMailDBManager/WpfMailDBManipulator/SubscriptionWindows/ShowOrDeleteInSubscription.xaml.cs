using WpfMailDBManager.DataModel;
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

namespace WpfMailDBManager.SubscriptionWindows
{
    /// <summary>
    /// Логика взаимодействия для ShowOrDeleteInSubscription.xaml
    /// </summary>
    public partial class ShowOrDeleteInSubscription : Window
    {
        public int id;

        public enum ModeWindow
        {
            Show,
            DeleteConfirm,
        }
        public ShowOrDeleteInSubscription()
        {
            InitializeComponent();
        }

        //Установка значения в соответствующие поля, а также выбор режима окна
        //index - id записи над котором проводится действие
        public void SetData(int index, ModeWindow MW)
        {
            SetMode(MW);

            using (MailContext context = new MailContext())
            {
                Subscription subscription = context.Subscriptions.FirstOrDefault(c => c.subscription_Id == index);

                SODSubscription_Tbox_Id.Text = subscription.subscription_Id.ToString();
                SODSubscription_TBox_id_Subscriber.Text = subscription.id_Subscriber.ToLongString();
                SODSubscription_TBox_id_Edition.Text = subscription.id_Edition.ToString();
                SODSubscription_TBox_date_Create.Text = subscription.date_Сreation.ToString();
                SODSubscription_Tbox_date_Expiration.Text = subscription.date_Expiration.ToString();

                id = subscription.subscription_Id;
            }
        }


        //Установка значения в соответствующие поля, а также выбор режима окна
        //subscription - запись над котором проводится действие
        public void SetData(Subscription subscription, ModeWindow MW)
        {
            SetMode(MW);

            using (MailContext context = new MailContext())
            {
                SODSubscription_Tbox_Id.Text = subscription.subscription_Id.ToString();
                SODSubscription_TBox_id_Subscriber.Text = subscription.id_Subscriber.ToString();
                SODSubscription_TBox_id_Edition.Text = subscription.id_Edition.ToString();
                SODSubscription_TBox_date_Create.Text = subscription.date_Сreation.ToString();
                SODSubscription_Tbox_date_Expiration.Text = subscription.date_Expiration.ToString();

                id = subscription.subscription_Id;
            }
        }

        //Установка значений в ComboBox и в строку даты
        private void SetMode(ModeWindow MW)
        {
            SODSubscription_button_OK.IsEnabled = false;
            SODSubscription_button_OK.Visibility = Visibility.Hidden;
            SODSubscription_button_OKInput.IsEnabled = false;
            SODSubscription_button_OKInput.Visibility = Visibility.Hidden;
            SODSubscription_button_CancelInput.IsEnabled = false;
            SODSubscription_button_CancelInput.Visibility = Visibility.Hidden;

            SODSubscription_textBlock_OKInput.Visibility = Visibility.Hidden;
            
            if (MW == ModeWindow.Show)
            {
                this.Title = "Просмотр записи из таблицы Подписок";

                SODSubscription_button_OK.IsEnabled = true;
                SODSubscription_button_OK.Visibility = Visibility.Visible;
            }

            if (MW == ModeWindow.DeleteConfirm)
            {
                this.Title = "Вы уверены что хотите удалить запись из таблицы Подписок?";

                SODSubscription_button_OKInput.IsEnabled = true;
                SODSubscription_button_OKInput.Visibility = Visibility.Visible;

                SODSubscription_button_CancelInput.IsEnabled = true;
                SODSubscription_button_CancelInput.Visibility = Visibility.Visible;

                SODSubscription_textBlock_OKInput.Visibility = Visibility.Visible;
            }
        }

        //Событие кнопки "ОК"
        private void SODSubscription_button_Click_OK(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Событие кнопки "ОК" в режиме Delete
        private void SODSubscription_button_OKDelete_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        //Событие кнопки "Отмена"
        private void SODSubscription_button_CancelDelete_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
