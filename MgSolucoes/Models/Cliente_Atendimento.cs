using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MgSolucoes.Models
{
    [Table("Cliente_Atendimento")]
    public class Cliente_Atendimento
    {
        [Key]
        public int Cliente_Atendimento_Id { get; set; }

        public int Atendimento_id { get; set; }
        public int Cliente_id { get; set; }
    }
}