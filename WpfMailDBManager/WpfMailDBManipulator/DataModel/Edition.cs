using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailDBManager.DataModel
{
    public class Edition
    {
        [Key]
        public int edition_Id { get; set; }

        [Required]
        virtual public Periodical id_type_Periodical { get; set; }

        [Required]
        [MaxLength(100)]
        public string name_Edition { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal cost { get; set; }

        public Edition()
        {

        }

        public Edition(int edition_Id, Periodical id_type_Periodical, string name_Edition, decimal cost)
        {
            this.edition_Id = edition_Id;
            this.id_type_Periodical = id_type_Periodical;
            this.name_Edition = name_Edition;
            this.cost = cost;
        }

        public override string ToString()
        {
            Periodical per = id_type_Periodical;

            return per.type_Edition.ToString() + " '" + name_Edition + "', " + Decimal.Round(cost, 2) + "₴";

            //return "Название: " + per.type_Edition.ToString()
            //    + " '" + name_Edition + "'"
            //    + " Цена: " + cost;

            //Periodical per = id_type_Periodical;

            ////return "Id: " + edition_Id + "; "
            ////       + "\n" + "Тип издания: " + "{" + per.ToString().Replace("\n", "") + "}" + "; "
            ////       + "\n" + "Название издания: " + name_Edition + "; "
            ////       + "\n" + "Цена: " + cost + "; ";
            //return "Id: " + edition_Id
            //            + "\n" + "Тип издания: (" + per.periodical_Id.ToString() + ") " + per.type_Edition.ToString()
            //            + "\n" + "Название издания: " + name_Edition
            //            + "\n" + "Цена: " + cost;
        }

        public string ShortToString()
        {
            Periodical per = id_type_Periodical;

            return "Название: " + per.type_Edition.ToString() 
                + " '" + name_Edition + "'" 
                + " Цена: " + cost + " грн";
        }
    }
}
