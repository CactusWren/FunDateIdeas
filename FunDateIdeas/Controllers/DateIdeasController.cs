using FunDateIdeas.BLL;
using FunDateIdeas.DAL.Data;
using FunDateIdeas.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<FunDateIdeasUser> _userManager;

        private readonly FunDateIdeasDbContext _funDateIdeasDbContext;
        public DateIdeasController(FunDateIdeasDbContext funDateIdeasDbContext, UserManager<FunDateIdeasUser> userManager)
        {
            _userManager = userManager;
            _funDateIdeasDbContext = funDateIdeasDbContext;
        }
        public IActionResult AddDateIdea()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDateIdea(string title)
        {
            DateIdea dateIdea = new DateIdea() { Title = title };
            _funDateIdeasDbContext.DateIdeas.Add(dateIdea);
            var saveResult = await _funDateIdeasDbContext.SaveChangesAsync();
            return View();
        }

        public IActionResult ManageDateIdeas()
        {
            DateIdeasViewModelService viewModelService = new DateIdeasViewModelService(_funDateIdeasDbContext);
            List<DateIdea> model = viewModelService.GetAllDateIdeas();
            return View(model);
        }

        public IActionResult Random()
        {
            DateIdeasViewModelService viewModelService = new DateIdeasViewModelService(_funDateIdeasDbContext);
            DateIdea model = viewModelService.GetRandomDateIdea();
            return View("RandomDateIdea", model);
        }

        public async Task<IActionResult> LikeDateIdeaAsync(string dateIdeaId)
        {
            DateIdeaLike dateIdeaLike = _funDateIdeasDbContext.DateIdeaLikes.Where(x => x.UserId == Guid.Parse(_userManager.GetUserId(User)) && x.DateIdeaId == Guid.Parse(dateIdeaId)).FirstOrDefault();
            if (dateIdeaLike == null)
            {
                dateIdeaLike = new DateIdeaLike() { UserId = Guid.Parse(_userManager.GetUserId(User)), DateIdeaId = Guid.Parse(dateIdeaId) };
                _funDateIdeasDbContext.DateIdeaLikes.Add(dateIdeaLike);
                await _funDateIdeasDbContext.SaveChangesAsync();
            }
            return View();
        }
        public async Task<IActionResult> UnlikeDateIdeaAsync(string dateIdeaId)
        {
            DateIdeaLike dateIdeaLike = _funDateIdeasDbContext.DateIdeaLikes.Where(x => x.UserId == Guid.Parse(_userManager.GetUserId(User))).FirstOrDefault();
            if (dateIdeaLike != null)
            {
                _funDateIdeasDbContext.DateIdeaLikes.Remove(dateIdeaLike);
                await _funDateIdeasDbContext.SaveChangesAsync();
            }
            return View();
        }
    }
}
