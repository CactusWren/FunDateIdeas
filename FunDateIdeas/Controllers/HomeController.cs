using FunDateIdeas.Data.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDateIdeas.Controllers
{
    public class HomeController : Controller
    {
        private readonly FunDateIdeasIdentityDbContext _dbContext;
        public HomeController(FunDateIdeasIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
