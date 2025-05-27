using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Domain.Entities.Cours
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<UserCourse> UserCourses { get; set; }
    }
}
