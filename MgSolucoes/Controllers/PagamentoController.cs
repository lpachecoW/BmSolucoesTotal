using System;
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
    public class PagamentoController : Controller
    {
        ConexaoDbContext db;

        public PagamentoController()
        {
            db = new ConexaoDbContext();
        }
        // GET: Pagamento
        public ActionResult Index(int? page)
        {
            string sortOrder = "";
            
            var pagamentos = db.Pagamentos.GroupBy(x => x.Clientes.Nome).Select(y => y.FirstOrDefault()).AsQueryable();
            
            switch (sortOrder)
            {
         
                default:
                    pagamentos = pagamentos.OrderBy(s => s.Valor_Pago);
                    break;
            }
            //var grupos = db.Grupos.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(pagamentos.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult BmParcelasAReceber()
        {
            var bmAReceber = db.Pagamentos.Where(x => x.Clienteid > 0).ToList();

            return View(bmAReceber);
        }


        // GET: Pagamento
        public ActionResult ContasAReceber(int? page)
        {

            ContasAPagar contas = new ContasAPagar();
                
            var aReceber = (from cli in db.Clientes
                           join gru in db.Grupos on cli.Grupo_id equals gru.Grupo_id
                           join com in db.Comissoes on cli.Grupo_id equals com.Grupo_id
                           join rep in db.Representacoes on cli.Representacao_id equals rep.Representacao_id
                           select new ContasAPagarViewModel { Nome = gru.Nome, nome1 = rep.Nome, Nu_parcelas = gru.Nu_parcelas, Valor_Credito = cli.Valor_Credito, ClienteId = cli.ClienteId });


            return View(aReceber);
        }

        public ActionResult ContasAReceberPorCliente(int? id)
        {
            
            var detalhePorCliente = db.Pagamentos.Where(x => x.Clienteid == id).ToList();

            return View(detalhePorCliente);
        }

        public ActionResult DetalhaPagamentoByClienteId(int? id)
        {

            Pagamento detalhePorCliente = db.Pagamentos.Find(id);
            return View(detalhePorCliente);
        }


        // GET: Pagamento
        public ActionResult ContasAReceberDetalhe(int id)
        {

            ContasAPagar contas = new ContasAPagar();

            var aReceber = (from cli in db.Clientes
                            join gru in db.Grupos on cli.Grupo_id equals gru.Grupo_id
                            join com in db.Comissoes on cli.Grupo_id equals com.Grupo_id
                            join rep in db.Representacoes on cli.Representacao_id equals rep.Representacao_id

                            select new ContasAPagarViewModel
                            {
                                Nome = gru.Nome,
                                nome1 = rep.Nome,
                                Nu_parcelas = gru.Nu_parcelas,
                                Valor_Credito = cli.Valor_Credito,
                                Rep_1 = (cli.Valor_Credito * com.Rep_1 / 100),
                                Rep_2 = (cli.Valor_Credito * com.Rep_2 / 100),
                                Rep_3 = (cli.Valor_Credito * com.Rep_3 / 100),
                                Rep_4 = (cli.Valor_Credito * com.Rep_4 / 100),
                                Rep_5 = (cli.Valor_Credito * com.Rep_5 / 100),
                                Rep_6 = (cli.Valor_Credito * com.Rep_6 / 100),
                                ClienteId = cli.ClienteId
                                
                            });

            aReceber = aReceber.Where(x => x.ClienteId.Equals(id));
            return View(aReceber);
        }


        public ActionResult Create(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            ViewBag.Grupos = db.Grupos.Find(cliente.Grupo_id).Nome;
            ViewBag.Cliente = db.Clientes.Find(cliente.ClienteId).Nome;
            var model = new PagamentoViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagamentoViewModel model, int id)
        {

            Cliente cliente = db.Clientes.Find(id);


            //if (ModelState.IsValid)
            //{
                
                var pagamento = new Pagamento();
                if (model.Dt_Pagamento == null) {
                    pagamento.Dt_Pagamento = DateTime.UtcNow;
                } else {
                    pagamento.Dt_Pagamento = model.Dt_Pagamento;
                }
                pagamento.Clienteid = cliente.ClienteId;
                
                pagamento.Grupo_id = cliente.Grupo_id;
                pagamento.Status_Pagamento = model.Status_Pagamento;
                pagamento.Valor_Pago = model.Valor_Pago;
                pagamento.Parcela_num = model.Parcela_num;


                try
                {
                    db.Pagamentos.Add(pagamento);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Pagamento");
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


            //}
            
        }
    }
}