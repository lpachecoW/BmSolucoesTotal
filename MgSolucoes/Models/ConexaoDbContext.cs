using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MgSolucoes.Models
{
    public class ConexaoDbContext : ApplicationDbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Grupos> Grupos { get; set; }
        public DbSet<Representacao> Representacoes { get; set; }
        public DbSet<Tipo_Bem> Tipo_Bens { get; set; }
        public DbSet<Atendimento> Atendimentos{ get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Pagamento>Pagamentos { get; set; }
        public DbSet<Status_Atendimento> Status_Atendimentos{ get; set; }
        public DbSet<Relatorios_Descretiva> relatorios_Descretivas { get; set; }
        public DbSet<Status_Pagamento> Status_Pagamentos { get; set; }
        public DbSet<Status_Lance> Status_Lance { get; set; }
        public DbSet<Cliente_Atendimento> Cliente_Atendimentos { get; set; }
        public DbSet<Metas> Metas { get; set; }
    }
}
