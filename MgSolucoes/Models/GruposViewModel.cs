using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MgSolucoes.Models
{
    public class GruposViewModel
    {

        public Int32 Grupo_id { get; set; }


        [Display(Name = "Nome do grupo")]
        public String Nome { get; set; }

        [Display(Name = "Tipo adesao")]
        public String Tipo_adesao { get; set; }

        [Display(Name = "Data de cadsatro")]
        public DateTime Dt_Cadastro { get; set; }

        [Display(Name = "Data de vencimento")]
        public DateTime Dt_Vencimento { get; set; }

        [Display(Name = "Numero de parcelas")]
        public Int32 Nu_parcelas { get; set; }

        [Display(Name = "Ref 1")]
        public string Dia{ get; set; }
        [Display(Name = "Ref 1")]
        public string Mes { get; set; }
        [Display(Name = "Ref 1")]
        public string Ano { get; set; }


    }
}