using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MgSolucoes.Models
{
    [Table("Relatorios_Descretiva")]
    public class Relatorios_Descretiva
    {
        [Key]
        public int Relatorios_Descretiva_id { get; set; }
        public string Nome { get; set; }
        public int? Valor { get; set; }
    }
}