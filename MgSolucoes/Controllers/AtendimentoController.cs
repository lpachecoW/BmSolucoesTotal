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

            return View(atendimentos);

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
                atendimento.Status_Atendimento_id = model.Status_Atendimento_id;
                atendimento.Status_Pagamento_id = model.Status_Pagamento_id;
                atendimento.Dt_Atendimento = DateTime.Now;
                atendimento.Texto = model.Texto;
                atendimento.Status_Lance_id = model.Status_Lance_id;
                atendimento.Atendente_id = "1";
                cliente.HasAtendimento = model.Status_Atendimento_id;
                atendimento.Valor_ofertado = model.Valor_ofertado;
                
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
    }
}
