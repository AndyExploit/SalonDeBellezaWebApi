using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Agregue las siguientes referencias al controlador 
using SalonBelleza.LogicaDeNegocio;
using SalonBelleza.EntidadesDeNegocio;
using System.Text.Json;//Libreria para seliarizar en el metodo Buscar
//Agregar la siguiente libreria para la seguridad JWT 
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalonBelleza.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitaController : ControllerBase
    {
        //Creando objeto de tipo privado e instanciandolo
        private CitaBL citaBL = new CitaBL();
        // GET: api/<CitaController>
        [HttpGet]
        /// <summary>  
        /// Metodo Get para obtener todas las Citas
        /// </summary>   
        /// <returns>Retorna una Lista de Citas</returns>  
        ///
        //Haciendo cambios dentro del metodo GET para trabajar con metodo asyncronico
        public async Task<IEnumerable<Cita>> Get()//Convirtimos el metodo en asincronico,procesamos informacion por medio de task y definimos respuesta de tipo usuario
        {
            return await citaBL.ObtenerTodosAsync();
        }


        // GET api/<CitaController>/5
        [HttpGet("{id}")]
        //Haciendo cambios dentro del metodo GET que espera un parametro Id
        /// <summary>  
        /// Metodo para Obtener una Cita por ID al hacer GET
        /// </summary>  
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna el objeto Cita con sus campos llenos</returns>  
        ///
        public async Task<Cita> Get(int id)
        {
            Cita cita = new Cita();
            cita.Id = id;
            return await citaBL.ObtenerPorIdAsync(cita);
        }

        // POST api/<CitaController>
        [HttpPost]
        //Haciendo cambios dentro del metodo POST para trabajar con metodo asyncronico
        /// <summary>  
        /// Metodo para crear una Cita al hacer Post
        /// </summary>  
        /// <param name="cita">Se espera un objeto del Tipo Cita el cual contenga todos sus datos menos el ID</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        public async Task<ActionResult> Post([FromBody] Cita cita)
        {
            try
            {
                await citaBL.CrearAsync(cita);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // PUT api/<CitaController>/5
        /// <summary>  
        /// Metodo para Modificar un Cita haiciendo PUT
        /// </summary>  
        /// <param name="cita">Se espera un objeto del Tipo cita con todos sus datos incluyendo ID</param> 
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        [HttpPut("{id}")]
        //Haciendo cambios dentro del metodo PUT que espera un parametro Id
        public async Task<ActionResult> Put(int id, [FromBody] Cita cita)
        {
            if ( cita.Id == id)
            {
                await citaBL.ModificarAsync(cita);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE api/<CitaController>/5
        [HttpDelete("{id}")]
        //Haciendo cambios dentro del metodo DELETE que espera un parametro Id
        /// <summary>  
        /// Metodo para Eliminar una Cita haiciendo DELETE 
        /// </summary>   
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Cita cita = new Cita();
                cita.Id = id;
                await citaBL.EliminarAsync(cita);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
        //Agregando metodo logico que nos permitira Buscar
        /// <summary>  
        /// Metodo para Buscar una Cita segun los parametros enviados
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita</param> 
        /// <returns>Retorna un objeto rol si encuentra coincidencias</returns>  
        ///
        [HttpPost("Buscar")]
        public async Task<List<Cita>> Buscar([FromBody] object pCita)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strCita = JsonSerializer.Serialize(pCita);
            Cita cita = JsonSerializer.Deserialize<Cita>(strCita, option);
            return await citaBL.BuscarAsync(cita);
        }
    }
}
