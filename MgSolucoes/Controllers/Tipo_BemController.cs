using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MgSolucoes.Models;

namespace MgSolucoes.Controllers
{
    public class Tipo_BemController : Controller
    {
        ConexaoDbContext db;

        public Tipo_BemController()
        {
            db = new ConexaoDbContext();
        }

        public ActionResult Create()
        {
            var model = new Tipo_BemViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tipo_BemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tipo_bem = new Tipo_Bem();
                tipo_bem.Nome = model.Nome;
                
                // Salvar a imagem para a pasta e pega o caminho

                db.Tipo_Bens.Add(tipo_bem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Se ocorrer um erro retorna para pagina
            ViewBag.Tipo_Bem = db.Tipo_Bens;
            return View(model);
        }


        // GET: Tipo_Bem
        public ActionResult Index()
        {
            var  tipo_bem = db.Tipo_Bens.ToList();
            return View(tipo_bem);
            
        }
    }
}