using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            var AtendimentosDiarios = db.Atendimentos.Where(x => DbFunctions.TruncateTime(x.Dt_Atendimento) == DateTime.Today).Count();
            var ClientesSemComunicacao = db.Clientes.Where(x => x.Email.Equals("bm@bmc.om.br") || x.Tel_Fixo.Equals("(00)0000-0000") || x.Tel_Movel.Equals("(00)0000-0000")).Count();

            ViewBag.AtendimentoDiario = AtendimentosDiarios;
            ViewBag.TotalDeClientes = NovosClientes;
            ViewBag.Aniversariantes = ClienteAniversario;
            ViewBag.ClientesSemNumEmail = ClientesSemComunicacao;

            ViewBag.Adimplentes = 0;
            ViewBag.Inadimplentes = 0;
            ViewBag.Cancelados = 0;
            ViewBag.ObjetivosConcluidos= 0;
            ViewBag.Pendente = 0;

            ViewBag.PagamentosMesAtual = 0;
            ViewBag.PagamentosPagos = 0;
            ViewBag.PagamentosAtraso = 0;

            ViewBag.BoletosNaoEnviados = 0;
            ViewBag.BoletosEnviados = 0;
            
            

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