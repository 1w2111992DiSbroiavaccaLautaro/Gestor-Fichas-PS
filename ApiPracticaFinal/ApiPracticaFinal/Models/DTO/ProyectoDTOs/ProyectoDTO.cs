using ApiPracticaFinal.Models.DTO.Areas;
using ApiPracticaFinal.Models.DTO.PresupuestoDTOs;
using ApiPracticaFinal.Models.DTO.PublicacionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Models.DTO.ProyectoDTOs
{
    public class ProyectoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string PaisRegion { get; set; }
        public string MontoContrato { get; set; }
        public int? MesInicio { get; set; }
        public int? AnioInicio { get; set; }
        public int? MesFinalizacion { get; set; }
        public int? AnioFinalizacion { get; set; }
        public bool? FichaLista { get; set; }
        public string Departamento { get; set; }
        public string Moneda { get; set; }
        public string Link { get; set; }
        public double? Monto { get; set; }
        public List<AreaDTO> ListaAreas {get; set;}
        public List<PublicacionDTO> ListaPublicaciones { get; set; }
        public List<PresupuestoDTO> ListaPresupuestos { get; set; }
    }
}
