using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MgSolucoes.Models
{
    public class Tipo_BemViewModel
    {
        public Int32 TipoBemId { get; set; }
        
        [Display(Name = "Nome do cliente")]
        public String Nome { get; set; }
    }
}