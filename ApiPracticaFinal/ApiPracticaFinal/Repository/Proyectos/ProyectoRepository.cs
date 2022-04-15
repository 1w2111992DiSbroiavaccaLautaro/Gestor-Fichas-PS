using ApiPracticaFinal.Models;
using ApiPracticaFinal.Models.DTO.Areas;
using ApiPracticaFinal.Models.DTO.PresupuestoDTOs;
using ApiPracticaFinal.Models.DTO.ProyectoDTOs;
using ApiPracticaFinal.Models.DTO.PublicacionDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Repository.Proyectos
{
    public class ProyectoRepository : IProyectoRepository
    {
        private readonly dd4snj9pkf64vpContext context;
        public ProyectoRepository(dd4snj9pkf64vpContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(ProyectoInsert proyecto)
        {
            Proyecto pro = new Proyecto();

            pro.Activo = true;
            pro.AnioFinalizacion = proyecto.AnioFinalizacion;
            pro.AnioInicio = proyecto.AnioInicio;
            //pro.SsmaTimestamp = new byte[5];
            pro.MontoContrato = proyecto.MontoContrato;
            //ver de agregar despues
            //pro.Monto = proyecto.Monto;
            pro.NroContrato = proyecto.NroContrato;
            pro.PaisRegion = proyecto.PaisRegion;
            pro.Titulo = proyecto.Titulo;
            pro.Certconformidad = proyecto.Certconformidad;
            pro.Certificadopor = proyecto.Certificadopor;
            pro.Moneda = proyecto.Moneda;
            pro.EnCurso = proyecto.EnCurso;
            pro.Descripcion = proyecto.Descripcion;
            pro.Departamento = proyecto.Departamento;
            pro.ConsultoresAsoc = proyecto.ConsultoresAsoc;
            pro.Resultados = proyecto.Resultados;
            pro.MesInicio = proyecto.MesInicio;
            pro.MesFinalizacion = proyecto.MesFinalizacion;
            pro.Contratante = proyecto.Contratante;
            pro.Dirección = proyecto.Dirección;
            pro.FichaLista = proyecto.FichaLista;
            pro.Link = proyecto.Link;
            pro.Convenio = proyecto.Convenio;

            await context.Proyectos.AddAsync(pro);
            var valor = await context.SaveChangesAsync();

            if (valor == 0)
            {
                throw new Exception("No se inserto el proyecto");
            }

            foreach (var i in proyecto.ListaAreas)
            {
                Areasxproyecto area = new Areasxproyecto();
                area.Idarea = i.IdArea;
                area.Idproyecto = pro.Id;
                await context.Areasxproyectos.AddAsync(area);

            }

            foreach (var j in proyecto.ListaPersonal)
            {
                Equipoxproyecto equipo = new Equipoxproyecto();
                equipo.IdPersonal = j.IdPersonal;
                equipo.Coordinador = j.Coordinador;
                equipo.IdProyecto = pro.Id;
                equipo.SsmaTimestamp = new byte[5];

                await context.Equipoxproyectos.AddAsync(equipo);
            }

            foreach (var p in proyecto.ListaPublicaciones)
            {
                Publicacionesxproyecto publi = new Publicacionesxproyecto();
                publi.Año = p.Año;
                publi.Codigobcs = p.Codigobcs;
                publi.IdProyecto = pro.Id;
                publi.Publicacion = p.Publicacion;

                await context.Publicacionesxproyectos.AddAsync(publi);
            }

            foreach (var v in proyecto.ListaPresupuestos)
            {
                Presupuesto presupuesto = new Presupuesto();
                presupuesto.Equipamiento = v.Equipamiento;
                presupuesto.Gastos = v.Gastos;
                presupuesto.Viatico = v.Viatico;
                presupuesto.Honorario = v.Honorario;
                presupuesto.Idproyecto = pro.Id;

                await context.Presupuestos.AddAsync(presupuesto);
            }

            valor = await context.SaveChangesAsync();

            if (valor == 0)
            {
                throw new Exception("No se pudo insertar el proyecto con area y/o personal y/o presupuesto y/o publicaciones");
            }
            return true;
        }

        public async Task<List<ProyectoDTO>> GetProyectos()
        {
            var proyectos = await context.Proyectos.Where(i => i.Activo.Equals(true)).OrderByDescending(x => x.Id).ToListAsync();
            var areasxproyectosBD = await context.Areasxproyectos.ToListAsync();
            var publixproyectoBD = await context.Publicacionesxproyectos.ToListAsync();
            var presupuestoxproyectoBD = await context.Presupuestos.ToListAsync();
            var areaBD = await context.Areas.ToListAsync();

            var listProyectoDto = new List<ProyectoDTO>();
            
            foreach (var i in proyectos)
            {
                //var areaxProyecto = await context.Areasxproyectos.Where(x => x.Idproyecto == i.Id).ToListAsync();
                var areaxProyecto = areasxproyectosBD.Where(x => x.Idproyecto == i.Id).ToList();
                var publixproyecto = publixproyectoBD.Where(x => x.IdProyecto == i.Id).ToList();
                var presupuestoxproyecto = presupuestoxproyectoBD.Where(x => x.Idproyecto == i.Id).ToList();

                var listaAreaDto = new List<AreaDTO>();
                var listaPublicacionesDto = new List<PublicacionDTO>();
                var listaPrespuestoDto = new List<PresupuestoDTO>();

                foreach (var j in areaxProyecto)
                {
                    //var area = await context.Areas.FirstOrDefaultAsync(x => x.Id == j.Idarea);
                    var area = areaBD.FirstOrDefault(x => x.Id == j.Idarea);

                    if (area != null)
                    {
                        var areaDto = new AreaDTO
                        {
                            Id = area.Id,
                            Area1 = area.Area1
                        };
                        listaAreaDto.Add(areaDto);
                    }
                }
                
                foreach (var p in publixproyecto)
                {
                    var publicacion = publixproyectoBD.FirstOrDefault(x => x.IdPublicacion == p.IdPublicacion);
                    if (publicacion != null)
                    {
                        var publiDTO = new PublicacionDTO
                        {
                            IdPublicacion = publicacion.IdPublicacion,
                            IdProyecto = publicacion.IdProyecto,
                            Año = publicacion.Año,
                            Codigobcs = publicacion.Codigobcs,
                            Publicacion = publicacion.Publicacion
                        };
                        listaPublicacionesDto.Add(publiDTO);
                    }
                }

                foreach (var item in presupuestoxproyecto)
                {
                    var presupuesto = presupuestoxproyectoBD.FirstOrDefault(x => x.Idpresupuesto == item.Idpresupuesto);
                    if (presupuesto != null)
                    {
                        var presupestoDto = new PresupuestoDTO
                        {
                            Idpresupuesto = item.Idpresupuesto,
                            Idproyecto = item.Idproyecto,
                            Honorario = item.Honorario,
                            Equipamiento = item.Equipamiento,
                            Gastos = item.Gastos,
                            Viatico = item.Viatico
                        };
                        listaPrespuestoDto.Add(presupestoDto);
                    }
                }
                var proyectoDto = new ProyectoDTO
                {
                    Id = i.Id,
                    Titulo = i.Titulo,
                    AnioFinalizacion = i.AnioFinalizacion,
                    AnioInicio = i.AnioInicio,
                    Departamento = i.Departamento,
                    ListaAreas = listaAreaDto,
                    FichaLista = i.FichaLista,
                    MesFinalizacion = i.MesFinalizacion,
                    MesInicio = i.MesInicio,
                    PaisRegion = i.PaisRegion,
                    MontoContrato = i.MontoContrato,
                    Moneda = i.Moneda,
                    Link = i.Link,
                    ListaPublicaciones = listaPublicacionesDto,
                    ListaPresupuestos = listaPrespuestoDto
                };

                listProyectoDto.Add(proyectoDto);
            }
            return listProyectoDto;
        }
    }
}
