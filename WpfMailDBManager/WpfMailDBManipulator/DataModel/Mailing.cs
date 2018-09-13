using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailDBManager.DataModel
{
    public class Mailing
    {
        [Key]
        public int mailing_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string type_Mailing { get; set; }

        public Mailing()
        {

        }

        public Mailing(int mailing_id, string type_Mailing)
        {
            this.mailing_Id = mailing_Id;
            this.type_Mailing = type_Mailing;
        }

        public override string ToString()
        {
            return  type_Mailing;
        }
    }
}
