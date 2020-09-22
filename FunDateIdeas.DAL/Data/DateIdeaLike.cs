using System;
using System.ComponentModel.DataAnnotations;

namespace FunDateIdeas.DAL.Data
{
    public class DateIdeaLike
    {
        [Key]
        public Guid UserId { get; set; }
        [Key]
        public Guid DateIdeaId { get; set; }
    }
}
