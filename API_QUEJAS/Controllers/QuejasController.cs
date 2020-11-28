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
    public class QuejasController : ControllerBase
    {
        [HttpPost("GuardarQueja")]
        public async Task<Cls_Response<string>> GuardarQueja(TbQueja queja)
        {
            Cls_Response<string> response = new Cls_Response<string>();

            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                if (queja.IdQueja == 0)
                {
                    queja.FechaCreacion = DateTime.Now;
                    context.TbQueja.Add(queja);
                    await context.SaveChangesAsync();
                    response.Body = "Queja guardada correctamento. Queja No. " + queja.IdQueja + "Puede utilizar este numero para darle seguimiento a su queja";
                }
                else
                {
                    queja.FechaModificacion = DateTime.Now;
                    context.Entry(queja).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    response.Body = "Queja modificada correctamente.";
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = "Error al guardar queja. " + ex.Message;
            }

            return response;
        }

        [HttpGet("GetQueja/{IdQueja:int}")]
        public async Task<Cls_Response<DetalleQueja>> GetQueja(int IdQueja)
        {
            Cls_Response<DetalleQueja> response = new Cls_Response<DetalleQueja>();
            response.Body = new DetalleQueja();
            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();
                var query = await (from q in context.TbQueja
                                   join e in context.TbEstado on q.IdEstado equals e.IdEstado
                                   join es in context.TbEstablecimiento on q.IdEstablecimiento equals es.IdEstablecimiento
                                   join em in context.TbEmpresa on es.IdEmpresa equals em.IdEmpresa
                                   join m in context.TbMunicipio on es.IdMunicipio equals m.IdMunicipio
                                   join d in context.TbDepartamento on m.IdDepartamento equals d.IdDepartamento
                                   where q.IdQueja == IdQueja
                                   select new
                                   {
                                       e.NombreEstado,
                                       q.Descripcion,
                                       Ubicacion = d.NombreDepartamento + " - " + m.NombreMunicipio,
                                       nombre = em.NombreEmpresa + (es.NombreComplementario!=null?" - "+ es.NombreComplementario:""),
                                       es.Direccion,
                                       q.DescripcionResuelve
                                   }).FirstOrDefaultAsync();

                if(query!= null)
                {
                    response.Body.EstadoQueja = query.NombreEstado;
                    response.Body.Queja = query.Descripcion;
                    response.Body.Ubicacion = query.Ubicacion;
                    response.Body.Comercio = query.nombre;
                    response.Body.Direccion = query.Direccion;
                    response.Body.Resolucion = query.DescripcionResuelve;
                }

            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al consultar queja.";
            }

            return response;
        }

        [HttpPost("AtenderQuejas")]
        public async Task<Cls_Response<string>> AtenderQuejas(List<ClsInfoInformQuejas> list)
        {
            Cls_Response<string> response = new Cls_Response<string>();

            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int cont = 0;
                        foreach (var item in list)
                        {
                            if (item.check)
                            {
                                var queja = await (from q in context.TbQueja
                                                   where q.IdQueja == item.IdQueja
                                                   select q).FirstOrDefaultAsync();

                                queja.IdEstado = 4;//resuelta
                                queja.DescripcionResuelve = item.DescripcioResuelve;
                                queja.FechaModificacion = DateTime.Now;
                                await context.SaveChangesAsync();
                                cont++;
                            }
                        }

                       await  transaction.CommitAsync();
                        response.Body = cont.ToString();
                    }
                    catch (Exception ex)
                    {
                        await transaction.DisposeAsync();
                        response.Error = true;
                        response.Message = "Error al guardar resoluciones";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = "Error al guardar resoluciones";
            }

            return response;
        }
    }
}
