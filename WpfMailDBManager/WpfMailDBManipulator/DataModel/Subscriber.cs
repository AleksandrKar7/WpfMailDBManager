using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailDBManager.DataModel
{
    public class Subscriber
    {
        [Key]
        public int subscriber_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string last_Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string first_Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string middle_Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string sub_Address { get; set; }

        public Subscriber()
        {

        }

        public Subscriber(int subscriber_Id, string last_Name, string first_Name, string middle_Name, string sub_Address)
        {
            this.subscriber_Id = subscriber_Id;
            this.last_Name = last_Name;
            this.first_Name = first_Name;
            this.middle_Name = middle_Name;
            this.sub_Address = sub_Address;
        }

        public string ToLongString()
        {
            return "ФИО подписчика: " + last_Name + " " + first_Name + " " + middle_Name + "; \n"
                         + "Адрес подписчика: " + sub_Address + ". ";
            //return "Id: " + subscriber_Id + "; "
            //            + "\n" + "Фамилия: " + last_Name + "; "
            //            + "\n" + "Имя: " + first_Name + "; "
            //            + "\n" + "Отчество: " + middle_Name + "; "
            //            + "\n" + "Адрес: " + sub_Address + "; "; 
        }

        public override string ToString()
        {
            return subscriber_Id.ToString();
        }
    }
}
