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
    public class ClienteController : ControllerBase
    {
        //Creando objeto de tipo privado e instanciandolo
        private ClienteBL clienteBL = new ClienteBL();
        // GET: api/<ClienteController>
        [HttpGet]
        /// <summary>  
        /// Metodo Get para obtener todos los Clientes
        /// </summary>   
        /// <returns>Retorna una Lista de Clientes</returns>  
        ///
        //Haciendo cambios dentro del metodo GET para trabajar con metodo asyncronico
        public async Task<IEnumerable<Cliente>> Get()//Convirtimos el metodo en asincronico,procesamos informacion por medio de task y definimos respuesta de tipo usuario
        {
            return await clienteBL.ObtenerTodosAsync();
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        //Haciendo cambios dentro del metodo GET que espera un parametro Id
        /// <summary>  
        /// Metodo para Obtener un Cliente por ID al hacer GET
        /// </summary>  
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna el objeto Cliente con sus campos llenos</returns>  
        ///
        public async Task<Cliente> Get(int id)
        {
            Cliente cliente = new Cliente();
            cliente.Id = id;
            return await clienteBL.ObtenerPorIdAsync(cliente);
        }

        // POST api/<ClienteController>
        [HttpPost]
        //Haciendo cambios dentro del metodo POST para trabajar con metodo asyncronico
        /// <summary>  
        /// Metodo para crear una Cliente al hacer Post
        /// </summary>  
        /// <param name="cliente">Se espera un objeto del Tipo Cliente el cual contenga todos sus datos menos el ID</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        public async Task<ActionResult> Post([FromBody] Cliente cliente)
        {
            try
            {
                await clienteBL.CrearAsync(cliente);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        //Haciendo cambios dentro del metodo PUT que espera un parametro Id
        /// <summary>  
        /// Metodo para Modificar un Cliente haiciendo PUT
        /// </summary>  
        /// <param name="cliente">Se espera un objeto del Tipo Cliente con todos sus datos incluyendo ID</param> 
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        public async Task<ActionResult> Put(int id, [FromBody] Cliente cliente)
        {
            if (cliente.Id == id)
            {
                await clienteBL.ModificarAsync(cliente);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        //Haciendo cambios dentro del metodo DELETE que espera un parametro Id
        /// <summary>  
        /// Metodo para Eliminar un Cliente haiciendo DELETE 
        /// </summary>   
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la respuesta de la Peticion</returns>  
        ///
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Id = id;
                await clienteBL.EliminarAsync(cliente);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
        //Agregando metodo logico que nos permitira Buscar
        [HttpPost("Buscar")]
        /// <summary>  
        /// Metodo para Buscar un Cliente segun los parametros enviados
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente</param> 
        /// <returns>Retorna un objeto Cliente si encuentra coincidencias</returns>  
        ///
        public async Task<List<Cliente>> Buscar([FromBody] object pCliente)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strCliente = JsonSerializer.Serialize(pCliente);
            Cliente cliente = JsonSerializer.Deserialize<Cliente>(strCliente, option);
            return await clienteBL.BuscarAsync(cliente);
        }
    }
}
