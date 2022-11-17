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
    public class DetalleCitaDAL
    {
        //Uy
        //comentario
        #region CRUD
        /// <summary>  
        /// Metodo para guardar de forma Asincronica. para que un metodo sea Asincronico debe llevar la palabra Async
        /// y usar al menos un metodo asincronico en el.
        /// </summary> 
        /// <param name="pDetalleCita">Se espera un objeto del Tipo DetalleCita, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<int> CrearAsync(DetalleCita pDetalleCita)
            {
                int result = 0;
                using (var dbContexto = new DBContexto())
                {

                    dbContexto.Add(pDetalleCita);
                    result = await dbContexto.SaveChangesAsync();
                }
                return result;
            }


        /// <summary>  
        /// Con este metodo Actualisaremos en la base de datos el DetalleCita.
        /// </summary>  
        /// <param name="pDetalleCita">Se espera un objeto del Tipo DetalleCita, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<int> ModificarAsync(DetalleCita pDetalleCita)
            {
                int result = 0;
                using (var dbContexto = new DBContexto())
                {
                    var detallecita = await dbContexto.DetalleCita.FirstOrDefaultAsync(s => s.Id == pDetalleCita.Id);
                    detallecita.IdCita = pDetalleCita.IdCita;
                    detallecita.IdServicio = pDetalleCita.IdServicio;
                    detallecita.Precio = pDetalleCita.Precio;
                    detallecita.Duracion = pDetalleCita.Duracion;

                dbContexto.Update(detallecita);
                    result = await dbContexto.SaveChangesAsync();
                }
                return result;
            }

        /// <summary>  
        /// Metodo para eliminar para Eliminar un DetalleCita en la Base de Datos.
        /// </summary>  
        /// <param name="pDetalleCita">Se espera un objeto del Tipo DetalleCita, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<int> EliminarAsync(DetalleCita pDetalleCita)
            {
                int result = 0;
                using (var dbContexto = new DBContexto())
                {
                    var servicio = await dbContexto.Cita.FirstOrDefaultAsync(s => s.Id == pDetalleCita.Id);
                    dbContexto.DetalleCita.Remove(pDetalleCita);
                    result = await dbContexto.SaveChangesAsync();
                }
                return result;
            }

        /// <summary>  
        /// Metodo para Obtener por Id un DetalleCita en la base de Datos
        /// </summary>  
        /// <param name="pDetalleCita">Se espera un objeto del Tipo DetalleCita, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<DetalleCita> ObtenerPorIdAsync(DetalleCita pDetalleCita)
            {
                var detallecita = new DetalleCita();
                using (var dbContexto = new DBContexto())
                {
                    detallecita = await dbContexto.DetalleCita.FirstOrDefaultAsync(s => s.Id == pDetalleCita.Id);
                }
                return detallecita;
            }

        /// <summary>  
        /// Metodo para obtener todos los DetalleCita en la base de Datos.'
        /// </summary>  
        /// <returns>Retorna una Lista de DetalleCita</returns>  
        ///
        public static async Task<List<DetalleCita>> ObtenerTodosAsync()
            {
                var detallecita = new List<DetalleCita>();
                using (var dbContexto = new DBContexto())
                {
                    detallecita = await dbContexto.DetalleCita.ToListAsync();
                }
                return detallecita;
            }

        /// <summary>  
        /// Metodo QuerySelect sirve para hacer filtros personalisados utilizando Entity, LinQ expresiones lanba.
        ///Iternal es para indicar que este Metodo se usara solo dentro del mismo NameSpace.
        /// </summary>  
        /// <param name="pDetalleCita">Se espera un objeto del Tipo DetalleCita, con sus valores llenos</param>  
        /// <returns>Retorna las coincidencias encontradas en la base de Datos</returns>  
        ///
        internal static IQueryable<DetalleCita> QuerySelect(IQueryable<DetalleCita> pQuery, DetalleCita pDetalleCita) //los internal solo funcionan en su respectivo namespace 
            {
                if (pDetalleCita.Id > 0)
                    pQuery = pQuery.Where(s => s.Id == pDetalleCita.Id);
                if (pDetalleCita.IdCita > 0)
                    pQuery = pQuery.Where(s => s.IdCita == pDetalleCita.IdCita);
                if (pDetalleCita.IdServicio > 0)
                    pQuery = pQuery.Where(s => s.IdServicio == pDetalleCita.IdServicio);
                if (pDetalleCita.Precio> 0)
                    pQuery = pQuery.Where(s => s.Precio == pDetalleCita.Precio);

                pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
                if (pDetalleCita.Top_Aux > 0)
                    pQuery = pQuery.Take(pDetalleCita.Top_Aux).AsQueryable();

                return pQuery;
            }

        /// <summary>  
        /// Metodo para Obtener por Id un DetalleCita en la base de Datos
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<List<DetalleCita>> BuscarAsync(DetalleCita pDetalleCita)
            {
                var detallecita = new List<DetalleCita>();
                using (var dbContexto = new DBContexto()) //la palabra using encierra
                {
                    var select = dbContexto.DetalleCita.AsQueryable(); //esto es como un SELECT * FROM
                    select = QuerySelect(select, pDetalleCita);
                    detallecita = await select.ToListAsync();
                }
                return detallecita;
            }

        /// <summary>  
        /// Metodo para Buscar con Citas Incluidos con DetalleCita
        /// </summary>  
        /// <param name="pDetalleCita">Se espera un objeto del Tipo DetalleCita, con sus valores llenos</param>  
        /// <returns>Objeto tipo List DetalleCita que contenga la lista de Servicio</returns>  
        ///
        public static async Task<List<DetalleCita>> BuscarIncluirServicioAsync(DetalleCita pDetalleCita)
        {
            var detallecita = new List<DetalleCita>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.DetalleCita.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pDetalleCita).Include(s=> s.Servicio);
                detallecita = await select.ToListAsync();
            }
            return detallecita;
        }
        #endregion

        /// <summary>  
        /// Metodo para crear detalles
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        ///
        public static void CrearDetalles(DBContexto pContext,List<DetalleCita> pDetalles, Cita pCita) 
        {
            if (pDetalles != null && pDetalles.Count > 0)
            {
                foreach (var item in pDetalles)
                {
                    item.IdCita = pCita.Id;
                    pContext.Add(item);
                }
            }
        }

        /// <summary>  
        /// Metodo para actualizar detalles
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        ///
        public static async Task ActualizarDetalles(DBContexto pContext, List<DetalleCita> pDetalles, Cita pCita) 
        {
            if (pDetalles != null && pDetalles.Count() > 0)
            {
                foreach (var item in pDetalles)
                {
                    if (item.TipoAccion_Aux == (byte)DetalleCita.TipoAccion.NUEVO)
                    {
                        item.IdCita = pCita.Id;
                        pContext.Add(item);
                    }
                    else if (item.TipoAccion_Aux == (byte)DetalleCita.TipoAccion.MODIFICAR && item.Id > 0)
                    {
                        var detallecita = await pContext.DetalleCita.FirstOrDefaultAsync(s => s.Id == item.Id);
                        detallecita.Precio = item.Precio;
                        detallecita.Duracion = item.Duracion;
                        pContext.Update(detallecita);
                    }
                    else if (item.TipoAccion_Aux == (byte)DetalleCita.TipoAccion.ELIMINAR && item.Id > 0)
                    {
                        var detallecita = await pContext.DetalleCita.FirstOrDefaultAsync(s => s.Id == item.Id);
                        pContext.DetalleCita.Remove(detallecita);
                    }
                }
            }
        }


    }
}
