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
using Microsoft.Extensions.Configuration;

namespace API_QUEJAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmonUsuariosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdmonUsuariosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetPersonas")]
        public async Task<Cls_Response<List<TbPersona>>> GetPersonas()
        {
            Cls_Response<List<TbPersona>> response = new Cls_Response<List<TbPersona>>();
            response.Body = new List<TbPersona>();

            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();

                response.Body = await (from p in context.TbPersona
                                   select p).ToListAsync();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = "Error al consultar personas";
            }

            return response;
        }

        [HttpPost("PostPersona")]
        public async Task<Cls_Response<TbPersona>> PostPersona(TbPersona persona)
        {
            Cls_Response<TbPersona> response = new Cls_Response<TbPersona>();
            response.Body = new TbPersona();

            try
            {
                DB_QUEJASContext context = new DB_QUEJASContext();
                context.TbPersona.Add(persona);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.Error = true;
                response.Message = "Error al crear persona.";
            }

            return response;
        }

        [HttpPost("Login")]
        public async Task<Cls_Response<ClsInfoUsuario>> Login(ClsLogin login)
        {
            Cls_Response<ClsInfoUsuario> response = new Cls_Response<ClsInfoUsuario>();
            response.Body = new ClsInfoUsuario();
            try
            {
               
                DB_QUEJASContext context = new DB_QUEJASContext();

                var query = await (from u in context.TbUsuario
                                   join r in context.TbRol on u.IdRol equals r.IdRol
                                   join p in context.TbPersona on u.IdPersona equals p.IdPersona
                                   let Nombres = p.PrimerNombre + " " + p.SegundoNombre + " "
                                   let Apellidos = p.PrimerApellido + " " + p.SegundoApellido + (p.ApellidoCasada != null ? " " + p.ApellidoCasada : "")
                                   where u.Correo.Trim().ToLower().Equals(login.Correo.Trim().ToLower())
                                   select new
                                   {u.Password, u.IdUsuario, u.Correo, r.IdRol, r.NombreRol, Nombre = Nombres + Apellidos }
                                   ).FirstOrDefaultAsync();

                if (query != null)
                {
                    if (query.Password.Trim().Equals(login.Password.Trim()))
                    {
                        Cls_HerramientasSeguridad seguridad = new Cls_HerramientasSeguridad(_configuration);
                        response.Body.JWT = seguridad.GetJWT(login);
                        response.Body.Correo = query.Correo;
                        response.Body.IdRol = query.IdRol;
                        response.Body.IdUsuario = query.IdUsuario;
                        response.Body.Nombre = query.Nombre;
                        response.Body.NombreRol = query.NombreRol;
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Contraseña incorrecta";
                    }
                }
                else
                {
                    response.Error = true;
                    response.Message = "El usuario no existe.";
                }

            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Error al iniciar sesion";
            }

            return response;
        }
    }
}
