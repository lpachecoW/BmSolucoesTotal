using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MgSolucoes.Models
{
    public class RelatoriosViewModel
    {
        public Cliente clientes { get; set; }
        public Pagamento pagamentos { get; set; }
        
    }
}