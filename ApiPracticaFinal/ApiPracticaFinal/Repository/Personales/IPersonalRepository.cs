using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPracticaFinal.Models;

namespace ApiPracticaFinal.Repository.Personales
{
    public interface IPersonalRepository
    {
        Task<List<Personal>> GetPersonalAsync();
    }
}
