using SalonBelleza.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SalonBelleza.AccesoADatos
{
    public class CitaDAL
    {
        #region CRUD
        /// <summary>  
        /// Metodo para guardar de forma Asincronica. para que un metodo sea Asincronico debe llevar la palabra Async
        /// y usar al menos un metodo asincronico en el.
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<int> CrearAsync(Cita pCita) 
        {
            int result = 0;
            using (var dbContexto = new DBContexto()) 
            {
                using (var transaccion = await dbContexto.Database.BeginTransactionAsync())
                {
                    try
                    {
                        pCita.FechaRegistrada = DateTime.Now; //Esta fecha se tomara al momento de crearse la cita
                        dbContexto.Add(pCita);
                      //  DetalleCitaDAL.CrearDetalles(dbContexto, pCita.DetalleCita, pCita);
                        result = await dbContexto.SaveChangesAsync();
                        await transaccion.CommitAsync();
                    }
                    catch (Exception ex) 
                    {
                        await transaccion.RollbackAsync();
                        throw ex;
                    }
                }

            }
            return result;
        }

        /// <summary>  
        /// Con este metodo Actualisaremos en la base de datos el Cita.
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<int> ModificarAsync(Cita pCita) 
        {
            int result = 0;
            using (var dbContexto = new DBContexto()) 
            {
                
                result = await dbContexto.SaveChangesAsync();
                using (var transaccion = await dbContexto.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var cita = await dbContexto.Cita.FirstOrDefaultAsync(s => s.Id == pCita.Id);
                        cita.IdUsuario = pCita.IdUsuario;
                        cita.IdCliente = pCita.IdCliente;
                        cita.FechaCita = pCita.FechaCita;
                        cita.Total = pCita.Total;
                        cita.Estado = pCita.Estado;
                        dbContexto.Update(cita);
                        await DetalleCitaDAL.ActualizarDetalles(dbContexto, pCita.DetalleCita, pCita);
                        result = await dbContexto.SaveChangesAsync();
                        await transaccion.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaccion.RollbackAsync();
                        throw ex;
                    }
                }
            }
            return result;
        }

        /// <summary>  
        /// Metodo para eliminar para Eliminar un Cita en la Base de Datos.
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<int> EliminarAsync(Cita pCita)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                using (var transaccion = await dbContexto.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var cita = await dbContexto.Cita.FirstOrDefaultAsync(s => s.Id == pCita.Id);
                        var detalles= await dbContexto.DetalleCita.Where(s => s.Id == pCita.Id).ToListAsync();
                        if (detalles != null) 
                        {
                            detalles.ForEach(s => s.TipoAccion_Aux = (byte)DetalleCita.TipoAccion.ELIMINAR);
                            await DetalleCitaDAL.ActualizarDetalles(dbContexto, detalles, pCita);
                        }
                        dbContexto.Cita.Remove(cita);
                        result = await dbContexto.SaveChangesAsync();
                        await transaccion.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaccion.RollbackAsync();
                        throw ex;
                    }
                }

            }
            return result;
        }

        /// <summary>  
        /// Metodo para Obtener por Id un Cita en la base de Datos
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<Cita> ObtenerPorIdAsync(Cita pCita)
        {
            var cita = new Cita();
            using (var dbContexto = new DBContexto())
            {
                cita = await dbContexto.Cita.FirstOrDefaultAsync(s => s.Id == pCita.Id);
            }
            return cita;
        }

        /// <summary>  
        /// Metodo para obtener todos los Cita en la base de Datos.'
        /// </summary>  
        /// <returns>Retorna una Lista de Cita</returns>  
        ///
        public static async Task<List<Cita>> ObtenerTodosAsync()
        {
            var cita = new List<Cita>();
            using (var dbContexto = new DBContexto())
            {
                cita = await dbContexto.Cita.ToListAsync();
            }
            return cita;
        }

        /// <summary>  
        /// Metodo QuerySelect sirve para hacer filtros personalisados utilizando Entity, LinQ expresiones lanba.
        ///Iternal es para indicar que este Metodo se usara solo dentro del mismo NameSpace.
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Retorna las coincidencias encontradas en la base de Datos</returns>  
        ///
        internal static IQueryable<Cita> QuerySelect(IQueryable<Cita> pQuery, Cita pCita) //los internal solo funcionan en su respectivo namespace 
        {
            if (pCita.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pCita.Id);
            if (pCita.IdUsuario > 0)
                pQuery = pQuery.Where(s => s.IdUsuario == pCita.IdUsuario);
            if (pCita.IdCliente > 0)
                pQuery = pQuery.Where(s => s.IdCliente == pCita.IdCliente);
            if (pCita.FechaRegistrada.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pCita.FechaRegistrada.Year, pCita.FechaRegistrada.Month, pCita.FechaRegistrada.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaRegistrada >= fechaInicial && s.FechaRegistrada <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pCita.FechaCita.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pCita.FechaCita.Year, pCita.FechaCita.Month, pCita.FechaCita.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaCita >= fechaInicial && s.FechaCita <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pCita.Total > 0)
                pQuery = pQuery.Where(s => s.Total == pCita.Total);
            if (pCita.Estado > 0)
                pQuery = pQuery.Where(s => s.Estado == pCita.Estado);
            if (pCita.Top_Aux > 0)
                pQuery = pQuery.Take(pCita.Top_Aux).AsQueryable();
            return pQuery;
        }


        /// <summary>  
        /// Metodo para Obtener por Id un Cita en la base de Datos
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<List<Cita>> BuscarAsync(Cita pCita)
        {
            var cita = new List<Cita>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.Cita.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pCita);
                cita = await select.ToListAsync();
            }
            return cita;
        }
        #endregion

        /// <summary>  
        /// Metodo para Buscar con Citas Incluidos con Usuarios
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Objeto tipo List Cita que contenga la lista de Usuario</returns>  
        ///
        public static async Task<List<Cita>> BuscarIncluirUsuarioAsync(Cita pCita)
        {
            var cita = new List<Cita>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.Cita.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pCita).Include(s => s.Usuario).AsQueryable();
                cita = await select.ToListAsync();
            }
            return cita;
        }

        /// <summary>  
        /// Metodo para Buscar con Citas Incluidos con Clientes
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Objeto tipo List Cita que contenga la lista de Clientes</returns>  
        ///
        public static async Task<List<Cita>> BuscarIncluirClienteAsync(Cita pCita)
        {
            var cita = new List<Cita>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.Cita.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pCita).Include(s => s.Cliente).AsQueryable();
                cita = await select.ToListAsync();
            }
            return cita;
        }

        /// <summary>  
        /// Metodo para Buscar con Citas Incluidos con Usuarios y Clientes
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Objeto tipo List Cita que contenga la lista de Usaurios y Clientes</returns>  
        ///
        public static async Task<List<Cita>> BuscarIncluirUsuarioClienteAsync(Cita pCita)
        {
            var cita = new List<Cita>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.Cita.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pCita).Include(s => s.Cliente).AsQueryable();
                // Esta linea es para agregar otro incluide si agrega otro mas solo duplicar esta linea y cambiar a la
                // clase que se desea incluir en la consulta
                select = select.Include(s=> s.Usuario).AsQueryable();
                cita = await select.ToListAsync(); 
            }
            return cita;
        }
    }
}
