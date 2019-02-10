using System;
using System.Globalization;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MgSolucoes.Models;
using PagedList;

namespace MgSolucoes.Controllers
{
    public class ClienteController : Controller
    {
        ConexaoDbContext db;

        public ClienteController() {
            db = new ConexaoDbContext();
        }

        // GET: Cliente
        public ActionResult Index(string sortOrder, string currentFilter, string clienteNome, string grupoNome, string cotaNome,string representacaoNome, string comAtendimento, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeSortParm = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            ViewBag.RepSortParam = String.IsNullOrEmpty(sortOrder) ? "representacoes" : "";
            var first = "";
            var last = "";
            var hasNome = false;
            var hasGrupo = false;
            var hasCota = false;
            

            if (clienteNome != null)
            {
                page = 1;
                List<string> strList = clienteNome.Split('/').ToList();
                first = strList.First();
                last = strList.Last();
            }
            else
            {
                clienteNome = currentFilter;
            }

            ViewBag.CurrentFilter = clienteNome;

            var clientes = from s in db.Clientes
                           select s;

            if (!String.IsNullOrEmpty(clienteNome)) {
                clientes = clientes.Where(s => s.Nome.Contains(clienteNome));
                hasNome = true;
            }


            if (!String.IsNullOrEmpty(grupoNome))
            {
                if (hasNome)
                {
                    clientes = clientes.Where(s => s.Nome.Contains(clienteNome) && s.Grupos.Nome.Contains(grupoNome));
                    hasGrupo = true;
                }
                else {
                    clientes = clientes.Where(s => s.Grupos.Nome.Contains(grupoNome));
                }
            }

            if (!String.IsNullOrEmpty(cotaNome))
            {
                hasCota = true;
                if (hasNome && hasGrupo)
                {
                    clientes = clientes.Where(s => s.Nome.Contains(clienteNome) && s.Grupos.Nome.Contains(grupoNome) && s.Cota_id.Contains(cotaNome));
                }
                else if (hasNome)
                {
                    clientes = clientes.Where(s => s.Nome.Contains(clienteNome) && s.Cota_id.Contains(cotaNome));
                }
                else if (hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome.Contains(grupoNome) && s.Cota_id.Contains(cotaNome));
                }
                else {
                    clientes = clientes.Where(s => s.Cota_id.Contains(cotaNome));
                }
                
            }

            if (!String.IsNullOrEmpty(representacaoNome))
            {
                if (hasNome && hasGrupo && hasCota)
                {
                    clientes = clientes.Where(s => s.Nome.Contains(clienteNome) && s.Grupos.Nome.Contains(grupoNome) && s.Cota_id.Contains(cotaNome) && s.Representacoes.Nome.Contains(representacaoNome));
                }
                else if (hasNome)
                {
                    clientes = clientes.Where(s => s.Nome.Contains(clienteNome) && s.Representacoes.Nome.Contains(representacaoNome));
                }
                else if (hasNome && hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome.Contains(grupoNome) && s.Grupos.Nome.Contains(grupoNome) && s.Cota_id.Contains(cotaNome));
                }
                else if (hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome.Contains(grupoNome) && s.Representacoes.Nome.Contains(representacaoNome));
                }
                else if (hasCota) {
                    clientes = clientes.Where(s => s.Representacoes.Nome.Contains(representacaoNome) && s.Cota_id.Contains(cotaNome));
                }
                else
                {
                    clientes = clientes.Where(s => s.Representacoes.Nome.Contains(representacaoNome));
                }
                
            }

            if (!String.IsNullOrEmpty(comAtendimento))
            {
                if (comAtendimento.Equals("S"))
                {
                    clientes = clientes.Where(s => s.HasAtendimento == 1);
                }
                if (comAtendimento.Equals("N"))
                {
                    clientes = clientes.Where(s => s.HasAtendimento == 0);
                }
            }

            //clientes = clientes.Where(x => x.HasAtendimento == 0);

            switch (sortOrder)
            {
                case "nome_desc":
                    clientes = clientes.OrderBy(s => s.Nome);
                    break;
                case "representacoes":
                    clientes = clientes.OrderBy(s => s.Representacoes);
                    break;
                 default:
                    clientes = clientes.OrderBy(s => s.Nome);
                    break;
            }
            


            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            return View(clientes.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create() {
            
            
            ViewBag.Grupos = db.Grupos;
            ViewBag.Representacao = db.Representacoes;
            ViewBag.TipoBem = db.Tipo_Bens;
            var model = new ClienteViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteViewModel model)
        {
            
                var cliente = new Cliente();
                

                if (model.Nome == null) {cliente.Nome = "Nome não cadastrado";}else{cliente.Nome = model.Nome;}
                if(model.Cpf == null){cliente.Cpf = "000.000.000-00";}else{cliente.Cpf = model.Cpf;}
                if (model.Nu_conta_ade == 0) {cliente.Nu_conta_ade = 0;} else{cliente.Nu_conta_ade = model.Nu_conta_ade;}
                if (model.Cota_id == null) {cliente.Cota_id = "0";} else {cliente.Cota_id = model.Cota_id;}
                if (model.Dt_Nascimento == null) {
                    cliente.Dt_nascimento = DateTime.UtcNow;
                } else {
                    cliente.Dt_nascimento = model.Dt_Nascimento;
                }
                if (model.Valor_Credito == 0) {cliente.Valor_Credito = 0;} else {cliente.Valor_Credito = model.Valor_Credito;}
                if (model.Email == null) {cliente.Email = "bm@bmc.om.br";} else {cliente.Email = model.Email;}
                if (model.Representacao_id == 0){cliente.Representacao_id = 5;}else{cliente.Representacao_id = model.Representacao_id;}
                if (model.Grupo_id == 0) { cliente.Grupo_id = 38; } else { cliente.Grupo_id = model.Grupo_id; }
                if (model.Tel_Fixo == null) { cliente.Tel_Fixo = "(00)0000-0000"; } else { cliente.Tel_Fixo = model.Tel_Fixo; }
                if (model.Tel_Movel == null){cliente.Tel_Movel = "(00)0000-0000";}else{cliente.Tel_Movel = model.Tel_Movel;}
                if (model.Vendedor == null){cliente.Vendedor = "BM ADM";}else{cliente.Vendedor = model.Vendedor;}
                if (model.TipoBemId == 0) { cliente.TipoBemId = 6; } else { cliente.TipoBemId = model.TipoBemId; }
                cliente.Dt_Cadastro = DateTime.UtcNow;
                cliente.HasAtendimento = 0;
                
                
                try
                {   
                    db.Clientes.Add(cliente);
                    db.SaveChanges();
                    AddPagamentosPorContrato(cliente.Nome, cliente.Nu_conta_ade);
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
            }
             
        }

        void AddContasAReceber(Cliente cli, Grupos gru, Comissao com)
        {

        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            cliente.Dt_nascimento.ToString("dd/MM/yyyy");
            cliente.Dt_nascimento.ToString("dd/MM/yyyy");
            if (cliente == null)
            {
                return HttpNotFound();
            }

            ViewBag.Grupos = db.Grupos.Find(cliente.Grupo_id).Nome;
            ViewBag.Representacao = db.Representacoes.Find(cliente.Representacao_id).Nome;
            ViewBag.ClienteId = cliente.ClienteId;
            
            return View(cliente);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            
            return View(cliente);
        }
        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cliente cliente = db.Clientes.Find(id);
            ViewBag.Grupos = db.Grupos;
            ViewBag.Representacao = db.Representacoes;
            ViewBag.TipoBem = db.Tipo_Bens;
            if (cliente == null)
            {
                return HttpNotFound();
            }
            
            return View(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,Nome,Cpf,Nu_conta_ade,Tel_Movel,Tel_Fixo,Email,Valor_Credito,Dt_nascimento,Vendedor, Grupo_id,Representacao_id,TipoBemId")] Cliente model)
        {
            
                var cliente = db.Clientes.Find(model.ClienteId);
                if (cliente == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                cliente.Nome = model.Nome;
                cliente.Cpf = model.Cpf;
                cliente.Nu_conta_ade = model.Nu_conta_ade;
                cliente.Tel_Movel = model.Tel_Movel;
                cliente.Tel_Fixo = model.Tel_Fixo;
                cliente.Email = model.Email;
                cliente.Valor_Credito = model.Valor_Credito;
                cliente.Dt_nascimento = model.Dt_nascimento;
                cliente.Vendedor = model.Vendedor;
                cliente.Grupo_id = model.Grupo_id;
                cliente.Representacao_id = model.Representacao_id;
                cliente.TipoBemId = model.TipoBemId;

                db.SaveChanges();
                return RedirectToAction("Index");
            
            //ViewBag.Categorias = db.Categorias;
            
        }

        public ActionResult IsAniversario()
        {
            int years = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            
            var clientes = from s in db.Clientes
                           select s ;
            clientes = clientes.Where(x => x.Dt_nascimento.Day == day && x.Dt_nascimento.Month == month);
            return View(clientes);
        }


        public ActionResult ClienteByGrupoVencimento(string grupoNome, int? page)
        {
            int years = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            var clientes = from s in db.Clientes
                           select s;
            
            if (!String.IsNullOrEmpty(grupoNome))
            {
                clientes = clientes.Where(x => x.Grupos.Nome.Equals(grupoNome));
            }
            else {
                clientes = clientes.Where(x => x.Grupos.Dt_Vencimento.Day == day && x.Grupos.Dt_Vencimento.Month == month);
            }
            
            var sortOrder = "asc";
            switch (sortOrder)
            {
                case "nome_desc":
                    clientes = clientes.OrderBy(s => s.Nome);
                    break;
                case "representacoes":
                    clientes = clientes.OrderBy(s => s.Representacoes);
                    break;
                default:
                    clientes = clientes.OrderBy(s => s.Nome);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(clientes.ToPagedList(pageNumber, pageSize));
        }


        Boolean AddPagamentosPorContrato(string cliente, int contrato) {
            bool enviaBanco = false;
            var clienteBancoId = 0;
            decimal valorContrato = 0;
            int numParcelas = 0;
            int grupoClienteId = 0;

            var clienteTemDados = from s in db.Clientes select s;
            clienteTemDados = clienteTemDados.Where(x => x.Nu_conta_ade == contrato && x.Nome == cliente);
            clienteBancoId = clienteTemDados.Select(x => x.ClienteId).FirstOrDefault();
            valorContrato = clienteTemDados.Select(x => x.Valor_Credito).FirstOrDefault();
            numParcelas = clienteTemDados.Select(x => x.Grupos.Nu_parcelas).FirstOrDefault();
            grupoClienteId = clienteTemDados.Select(x => x.Grupos.Grupo_id).FirstOrDefault();

            

            

            for (var i = 0; i < numParcelas-1; i++)
            {
                Pagamento clienteAInserir = new Pagamento();
                clienteAInserir.Clienteid = clienteBancoId;
                clienteAInserir.Grupo_id = grupoClienteId;
                if (i == 0) {
                    clienteAInserir.Dt_Pagamento = DateTime.Today;
                    clienteAInserir.Dt_Vencimento = DateTime.Today;
                    clienteAInserir.Status_Pagamento = "PAGO";
                }
                else
                {
                    var tempVenc = DateTime.Today.AddMonths(i);
                    var newVenc = new DateTime(tempVenc.Year, tempVenc.Month, tempVenc.Day); //create 
                    clienteAInserir.Dt_Vencimento = newVenc;
                    var tempPag = DateTime.Today.AddMonths(i);
                    var newPag = new DateTime(tempPag.Year, tempPag.Month, tempPag.Day); //create 
                    clienteAInserir.Dt_Pagamento = newPag;
                    clienteAInserir.Status_Pagamento = "NÃO PAGO";
                }
                clienteAInserir.Valor_Pago = valorContrato * 0.1m / 100;
                
                clienteAInserir.Parcela_num = i+1;
                db.Pagamentos.Add(clienteAInserir);
                db.SaveChanges();
            }
                

            return enviaBanco;
        }


    }
}