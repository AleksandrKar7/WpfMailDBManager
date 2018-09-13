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
    /// Логика взаимодействия для InsertOrUpdateToSubscriber.xaml
    /// </summary>
    public partial class InsertOrUpdateToSubscriber : Window
    {
        public enum ModeWindow
        {
            Insert,
            Update
        }
        public MainWindow mainWindow { get; set; }

        private ModeWindow currentMW { get; set; } = 0;
        Subscriber updateSubscriber = null;

        public InsertOrUpdateToSubscriber()
        {
            InitializeComponent();
        }

        //Установка режима роботы окна
        public void SetMode(ModeWindow MW)
        {
            currentMW = MW;
            if (currentMW == ModeWindow.Insert)
            {
                this.Title = "Добавление записи в таблицу Подписчиков";
            }
            if (currentMW == ModeWindow.Update)
            {
                this.Title = "Редактирование записи в таблице Подписчиков";
            }
        }

        //Установка значений в соответствующие поля для редактирования
        // subscriber - запись которая редактируется
        public void ChooseData(Subscriber subscriber)
        {
            updateSubscriber = subscriber;

            IOUSubscriber_TBox_last_Name.Text = subscriber.last_Name;
            IOUSubscriber_TBox_first_Name.Text = subscriber.first_Name;
            IOUSubscriber_TBox_middle_Name.Text = subscriber.middle_Name;
            IOUSubscriber_TBox_sub_Address.Text = subscriber.sub_Address;
        }

        int maxLenghtStringName = 25;

        //Событие кнопки "ОК"
        private void button_Click_OK(object sender, RoutedEventArgs e)
        {
            using (MailContext context = new MailContext())
            {
                Subscriber subscriber = new Subscriber();

                if (currentMW == ModeWindow.Update)
                {
                    subscriber = context.Subscribers.FirstOrDefault(c => c.subscriber_Id == updateSubscriber.subscriber_Id);
                }

                string lastName = IOUSubscriber_TBox_last_Name.Text;
                lastName.Trim();
                if (lastName.Length > maxLenghtStringName)
                {
                    MessageBox.Show("Ошибка (Фамилия слишком большая, максиальное количество символов: "+ maxLenghtStringName +")", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string firstName = IOUSubscriber_TBox_first_Name.Text;
                firstName.Trim();
                if (firstName.Length > maxLenghtStringName)
                {
                    MessageBox.Show("Ошибка (Имя слишком большое, максиальное количество символов: " + maxLenghtStringName + ")", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string middleName = IOUSubscriber_TBox_middle_Name.Text;
                middleName.Trim();
                if (middleName.Length > maxLenghtStringName)
                {
                    MessageBox.Show("Ошибка (Отчество слишком большое, максиальное количество символов: " + maxLenghtStringName + ")", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string subAddress = IOUSubscriber_TBox_sub_Address.Text;
                subAddress.Trim();
                if (isValidAddress(subAddress) == false)
                {
                    MessageBox.Show("Ошибка! (Адрес подписчика введен неправильно или не введен вовсе)"
                        + "\n" + "Внимание! Максимальная длинна адреса " + maxLenghtStringAddress +" символов"
                        + "\n" + "Форма записи(Страна, город, 'район', улица, дом, 'картира'"
                        + "\n" + "' ' - необязательны для ввода в случае их отсутствия"
                        , "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    subAddress = null;
                    return;
                }

                if (lastName != null && firstName != null
                    && middleName != null && subAddress != null)
                {
                    subscriber.last_Name = lastName;
                    subscriber.first_Name = firstName;
                    subscriber.middle_Name = middleName;
                    subscriber.sub_Address = subAddress;

                    if (currentMW == ModeWindow.Insert)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы уверенны что хотите добавить новую запись в таблицу 'Подписчики'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.None);

                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                        context.Subscribers.Add(subscriber);
                        context.SaveChanges();
                        MessageBox.Show("Добавление записи в таблицу 'Подписчики' прошло успешно", "Успешно", MessageBoxButton.OK, MessageBoxImage.None);
                    }

                    if (currentMW == ModeWindow.Update)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы уверенны что хотите изменить " + subscriber.subscriber_Id + " запись в таблицу 'Подписчики'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.None);

                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                        context.SaveChanges();
                        MessageBox.Show("Изменение записи в таблицу 'Подписчики' прошло успешно", "Успешно", MessageBoxButton.OK, MessageBoxImage.None);
                    }
                }
                else
                {
                    MessageBox.Show("Невозможно продолжить операцию: (Данные потеряны)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            mainWindow.RefreshSubscriberTable();
            this.Close();
        }

        //Событие кнопки "Отмена"
        private void button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
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
