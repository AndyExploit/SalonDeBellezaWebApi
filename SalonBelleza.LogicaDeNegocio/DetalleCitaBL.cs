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
    /// Esta clase es de la entidad DetalleCita de la capa Logica de De Negocio
    /// Esta clase contiene Los metodos CRUD de Detallecita
    /// 
    /// </summary> 
    public class DetalleCitaBL
    {
        /// <summary>  
        /// Aqui se retorna el Metodo para guardar de forma Asincronica. para que un metodo sea Asincronico debe llevar la palabra Async
        /// y usar al menos un metodo asincronico en el.
        /// </summary> 
        /// <param name="pDetalleCita">Se espera un objeto del Tipo DetalleCita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///
        public async Task<int> CrearAsync(DetalleCita pDetalleCita)
        {
            return await DetalleCitaDAL.CrearAsync(pDetalleCita);
        }

        /// <summary>  
        /// Con este metodo Actualisaremos en la base de datos el DetalleCita.
        /// </summary>  
        /// <param name="pDetalleCita">Se espera un objeto del Tipo DetalleCita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///

        public async Task<int> ModificarAsync(DetalleCita pDetalleCita)
        {
            return await DetalleCitaDAL.ModificarAsync(pDetalleCita);
        }

        /// <summary>  
        /// Metodo para eliminar para Eliminar un DetalleCita en la Base de Datos.
        /// </summary>  
        /// <param name="pDetalleCita">Se espera un objeto del Tipo DetalleCita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///

        public async Task<int> EliminarAsync(DetalleCita pDetalleCita)
        {
            return await DetalleCitaDAL.EliminarAsync(pDetalleCita);
        }

        /// <summary>  
        /// Metodo para Obtener por Id un DetalleCita en la base de Datos
        /// </summary>  
        /// <param name="pDetalleCita">Se espera un objeto del Tipo DetalleCita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///

        public async Task<DetalleCita> ObtenerPorIdAsync(DetalleCita pDetalleCita)
        {
            return await DetalleCitaDAL.ObtenerPorIdAsync(pDetalleCita);
        }

        /// <summary>  
        /// Metodo para obtener todos los DetalleCita en la base de Datos.'
        /// </summary>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///

        public async Task<List<DetalleCita>> ObtenerTodosAsync()
        {
            return await DetalleCitaDAL.ObtenerTodosAsync();
        }

        /// <summary>  
        /// Metodo para Obtener por Id un DetalleCita en la base de Datos
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita, con sus valores llenos</param>  
        /// <returns>Retorna una tarea Asyncrona</returns>  
        ///

        public async Task<List<DetalleCita>> BuscarAsync(DetalleCita pDetalleCita)
        {
            return await DetalleCitaDAL.BuscarAsync(pDetalleCita);
        }
        public async Task<List<DetalleCita>> BuscarIncluirServicioAsync(DetalleCita pDetalleCita)
        {
            return await DetalleCitaDAL.BuscarIncluirServicioAsync(pDetalleCita);
        }

        // 


    }
}
