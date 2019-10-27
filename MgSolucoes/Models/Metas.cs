using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MgSolucoes.Models
{
    [Table("Metas")]
    public class Metas
    {
        [Key]
        public int Meta_Id { get; set; }
        public int MetaMensal { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public int MetaDiaria { get; set; }
    }
}