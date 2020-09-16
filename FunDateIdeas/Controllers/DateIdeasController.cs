using FunDateIdeas.DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDateIdeas.Controllers
{
    [AllowAnonymous]
    public class DateIdeasController : Controller
    {
        private readonly FunDateIdeasDbContext _funDateIdeasDbContext;
        public DateIdeasController(FunDateIdeasDbContext funDateIdeasDbContext)
        {
            _funDateIdeasDbContext = funDateIdeasDbContext;
        }
        public async Task<IActionResult> AddDateIdea()
        {
            DateIdea dateIdea = new DateIdea() { Title = "test" };
            _funDateIdeasDbContext.DateIdeas.Add(dateIdea);
            var saveResult = await _funDateIdeasDbContext.SaveChangesAsync();
            return View();
        }
    }
}
