using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MgSolucoes.Models
{
    public class AtendimentoViewModel
    {
        public Int32 Atendimento_id { get; set; }


        [Display(Name = "Texto")]
        public String Texto { get; set; }
        
        [Display(Name = "Status do atendimento")]
        public int Status_Atendimento { get; set; }

        [Display(Name = "Status do Boleto")]
        public int Status_Boleto { get; set; }

        [Display(Name = "Data do atendimento")]
        public DateTime Dt_Atendimento { get; set; }
        

        [Display(Name = "Cliente")]
        public Int32 Clienteid { get; set; }

        [Display(Name = "Status Atendimento")]
        public Int32 Status_Atendimento_id { get; set; }

        [Display(Name = "Status Pagamento")]
        public Int32 Status_Pagamento_id { get; set; }

        [Display(Name = "Lance Foi Ofertado?")]
        public Int32 Status_Lance_id { get; set; }

        public Int32? Contatado { get; set; }

        public Int32? Procon { get; set; }

        [Display(Name = "Valor Ofertado")]
        public decimal Valor_ofertado { get; set; }

        public String Atendente_id { get; set; }

        public Cliente cliente { get; set; }
    }
}