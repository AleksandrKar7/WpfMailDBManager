using WpfMailDBManager.DataModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailDBManager
{
    //Класс содержит методы для заполнения таблиц БД тестовыми значениями
    class AutoInsert
    {
        //Метод добавения тестовых значений во все таблицы
        public void AutoInsertAll()
        {
            AutoInsertOperations();
            AutoInsertMailings();
            AutoInsertPeriodicals();
            AutoInsertEditions();
            AutoInsertSubscribers();
            AutoInsertSubscriptions();
            AutoInsertMainTable(); 
        }

        //Метод добавления значений в второстепенные таблицы
        public void AutoInsertSecondaryTable()
        {
            AutoInsertOperations();
            AutoInsertMailings();
            AutoInsertPeriodicals();
            AutoInsertEditions();
        }

        //Метод добавления значений в основные таблицы
        public void AutoInsertPrimaryTable()
        {
            AutoInsertSubscribers();
            AutoInsertSubscriptions();
            AutoInsertMainTable();
        }

        //Метод проверки на заполненность второстепенных таблиц
        public bool CanAutoInsertPrimaryTable()
        {
            using (MailContext context = new MailContext())
            {
                List<Operation> operations = context.Operations.Select(c => c).ToList();
                List<Mailing> mailings = context.Mailings.Select(c => c).ToList();
                List<Periodical> periodicals = context.Periodicals.Select(c => c).ToList();
                List<Edition> editions = context.Editions.Select(c => c).ToList();

                if (operations.Count == 0 || mailings.Count == 0
                    || periodicals.Count == 0 || editions.Count == 0)
                {
                    return false;
                }
            }
            return true;
        }

        //Метод для заполнения  таблицы 'Типы операций' тестовыми значениями 
        private void AutoInsertOperations()
        {
            string[] typeOperations = new string[] { "Прием", "Отправка" };

            using (MailContext context = new MailContext())
            {
                Operation operation = new Operation();

                foreach (string tp in typeOperations)
                {
                    operation.type_Operation = tp;
                    context.Operations.Add(operation);

                    context.SaveChanges();
                }
            }
        }

        //Метод для заполнения  таблицы 'Типы почтовых отправлений' тестовыми значениями 
        private void AutoInsertMailings()
        {
            string[] typeMailings = new string[] { "Закрытые письма", "Почтовые карточки",
            "Бандероли", "Посылки", "Денежные переводы", "Периодические издания"};          

            using (MailContext context = new MailContext())
            {
                Mailing mailing = new Mailing();

                foreach (string tm in typeMailings)
                {
                    mailing.type_Mailing = tm;
                    context.Mailings.Add(mailing);

                    context.SaveChanges();
                } 
            }
        }

        //Метод для заполнения таблицы 'Типы периодических изданий' тестовыми значениями 
        private void AutoInsertPeriodicals()
        {
            string[] typePeriodicals = new string[] { "Газета", "Журнал", "Ежегодник"
                , "Научный журнал", "Календари", "Реферативный сборник", "Библиографический указатель"
                , "Информационный бюллетень", "Справочник", "Литературный журнал"};         

            using (MailContext context = new MailContext())
            {
                Periodical periodical = new Periodical();

                foreach (string tp in typePeriodicals)
                {
                    periodical.type_Edition = tp;
                    context.Periodicals.Add(periodical);

                    context.SaveChanges();
                }                     
            }
        }

        //Метод для заполнения таблицы 'Издания' тестовыми значениями 
        private void AutoInsertEditions()
        {
            string[] nameEditions = new string[]
            {
                "Правда", "Телеском", "Книга рекордов Гинеса", "Умник"
                , "Фазы луны", "Студентские рефераты", "Туды кого то там"
                , "Все о всем", "Все телефоны Украины", "Октябрь"
            };

            using (MailContext context = new MailContext())
            {
                try
                {
                    IQueryable<Periodical> periodicals = context.Periodicals.Select(c => c);

                    Edition edition = new Edition();
                    List<Edition> editions = new List<Edition>();

                    int i = 0;

                    foreach (Periodical periodical in periodicals)
                    {
                        i++;
                        edition.id_type_Periodical = periodical;
                        if (i == nameEditions.Length)
                        {
                            i = 0;
                        }
                        edition.name_Edition = nameEditions[i];
                        edition.cost = nameEditions.Length - i;
                        editions.Add(edition);
                        edition = new Edition();
                    }

                    context.Editions.AddRange(editions);

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка при заполнении таблицы 'Издания' Текст ошибки:" + e.ToString());
                }
            }
        }

        //Метод для заполнения таблицы 'Подписчики на периодические издания' тестовыми значениями 
        private void AutoInsertSubscribers()
        {
            string[] lastNames = new string[]
            {
                "Пупкин", "Печкин", "Куров", "Синицын", "Скороход"
                , "Домов", "Колотушкин", "Карченко", "Пушкин", "Тарин"
            };

            string[] firstNames = new string[]
            {
                "Владислав", "Илья", "Александр", "Дмитрий", "Виктор"
                , "Станислав", "Константин", "Ян", "Максим", "Пётр"
            };

            string[] midleNames = new string[]
            {
                "Александрович", "Максимов", "Петрович", "Янов", "Владиславович"
                , "Ильич", "Дмитрович", "Константинович", "Станиславович", "Викторович"
            };

            string[] subAddresses = new string[]
            {
                "Украина г.Украинский, ул.Украины, д.32, кв.5"
                , "Россия г.Российсткий, ул.России, д.24, кв.12"
                , "Белоруссия г.Белороссийский, ул.Белорусии, д.43, кв.34"
                , "Польша г.Польский, ул.Польши, д.17, кв.45"
                , "США г.Штацкий, ул.Штатов, д.42, кв.15"
                , "ЮАР г.ЮАРский, ул.ЮАРцев, д.36, кв.8"
                , "Турция г.Турецкий, ул.Турции, д.7, кв.44"
                , "Франция г.Французкий, ул.Франции, д.34, кв.25"
                , "Германия г.Германский, ул.Германии, д.54, кв.6"
                , "Мексика г.Мексиканский, ул.Мексики, д.31, кв.17"
            };          

            using (MailContext context = new MailContext())
            {
                try
                {
                    Subscriber subscriber = new Subscriber();
                    List<Subscriber> subscribers = new List<Subscriber>();

                    for (int i = 0; i < lastNames.Length; i++)
                    {
                        subscriber.last_Name = lastNames[i];
                        subscriber.first_Name = firstNames[i];
                        subscriber.middle_Name = midleNames[i];
                        subscriber.sub_Address = subAddresses[i];
                        subscribers.Add(subscriber);
                        subscriber = new Subscriber();
                    }

                    context.Subscribers.AddRange(subscribers);

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка при заполнении таблицы 'Подписчики на периодические издания' Текст ошибки:" + e.ToString());
                }
            }
        }

        //Метод для заполнения таблицы 'Подписки' тестовыми значениями 
        private void AutoInsertSubscriptions()
        {         
            using (MailContext context = new MailContext())
            {
                try
                {
                    Subscription subscription = new Subscription();
                    List<Subscription> subscriptions = new List<Subscription>();

                    IQueryable<Edition> editions = context.Editions.Select(c => c);
                    IQueryable<Subscriber> subscribers = context.Subscribers.Select(c => c);

                    Edition[] editionsArr = new Edition[editions.Count()];

                    int i = 0;
                    foreach (Edition edition in editions)
                    {
                        editionsArr[i] = edition;
                        i++;
                    }

                    int j = 0;
                    foreach (Subscriber subscriber in subscribers)
                    {
                        subscription.id_Subscriber = subscriber;

                        if (j == editionsArr.Length)
                        {
                            j = 0;
                        }
                        subscription.id_Edition = editionsArr[j];
                        subscription.date_Сreation = DateTime.Today;
                        subscription.date_Expiration = DateTime.Parse((DateTime.Today.AddMonths(j)).ToString());

                        subscriptions.Add(subscription);
                        j++;

                        subscription = new Subscription();
                    }

                    context.Subscriptions.AddRange(subscriptions);

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка при заполнении таблицы 'Подписки' Текст ошибки:" + e.ToString());
                }
            }
        }

        //Метод для заполнения Главной таблицы тестовыми значениями 
        private void AutoInsertMainTable()
        {
            string[] senderAddress = new string[]
           {
                "Украина г.Украинский, ул.Украины, д.32, кв.5"
                , "Россия г.Российсткий, ул.России, д.24, кв.12"
                , "Белоруссия г.Белороссийский, ул.Белорусии, д.43, кв.34"
                , "Польша г.Польский, ул.Польши, д.17, кв.45"
                , "США г.Штацкий, ул.Штатов, д.42, кв.15"
                , "ЮАР г.ЮАРский, ул.ЮАРцев, д.36, кв.8"
                , "Турция г.Турецкий, ул.Турции, д.7, кв.44"
                , "Франция г.Французкий, ул.Франции, д.34, кв.25"
                , "Германия г.Германский, ул.Германии, д.54, кв.6"
                , "Мексика г.Мексиканский, ул.Мексики, д.31, кв.17"
           };

            string[] recipientAddress = new string[]
            {
                "Украина г.Украинский, ул.Украины, д.32, кв.5"
                , "Россия г.Российсткий, ул.России, д.24, кв.12"
                , "Белоруссия г.Белороссийский, ул.Белорусии, д.43, кв.34"
                , "Польша г.Польский, ул.Польши, д.17, кв.45"
                , "США г.Штацкий, ул.Штатов, д.42, кв.15"
                , "ЮАР г.ЮАРский, ул.ЮАРцев, д.36, кв.8"
                , "Турция г.Турецкий, ул.Турции, д.7, кв.44"
                , "Франция г.Французкий, ул.Франции, д.34, кв.25"
                , "Германия г.Германский, ул.Германии, д.54, кв.6"
                , "Мексика г.Мексиканский, ул.Мексики, д.31, кв.17"
            };
            
            using (MailContext context = new MailContext())
            {
                try
                {
                    Mail mail = new Mail();
                    List<Mail> mails = new List<Mail>();

                    Operation[] operations = context.Operations.Select(c => c).ToArray();
                    Mailing[] mailings = context.Mailings.Select(c => c).ToArray();
                    Subscription[] subscriptions = context.Subscriptions.Select(c => c).ToArray();

                    int o = 0;
                    int m = 0;
                    int s = 0;
                    int sa = 0;
                    int ra = 1;

                    for (int x = 0; x < senderAddress.Length; x++)
                    {
                        if (o == operations.Length)
                        {
                            o = 0;
                        }
                        if (m == mailings.Length)
                        {
                            m = 0;
                        }
                        if (s == subscriptions.Length)
                        {
                            s = 0;
                        }
                        if (sa == senderAddress.Length)
                        {
                            sa = 0;
                        }
                        if (ra == recipientAddress.Length + 1)
                        {
                            ra = 1;
                        }

                        mail.id_Operation = operations[o];
                        mail.id_Mailing = mailings[m];
                        mail.id_Subscription = subscriptions[s];

                        mail.sender_Address = senderAddress[sa];
                        mail.recipient_Address = recipientAddress[recipientAddress.Length - ra];

                        mail.weight_Package = x / (10 - x);

                        mail.date_Operation = DateTime.Now;

                        mail.price = x * (10 - x);

                        mails.Add(mail);

                        mail = new Mail();

                        o++;
                        m++;
                        s++;
                        sa++;
                        ra++;
                    }
                    context.Mail.AddRange(mails);

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка при заполнении главной таблицы Текст ошибки:" + e.ToString());
                }
            }
        }
    }
}
