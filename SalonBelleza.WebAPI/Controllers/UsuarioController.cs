using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Agregue las siguientes referencias al controlador 
using SalonBelleza.WebAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using SalonBelleza.LogicaDeNegocio;
using SalonBelleza.EntidadesDeNegocio;
using System.Text.Json;//Libreria para seliarizar en el metodo Buscar

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalonBelleza.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //Agregar el siguiente metadato para autorizar JWT la Web API
    public class UsuarioController : ControllerBase
    {
        private UsuarioBL usuarioBL = new UsuarioBL();
        // Codigo para agregar la seguridad JWT
        private readonly IJwtAuthenticationService authService;
        /// <summary>  
        /// Metodo para generar el token
        /// </summary>  
        /// <param name="pAuthService">Se espera un objeto del Tipo IJwtAuthenticationService</param>
        public UsuarioController(IJwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        //************************************************

        /// <summary>  
        /// Metodo para Obtener un usuario haciendo peticion a la DAL
        /// </summary> 
        /// <returns>Objeto tipo usuario con sus campos llenos</returns>  
        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await usuarioBL.ObtenerTodosAsync();
        }

        /// <summary>  
        /// Metodo para Obtener por Id un usuario haciendo peticion a la DAL
        /// </summary>  
        /// <param name="id">Se espera un parametro del Tipo int el cual tenga el Id</param>  
        /// <returns>Objeto tipo usuario con sus campos llenos</returns>  
        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<Usuario> Get(int id)
        {
            Usuario usuario = new Usuario();
            usuario.Id = id;
            return await usuarioBL.ObtenerPorIdAsync(usuario);
        }

        /// <summary>  
        /// Metodo para Crear un usuario haciendo peticion a la DAL
        /// </summary>  
        /// <param name="usuario">Se espera un objeto del Tipo Usuario</param>  
        /// <returns>Devuelve un numero de estado correspondiendo a si funciono o no</returns>  
        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                await usuarioBL.CrearAsync(usuario);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        /// <summary>  
        /// Metodo para editar un usuario haciendo peticion a la DAL
        /// </summary>  
        /// <param name="id">Se espera un entero con el id del Usuario</param>
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario</param>  
        /// <returns>Devuelve un numero de estado correspondiendo a si funciono o no</returns>  
        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] object pUsuario)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
            if (usuario.Id == id)
            {
                await usuarioBL.ModificarAsync(usuario);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        /// <summary>  
        /// Metodo para eliminar un usuario haciendo peticion a la DAL
        /// </summary>  
        /// <param name="id">Se espera un entero con el id del Usuario</param>
        /// <returns>Devuelve un numero de estado correspondiendo a si funciono o no</returns> 
        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Id = id;
                await usuarioBL.EliminarAsync(usuario);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>  
        /// Metodo para buscar un usuario haciendo peticion a la DAL
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario</param>  
        /// <returns>Devuelve un listado de objetos tipo usuario con sus campos llenos</returns> 
        [HttpPost("Buscar")]
        public async Task<List<Usuario>> Buscar([FromBody] object pUsuario)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
            var usuarios = await usuarioBL.BuscarIncluirRolesAsync(usuario);
            usuarios.ForEach(s => s.Rol.Usuario = null); // Evitar la redundacia de datos
            return usuarios;

        }

        /// <summary>  
        /// Metodo para iniciar sesion haciendo peticion a la DAL
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario</param>  
        /// <returns>Devuelve un numero de estado correspondiendo a si funciono o no</returns> 
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] object pUsuario)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
            // codigo para autorizar el usuario por JWT
            Usuario usuario_auth = await usuarioBL.LoginAsync(usuario);
            if (usuario_auth != null && usuario_auth.Id > 0 && usuario.Login == usuario_auth.Login)
            {
                var token = authService.Authenticate(usuario_auth);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
            // *********************************************
        }

        /// <summary>  
        /// Metodo para cambiar contraseña haciendo peticion a la DAL
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario</param>  
        /// <returns>Devuelve un numero de estado correspondiendo a si funciono o no</returns> 
        [HttpPost("CambiarPassword")]
        public async Task<ActionResult> CambiarPassword([FromBody] Object pUsuario)
        {
            try
            {
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                string strUsuario = JsonSerializer.Serialize(pUsuario);
                Usuario usuario = JsonSerializer.Deserialize<Usuario>(strUsuario, option);
                await usuarioBL.CambiarPasswordAsync(usuario, usuario.ConfirmarPassword_aux);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
