using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MgSolucoes.Models
{
    public class ContratoViewModel
    {
        public Int32 Contrato_id { get; set; }
        public int Numero { get; set; }
        public int N_Parcelas { get; set; }
        public Int32 Grupo_id { get; set; }
        public Int32 Cota_id { get; set; }
        public Int32 TipoBemId { get; set; }
        public string Vendedor_id { get; set; }
        public Int32 Representacao_id { get; set; }
        public Int32 ClienteId { get; set; }
        public string Tabela { get; set; }
        public decimal Percentual_bem { get; set; }
        public decimal Valor_bem { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime Dt_pagamento { get; set; }


    }
}