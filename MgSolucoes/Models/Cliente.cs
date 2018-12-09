using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MgSolucoes.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int Nu_conta_ade { get; set; }
        public string Cota_id { get; set; }
        public string Tel_Movel { get; set; }
        public string Tel_Fixo { get; set; }
        public string Email { get; set; }
        public DateTime Dt_nascimento { get; set; }
        public DateTime Dt_Cadastro { get; set; }
        public decimal Valor_Credito { get; set; }
        public string Atendente_id { get; set; }
        public string Vendedor { get; set; }
        public int HasAtendimento { get; set; }

        //Dropdowns
        public int Representacao_id { get; set; }
        public virtual Representacao Representacoes { get; set; }
        public int TipoBemId { get; set; }
        public virtual Tipo_Bem Tipo_Bem { get; set; }
        public int Grupo_id { get; set; }
        public virtual Grupos Grupos { get; set; }
        

    }
}

