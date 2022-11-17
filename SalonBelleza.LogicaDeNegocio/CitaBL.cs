using SalonBelleza.AccesoADatos;
using SalonBelleza.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonBelleza.LogicaDeNegocio
{
    /// <summary>  
    /// Esta clase es de la entidad Cita de la capa Logica de De Negocio
    /// Esta clase contiene Los metodos CRUD de cita
    /// 
    /// </summary> 
    public class CitaBL
    {
        #region CRUD

        /// <summary>  
        /// Metodo para guardar de forma Asincronica. para que un metodo sea Asincronico debe llevar la palabra Async
        /// y usar al menos un metodo asincronico en el.
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<int> CrearAsync(Cita pCita)
        {
            return await CitaDAL.CrearAsync(pCita);
        }

        /// <summary>  
        /// Con este metodo Actualisaremos en la base de datos el Cita.
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///

        public async Task<int> ModificarAsync(Cita pCita)
        {
            return await CitaDAL.ModificarAsync(pCita);
        }

        /// <summary>  
        /// Metodo para eliminar para Eliminar un Cita en la Base de Datos.
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///

        public async Task<int> EliminarAsync(Cita pCita)
        {
            return await CitaDAL.EliminarAsync(pCita);
        }

        /// <summary>  
        /// Metodo para Obtener por Id un Cita en la base de Datos
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<Cita> ObtenerPorIdAsync(Cita pCita)
        {
            return await CitaDAL.ObtenerPorIdAsync(pCita);
        }

        /// <summary>  
        /// Metodo para obtener todos los Cita en la base de Datos.'
        /// </summary>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<List<Cita>> ObtenerTodosAsync()
        {
            return await CitaDAL.ObtenerTodosAsync();
        }

        /// <summary>  
        /// Metodo para Obtener por Id un Cita en la base de Datos
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<List<Cita>> BuscarAsync(Cita pCita)
        {
            return await CitaDAL.BuscarAsync(pCita);
        }

        #endregion

        /// <summary>  
        /// Metodo para Buscar con Citas Incluidos con Usuarios
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<List<Cita>> BuscarIncluirUsuarioAsync(Cita pCita)
        {
            return await CitaDAL.BuscarIncluirUsuarioAsync(pCita);
        }

        /// <summary>  
        /// Metodo para Buscar con Citas Incluidos con Clientes
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<List<Cita>> BuscarIncluirClienteAsync(Cita pCita) 
        {
            return await CitaDAL.BuscarIncluirClienteAsync(pCita);
        }

        /// <summary>  
        /// Metodo para Buscar con Citas Incluidos con Usuarios y Clientes
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<List<Cita>> BuscarIncluirUsuarioClienteAsync(Cita pCita) 
        {
            return await CitaDAL.BuscarIncluirUsuarioClienteAsync(pCita);
        }
    }
}
