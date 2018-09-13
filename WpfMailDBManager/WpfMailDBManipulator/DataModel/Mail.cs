using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailDBManager.DataModel
{
    public class Mail
    {
        [Key]
        [DisplayName("Индекс")]
        public int mail_Id { get; set; }

        [Required]
        [DisplayName("Операция")]
        virtual public Operation id_Operation { get; set; }

        [Required]
        [DisplayName("Тип")]
        virtual public Mailing id_Mailing { get; set; }

        [DisplayName("Код подписки")]
        virtual public Subscription id_Subscription { get; set; }

        [Required]
        [MaxLength(200)]
        [DisplayName("Адрес отправителя")]
        public string sender_Address { get; set; }

        [Required]
        [MaxLength(200)]
        [DisplayName("Адрес получателя")]
        public string recipient_Address { get; set; }

        [Required]
        [DisplayName("Вес пакета (кг)")]
        public float weight_Package { get; set; }

        [Required]
        [DisplayName("Дата операции")]
        public DateTime date_Operation { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        [DisplayName("Цена (грн)")]
        public decimal price { get; set; }

        public Mail()
        {

        }

        public Mail(int mail_Id, Operation id_Operation, Mailing id_Mailing, Subscription id_Subscription
            , string sender_Address, string recipient_Address, float weight_Package, DateTime date_Operation, decimal price)
        {
            this.mail_Id = mail_Id;
            this.id_Operation = id_Operation;
            this.id_Mailing = id_Mailing;
            this.id_Subscription = id_Subscription;
            this.sender_Address = sender_Address;
            this.recipient_Address = recipient_Address;
            this.weight_Package = weight_Package;
            this.date_Operation = date_Operation;
            this.price = price;
        }

        //public override string ToString()
        //{
        //    Operation operation = id_Operation;

        //    Mailing mailings = id_Mailing;

        //    Subscription subscriptions = id_Subscription;

        //    //return "Id: " + mail_Id + "; "
        //    //    + "\n" + "Тип операции: " + "{" + operation.ToString().Replace("\n", "") + "}" + "; "
        //    //    + "\n" + "Тип почтового отправления: " + "{" + mailings.ToString().Replace("\n", "") + "}" + "; "
        //    //    + "\n" + "Номер подписки на периодическое издание: " + subscriptions.subscription_Id + "; "
        //    //    + "\n" + "Адрес отправителя: " + sender_Address + "; "
        //    //    + "\n" + "Адрес назначения: " + recipient_Address + "; "
        //    //    + "\n" + "Вес пакета: " + weight_Package + "; "
        //    //    + "\n" + "Дата операции: " + date_Operation + "; "
        //    //    + "\n" + "Цена: " + price + "; ";

        //    return "Id: " + mail_Id
        //        + "\n" + "Тип операции: " + "(" + operation.operation_Id + ") " + operation.type_Operation
        //        + "\n" + "Тип почтового отправления: " + "(" + mailings.mailing_Id + ") " + mailings.type_Mailing
        //        + "\n" + "Номер подписки на периодическое издание: " + subscriptions.subscription_Id
        //        + "\n" + "Адрес отправителя: " + sender_Address
        //        + "\n" + "Адрес назначения: " + recipient_Address
        //        + "\n" + "Вес пакета: " + weight_Package
        //        + "\n" + "Дата операции: " + date_Operation
        //        + "\n" + "Цена: " + price;
        //}
    }
}
