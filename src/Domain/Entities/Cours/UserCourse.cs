using BlazorHero.CleanArchitecture.Domain.Contracts;

using System;

namespace BlazorHero.CleanArchitecture.Domain.Entities.Cours
{
    public class UserCourse : AuditableEntity<int>
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; } = false;
        public double Progress { get; set; } = 0; // % de progression, optionnel
    }
}
