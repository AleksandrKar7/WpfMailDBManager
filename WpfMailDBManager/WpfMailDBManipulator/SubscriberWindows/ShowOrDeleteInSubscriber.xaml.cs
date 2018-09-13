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

namespace WpfMailDBManager.SubscriberWindows
{
    /// <summary>
    /// Логика взаимодействия для ShowOrDeleteInSubscriber.xaml
    /// </summary>
    public partial class ShowOrDeleteInSubscriber : Window
    {
        public int id;
        public enum ModeWindow
        {
            Show,
            DeleteConfirm,
        }
        public ShowOrDeleteInSubscriber()
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
                Subscriber subscriber = (Subscriber)context.Subscribers.FirstOrDefault(c => c.subscriber_Id == index);

                SODSubscriber_TBox_subscriber_Id.Text = subscriber.subscriber_Id.ToString();
                SODSubscriber_TBox_last_Name.Text = subscriber.last_Name;
                SODSubscriber_TBox_first_Name.Text = subscriber.first_Name;
                SODSubscriber_TBox_middle_Name.Text = subscriber.middle_Name;
                SODSubscriber_TBox_sub_Address.Text = subscriber.sub_Address;

                id = subscriber.subscriber_Id;
            }
        }

        //Установка значения в соответствующие поля, а также выбор режима окна
        //subscriber - запись над котором проводится действие
        public void SetData(Subscriber subscriber, ModeWindow MW)
        {
            SetMode(MW);

            using (MailContext context = new MailContext())
            {
                SODSubscriber_TBox_subscriber_Id.Text = subscriber.subscriber_Id.ToString();
                SODSubscriber_TBox_last_Name.Text = subscriber.last_Name;
                SODSubscriber_TBox_first_Name.Text = subscriber.first_Name;
                SODSubscriber_TBox_middle_Name.Text = subscriber.middle_Name;
                SODSubscriber_TBox_sub_Address.Text = subscriber.sub_Address;

                id = subscriber.subscriber_Id;
            }
        }

        //Установка значений в ComboBox и в строку даты
        private void SetMode(ModeWindow MW)
        {
            SODSubscriber_button_OK.IsEnabled = false;
            SODSubscriber_button_OK.Visibility = Visibility.Hidden;
            SODSubscriber_button_OKInput.IsEnabled = false;
            SODSubscriber_button_OKInput.Visibility = Visibility.Hidden;
            SODSubscriber_button_CancelInput.IsEnabled = false;
            SODSubscriber_button_CancelInput.Visibility = Visibility.Hidden;

            SODSubscriber_textBlock_OKInput.Visibility = Visibility.Hidden;

            if (MW == ModeWindow.Show)
            {
                this.Title = "Просмотр записи из таблицы Подписчиков";

                SODSubscriber_button_OK.IsEnabled = true;
                SODSubscriber_button_OK.Visibility = Visibility.Visible;
            }
           
            if (MW == ModeWindow.DeleteConfirm)
            {
                this.Title = "Вы уверены что хотите удалить запись из таблицы Подписчиков?";

                SODSubscriber_button_OKInput.IsEnabled = true;
                SODSubscriber_button_OKInput.Visibility = Visibility.Visible;

                SODSubscriber_button_CancelInput.IsEnabled = true;
                SODSubscriber_button_CancelInput.Visibility = Visibility.Visible;

                SODSubscriber_textBlock_OKInput.Visibility = Visibility.Visible;
            }
        }

        //Событие кнопки "ОК"
        private void SODSubscriber_button_Click_OK(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Событие кнопки "ОК" в режиме Delete
        private void SODSubscriber_button_OKDelete_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        //Событие кнопки "Отмена"
        private void SODSubscriber_button_CancelDelete_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
