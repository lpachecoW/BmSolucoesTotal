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
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Valor_Credito { get; set; }
        public string Atendente_id { get; set; }
        public string Vendedor { get; set; }
        public int HasAtendimento { get; set; }
        public int? Cancelado { get; set; }
        public int? DiasEmAtraso { get; set; }
        public int? pagamentoGerado { get; set; }
        public string CorStatusPagamento { get; set; }
        //Dropdowns
        public int Representacao_id { get; set; }
        public virtual Representacao Representacoes { get; set; }
        public int TipoBemId { get; set; }
        public virtual Tipo_Bem Tipo_Bem { get; set; }
        public int Grupo_id { get; set; }
        public virtual Grupos Grupos { get; set; }
        public int Cliente_Atendimento_Id { get; set; }
        public virtual Cliente_Atendimento Cliente_Atendimento { get; set; }
        public int Status_Atendimento_Id { get; set; }
        public virtual Status_Atendimento Status_Atendimento { get; set; }
    

        public int Status_Pagamento_Id {get;set;}

    

        
        public Pagamento pagamentos;
        

    }
}


