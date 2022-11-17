using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//******************************
using Microsoft.EntityFrameworkCore;
using SalonBelleza.EntidadesDeNegocio;
namespace SalonBelleza.AccesoADatos
{
    public class RolDAL
    {

        /// <summary>  
        /// Metodo para guardar de forma Asincronica. para que un metodo sea Asincronico debe llevar la palabra Async
        /// y usar al menos un metodo asincronico en el.
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<int> CrearAsync(Rol pRol) 
        {
            int result = 0;
            using (var dbContexto = new DBContexto()) 
            {
                dbContexto.Add(pRol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        /// <summary>  
        /// Con este metodo Actualisaremos en la base de datos el Rol.
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<int> ModificarAsync(Rol pRol) 
        {
            int result = 0;
            using (var dbContexto = new DBContexto()) 
            {
                var rol = await dbContexto.Rol.FirstOrDefaultAsync(s => s.Id == pRol.Id);
                rol.Nombre = pRol.Nombre;
                dbContexto.Update(rol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        /// <summary>  
        /// Metodo para eliminar para Eliminar un Rol en la Base de Datos.
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<int> EliminarAsync(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new DBContexto())
            {
                var rol = await bdContexto.Rol.FirstOrDefaultAsync(s => s.Id == pRol.Id);
                bdContexto.Rol.Remove(rol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        /// <summary>  
        /// Metodo para Obtener por Id un Rol en la base de Datos
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<Rol> ObtenerPorIdAsync(Rol pRol) 
        {
            var rol = new Rol();
            using (var bdContexto = new DBContexto()) 
            {
                rol = await bdContexto.Rol.FirstOrDefaultAsync(s => s.Id == pRol.Id);
            }
            return rol;
        }

        /// <summary>  
        /// Metodo para obtener todos los Roles en la base de Datos.'
        /// </summary>  
        /// <returns>Retorna una Lista de Roles</returns>  
        ///
        public static async Task<List<Rol>> ObtenerTodosAsync() 
        {
            var roles = new List<Rol>();
            using (var bdContexto = new DBContexto()) 
            {
                roles = await bdContexto.Rol.ToListAsync();
            }
            return roles;
        }

        /// <summary>  
        /// Metodo QuerySelect sirve para hacer filtros personalisados utilizando Entity, LinQ expresiones lanba.
        ///Iternal es para indicar que este Metodo se usara solo dentro del mismo NameSpace.
        /// </summary>  
        /// <param name="pRol1">Se espera un objeto del Tipo Rol, con sus valores llenos</param>  
        /// <returns>Retorna las coincidencias encontradas en la base de Datos</returns>  
        ///
        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol1)
        {
            if (pRol1.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pRol1.Id);
            if (!string.IsNullOrWhiteSpace(pRol1.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pRol1.Nombre));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pRol1.Top_Aux > 0)
                pQuery = pQuery.Take(pRol1.Top_Aux).AsQueryable();
            return pQuery;
        }

        /// <summary>  
        /// Metodo para Obtener por Id un Rol en la base de Datos
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<List<Rol>> BuscarAsync (Rol pRol) 
        {
            var roles = new List<Rol>();
            using (var bdContecto = new DBContexto()) 
            {
                var select = bdContecto.Rol.AsQueryable();
                select = QuerySelect(select, pRol);
                roles = await select.ToListAsync();
            }
            return roles;
        }
        
    }   
}
