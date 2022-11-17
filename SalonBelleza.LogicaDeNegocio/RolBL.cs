using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalonBelleza.EntidadesDeNegocio;
using SalonBelleza.AccesoADatos;

namespace SalonBelleza.LogicaDeNegocio
{
    /// <summary>  
    /// Esta clase es de la entidad Rol de la capa Logica de De Negocio
    /// Esta clase contiene Los metodos CRUD de Rol
    /// 
    /// </summary> 
    public class RolBL
    {

        /// <summary>  
        /// Metodo para guardar de forma Asincronica. para que un metodo sea Asincronico debe llevar la palabra Async
        /// y usar al menos un metodo asincronico en el.
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<int> CrearAsync(Rol pRol) 
        {
            return await RolDAL.CrearAsync(pRol);
        }

        /// <summary>  
        /// Con este metodo Actualisaremos en la base de datos el Rol.
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<int> ModificarAsync(Rol pRol) 
        {
            return await RolDAL.ModificarAsync(pRol);
        }

        /// <summary>  
        /// Metodo para eliminar para Eliminar un Rol en la Base de Datos.
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<int> EliminarAsync(Rol pRol) 
        {
            return await RolDAL.EliminarAsync(pRol);
        }

        /// <summary>  
        /// Metodo para Obtener por Id un Rol en la base de Datos
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<Rol> ObtenerPorIdAsync(Rol pRol) 
        {
            return await RolDAL.ObtenerPorIdAsync(pRol);
        }

        /// <summary>  
        /// Metodo para obtener todos los Roles en la base de Datos.'
        /// </summary>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<List<Rol>> ObtenerTodosAsync() 
        {
            return await RolDAL.ObtenerTodosAsync();
        }

        /// <summary>  
        /// Metodo para Obtener por Id un Rol en la base de Datos
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<List<Rol>> BuscarAsync(Rol pRol) 
        {
            return await RolDAL.BuscarAsync(pRol);
        }
    }
}
