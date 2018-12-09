using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgSolucoes.Models
{
    [Table("Contrato")]
    public class Contrato
    {
        [Key]
        public int Contrato_id { get; set; }
        [Display(Name = "Numero do contrato")]
        public int Numero { get; set; }

        [Display(Name = "Tabela")]
        public string Tabela { get; set; }

        [Display(Name = "Percentual do bem")]
        public decimal Percentual_bem { get; set; }

        [Display(Name = "Valor do bem")]
        public decimal Valor_bem { get; set; }

        [Display(Name = "Numero de parcelas")]
        public int N_Parcelas { get; set; }

        [Display(Name = "Vencimento da parcela")]
        public DateTime Vencimento { get; set; }

        [Display(Name = "Data de pagamento da parcela")]
        public DateTime Dt_Pagamento { get; set; }

        //Dropdowns
        public int ClienteId { get; set; }
        public virtual Cliente Clientes { get; set; }
        public int Representacao_id { get; set; }
        public virtual Representacao Representacoes { get; set; }
        public int TipoBemId { get; set; }
        public virtual Tipo_Bem Tipo_Bem { get; set; }
        public int Grupo_id { get; set; }
        public virtual Grupos Grupos { get; set; }
        
        
    }
}