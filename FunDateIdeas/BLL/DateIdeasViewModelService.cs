using FunDateIdeas.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDateIdeas.BLL
{
    public class DateIdeasViewModelService
    {
        private readonly FunDateIdeasDbContext _ctx;
        public DateIdeasViewModelService(FunDateIdeasDbContext ctx)
        {
            _ctx = ctx;
        }
        public List<DateIdea> GetAllDateIdeas()
        {
            return _ctx.DateIdeas.ToList();
        }

        public DateIdea GetRandomDateIdea()
        {
            DateIdea dateIdea = _ctx.DateIdeas.OrderBy(x => Guid.NewGuid()).Take(1).FirstOrDefault();
            return dateIdea;
        }
    }
}
