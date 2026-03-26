using BlazorHero.CleanArchitecture.Domain.Contracts;

using System.Collections.Generic;

namespace BlazorHero.CleanArchitecture.Domain.Entities.Cours
{
    public class Module : AuditableEntity<int>
    {
        public string Title { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
