using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StartupDemo.Controllers
{
    public class TesteController : Controller
    {
        [Route("dashboard/tela-inicial")]
        [Route("dashboard/tela-inicial/{id:int}/{valor:guid}")]
        public IActionResult Index(int id, Guid valor)
        {
            //Terminar redireciona para o meu método Teste()
            return RedirectToAction("Teste");
        }

        public IActionResult Teste()
        {
            return View("Index");
        }
    }
}