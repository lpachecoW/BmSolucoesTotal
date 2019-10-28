using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MgSolucoes.Models
{
    public class MetaViewModel
    {
        public Int32 Meta_Id { get; set; }


        [Display(Name = "Valor para meta mensal")]
        public Int32 MetaMensal { get; set; }

        [Display(Name = "Tipo adesao")]
        public DateTime? Inicio { get; set; }

        [Display(Name = "Data de cadsatro")]
        public DateTime? Fim { get; set; }

        [Display(Name = "Meta diria")]
        public Int32 MetaDiaria { get; set; }
    }
}