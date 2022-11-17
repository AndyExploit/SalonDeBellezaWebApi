using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalonBelleza.EntidadesDeNegocio;
using SalonBelleza.AccesoADatos;

namespace SalonBelleza.LogicaDeNegocio
{
    //Soy un cliente.
    //Soy el segundo cliente

    /// <summary>  
    /// Esta clase es de la entidad Cliente de la capa Logica de De Negocio
    /// Esta clase contiene Los metodos CRUD de Cliente
    /// 
    /// </summary> 
    public class ClienteBL
    {
        /// <summary>  
        /// Metodo para crear un Nuevo cliente.
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<int> CrearAsync(Cliente pCliente) 
        {
            return await ClienteDAL.CrearAsync(pCliente);
        }

        /// <summary>  
        /// Metodo para Modificar un Cliente segun los parametros agregados.
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del tipo Cliente con todas sus propiedades llenas</param>
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<int> ModificarAsync(Cliente pCliente) 
        {
            return await ClienteDAL.ModificarAsync(pCliente);
        }

        /// <summary>  
        /// Metodo para Eliminar un Cliente segun el Id.
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<int> EliminarAsync(Cliente pCliente) 
        {
            return await ClienteDAL.EliminarAsync(pCliente);
        }

        /// <summary>  
        /// Metodo para obtener por ID un cliente ingresado
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<Cliente> ObtenerPorIdAsync(Cliente pCliente) 
        {
            return await ClienteDAL.ObtenerPorIdAsync(pCliente);
        }

        /// <summary>  
        /// Metodo para Obtener todos los Cliente.
        /// </summary>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<List<Cliente>> ObtenerTodosAsync() 
        {
            return await ClienteDAL.ObtenerTodosAsync();
        }

        /// <summary>  
        /// Metodo para Buscar un Cliente Async
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<List<Cliente>> BuscarAsync(Cliente pCliente1) 
        {
            return await ClienteDAL.BuscarAsync(pCliente1);
        }
    }
}
