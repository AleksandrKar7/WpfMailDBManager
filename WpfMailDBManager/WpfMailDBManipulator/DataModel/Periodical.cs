using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailDBManager.DataModel
{
    public class Periodical
    {
        [Key]
        public int periodical_Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string type_Edition { get; set; }

        public Periodical()
        {

        }

        public Periodical(int periodical_Id, string type_Edition)
        {
            this.periodical_Id = periodical_Id;
            this.type_Edition = type_Edition;
        }

        public override string ToString()
        {
            return "Id: " + periodical_Id + "; "
                        + "\n" + "Тип периодического издания: " + type_Edition;
        }

        public string ShortToString()
        {
            return "Тип периодического издания: " + type_Edition;
        }
    }
}
