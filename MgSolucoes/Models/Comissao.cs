using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgSolucoes.Models
{
    [Table("Comissao")]
    public class Comissao
    {
        [Key]
        public int ComissaoId { get; set; }

        public decimal Rep_1 { get; set; }
        public decimal Rep_2 { get; set; }
        public decimal Rep_3 { get; set; }
        public decimal Rep_4 { get; set; }
        public decimal Rep_5 { get; set; }
        public decimal Rep_6 { get; set; }

        public int Representacao_id { get; set; }
        public virtual Representacao Representacoes { get; set; }
        public int Grupo_id { get; set; }
        public virtual Grupos Grupos { get; set; }
    }
}