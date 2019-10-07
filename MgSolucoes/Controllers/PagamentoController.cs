using System;
using System.Collections.Generic;
using System.Globalization;
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

        public ActionResult RelatoriosBm(string sortOrder, string currentFilter, string tipoRelatorio, string Representacao_id, string periodoIni, string periodoFim, string parcelaNum, int? page) {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeSortParm = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            ViewBag.RepSortParam = String.IsNullOrEmpty(sortOrder) ? "representacoes" : "";

            ViewBag.ddlGrupo = db.Grupos;
            ViewBag.ddlRepresentacao = db.Representacoes;
            ViewBag.ddlStatusAtendimento = db.Status_Atendimentos;

            var pagamentos = from s in db.Pagamentos
                           select s;



            switch (sortOrder)
            {
                case "nome_desc":
                    pagamentos = pagamentos.OrderBy(s => s.Representacao_id);
                    break;
                case "representacoes":
                    pagamentos = pagamentos.OrderBy(s => s.Representacao_id);
                    break;
                default:
                    pagamentos = pagamentos.OrderBy(s => s.Representacao_id);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(pagamentos.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult BmParcelasAReceber()
        {
            var bmAReceber = db.Pagamentos.Where(x => x.Clienteid > 0 && x.Status_Pagamento == "PAGO").ToList();

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
            var totalPagamentos = db.Pagamentos.Where(x => x.Clienteid == id && x.Status_Pagamento.Equals("PAGO")).ToList();
          
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
            int parcelas = cliente.Grupos.Nu_parcelas;
            var pagamento = new Pagamento();
            var olddate = DateTime.Now;

            DateTime? isVencimento = null;
            DateTime? isPagamento = null;

            isVencimento = model.Dt_Vencimento;
            isPagamento = model.Dt_Pagamento;

            for (var i = 0; i < parcelas; i++)
            {
                if (model.Dt_Pagamento == null)
                {
                    pagamento.Dt_Pagamento = DateTime.Now;
                }
                else
                {
                    pagamento.Dt_Pagamento = model.Dt_Pagamento;
                }

                if (model.Dt_Vencimento == null && i == 0)
                {
                    pagamento.Dt_Vencimento = DateTime.Now;
                }
                
                pagamento.Clienteid = cliente.ClienteId;

                pagamento.Grupo_id = cliente.Grupo_id;
                pagamento.Representacao_id = cliente.Representacao_id;

                if (i == 0)
                {
                    pagamento.Status_Pagamento = "PAGO";
                    pagamento.Dt_Vencimento = model.Dt_Vencimento;
                    
                }
                else
                {
                    pagamento.Status_Pagamento = "NÃO PAGO";
                }
                
                if (isVencimento.HasValue)
                {
                    if (i > 0)
                    {
                        var tempDate = model.Dt_Vencimento.AddMonths(i);
                        var newDate = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day); //create 
                        pagamento.Dt_Vencimento = newDate;
                    }
                    else {
                        pagamento.Dt_Vencimento = model.Dt_Vencimento;
                    }
                }

                pagamento.Valor_Pago = model.Valor_Pago;
                pagamento.Parcela_num = i+1;

                db.Pagamentos.Add(pagamento);
                cliente.pagamentoGerado = 1;
                db.SaveChanges();
            }

            

            try
            {
                
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


        //[HttpPost]
        //public ActionResult BaixaPagamento(int idPagamento)
        //{

        //    var pagamento = db.Pagamentos.Find(idPagamento);
        //    if (pagamento == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    pagamento.Dt_Pagamento = DateTime.Today;
        //    pagamento.Status_Pagamento = "PAGO";

        //    db.SaveChanges();
        //    return RedirectToAction("ContasAReceberPorCliente", pagamento.Clienteid );

        //    //ViewBag.Categorias = db.Categorias;

        //}

        public JsonResult BaixaPagamento(int idPagamento)
        {
            var pagamento = db.Pagamentos.Find(idPagamento);
            pagamento.Dt_Pagamento = DateTime.Today;
            pagamento.Status_Pagamento = "PAGO";

            db.SaveChanges();

            var resultado = new
            {
                Sucess = true
            };
            return Json(resultado, JsonRequestBehavior.AllowGet);
            
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pagamento pagamento = db.Pagamentos.Find(id);
            
            if (pagamento == null)
            {
                return HttpNotFound();
            }

            return View(pagamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PagamentoId, Dt_Pagamento, Dt_Vencimento,Valor_Pago,Status_Pagamento, Parcela_num,Comentario_Pagamento")] Pagamento model)
        {

            var pagamento = db.Pagamentos.Find(model.PagamentoId);
            if (pagamento == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                pagamento.Dt_Pagamento = model.Dt_Pagamento;
                pagamento.Dt_Vencimento = model.Dt_Vencimento;
                pagamento.Valor_Pago = model.Valor_Pago;
                pagamento.Status_Pagamento = model.Status_Pagamento;
                pagamento.Parcela_num = model.Parcela_num;
                if (String.IsNullOrEmpty(model.Comentario_Pagamento))
                {
                    pagamento.Comentario_Pagamento = "Não há comentario cadastrado.";
                }
                else {

                    pagamento.Comentario_Pagamento = model.Comentario_Pagamento;
                }
                
                db.SaveChanges();
            }
            

            
            return RedirectToAction("ContasAReceberPorCliente/" + pagamento.Clienteid);

            //ViewBag.Categorias = db.Categorias;

        }

        public JsonResult GetComentario(int pagamentoId)
        {
            Pagamento pagamento = db.Pagamentos.Find(pagamentoId);

            var resultado = pagamento.Comentario_Pagamento.ToString();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

    }
}