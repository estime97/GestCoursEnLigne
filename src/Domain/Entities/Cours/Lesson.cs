using BlazorHero.CleanArchitecture.Domain.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Domain.Entities.Cours
{
    public class Lesson : AuditableEntity<int>
    {
        public string Title { get; set; }
        public string ContentUrl { get; set; }
        public string Type { get; set; } // "video", "pdf"
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }
    }
}
