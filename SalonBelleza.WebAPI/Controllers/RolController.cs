using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalonBelleza.LogicaDeNegocio;
using SalonBelleza.EntidadesDeNegocio;
using System.Text.Json;

//Agregar la siguiente libreria para la seguridad JWT 
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalonBelleza.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //Agregar el siguiente metadato para autorizar JWT la Web API
    public class RolController : ControllerBase
    {
        private RolBL rolBL = new RolBL();


        /// <summary>  
        /// Metodo Get para obtener todos los Roles
        /// </summary>   
        /// <returns>Retorna una Lista de Roles</returns>  
        ///
        // GET: api/<RolController>
        [HttpGet]
        public async Task <IEnumerable<Rol>> Get()
        {
            return await rolBL.ObtenerTodosAsync();
        }

        /// <summary>  
        /// Metodo para Obtener un Rol por ID al hacer GET
        /// </summary>  
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna el objeto rol con sus campos llenos</returns>  
        ///
        // GET api/<RolController>/5
        [HttpGet("{id}")]
        public async Task<Rol> Get(int id)
        {
            Rol rol = new Rol();
            rol.Id = id;
            return await rolBL.ObtenerPorIdAsync(rol);      
        }

        /// <summary>  
        /// Metodo para crear un Rol al hacer Post
        /// </summary>  
        /// <param name="rol">Se espera un objeto del Tipo rol el cual contenga todos sus datos menos el ID</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        // POST api/<RolController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Rol rol)
        {
            try
            {
                await rolBL.CrearAsync(rol);
                return Ok();
            }

            catch (Exception)
            {

                return BadRequest();
            }

        }

        /// <summary>  
        /// Metodo para Modificar un Rol haiciendo PUT
        /// </summary>  
        /// <param name="rol">Se espera un objeto del Tipo rol con todos sus datos incluyendo ID</param> 
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        // PUT api/<RolController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Rol rol )
        {
            if (rol.Id == id)
            {
                await rolBL.ModificarAsync(rol);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        /// <summary>  
        /// Metodo para Eliminar un Rol haiciendo DELETE 
        /// </summary>   
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        // DELETE api/<RolController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            try
            {

                Rol rol = new Rol();
                rol.Id = id;
                await rolBL.EliminarAsync(rol);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>  
        /// Metodo para Buscar un Rol segun los parametros enviados
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol</param> 
        /// <returns>Retorna un objeto rol si encuentra coincidencias</returns>  
        ///
        [HttpPost("Buscar")]
        public async Task<List<Rol>> Buscar([FromBody] Object pRol)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strRol = JsonSerializer.Serialize(pRol);
            Rol rol = JsonSerializer.Deserialize<Rol>(strRol, option);
            return await rolBL.BuscarAsync(rol);
        }
    }
}
