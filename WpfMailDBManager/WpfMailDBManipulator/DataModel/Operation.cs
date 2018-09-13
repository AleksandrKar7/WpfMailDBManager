using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailDBManager.DataModel
{
    public class Operation
    {
        [Key]
        public int operation_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string type_Operation { get; set; }

        public Operation()
        {

        }

        public Operation(int operation_Id, string type_Operation)
        {
            this.operation_Id = operation_Id;
            this.type_Operation = type_Operation;
        }

        public override string ToString()
        {
            return  type_Operation;
            //return base.ToString();
        }
    }
}
