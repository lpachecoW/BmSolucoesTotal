using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgSolucoes.Models
{
    [Table("Representacao")]
    public class Representacao
    {
        [Key]
        public int Representacao_id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime Dt_nascimento { get; set; }
        public string Funcao { get; set; }
}
}