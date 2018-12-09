using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MgSolucoes.Models
{
    public class Status_AtendimentoViewModel
    {

        public Int32 Status_Atendimento_id { get; set; }
        
        [Display(Name = "Nome")]
        public String Nome { get; set; }
    }
}