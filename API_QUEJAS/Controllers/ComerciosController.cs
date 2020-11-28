using API_QUEJAS.Helpers;
using API_QUEJAS.ModelosPersonalizados;
using API_QUEJAS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_QUEJAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComerciosController : ControllerBase
    {
        [HttpPost("GetComercios")]
        public async Task<Cls_Response<List<ClsInfoSucursales>>> GetComercios(ClsFiltroBusquedaComercio filtros)
        {
            Cls_Response<List<ClsInfoSucursales>> response = new Cls_Response<List<ClsInfoSucursales>>();
            response.Body = new List<ClsInfoSucursales>();

            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();


                var Establecimientos = await (from e in context.TbEmpresa
                                              join es in context.TbEstablecimiento on e.IdEmpresa equals es.IdEmpresa
                                              join m in context.TbMunicipio on es.IdMunicipio equals m.IdMunicipio
                                              join d in context.TbDepartamento on m.IdDepartamento equals d.IdDepartamento
                                              let NombreCompleto = e.NombreEmpresa + (es.NombreComplementario != null ? " - " + es.NombreComplementario : "")
                                              where es.IdEstado == 1
                                              select new
                                              {
                                                  d.IdDepartamento,
                                                  m.IdMunicipio,
                                                  es.IdEstablecimiento,
                                                  NombreCompleto,
                                                  es.Direccion,
                                                  ubicacion = d.NombreDepartamento + " - " + m.NombreMunicipio
                                              }).ToListAsync();

              


                    Establecimientos = (from q in Establecimientos
                                        where q.IdDepartamento == filtros.IdDepto
                             select q).ToList();


                if (filtros.IdMuni > 0)
                {
                    Establecimientos = (from q in Establecimientos
                                        where q.IdMunicipio == filtros.IdMuni
                             select q).ToList();
                }

                if (filtros.Nombre != null)
                {
                    if (!filtros.Nombre.Trim().Equals(""))
                    {
                        Establecimientos = (from q in Establecimientos
                                            where q.NombreCompleto.Trim().ToLower().Contains(filtros.Nombre.Trim().ToLower())
                                 select q).ToList();
                    }
                }

                foreach (var item in Establecimientos)
                {
                    ClsInfoSucursales comercio = new ClsInfoSucursales
                    {
                        IdSucursal = item.IdEstablecimiento,
                        Nombre = item.NombreCompleto,
                        Direccion = item.Direccion,
                        Ubicacion = item.ubicacion
                    };

                    response.Body.Add(comercio);
                }
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al consultar comercios";
            }

            return response;
        }

        [HttpPost("GetEmpresas")]
        public async Task<Cls_Response<List<TbEmpresa>>> GetEmpresas(ClsFiltroBusquedaComercio filtros)
        {
            Cls_Response<List<TbEmpresa>> response = new Cls_Response<List<TbEmpresa>>();

            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                var query = await (from q in context.TbEmpresa
                                   where (filtros.IdEstado != null ? q.IdEstado == filtros.IdEstado : true)
                                   select q).ToListAsync();

                if(filtros.Nombre!= null)
                {
                    query = (from q in query
                             where q.NombreEmpresa.Trim().ToLower().Equals(filtros.Nombre.Trim().ToLower())
                             select q).ToList();
                }

                if (filtros.Nit != null)
                {
                    query = (from q in query
                             where q.Nit.Trim().ToLower().Equals(filtros.Nit.Trim().ToLower())
                             select q).ToList();
                }


                response.Body = query;
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al consultar empresas";
            }

            return response;
        } 

        [HttpPost("GuardarEmpresa")]
        public async Task<Cls_Response<string>> GuardarEmpresa(TbEmpresa emp)
        {
            Cls_Response<string> response = new Cls_Response<string>();
            try
            {

                DB_QUEJASContext context = new DB_QUEJASContext();

                if (emp.IdEmpresa > 0)
                {
                    context.Entry(emp).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    response.Body = "Empresa modificada correctamente";
                }
                else
                {
                    context.TbEmpresa.Add(emp);
                    await context.SaveChangesAsync();
                    response.Body = "Empresa agregada correctamente";
                }

            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al guardar empresa";
            }
            return response;
        }

        [HttpGet("GetEstablecimientosById/{IdEmpresa:int}")]
        public async Task<Cls_Response<List<ClsInfoSucursales>>> GetEstablecimientosById(int IdEmpresa)
        {
            Cls_Response<List<ClsInfoSucursales>> response = new Cls_Response<List<ClsInfoSucursales>>();
            response.Body = new List<ClsInfoSucursales>();
            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                var Establecimientos = await (from e in context.TbEmpresa
                                              join es in context.TbEstablecimiento on e.IdEmpresa equals es.IdEmpresa
                                              join m in context.TbMunicipio on es.IdMunicipio equals m.IdMunicipio
                                              join d in context.TbDepartamento on m.IdDepartamento equals d.IdDepartamento
                                              where es.IdEmpresa == IdEmpresa
                                              select new
                                              {
                                                  d.IdDepartamento,
                                                  m.IdMunicipio,
                                                  es.IdEstablecimiento,
                                                  NombreCompleto = es.NombreComplementario,
                                                  es.Direccion,
                                                  ubicacion = d.NombreDepartamento + " - " + m.NombreMunicipio,
                                                  es.IdEstado,
                                                  es.PatenteComercio
                                              }).ToListAsync();

                foreach (var item in Establecimientos)
                {
                    ClsInfoSucursales comercio = new ClsInfoSucursales
                    {
                        IdSucursal = item.IdEstablecimiento,
                        Nombre = item.NombreCompleto,
                        Direccion = item.Direccion,
                        Ubicacion = item.ubicacion,
                        IdDepartamento = item.IdDepartamento,
                        IdMunicipio = item.IdMunicipio,
                        IdEstado = item.IdEstado,
                        Patente = item.PatenteComercio
                    };

                    response.Body.Add(comercio);
                }

            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al consultar establecimientos asociados";
            }

            return response;
        }

        [HttpPost("GuardarEstablecimiento")]
        public async Task<Cls_Response<string>> GuardarEstablecimiento(TbEstablecimiento establecimiento)
        {
            Cls_Response<string> response = new Cls_Response<string>();

            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                if(establecimiento.IdEstablecimiento > 0)
                {
                    context.Entry(establecimiento).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    response.Body = "Establecimiento modificado correctamente";
                }
                else
                {
                    context.TbEstablecimiento.Add(establecimiento);
                    await context.SaveChangesAsync();
                    response.Body = "Establecimiento agregado correctamente";
                }
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al guardar establecimiento";
            }

            return response;
        }
    }
}
