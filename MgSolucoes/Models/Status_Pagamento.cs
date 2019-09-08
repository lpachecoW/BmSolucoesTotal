using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgSolucoes.Models
{
    [Table("Status_pagamento")]
    public class Status_Pagamento
    {
        [Key]
        public int Status_Pagamento_id { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
    }
}