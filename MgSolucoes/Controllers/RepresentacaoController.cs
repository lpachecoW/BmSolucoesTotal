using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MgSolucoes.Models;

namespace MgSolucoes.Controllers
{
    public class RepresentacaoController : Controller
    {

        ConexaoDbContext db;

        public RepresentacaoController()
        {
            db = new ConexaoDbContext();
        }


        public ActionResult Create()
        {
            var model = new RepresentacaoViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RepresentacaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var representacao = new Representacao();
                representacao.Cpf = model.Cpf;
                representacao.Dt_nascimento = DateTime.Today;
                representacao.Funcao = model.Funcao;
                representacao.Nome = model.Nome;

                // Salvar a imagem para a pasta e pega o caminho

                db.Representacoes.Add(representacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Se ocorrer um erro retorna para pagina
            ViewBag.Representacao = db.Representacoes;
            return View(model);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Representacao representacao = db.Representacoes.Find(id);
            if (representacao == null)
            {
                return HttpNotFound();
            }

            return View(representacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nome, Dt_nascimento, Cpf, Funcao")] Representacao model)
        {

            var representacao = db.Representacoes.Find(model.Representacao_id);
            if (representacao == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            representacao.Nome = model.Nome;
            representacao.Dt_nascimento = model.Dt_nascimento;
            representacao.Cpf = model.Cpf;
            representacao.Funcao = model.Funcao;

            db.SaveChanges();
            return RedirectToAction("Index");

            //ViewBag.Categorias = db.Categorias;

        }

        // GET: Representacao
        public ActionResult Index()
        {
            var representacao = db.Representacoes.ToList();
            return View(representacao);
        }
    }
}