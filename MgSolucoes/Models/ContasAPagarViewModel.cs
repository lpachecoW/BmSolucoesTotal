using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MgSolucoes.Models
{
    public class ContasAPagarViewModel
    {
        public int Representacao_id { get; set; }
        public string Nome { get; set; }
        public string nome1 { get; set; }
        public int Nu_parcelas { get; set; }
        public decimal Valor_Credito { get; set; }
        public int ClienteId { get; set; }
        public int Grupo_id { get; set; }
        public string Cliente_Nome { get; set; }
        public string Vendedor { get; set; }
        public DateTime Dt_Vencimento { get; set; }
        public decimal Rep_1 { get; set; }
        public decimal Rep_2 { get; set; }
        public decimal Rep_3 { get; set; }
        public decimal Rep_4 { get; set; }
        public decimal Rep_5 { get; set; }
        public decimal Rep_6 { get; set; }


    }
}