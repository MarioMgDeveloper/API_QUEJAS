using API_QUEJAS.Helpers;
using API_QUEJAS.ModelosPersonalizados;
using API_QUEJAS.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InformesController : ControllerBase
    {
        [HttpPost("GetInformeQuejas")]
        public async Task<Cls_Response<List<ClsInfoInformQuejas>>> GetInformeQuejas(ClsFiltrosInformesQuejas filtro)
        {
            Cls_Response<List<ClsInfoInformQuejas>> response = new Cls_Response<List<ClsInfoInformQuejas>>();
            response.Body = new List<ClsInfoInformQuejas>();

            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                var query = await (from q in context.TbQueja
                                   join e in context.TbEstablecimiento on q.IdEstablecimiento equals e.IdEstablecimiento
                                   join emp in context.TbEmpresa on e.IdEmpresa equals emp.IdEmpresa
                                   join mun in context.TbMunicipio on e.IdMunicipio equals mun.IdMunicipio
                                   join dep in context.TbDepartamento on mun.IdDepartamento equals dep.IdDepartamento
                                   join reg in context.TbRegion on dep.IdRegion equals reg.IdRegion
                                   join est in context.TbEstado on q.IdEstado equals est.IdEstado
                                   select new
                                   {
                                       NombreEmpresa = emp.NombreEmpresa + (e.NombreComplementario != null ? " - " + e.NombreComplementario : ""),
                                       Ubicacion = dep.NombreDepartamento + " - " + mun.NombreMunicipio,
                                       reg.IdRegion,
                                       reg.NombrRegion,
                                       dep.IdDepartamento,
                                       mun.IdMunicipio,
                                       e.Direccion,
                                       q.IdEstado,
                                       est.NombreEstado,
                                       q.Descripcion,
                                       q.DescripcionResuelve,
                                       q.FechaCreacion,
                                       q.FechaModificacion,
                                       q.IdQueja
                                   }).ToListAsync();

                if (filtro.IdRegion > 0)
                {
                    query = (from q in query
                             where q.IdRegion == filtro.IdRegion
                             select q).ToList();
                }

                if(filtro.IdDepartamento > 0)
                {
                    query = (from q in query
                             where q.IdDepartamento == filtro.IdDepartamento
                             select q).ToList();
                }

                if(filtro.IdMunicipio > 0)
                {
                    query = (from q in query
                             where q.IdMunicipio == filtro.IdMunicipio
                             select q).ToList();
                }

                if (filtro.IdEstado > 0)
                {
                    query = (from q in query
                             where q.IdEstado == filtro.IdEstado
                             select q).ToList();
                }

                if (filtro.Nombrecomercio != null)
                {
                    if (!filtro.Nombrecomercio.Equals(""))
                    {
                        query = (from q in query
                                 where q.NombreEmpresa.Trim().ToLower().Contains(filtro.Nombrecomercio.Trim().ToLower())
                                 select q).ToList();
                    }
                }

                if(filtro.Del != null)
                {
                    query = (from q in query
                             where q.FechaCreacion.Year >= filtro.Del.Value.Year &&
                             q.FechaCreacion.Month >= filtro.Del.Value.Month &&
                              q.FechaCreacion.Day >= filtro.Del.Value.Day
                             select q).ToList();
                }

                if (filtro.Al != null)
                {
                    query = (from q in query
                             where q.FechaCreacion.Year <= filtro.Al.Value.Year &&
                             q.FechaCreacion.Month <= filtro.Al.Value.Month &&
                              q.FechaCreacion.Day <= filtro.Al.Value.Day
                             select q).ToList();
                }

                query = query.OrderBy(x => x.IdRegion).ThenBy(x => x.IdDepartamento).ThenBy(x => x.IdMunicipio).ToList();

                foreach (var item in query)
                {
                    ClsInfoInformQuejas r = new ClsInfoInformQuejas 
                    {
                         Empresa = item.NombreEmpresa,
                         Region = item.NombrRegion,
                         Ubicacion = item.Ubicacion,
                         Direccion = item.Direccion,
                         Estado = item.NombreEstado,
                         IdEstado = item.IdEstado,
                         Queja = item.Descripcion,
                         Resolucion = item.DescripcionResuelve,
                         FechaCreacion = item.FechaCreacion.ToShortDateString(),
                         FechaModificacion = item.FechaModificacion==null?null: item.FechaModificacion.Value.ToShortDateString(),
                         IdQueja = item.IdQueja
                    };

                    response.Body.Add(r);
                }

            }
            catch (Exception)
            {

            }

            return response;
        }

    }
}
