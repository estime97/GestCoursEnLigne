using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Domain.Entities.Cours
{
   public interface IApplicationUser
    {
        string Id { get; }
        string FirstName { get; }
        string LastName { get; }
    }
}
