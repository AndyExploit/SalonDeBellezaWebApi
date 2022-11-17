using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//*-----------------------------
using SalonBelleza.EntidadesDeNegocio;
using SalonBelleza.AccesoADatos;

namespace SalonBelleza.LogicaDeNegocio
{
    /// <summary>  
    /// Esta clase es de la entidad Servicio de la capa Logica de De Negocio
    /// Esta clase contiene Los metodos CRUD de Servicio
    /// 
    /// </summary> 
    public class ServicioBL
    {

        /// <summary>  
        /// Metodo para crear un Nuevo Servicio.
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<int> CrearAsync(Servicio pServicio)
        {
            return await ServicioDAL.CrearAsync(pServicio);
        }


        /// <summary>  
        /// Metodo para Modificar un Servicio si encuentra coincidencia en la base de datos.
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<int> ModificarAsync(Servicio pServicio)
        {
            return await ServicioDAL.ModificarAsync(pServicio);
        }

        /// <summary>  
        /// Metodo para Eliminar un servicio si encuentra un Id como coincidencia.
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<int> EliminarAsync(Servicio pServicio)
        {
            return await ServicioDAL.EliminarAsync(pServicio);
        }

        /// <summary>  
        /// Metodo para Obtener un Servicio Por ID
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Cliente, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<Servicio> ObtenerPorIdAsync(Servicio pServicio)
        {
            return await ServicioDAL.ObtenerPorIdAsync(pServicio);
        }

        /// <summary>  
        /// Metodo para Obtener Todos los Servicios
        /// </summary>   
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<List<Servicio>> ObtenerTodosAsync()
        {
            return await ServicioDAL.ObtenerTodosAsync();
        }

        /// <summary>  
        /// Metodo para Buscar un Cliente Async
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<List<Servicio>> BuscarAsync(Servicio pServicio)
        {
            return await ServicioDAL.BuscarAsync(pServicio);
        }



    }
}
