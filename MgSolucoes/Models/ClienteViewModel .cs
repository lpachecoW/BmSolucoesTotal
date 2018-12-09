using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MgSolucoes.Models
{
    public class ClienteViewModel
    {
        public Int32 ClienteId { get; set; }

        
        [Display(Name = "Nome do cliente")]
        public String Nome { get; set; }

        [Display(Name = "CPF/CNPJ")]
        public String Cpf { get; set; }

        [Display(Name = "Numero de contrato")]
        public Int32 Nu_conta_ade { get; set; }

        [Display(Name = "Telefone celular")]
        public String Tel_Movel { get; set; }

        [Display(Name = "Telefone Fixo")]
        public String Tel_Fixo { get; set; }

        [Display(Name = "Email")]
        public String Email { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime Dt_Cadastro { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime Dt_Nascimento { get; set; }

        [Display(Name = "Valor do credito")]
        public decimal Valor_Credito { get; set; }

        [Display(Name = "Representacao")]
        public Int32 Representacao_id { get; set; }

        [Display(Name = "Cota")]
        public string Cota_id { get; set; }

        
        [Display(Name = "Grupo")]
        public Int32 Grupo_id { get; set; }

        [Display(Name = "Tipo_bem")]
        public Int32 TipoBemId { get; set; }


        [Display(Name = "Nome Vendedor")]
        public String Vendedor { get; set; }
        
        public int HasAtendimento { get; set; }
    }
}