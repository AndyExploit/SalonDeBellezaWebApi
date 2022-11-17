using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//referencias.
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
    [Authorize]
    public class ServicioController : ControllerBase
    {
        private ServicioBL servicioBL = new ServicioBL();
        /// <summary>  
        /// Metodo Get para obtener todos los Servicios
        /// </summary>   
        /// <returns>Retorna una Lista de Servicios</returns>  
        ///
        // GET: api/<ServicioController>
        [HttpGet]
        public async Task<IEnumerable<Servicio>> Get() //Convertimos el metodo a Async, ya que en la logica de negocio el metodo es Async.
        {
            return await servicioBL.ObtenerTodosAsync(); //De esta forma nos va a retornas una lista de Servicios.
        }

        // GET api/<ServicioController>/5
        //Haciendo cambios dentro del metodo GET que espera un parametro Id
        /// <summary>  
        /// Metodo para Obtener una Servicio por ID al hacer GET
        /// </summary>  
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna el objeto Servicio con sus campos llenos</returns>  
        ///
        [HttpGet("{id}")]
        public async Task<Servicio> Get(int id)
        {
            Servicio servicio = new Servicio();
            servicio.Id = id; //Creamos un objeto de la clase servicio para indicarle que el Id, sera el mismo que el que se envia como parametro.
            return await servicioBL.ObtenerPorIdAsync(servicio);
        }

        // POST api/<ServicioController>
        //Haciendo cambios dentro del metodo POST para trabajar con metodo asyncronico
        /// <summary>  
        /// Metodo para crear una Servicio al hacer Post
        /// </summary>  
        /// <param name="servicio">Se espera un objeto del Tipo Servicio el cual contenga todos sus datos menos el ID</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Servicio servicio)
        {
            try
            {
                await servicioBL.CrearAsync(servicio);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        // PUT api/<ServicioController>/5
        /// <summary>  
        /// Metodo para Modificar un Servicio haiciendo PUT
        /// </summary>  
        /// <param name="servicio">Se espera un objeto del Tipo Servicio con todos sus datos incluyendo ID</param> 
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Servicio servicio)
        {
            if (servicio.Id == id)
            {
                await servicioBL.ModificarAsync(servicio);
                return Ok();
            }
            else {
                return BadRequest();
            }


        }

        // DELETE api/<ServicioController>/5
        /// <summary>  
        /// Metodo para Eliminar un Servicio haiciendo DELETE 
        /// </summary>   
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Servicio servicio = new Servicio();
                servicio.Id = id;
                await servicioBL.EliminarAsync(servicio);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        /// <summary>  
        /// Metodo para Buscar un Servicio segun los parametros enviados
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio</param> 
        /// <returns>Retorna un objeto Servicio si encuentra coincidencias</returns>  
        ///
        [HttpPost("Buscar")]
        public async Task<List<Servicio>> Buscar([FromBody] object pServicio)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strServicio = JsonSerializer.Serialize(pServicio);
            Servicio servicio = JsonSerializer.Deserialize<Servicio>(strServicio, option);
            return await servicioBL.BuscarAsync(servicio);
        }

        
    }
}
