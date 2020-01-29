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
        ConexaoDbContext dbPag;



        public ClienteController() {
            db = new ConexaoDbContext();
            dbPag = new ConexaoDbContext();
        }

        // GET: Cliente
        public ActionResult Index(string sortOrder, string currentFilter, string clienteNome, string grupoNome, string cotaNome,string representacaoNome, string comAtendimento, string CPF,string statusCli, int? Representacao_id, int? Status_Atendimento_id, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeSortParm = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            ViewBag.RepSortParam = String.IsNullOrEmpty(sortOrder) ? "representacoes" : "";

            
            ViewBag.ddlGrupo = db.Grupos;
            ViewBag.ddlRepresentacao = db.Representacoes;
            ViewBag.ddlStatusAtendimento = db.Status_Atendimentos;

            if (grupoNome == "39") {
                grupoNome = null;
            }

            if (Representacao_id == 5) {
                Representacao_id = 0;
            }


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

            var pagamentos = dbPag.Pagamentos.ToList();

            if (!String.IsNullOrEmpty(clienteNome)) {
                clientes = clientes.Where(s => s.Nome.Contains(clienteNome.ToUpper()));
                hasNome = true;
            }


            if (!String.IsNullOrEmpty(grupoNome))
            {
                if (hasNome)
                {
                    clientes = clientes.Where(s => s.Nome == clienteNome && s.Grupos.Nome == grupoNome);
                    hasGrupo = true;
                }
                else {
                    clientes = clientes.Where(s => s.Grupos.Nome == grupoNome);
                }
            }

            if (!String.IsNullOrEmpty(cotaNome))
            {
                hasCota = true;
                if (hasNome && hasGrupo)
                {
                    clientes = clientes.Where(s => s.Nome == clienteNome && s.Grupos.Nome == grupoNome && s.Cota_id == cotaNome);
                }
                else if (hasNome)
                {
                    clientes = clientes.Where(s => s.Nome == clienteNome && s.Cota_id == cotaNome);
                }
                else if (hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome == grupoNome && s.Cota_id == cotaNome);
                }
                else {
                    clientes = clientes.Where(s => s.Cota_id ==cotaNome);
                }
                
            }

            if (!String.IsNullOrEmpty(representacaoNome))
            {
                if (hasNome && hasGrupo && hasCota)
                {
                    clientes = clientes.Where(s => s.Nome == clienteNome && s.Grupos.Nome == grupoNome && s.Cota_id == cotaNome && s.Representacoes.Nome == representacaoNome);
                }
                else if (hasNome)
                {
                    clientes = clientes.Where(s => s.Nome == clienteNome && s.Representacoes.Nome == representacaoNome);
                }
                else if (hasNome && hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome == grupoNome && s.Grupos.Nome == grupoNome && s.Cota_id == cotaNome);
                }
                else if (hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome == grupoNome && s.Representacoes.Nome == representacaoNome);
                }
                else if (hasCota) {
                    clientes = clientes.Where(s => s.Representacoes.Nome == representacaoNome && s.Cota_id == cotaNome);
                }
                else
                {
                    clientes = clientes.Where(s => s.Representacoes.Nome == representacaoNome);
                }
                
            }

            if (!String.IsNullOrEmpty(comAtendimento))
            {
                if (comAtendimento == "S")
                {
                    clientes = clientes.Where(s => s.HasAtendimento == 1);
                }
                if (comAtendimento == "N")
                {
                    clientes = clientes.Where(s => s.HasAtendimento == 0);
                }
            }

            if (!String.IsNullOrEmpty(CPF))
            {
                if (hasNome && hasGrupo && hasCota)
                {
                    clientes = clientes.Where(s => s.Nome == clienteNome && s.Grupos.Nome == grupoNome && s.Cota_id == cotaNome && s.Cpf == CPF);
                }
                else if (hasNome)
                {
                    clientes = clientes.Where(s => s.Nome == clienteNome && s.Cpf == CPF);
                }
                else if (hasNome && hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome == grupoNome && s.Grupos.Nome == grupoNome && s.Cota_id == cotaNome && s.Cpf == CPF);
                }
                else if (hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome == grupoNome && s.Cpf == CPF);
                }
                else if (hasCota)
                {
                    clientes = clientes.Where(s => s.Cpf == CPF && s.Cota_id == cotaNome);
                }
                else if (Representacao_id != 0)
                {
                    clientes = clientes.Where(s => s.Cpf == CPF && s.Representacoes.Representacao_id == Representacao_id);
                }
                else
                {
                    clientes = clientes.Where(s => s.Cpf == CPF);
                }

            }

            if (Representacao_id != 0 && Representacao_id != null)
            {
                if (hasNome && hasGrupo && hasCota)
                {
                    clientes = clientes.Where(s => s.Nome == clienteNome && s.Grupos.Nome == grupoNome && s.Cota_id == cotaNome && s.Representacoes.Representacao_id == Representacao_id);
                }
                else if (hasNome)
                {
                    clientes = clientes.Where(s => s.Nome == clienteNome && s.Cpf == CPF);
                }
                else if (hasNome && hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome == grupoNome && s.Grupos.Nome == grupoNome && s.Cota_id == cotaNome && s.Representacoes.Representacao_id == Representacao_id);
                }
                else if (hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome == grupoNome && s.Representacoes.Representacao_id == Representacao_id);
                }
                else if (hasCota)
                {
                    clientes = clientes.Where(s => s.Representacoes.Representacao_id == Representacao_id && s.Cota_id == cotaNome);
                }
                else if (Representacao_id != null)
                {
                    clientes = clientes.Where(s => s.Representacoes.Representacao_id == Representacao_id);
                }
                else
                {
                    clientes = clientes.Where(s => s.Cpf == CPF);
                }

            }


            if (Status_Atendimento_id != 0 && Status_Atendimento_id != null)
            {
                if (hasNome && hasGrupo && hasCota)
                {
                    clientes = clientes.Where(s => s.Nome == clienteNome && s.Grupos.Nome == grupoNome && s.Cota_id == cotaNome && s.Representacoes.Representacao_id == Representacao_id && s.Status_Atendimento_Id == Status_Atendimento_id);
                }
                else if (hasNome)
                {
                    clientes = clientes.Where(s => s.Nome == clienteNome && s.Cpf == CPF);
                }
                else if (hasNome && hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome == grupoNome && s.Grupos.Nome == grupoNome && s.Cota_id == cotaNome && s.Representacoes.Representacao_id == Representacao_id && s.Status_Atendimento_Id == Status_Atendimento_id);
                }
                else if (hasGrupo)
                {
                    clientes = clientes.Where(s => s.Grupos.Nome == grupoNome && s.Representacoes.Representacao_id == Representacao_id && s.Status_Atendimento_Id == Status_Atendimento_id);
                }
                else if (hasCota)
                {
                    clientes = clientes.Where(s => s.Representacoes.Representacao_id == Representacao_id && s.Cota_id == cotaNome && s.Status_Atendimento_Id == Status_Atendimento_id);
                }
                else if (Representacao_id != null)
                {
                    clientes = clientes.Where(s => s.Representacoes.Representacao_id == Representacao_id && s.Status_Atendimento_Id == Status_Atendimento_id);
                }
                else
                {
                    clientes = clientes.Where(s => s.Cpf == CPF && s.Status_Atendimento_Id == Status_Atendimento_id);
                }
            }

            //Representacao_id


            //clientes = clientes.Where

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

            

            foreach (var addAtraso in clientes)
            {
                var parcela_um_paga = pagamentos.Where(x => x.Clienteid == addAtraso.ClienteId && x.Parcela_num == 1 && x.Status_Pagamento == "NÃO PAGO").Count();
                var parcela_cliente_emDia = pagamentos.Where(x => x.Clienteid == addAtraso.ClienteId && x.Dt_Vencimento.Month == DateTime.Today.Month && x.Status_Pagamento == "NÃO PAGO").Count();
                var parcela_vencida_venc_grupo = pagamentos.Where(x => x.Clienteid == addAtraso.ClienteId && x.Status_Pagamento == "NÃO PAGO" && x.Dt_Vencimento.Month > addAtraso.Grupos.Dt_Vencimento.Month).Count();
                var ultimas_parcelas_pagas = pagamentos.Where(x => x.Clienteid == addAtraso.ClienteId && x.Status_Pagamento == "PAGO" && x.Parcela_num == 5).Count();
                var in_pag_clientes = pagamentos.Where(x => x.Clienteid == addAtraso.ClienteId).Count();

                if (addAtraso.Cancelado == 1) {
                    addAtraso.DiasEmAtraso = 0;
                    addAtraso.Status_Pagamento_Id = 11;
                    addAtraso.CorStatusPagamento = "#788283";
                    continue;
                }

                if (parcela_um_paga > 0) {
                    var dadosByClienteId = pagamentos.Where(x => x.Clienteid == addAtraso.ClienteId && x.Dt_Vencimento.Month == DateTime.Today.Month && x.Status_Pagamento == "NÃO PAGO").FirstOrDefault();
                    if (dadosByClienteId == null)
                    {
                        addAtraso.DiasEmAtraso = 0;
                    }
                    else {
                        addAtraso.DiasEmAtraso = Convert.ToInt32((addAtraso.Grupos.Dt_Vencimento - dadosByClienteId.Dt_Vencimento).TotalDays);
                    }
                    addAtraso.CorStatusPagamento = "#ffff00";
                    addAtraso.Status_Pagamento_Id = 13;
                    continue;
                }

                if (parcela_cliente_emDia > 0) {
                    var dadosByClienteId = pagamentos.Where(x => x.Clienteid == addAtraso.ClienteId && x.Dt_Vencimento.Month == DateTime.Today.Month && x.Status_Pagamento == "NÃO PAGO").FirstOrDefault();
                    if (dadosByClienteId == null)
                    {
                        addAtraso.DiasEmAtraso = 0;
                    }
                    else
                    {
                        addAtraso.DiasEmAtraso = Convert.ToInt32((addAtraso.Grupos.Dt_Vencimento - dadosByClienteId.Dt_Vencimento).TotalDays);
                    }
                    addAtraso.CorStatusPagamento = "#5FE141";
                    addAtraso.Status_Pagamento_Id = 8;
                    continue;
                }

                if (parcela_vencida_venc_grupo > 0)
                {
                    var dadosByClienteId = pagamentos.Where(x => x.Clienteid == addAtraso.ClienteId && x.Dt_Vencimento.Month == DateTime.Today.Month && x.Status_Pagamento == "NÃO PAGO").FirstOrDefault();
                    if (dadosByClienteId == null)
                    {
                        addAtraso.DiasEmAtraso = 0;
                    }
                    else
                    {
                        addAtraso.DiasEmAtraso = Convert.ToInt32((addAtraso.Grupos.Dt_Vencimento - dadosByClienteId.Dt_Vencimento).TotalDays);
                    }
                    addAtraso.CorStatusPagamento = "#F50606";
                    addAtraso.Status_Pagamento_Id = 9;
                    continue;
                }

                if (ultimas_parcelas_pagas > 0)
                {
                    var dadosByClienteId = pagamentos.Where(x => x.Clienteid == addAtraso.ClienteId && x.Dt_Vencimento.Month == DateTime.Today.Month && x.Status_Pagamento == "NÃO PAGO").FirstOrDefault();
                    if (dadosByClienteId == null)
                    {
                        addAtraso.DiasEmAtraso = 0;
                    }
                    else
                    {
                        addAtraso.DiasEmAtraso = Convert.ToInt32((addAtraso.Grupos.Dt_Vencimento - dadosByClienteId.Dt_Vencimento).TotalDays);
                    }
                    addAtraso.CorStatusPagamento = "#2B06F5";
                    addAtraso.Status_Pagamento_Id = 10;
                    continue;
                }

            }
            

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            return View(clientes.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Pesquisa(ClienteViewModel _clienteVm) {

            {
                var clientePesquisa = from clienteRc in db.Clientes
                                      where ((_clienteVm.Nome == null) || clienteRc.Nome == _clienteVm.Nome.Trim())
                                      && ((_clienteVm.Cpf == null) || clienteRc.Cpf == _clienteVm.Cpf.Trim())
                                      && ((_clienteVm.Representacao_id == 5) || clienteRc.Representacao_id == _clienteVm.Representacao_id)
                                      && ((_clienteVm.Grupo_id == 39) || clienteRc.Grupo_id == _clienteVm.Grupo_id)
                                      && ((_clienteVm.Cota_id == null) || clienteRc.Cota_id == _clienteVm.Cota_id)
                                      && ((_clienteVm.Status_Atendimento == 0) || clienteRc.Status_Atendimento.Status_Atendimento_id == _clienteVm.Status_Atendimento)
                                      select new {

                                          Id = clienteRc.ClienteId,
                                          Nome = clienteRc.Nome,
                                          Grupo = clienteRc.Grupos.Nome,
                                          Cota = clienteRc.Cota_id,
                                          Tel_Movel = clienteRc.Tel_Movel,
                                          Tel_Fixo = clienteRc.Tel_Fixo,
                                          RepresentacaoNome = clienteRc.Representacoes.Nome,
                                          DiasEmAtraso = 0
                                      };

                List<Cliente> listaClientes = new List<Cliente>();

                foreach (var reg in clientePesquisa)
                {
                    Cliente clienteValor = new Cliente();
                    clienteValor.ClienteId = reg.Id;
                    clienteValor.Nome = reg.Nome;
                    clienteValor.Grupos.Nome = reg.Grupo;
                    clienteValor.Cota_id = reg.Cota;
                    clienteValor.Tel_Fixo = reg.Tel_Fixo;
                    clienteValor.Tel_Movel = reg.Tel_Movel;
                    clienteValor.Representacoes.Nome = reg.RepresentacaoNome;
                    clienteValor.DiasEmAtraso = reg.DiasEmAtraso;
                    listaClientes.Add(clienteValor);
                }

                _clienteVm.Clientes = listaClientes;

                return View(_clienteVm);
            }

            
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
                if (model.Email == null) {cliente.Email = "";} else {cliente.Email = model.Email;}
                if (model.Representacao_id == 0){cliente.Representacao_id = 5;}else{cliente.Representacao_id = model.Representacao_id;}
                if (model.Grupo_id == 0) { cliente.Grupo_id = 38; } else { cliente.Grupo_id = model.Grupo_id; }
                if (model.Tel_Fixo == null) { cliente.Tel_Fixo = "(00)0000-0000"; } else { cliente.Tel_Fixo = model.Tel_Fixo; }
                if (model.Tel_Movel == null){cliente.Tel_Movel = "(00)0000-0000";}else{cliente.Tel_Movel = model.Tel_Movel;}
                if (model.Vendedor == null){cliente.Vendedor = "BM ADM";}else{cliente.Vendedor = model.Vendedor;}
                if (model.TipoBemId == 0) { cliente.TipoBemId = 6; } else { cliente.TipoBemId = model.TipoBemId; }
                cliente.Dt_Cadastro = DateTime.UtcNow;
                cliente.HasAtendimento = 0;
                cliente.Cliente_Atendimento_Id = 0;
                cliente.Status_Atendimento_Id = 13;
                cliente.Cancelado = 0;
                cliente.Status_Pagamento_Id = 12;
                cliente.CorStatusPagamento = "";
                cliente.pagamentoGerado = 0;
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
            ViewBag.TemPagamento = cliente.pagamentoGerado;


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

        public ActionResult CancelarCliente(int? id)
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

        [HttpPost, ActionName("CancelarCliente")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmaCancelamento(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            cliente.Cancelado = 1;
            cliente.Status_Atendimento_Id = 7;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            cliente.Cancelado = 1;
            cliente.CorStatusPagamento = "#696969";
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
        public ActionResult Edit([Bind(Include = "ClienteId,Nome,Cpf,Nu_conta_ade,Tel_Movel,Tel_Fixo,Email,Valor_Credito,Dt_nascimento,Vendedor, Grupo_id,Representacao_id,TipoBemId,Cota_id")] Cliente model)
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
                cliente.Cota_id = model.Cota_id;

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

        public ActionResult GerarParcelasPagamento(int clienteId)
        {

            Cliente cliente = db.Clientes.Find(clienteId);
            Grupos grupo = db.Grupos.Find(cliente.Grupo_id);

            if (cliente.pagamentoGerado == 0)
            {

                int parcelas = cliente.Grupos.Nu_parcelas;
                var pagamento = new Pagamento();
                var olddate = DateTime.Now;

                DateTime? isVencimento = null;


                isVencimento = grupo.Dt_Vencimento;

                int i = 0;
                while (parcelas > 0)

                {
                    pagamento.Clienteid = cliente.ClienteId;

                    pagamento.Grupo_id = cliente.Grupo_id;
                    pagamento.Representacao_id = cliente.Representacao_id;

                    pagamento.Status_Pagamento = "NÃO PAGO";
                    i++;

                    if (i >= 0)
                    {
                        var tempDate = isVencimento.Value.AddMonths(i);
                        DateTime dataAtual = DateTime.Today;
                        var newDate = new DateTime(dataAtual.Year, tempDate.Month, tempDate.Day); //create 
                        pagamento.Dt_Vencimento = newDate;
                    }
                    pagamento.Valor_Pago = cliente.Valor_Credito * 1 / 100;
                    pagamento.Parcela_num = i;
                    parcelas = parcelas - 1;


                    db.Pagamentos.Add(pagamento);
                    db.SaveChanges();
                }

                cliente.pagamentoGerado = 1;
                db.SaveChanges();

            }
            var resultado = new
            {
                Sucess = true
            };
            return Json(resultado, JsonRequestBehavior.AllowGet);
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
                clienteAInserir.Valor_Pago = valorContrato/100;
                
                clienteAInserir.Parcela_num = i+1;
                db.Pagamentos.Add(clienteAInserir);
                db.SaveChanges();
            }
                

            return enviaBanco;
        }


    }
}