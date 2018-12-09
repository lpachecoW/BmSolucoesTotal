using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MgSolucoes.Models;
using PagedList;

namespace MgSolucoes.Controllers
{
    public class GruposController : Controller
    {
        ConexaoDbContext db;

        public GruposController()
        {
            db = new ConexaoDbContext();
        }
        // GET: Grupos
        public ActionResult Index(int? page)
        {
            string sortOrder = "";
            
            var grupos = from s in db.Grupos
                           select s;
            

            switch (sortOrder)
            {
                case "nome_desc":
                    grupos = grupos.OrderByDescending(s => s.Dt_Cadastro);
                    break;
                case "representacoes":
                    grupos = grupos.OrderBy(s => s.Dt_Cadastro);
                    break;
                //case "date_desc":
                //    clientes = clientes.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:
                    grupos = grupos.OrderBy(s => s.Dt_Cadastro);
                    break;
            }
            //var grupos = db.Grupos.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(grupos.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {   
            var model = new GruposViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GruposViewModel model)
        {
            if (ModelState.IsValid)
            {
                var grupo = new Grupos();
                grupo.Nome = model.Nome;
                grupo.Nu_parcelas = model.Nu_parcelas;
                grupo.Tipo_adesao = model.Tipo_adesao;
                grupo.Dt_Vencimento = model.Dt_Vencimento;
                grupo.Dt_Cadastro = DateTime.Today;
                grupo.Ano = "1";
                grupo.Dia = "1";
                grupo.Mes = "1";

                // Salvar a imagem para a pasta e pega o caminho

                db.Grupos.Add(grupo);
                db.SaveChanges();
                //return RedirectToAction("Create", "Contrato", new { id=cliente.ClienteId});
                return RedirectToAction("Index");
            }
            // Se ocorrer um erro retorna para pagina
            
            return View(model);
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Grupos grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }
        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grupos grupo = db.Grupos.Find(id);
            db.Grupos.Remove(grupo);
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
            Grupos grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }

            return View(grupo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Grupo_id,Nome,Dt_Vencimento,Dt_Cadastro, Tipo_adesao,Nu_parcelas")] Grupos model)
        {
            if (ModelState.IsValid)
            {
                var grupo = db.Grupos.Find(model.Grupo_id);
                if (grupo == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                grupo.Nome = model.Nome;
                grupo.Dt_Vencimento = model.Dt_Vencimento;
                grupo.Dt_Cadastro = model.Dt_Cadastro;
                grupo.Tipo_adesao = model.Tipo_adesao;
                grupo.Nu_parcelas = model.Nu_parcelas;
                
                

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Categorias = db.Categorias;
            return View(model);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupos grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            

            return View(grupo);
        }
    }
}