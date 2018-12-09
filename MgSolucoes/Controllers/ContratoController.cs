using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using MgSolucoes.Models;

namespace MgSolucoes.Controllers
{
    public class ContratoController : Controller
    {

        ConexaoDbContext db;

        public ContratoController()
        {
            db = new ConexaoDbContext();
        }
        // GET: Contrato
        public ActionResult Index()
        {
            var contrato = db.Contratos.ToList();
            return View(contrato);
            
        }


        public ActionResult Create(int id)
        {
            var cliente = db.Clientes.Find(id);
            ViewBag.Grupos = db.Grupos;
            ViewBag.Representacao = db.Representacoes;
            ViewBag.Cliente = cliente;
            ViewBag.Tipo_Bem = db.Tipo_Bens;
            var model = new ContratoViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContratoViewModel model, int id)
        {
            var a = ViewBag.Cliente;
            var cliente = db.Clientes.Find(id);
            
            if (ModelState.IsValid)
            {
                var contrato = new Contrato();
                contrato.Grupo_id = cliente.Grupo_id;
                contrato.ClienteId = cliente.ClienteId;
                contrato.Representacao_id = cliente.Representacao_id;
                contrato.Numero = model.Numero;
                contrato.N_Parcelas = model.N_Parcelas;
                contrato.Percentual_bem = model.Percentual_bem;
                contrato.Tabela = model.Tabela;
                contrato.TipoBemId = model.TipoBemId;
                contrato.Valor_bem = model.Valor_bem;
                contrato.Vencimento = model.Vencimento;

                

                db.Contratos.Add(contrato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Se ocorrer um erro retorna para pagina
            ViewBag.Clientes = db.Clientes;
            return View(model);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato= db.Contratos.Find(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }

            ViewBag.Grupos = db.Grupos.Find(contrato.Grupo_id).Nome;
            ViewBag.Representacao = db.Representacoes.Find(contrato.Representacao_id).Nome;
            ViewBag.Tipo_Bem = db.Tipo_Bens.Find(contrato.TipoBemId).Nome;

            return View(contrato);
        }
    }
}