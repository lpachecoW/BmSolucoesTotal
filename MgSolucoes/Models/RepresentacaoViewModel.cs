using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MgSolucoes.Models
{
    public class RepresentacaoViewModel
    {
        public Int32 Representacao_id { get; set; }


        [Display(Name = "Nome do Representante")]
        public String Nome { get; set; }


        [Display(Name = "CPF/CNPJ")]
        public String Cpf { get; set; }


        [Display(Name = "Data")]
        public DateTime Dt_Nascimento { get; set; }


        [Display(Name = "COD Representacao")]
        public String Funcao { get; set; }

    }
}