using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//referencias de ensamblado
using SalonBelleza.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace SalonBelleza.AccesoADatos
{
    public class ServicioDAL
    {


        #region CRUD
        /// <summary>  
        /// Metodo para crear un Nuevo Servicio.
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio, con sus valores llenos</param>  
        /// <returns>Devuelve un entero para conocer la respuesta del Metodo</returns>  
        /// 
        public static async Task<int> CrearAsync(Servicio pServicio)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
              
                dbContexto.Add(pServicio);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        /// <summary>  
        /// Metodo para Modificar un Servicio si encuentra coincidencia en la base de datos.
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio, con sus valores llenos</param>  
        /// <returns>Devuelve un entero para conocer la respuesta del Metodo</returns>  
        /// 
        public static async Task<int> ModificarAsync(Servicio pServicio)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var servicio = await dbContexto.Servicio.FirstOrDefaultAsync(s => s.Id == pServicio.Id);
                servicio.Nombre = pServicio.Nombre;
                servicio.Descripcion = pServicio.Descripcion;
                servicio.Precio = pServicio.Precio;
                servicio.Duracion = pServicio.Duracion;

                dbContexto.Update(servicio);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        /// <summary>  
        /// Metodo para Eliminar un servicio si encuentra un Id como coincidencia.
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio, con sus valores llenos</param>  
        /// <returns>Devuelve un entero para conocer la respuesta del Metodo</returns>  
        /// 
        public static async Task<int> EliminarAsync(Servicio pServicio)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var servicio = await dbContexto.Servicio.FirstOrDefaultAsync(s => s.Id == pServicio.Id);
                dbContexto.Servicio.Remove(servicio);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        /// <summary>  
        /// Metodo para Obtener un Servicio Por ID
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Cliente, con sus valores llenos</param>  
        /// <returns>Devuelve un entero para conocer la respuesta del Metodo</returns>  
        /// 
        public static async Task<Servicio> ObtenerPorIdAsync(Servicio pServicio)
        {
            var servicio = new Servicio();
            using (var dbContexto = new DBContexto())
            {
                servicio = await dbContexto.Servicio.FirstOrDefaultAsync(s => s.Id == pServicio.Id);
            }
            return servicio;
        }

        /// <summary>  
        /// Metodo para Obtener Todos los Servicios
        /// </summary>   
        /// <returns>Devuelve un entero para conocer la respuesta del Metodo</returns>  
        /// 
        public static async Task<List<Servicio>> ObtenerTodosAsync()
        {
            var servicio = new List<Servicio>();
            using (var dbContexto = new DBContexto())
            {
                servicio = await dbContexto.Servicio.ToListAsync();
            }
            return servicio;
        }

        /// <summary>  
        /// filtros personalizados usando un Iqueryable con expresiones lambday linQ(en proceso)
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Cliente, con sus valores llenos</param>  
        /// /// <param name="pQuery">Se espera una IQueryable de Cliente</param>
        /// <returns>Devuelve un pQuery con las coincidencias encontradas en la base de Datos</returns>  
        /// 
        
        internal static IQueryable<Servicio> QuerySelect(IQueryable<Servicio> pQuery, Servicio pServicio) //los internal solo funcionan en su respectivo namespace 
        {
            if (pServicio.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pServicio.Id);
            if (!string.IsNullOrWhiteSpace(pServicio.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pServicio.Nombre));
            if (!string.IsNullOrWhiteSpace(pServicio.Descripcion))
                pQuery = pQuery.Where(s => s.Descripcion.Contains(pServicio.Descripcion));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pServicio.Top_Aux > 0)
                pQuery = pQuery.Take(pServicio.Top_Aux).AsQueryable();

         return pQuery;
        }

        /// <summary>  
        /// Metodo para Buscar un Cliente Async
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una Lista de Clientes con las coincidencias encontradas</returns>  
        /// 
        public static async Task<List<Servicio>> BuscarAsync(Servicio pServicio)
        {
            var servicio = new List<Servicio>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.Servicio.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pServicio);
                servicio = await select.ToListAsync();
            }
            return servicio;
        }
        #endregion




    }
}
