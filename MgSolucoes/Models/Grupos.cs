using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgSolucoes.Models
{
    [Table("Grupos")]
    public class Grupos
    {
        [Key]
        public int Grupo_id { get; set; }
        public string Nome { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime Dt_Cadastro { get; set; }


        [Display(Name = "Data de vencimento do grupo")]
        public DateTime Dt_Vencimento { get; set; }

        [Display(Name = "Numero de parcelas")]
        public  int Nu_parcelas{ get; set; }

        [Display(Name = "Tipo de adesao")]
        public string Tipo_adesao { get; set; }

        [Display(Name = "Dia")]
        public string Dia { get; set; }

        [Display(Name = "Mes")]
        public string Mes { get; set; }

        [Display(Name = "Ano")]
        public string Ano { get; set; }


        public List<Cliente> Clientes { get; set; }
    }
}
