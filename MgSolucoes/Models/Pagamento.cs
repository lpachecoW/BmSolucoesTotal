using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgSolucoes.Models
{
    [Table("Pagamento")]
    public class Pagamento
    {
        [Key]
        public int PagamentoId { get; set; }
        public DateTime? Dt_Pagamento { get; set; }
        public DateTime Dt_Vencimento { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Valor_Pago { get; set; }
        public string Status_Pagamento { get; set; }
        public int Parcela_num { get; set; }
        public string Comentario_Pagamento { get; set; }

        public int Clienteid { get; set; }
        public virtual Cliente Clientes { get; set; }

        public int Grupo_id { get; set; }
        public virtual Grupos Grupos { get; set; }

        public int Representacao_id { get; set; }
        public virtual Representacao Representacao { get; set; }
    }
}