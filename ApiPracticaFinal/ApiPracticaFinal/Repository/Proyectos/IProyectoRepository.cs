﻿using ApiPracticaFinal.Models.DTO.ProyectoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Repository.Proyectos
{
    public interface IProyectoRepository
    {
        Task<List<ProyectoDTO>> GetProyectos();
    }
}
