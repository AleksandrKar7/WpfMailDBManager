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

namespace WpfMailDBManager.MainTableWindows
{
    /// <summary>
    /// Логика взаимодействия для ShowInMainTable.xaml
    /// </summary>
    public partial class ShowOrDeleteInMainTable : Window
    {
        public int id;

        public enum ModeWindow
        {
            Show,
            DeleteConfirm
        }
        public ShowOrDeleteInMainTable()
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
                Mail mail = (Mail)context.Mail.FirstOrDefault(c => c.mail_Id == index);
                SODMainTable_TBox_mail_id.Text = mail.mail_Id.ToString();
                SODMainTable_TBox_id_Operation.Text = mail.id_Operation.type_Operation.ToString();
                SODMainTable_TBox_id_Mailing.Text = mail.id_Mailing.ToString();
                if (mail.id_Subscription != null)
                {
                    SODMainTable_TBox_id_Subscription.Text = mail.id_Subscription.LongToString();
                }
                SODMainTable_TBox_sender_Address.Text = mail.sender_Address;
                SODMainTable_TBox_recipient_Address.Text = mail.recipient_Address;
                SODMainTable_TBox_weight_Package.Text = mail.weight_Package.ToString();
                SODMainTable_TBox_date_Operation.Text = mail.date_Operation.ToString();
                SODMainTable_TBox_price.Text = mail.price.ToString();

                id = mail.mail_Id;
            }
        }

        //Установка значения в соответствующие поля, а также выбор режима окна
        //mail - запись над котором проводится действие
        public void SetData(Mail mail, ModeWindow MW)
        {
            SetMode(MW);

            using (MailContext context = new MailContext())
            {
                SODMainTable_TBox_mail_id.Text = mail.mail_Id.ToString();
                SODMainTable_TBox_id_Operation.Text = mail.id_Operation.type_Operation.ToString();
                SODMainTable_TBox_id_Mailing.Text = mail.id_Mailing.ToString();
                if (mail.id_Subscription != null)
                {
                    SODMainTable_TBox_id_Subscription.Text = mail.id_Subscription.LongToString();
                }
                SODMainTable_TBox_sender_Address.Text = mail.sender_Address;
                SODMainTable_TBox_recipient_Address.Text = mail.recipient_Address;
                SODMainTable_TBox_weight_Package.Text = mail.weight_Package.ToString();
                SODMainTable_TBox_date_Operation.Text = mail.date_Operation.ToString();
                SODMainTable_TBox_price.Text = mail.price.ToString();

                id = mail.mail_Id;
            }
        }

        //Установка значений в ComboBox и в строку даты
        private void SetMode (ModeWindow MW)
        {
            button_OK.IsEnabled = false;
            button_OK.Visibility = Visibility.Hidden;
            button_OKInput.IsEnabled = false;
            button_OKInput.Visibility = Visibility.Hidden;
            button_CancelInput.IsEnabled = false;
            button_CancelInput.Visibility = Visibility.Hidden;

            textBlock_OKInput.Visibility = Visibility.Hidden;

            if (MW == ModeWindow.Show)
            {
                this.Title = "Просмотр записи из Главной таблицы";

                button_OK.IsEnabled = true;
                button_OK.Visibility = Visibility.Visible;
            }
         
            if (MW == ModeWindow.DeleteConfirm)
            {
                this.Title = "Вы уверены что хотите удалить запись из Главной таблицы?";

                button_OKInput.IsEnabled = true;
                button_OKInput.Visibility = Visibility.Visible;

                button_CancelInput.IsEnabled = true;
                button_CancelInput.Visibility = Visibility.Visible;

                textBlock_OKInput.Visibility = Visibility.Visible;
            }
        }

        //Событие кнопки "ОК"
        private void button_Click_OK(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Событие кнопки "ОК" в режиме Delete
        private void button_OKDelete_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        //Событие кнопки "Отмена"
        private void button_CancelDelete_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
