using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgSolucoes.Models
{
    [Table("Tipo_Bem")]
    public class Tipo_Bem
    {
        [Key]
        public int TipoBemId { get; set; }
        
        public string Nome { get; set; }
    }
}