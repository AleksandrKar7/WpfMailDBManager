using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailDBManager.DataModel
{
    public class Subscription
    {
        [Key]
        public int subscription_Id { get; set; }

        [Required]
        virtual public Subscriber id_Subscriber { get; set; }

        [Required]
        virtual public Edition id_Edition { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime date_Сreation { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime date_Expiration { get; set; }

        public Subscription()
        {

        }

        public Subscription(int subscription_Id, Subscriber id_Subscriber, Edition id_Edition,
            DateTime date_Сreation, DateTime date_Expiration)
        {
            this.subscription_Id = subscription_Id;
            this.id_Subscriber = id_Subscriber;
            this.id_Edition = id_Edition;
            this.date_Сreation = date_Сreation;
            this.date_Expiration = date_Expiration;
        }

        public override string ToString()
        {
            Subscriber sub = id_Subscriber;

            Edition edition = id_Edition;

            Periodical per = edition.id_type_Periodical;

            //return "Id: " + subscription_Id + "; "
            //    + "\n" + "Информация о подписчике: " + "{" + sub.ToString().Replace("\n", "") + "}" + "; "
            //    + "\n" + "Издание: " + "{" + edition.ToString().Replace("\n", "") + "}" + "; "
            //    + "\n" + "Дата создания: " + date_Сreation
            //    + "\n" + "Дата завершения подписки: " + date_Expiration;

            return subscription_Id.ToString();
        }

        public string LongToString()
        {
            Subscriber sub = id_Subscriber;

            Edition edition = id_Edition;

            Periodical per = edition.id_type_Periodical;

            return "Код подписки: " + subscription_Id + "; "
                + "\n" + "Информация о подписчике: " + sub.ToLongString() + "; "
                + "\n" + "Издание: " + edition.ToString() + "; "
                + "\n" + "Дата начала подписки: " + date_Сreation
                + "\n" + "Дата завершения подписки: " + date_Expiration;
        }
    }
}
