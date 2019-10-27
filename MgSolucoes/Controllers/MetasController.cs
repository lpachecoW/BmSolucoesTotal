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
    public class MetasController : Controller
    {
        ConexaoDbContext db;

        public MetasController()
        {
            db = new ConexaoDbContext();
        }

        // GET: Metas
        public ActionResult Index(int? page)
        {

            string sortOrder = "";

            var metas = from s in db.Metas
                         select s;


            switch (sortOrder)
            {
                case "nome_desc":
                    metas = metas.OrderByDescending(s => s.Inicio);
                    break;
                case "Inicio":
                    metas = metas.OrderBy(s => s.Inicio);
                    break;
                //case "date_desc":
                //    clientes = clientes.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:
                    metas = metas.OrderBy(s => s.Inicio);
                    break;
            }
            //var grupos = db.Grupos.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(metas.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Create()
        {
            var model = new MetaViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MetaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var meta = new Metas();
                meta.MetaDiaria = model.MetaDiaria;
                meta.Inicio = model.Inicio;
                meta.Fim = model.Fim;
                meta.MetaDiaria = model.MetaDiaria;
                
                // Salvar a imagem para a pasta e pega o caminho

                db.Metas.Add(meta);
                db.SaveChanges();
                //return RedirectToAction("Create", "Contrato", new { id=cliente.ClienteId});
                return RedirectToAction("Index");
            }
            // Se ocorrer um erro retorna para pagina

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metas meta = db.Metas.Find(id);
            if (meta == null)
            {
                return HttpNotFound();
            }


            return View(meta);
        }
    }
}