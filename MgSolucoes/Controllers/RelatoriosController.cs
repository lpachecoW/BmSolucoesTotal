using System;
using System.Globalization;
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
    public class RelatoriosController : Controller
    {
        ConexaoDbContext db;

        public RelatoriosController()
        {
            db = new ConexaoDbContext();
        }

        // GET: Relatorios
        public ActionResult Index(string sortOrder, string currentFilter)
        {
            ViewBag.TipoRelatorio = db.relatorios_Descretivas;
            ViewBag.Representacao = db.Representacoes;
            return View();
            
        }
    }
}