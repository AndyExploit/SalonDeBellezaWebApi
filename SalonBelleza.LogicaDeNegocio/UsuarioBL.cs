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
    /// Esta clase es de la entidad Usuario de la capa Logica de De Negocio
    /// Esta clase contiene Los metodos CRUD de Usuario
    /// 
    /// </summary> 
    public class UsuarioBL
    {
        #region CRUD

        /// <summary>  
        /// Metodo para guardar de forma Asincronica. para que un metodo sea Asincronico debe llevar la palabra Async
        /// y usar al menos un metodo asincronico en el.
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<int> CrearAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.CrearAsync(pUsuario);
        }

        /// <summary>  
        /// Con este metodo Actualisaremos en la base de datos el Usuario.
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<int> ModificarAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.ModificarAsync(pUsuario);
        }

        /// <summary>  
        /// Metodo para eliminar para Eliminar un Usuario en la Base de Datos.
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<int> EliminarAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.EliminarAsync(pUsuario);
        }

        /// <summary>  
        /// Metodo para Obtener por Id un usuario en la base de Datos
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<Usuario> ObtenerPorIdAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.ObtenerPorIdAsync(pUsuario);
        }

        /// <summary>  
        /// Metodo para obtener todos los Usuarios en la base de Datos.'
        /// </summary>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            return await UsuarioDAL.ObtenerTodosAsync();
        }

        /// <summary>  
        /// Metodo para Obtener por Id un usuario en la base de Datos
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<List<Usuario>> BuscarAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.BuscarAsync(pUsuario);
        }
        #endregion          

        /// <summary>  
        /// Metodo para Iniciar el login y validar si esas credenciales existen en el sistema.
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<Usuario> LoginAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.LoginAsync(pUsuario);
        }

        /// <summary>  
        /// Metodo para cambiar el Password, Comparando si esta correcto el password actual.
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// /// <param name="pPasswordAnt">Se espera una variable la cual contenga la PasswordAnterior</param>
        /// <returns>Retorna una tarea Asyncrona</returns>  
        /// 
        public async Task<int> CambiarPasswordAsync(Usuario pUsuario, string pPasswordAnt)
        {
            return await UsuarioDAL.CambiarPasswordAsync(pUsuario, pPasswordAnt);
        }

        /// <summary>  
        /// Metodo para Buscar con Roles Incluidos
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<List<Usuario>> BuscarIncluirRolesAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.BuscarIncluirRolesAsync(pUsuario);
        }
    }
}
