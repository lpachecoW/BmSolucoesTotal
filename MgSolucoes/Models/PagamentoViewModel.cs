using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MgSolucoes.Models
{
    public class PagamentoViewModel
    {
        public Int32 PagamentoId { get; set; }

        [Display(Name = "Grupo")]
        public Int32 Grupo_id { get; set; }

        [Display(Name = "Cota")]
        public Int32 Clienteid { get; set; }

        [Display(Name = "Data de pagamento")]
        public DateTime Dt_Pagamento { get; set; }

        [Display(Name = "Data de vencimento")]
        public DateTime Dt_Vencimento { get; set; }

        [Display(Name = "Valor Pago")]
        public decimal Valor_Pago { get; set; }

        [Display(Name = "Status de pagamento")]
        public string Status_Pagamento { get; set; }

        [Display(Name = "Parcela")]
        public int Parcela_num { get; set; }

        [Display(Name = "Comentario Pagamento")]
        public string Comentario_Pagamento{ get; set; }


    }
}