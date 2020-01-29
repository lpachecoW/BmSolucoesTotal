using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MgSolucoes.Models;

namespace MgSolucoes.Controllers
{
    public class AtendimentoController : Controller
    {
        ConexaoDbContext db;

        public AtendimentoController()
        {
            db = new ConexaoDbContext();
        }
        // GET: Atendimento
        public ActionResult ListaAtendimentosByClienteId(int id)
        {
            //var atendimento = db.Atendimentos.ToList();
            var atendimentos = db.Atendimentos.Where(x=>x.Clienteid == id).ToList();
            Cliente cliente = db.Clientes.Find(id);

            ViewBag.Representacao = db.Representacoes.Find(cliente.Representacao_id).Nome;
            ViewBag.ClienteNome = cliente.Nome;
            ViewBag.Grupo = cliente.Grupos.Nome;
            ViewBag.Cota = cliente.Cota_id;
            ViewBag.ClienteId = id;

            return View(atendimentos);

        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Atendimento atendimento = db.Atendimentos.Find(id);
            
            if (atendimento == null)
            {
                return HttpNotFound();
            }

            return View(atendimento);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AtendimentoViewModel model, int id)
        {

            var atendimento = db.Atendimentos.Find(model.Atendimento_id);
            if (atendimento == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            atendimento.Texto = atendimento.Texto;
            atendimento.Dt_Atendimento = atendimento.Dt_Atendimento;
            atendimento.Alteracao = DateTime.UtcNow;
            atendimento.Clienteid = atendimento.Clienteid;
            atendimento.Status_Atendimento_id = atendimento.Status_Atendimento_id;
            atendimento.Valor_ofertado = atendimento.Valor_ofertado;
            atendimento.Procon = atendimento.Procon;
            atendimento.Boleto_Enviado = atendimento.Boleto_Enviado;


            db.SaveChanges();

            atendimento.Texto = model.Texto;
            atendimento.Dt_Atendimento = model.Dt_Atendimento;
            atendimento.Clienteid = model.Clienteid;
            atendimento.Status_Atendimento_id = model.Status_Atendimento_id;
            atendimento.Valor_ofertado = model.Valor_ofertado;
            atendimento.Procon = model.Procon;
            atendimento.Boleto_Enviado = model.Boleto_Enviado;



            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //Action para o botao de retorno
        public ActionResult Inicio()
        {
            return RedirectToAction("Index", "Cliente");

        }
        
        // GET: Atendimento
        public ActionResult AtendimentosRealizados()
        {
            var atendimentos = db.Atendimentos.GroupBy(x => x.Clientes.Nome).Select(y => y.FirstOrDefault()).ToList();
            atendimentos = atendimentos.OrderBy(x => x.Dt_Atendimento).ToList();
            return View(atendimentos);

        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var atendimentos = (from s in db.Atendimentos
                                where s.Clienteid == id
                                select s).ToList();

            if (atendimentos == null)
            {
                return HttpNotFound();
            }

            return View(atendimentos);
        }


        public ActionResult DetailsByIdDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Atendimento atendimento = db.Atendimentos.Find(id);

            if (atendimento == null)
            {
                return HttpNotFound();
            }

            return View(atendimento);
        }

        public ActionResult Create(int id)
        {
            ViewBag.Atendimento = db.Status_Atendimentos;
            ViewBag.Pagamento = db.Status_Pagamentos;
            ViewBag.Lance = db.Status_Lance;
            var model = new AtendimentoViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AtendimentoViewModel model, int id)
        {
            if (ModelState.IsValid)
            {

                var atendimento = new Atendimento();
                Cliente cliente = db.Clientes.Find(id);

                atendimento.Clienteid = id;
                atendimento.Status_Pagamento_id = 0;
                atendimento.Status_Atendimento_id = model.Status_Atendimento_id;
                atendimento.Boleto_Enviado = model.Boleto_Enviado;
                atendimento.Dt_Atendimento = DateTime.Now;
                atendimento.Texto = model.Texto;
                atendimento.Status_Lance_id = model.Status_Lance_id;
                atendimento.Atendente_id = 1;
                if (model.Status_Atendimento_id == 0)
                {
                    cliente.HasAtendimento = 0;
                }
                else {
                    cliente.HasAtendimento = 1;
                    cliente.Status_Atendimento_Id = model.Status_Atendimento_id;
                }
                
                atendimento.Valor_ofertado = model.Valor_ofertado;
                atendimento.Procon = model.Procon;
                atendimento.Contatado = model.Contatado;

                try
                {   
                    db.Atendimentos.Add(atendimento);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cliente");
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
            return View(model);
        }



        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Atendimento atendimento = db.Atendimentos.Find(id);
            if (atendimento == null)
            {
                return HttpNotFound();
            }

            return View(atendimento);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Atendimento atendimento = db.Atendimentos.Find(id);
            db.Atendimentos.Remove(atendimento);
            db.SaveChanges();
            return RedirectToAction("AtendimentosRealizados");
        }

        public JsonResult GetComentario(int atendimentoId)
        {
            Atendimento atendimento = db.Atendimentos.Find(atendimentoId);
            
            var resultado = atendimento.Texto.ToString();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

    }
}
