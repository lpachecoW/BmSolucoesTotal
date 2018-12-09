using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MgSolucoes.Models;
using PagedList;

namespace MgSolucoes.Controllers
{
    public class HomeController : Controller
    {
        ConexaoDbContext db;

        public HomeController()
        {
            db = new ConexaoDbContext();
        }

        public ActionResult Index()
        {
            var AtendimentoRealizados = db.Atendimentos.Where(x=> x.Status_Atendimento_id == 1 && x.Status_Pagamento_id == 1).Count();
            var TotalAtendimentos = db.Atendimentos.Count();
            var NovosClientes = db.Clientes.Count();
            var ClientesEmAtraso = db.Pagamentos.Where(x => x.Status_Pagamento != "PAGO").Count();
            int years = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            var ClienteAniversario = db.Clientes.Where(x => x.Dt_nascimento.Day == day && x.Dt_nascimento.Month == month ).Count();

            ViewBag.Aniversariantes = ClienteAniversario;
            ViewBag.ClientesEmAtraso = ClientesEmAtraso;
            ViewBag.NovosClientes = NovosClientes;
            ViewBag.AtendimentoRealizados = AtendimentoRealizados;
            ViewBag.TotalAtendimentos = TotalAtendimentos;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}