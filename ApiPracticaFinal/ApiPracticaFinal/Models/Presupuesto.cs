using System;
using System.Collections.Generic;

#nullable disable

namespace ApiPracticaFinal.Models
{
    public partial class Presupuesto
    {
        public int Idpresupuesto { get; set; }
        public double Honorario { get; set; }
        public double Viatico { get; set; }
        public double Equipamiento { get; set; }
        public double Gastos { get; set; }
        public int Idproyecto { get; set; }
        public char? Trial477 { get; set; }

        public virtual Proyecto IdproyectoNavigation { get; set; }
    }
}
