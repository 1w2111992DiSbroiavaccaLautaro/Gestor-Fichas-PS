﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Models.DTO.PresupuestoDTOs
{
    public class PresupuestoUpdateProyecto
    {
        public int IdPresupuesto { get; set; }
        public double Honorario { get; set; }
        public double Viatico { get; set; }
        public double Equipamiento { get; set; }
        public double Gastos { get; set; }
        public int Idproyecto { get; set; }
    }
}
