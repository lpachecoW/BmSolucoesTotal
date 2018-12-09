using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgSolucoes.Models
{
    [Table("Status_atendimento")]
    public class Status_Atendimento
    {
        [Key]
        public int Status_Atendimento_id { get; set; }
        public string Nome { get; set; }
    }
}