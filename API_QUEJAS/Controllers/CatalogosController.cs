using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_QUEJAS.Helpers;
using API_QUEJAS.ModelosPersonalizados;
using API_QUEJAS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_QUEJAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {

        [HttpGet("GetRegiones")]
        public async Task<Cls_Response<List<TbRegion>>> GetRegiones()
        {
            Cls_Response<List<TbRegion>> response = new Cls_Response<List<TbRegion>>();
            response = new Cls_Response<List<TbRegion>>();

            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();
                response.Body = await (from r in context.TbRegion
                                       select r).OrderBy(x => x.IdRegion).ToListAsync();
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al obtener regiones";
            }

            return response;
        }

        [HttpGet("GetDepartamentosPorRegion/{IdRegion:int}")]
        public async Task<Cls_Response<List<TbDepartamento>>> GetDepartamentos(int IdRegion)
        {
            Cls_Response<List<TbDepartamento>> response = new Cls_Response<List<TbDepartamento>>();
            response.Body = new List<TbDepartamento>();
            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                response.Body = await (from dep in context.TbDepartamento
                                       where (IdRegion>0?dep.IdRegion == IdRegion:true)
                                       select dep).OrderBy(x => x.CodigoDepartamento).ToListAsync();
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al consultar departamentos";
            }

            return response;
        }

        [HttpGet("GetDepartamentos")]
        public async Task<Cls_Response<List<TbDepartamento>>> GetDepartamentos()
        {
            Cls_Response<List<TbDepartamento>> response = new Cls_Response<List<TbDepartamento>>();
            response.Body = new List<TbDepartamento>();
            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                response.Body = await (from dep in context.TbDepartamento
                                       select dep).OrderBy(x => x.CodigoDepartamento).ToListAsync();
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al consultar departamentos";
            }

            return response;
        }

        [HttpGet("GetMunicipios/{IdDepartamento:int}")]
        public async Task<Cls_Response<List<TbMunicipio>>> GetMunicipios(int IdDepartamento)
        {
            Cls_Response<List<TbMunicipio>> response = new Cls_Response<List<TbMunicipio>>();
            response.Body = new List<TbMunicipio>();

            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                response.Body = await (from m in context.TbMunicipio
                                       where m.IdDepartamento == IdDepartamento
                                       select m).OrderBy(x => x.CodigoMunicipio).ToListAsync();

            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al cosnultar municpios";
            }
            return response;
        }

        [HttpGet("GetListTipoQueja")]
        public async Task<Cls_Response<List<TbCategoriaQueja>>> GetListTipoQueja()
        {
            Cls_Response<List<TbCategoriaQueja>> response = new Cls_Response<List<TbCategoriaQueja>>();
            response.Body = new List<TbCategoriaQueja>();

            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                response.Body = await (from c in context.TbCategoriaQueja
                                       select c).ToListAsync();
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al obtener categorias de queja";
            }

            return response;
        }
        [HttpPost("GetEstados")]
        public async Task<Cls_Response<List<TbEstado>>> GetEstados(List<int> estados)
        {
            Cls_Response<List<TbEstado>> response = new Cls_Response<List<TbEstado>>();
            response.Body = new List<TbEstado>();
            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                response.Body = await (from q in context.TbEstado
                                   where estados.Contains(q.IdEstado)
                                   select q).ToListAsync();


            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al buscar estados";
            }

            return response;
        }

    }
}
