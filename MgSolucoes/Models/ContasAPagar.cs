using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MgSolucoes.Models
{
    public class ContasAPagar
    {
        public int Representacao_id { get; set; }
        public string Nome { get; set; }
        public string nome1 { get; set; }
        public int Nu_parcelas { get; set; }
        public decimal Valor_Credito { get; set; }
    }
}