using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgSolucoes.Models
{
    [Table("Atendimento")]
    public class Atendimento
    {
        [Key]
        public int Atendimento_id { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Texto { get; set; }
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public DateTime Dt_Atendimento { get; set; }
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Atendente_id { get; set; }
        public decimal Valor_ofertado { get; set; }

        public int? Contatado { get; set; }
        public int? Procon { get; set; }
        public DateTime? Alteracao { get; set; }

        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int? Boleto_Enviado { get; set; }

        public int Status_Lance_id { get; set; }
        public virtual Status_Lance Lances { get; set; }

        public int Status_Atendimento_id { get; set; }
        public virtual Status_Atendimento Atendimentos { get; set; }

        public int Status_Pagamento_id { get; set; }
        public virtual Status_Pagamento Pagamentos { get; set; }

        public int Clienteid { get; set; }
        public virtual Cliente Clientes { get; set; }
        //public int Cliente_Atendimento_Id { get; set; }
        //public virtual Cliente_Atendimento Cliente_Atendimento { get; set; }


    }
}