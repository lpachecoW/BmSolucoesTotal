using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MgSolucoes.Models
{
    public class ComissaoViewModel
    {
        public Int32 ComissaoId { get; set; }

        [Display(Name = "Representacao_1")]
        public decimal Rep_1 { get; set; }

        [Display(Name = "Representacao_2")]
        public decimal Rep_2 { get; set; }

        [Display(Name = "Representacao_3")]
        public decimal Rep_3 { get; set; }

        [Display(Name = "Representacao_4")]
        public decimal Rep_4 { get; set; }

        [Display(Name = "Representacao_5")]
        public decimal Rep_5 { get; set; }

        [Display(Name = "Representacao_6")]
        public decimal Rep_6 { get; set; }
        


        [Display(Name = "Representacao")]
        public Int32 Representacao_id { get; set; }


        [Display(Name = "Grupo")]
        public Int32 Grupo_id { get; set; }


    }
}