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
    public class ComissaoController : Controller
    {
        ConexaoDbContext db;

        public ComissaoController()
        {
            db = new ConexaoDbContext();
        }
        // GET: Comissao
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeSortParm = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            ViewBag.RepSortParam = String.IsNullOrEmpty(sortOrder) ? "representacoes" : "";
            var first = "";
            var last = "";



            if (searchString != null)
            {
                page = 1;
                List<string> strList = searchString.Split('/').ToList();
                first = strList.First();
                last = strList.Last();
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var comissoes = from s in db.Comissoes
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {

            }

            switch (sortOrder)
            {

                default:
                    comissoes = comissoes.OrderBy(s => s.ComissaoId);
                    break;
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(comissoes.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {


            ViewBag.Grupos = db.Grupos;
            ViewBag.Representacao = db.Representacoes;
            var model = new ComissaoViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComissaoViewModel model)
        {

            var comissao = new Comissao();
            comissao.Grupo_id = model.Grupo_id;
            comissao.Representacao_id = model.Representacao_id;
            comissao.Rep_1 = model.Rep_1;
            comissao.Rep_2 = model.Rep_2;
            comissao.Rep_3 = model.Rep_3;
            comissao.Rep_4 = model.Rep_4;
            comissao.Rep_5 = model.Rep_5;
            comissao.Rep_6 = model.Rep_6;
            

            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges
                db.Comissoes.Add(comissao);
                db.SaveChanges();
                //return RedirectToAction("Create", "Contrato", new { id=cliente.ClienteId});
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
                // Se ocorrer um erro retorna para pagina
                //ViewBag.Clientes = db.Clientes;
                //return View(model);
            }



        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comissao comissao = db.Comissoes.Find(id);
            if (comissao == null)
            {
                return HttpNotFound();
            }

            ViewBag.Grupos = db.Grupos.Find(comissao.Grupo_id).Nome;
            ViewBag.Representacao = db.Representacoes.Find(comissao.Representacao_id).Nome;
            ViewBag.ComissaoId = comissao.ComissaoId;

            return View(comissao);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Comissao comissao = db.Comissoes.Find(id);
            if (comissao == null)
            {
                return HttpNotFound();
            }

            return View(comissao);
        }
        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comissao comissao = db.Comissoes.Find(id);
            db.Comissoes.Remove(comissao);
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

            Comissao comissao= db.Comissoes.Find(id);
            ViewBag.Grupos = db.Grupos;
            ViewBag.Representacao = db.Representacoes;
           
            if (comissao == null)
            {
                return HttpNotFound();
            }

            return View(comissao);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComissaoId,Representacao_id,Grupo_id,Rep_1,Rep_2,Rep_3,Rep_4,Rep_5,Rep_6")] Comissao model)
        {
            if (ModelState.IsValid)
            {
                var comissao = db.Comissoes.Find(model.ComissaoId);
                if (comissao == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                comissao.Rep_1 = model.Rep_1;
                comissao.Rep_2 = model.Rep_2;
                comissao.Rep_3 = model.Rep_3;
                comissao.Rep_4 = model.Rep_4;
                comissao.Rep_5 = model.Rep_5;
                comissao.Rep_6 = model.Rep_6;
                comissao.Grupo_id = model.Grupo_id;
                comissao.Representacao_id = model.Representacao_id;
                

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Categorias = db.Categorias;
            return View(model);
        }


    }
}