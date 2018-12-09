using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MgSolucoes.Models;
using System.Data.Entity;

namespace MgSolucoes.Models
{
   
    public class ClienteSearchLogic
    {

        private ConexaoDbContext context;
        public ClienteSearchLogic()
        {
            context = new ConexaoDbContext();
        }

        public IQueryable<Cliente> GetClientes(ClienteSearchFilter searchModel)
        {
            var result = context.Clientes.AsQueryable();
            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.cota))
                    result = result.Where(x => x.Cota_id.Contains(searchModel.cota));
            }
            return result;
        }
    }
}