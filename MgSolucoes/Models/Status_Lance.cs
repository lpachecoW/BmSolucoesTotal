using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgSolucoes.Models
{
    [Table("Status_Lance_Ofertado")]
    public class Status_Lance
    {
        [Key]
        public int Status_lance_id { get; set; }
        public string Nome { get; set; }
    }
}