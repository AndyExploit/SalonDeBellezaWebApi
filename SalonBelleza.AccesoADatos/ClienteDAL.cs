using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//***************
using Microsoft.EntityFrameworkCore;
using SalonBelleza.EntidadesDeNegocio;
namespace SalonBelleza.AccesoADatos
{
    public class ClienteDAL
    {

        #region CRUD
        /// <summary>  
        /// Metodo para crear un Nuevo cliente.
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente, con sus valores llenos</param>  
        /// <returns>Devuelve un entero para conocer la respuesta del Metodo</returns>  
        /// 
        public static async Task<int> CrearAsync(Cliente pCliente) 
        {
            int result = 0;
            using (var bdContexto = new DBContexto()) 
            {
                bdContexto.Add(pCliente);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        /// <summary>  
        /// Metodo para Modificar un Cliente segun los parametros agregados.
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del tipo Cliente con todas sus propiedades llenas</param>
        /// <returns>Devuelve un entero para conocer la respuesta del metodo </returns>  
        /// 
        public static async Task<int> ModificarAsync(Cliente pCliente)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var cliente = await dbContexto.Cliente.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
                cliente.Nombre = pCliente.Nombre;
                cliente.Apellido = pCliente.Apellido;
                cliente.Numero = pCliente.Numero;
                cliente.Dui = pCliente.Dui;
                dbContexto.Update(cliente);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        /// <summary>  
        /// Metodo para Eliminar un Cliente segun el Id.
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente, con sus valores llenos</param>  
        /// <returns>Devuelve un entero para conocer la respuesta del metodo</returns>  
        /// 
        public static async Task<int> EliminarAsync(Cliente pCliente)
        {
            int result = 0;
            using (var bdContexto = new DBContexto())
            {
                var cliente = await bdContexto.Cliente.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
                bdContexto.Cliente.Remove(cliente);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        /// <summary>  
        /// Metodo para obtener por ID un cliente ingresado
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente, con sus valores llenos</param>  
        /// <returns>Devuelve un entero para conocer la respuesta del metodo</returns>  
        /// 
        public static async Task<Cliente> ObtenerPorIdAsync(Cliente pCliente)
        {
            var cliente = new Cliente();
            using (var bdContexto = new DBContexto())
            {
                cliente = await bdContexto.Cliente.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
            }
            return cliente;
        }

        /// <summary>  
        /// Metodo para Obtener todos los Cliente.
        /// </summary>  
        /// <returns>Devuelve una Lista de Clientes</returns>  
        /// 
        public static async Task<List<Cliente>> ObtenerTodosAsync()
        {
            var clientes = new List<Cliente>();
            using (var bdContexto = new DBContexto())
            {
                clientes = await bdContexto.Cliente.ToListAsync();
            }
            return clientes;
        }

        /// <summary>  
        /// Metodo para Buscar por parametros
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente, con sus valores llenos</param>  
        /// /// <param name="pQuery">Se espera una IQueryable de Cliente</param>
        /// <returns>Devuelve un pQuery con las coincidencias encontradas en la base de Datos</returns>  
        /// 
        internal static IQueryable<Cliente> QuerySelect(IQueryable<Cliente> pQuery, Cliente pCliente)
        {
            if (pCliente.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pCliente.Id);
            if (!string.IsNullOrWhiteSpace(pCliente.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pCliente.Nombre));

            if (!string.IsNullOrWhiteSpace(pCliente.Dui))
                pQuery = pQuery.Where(s => s.Dui.Contains(pCliente.Dui));

            if (!string.IsNullOrWhiteSpace(pCliente.Apellido))
                pQuery = pQuery.Where(s => s.Apellido.Contains(pCliente.Apellido));

            if (pCliente.Numero > 0)
                pQuery = pQuery.Where(s => s.Numero == pCliente.Numero);

            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pCliente.Top_Aux > 0)
                pQuery = pQuery.Take(pCliente.Top_Aux).AsQueryable();
            return pQuery;
        }

        /// <summary>  
        /// Metodo para Buscar un Cliente Async
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna una Lista de Clientes con las coincidencias encontradas</returns>  
        /// 
        public static async Task<List<Cliente>> BuscarAsync(Cliente pCliente)
        {
            var clientes = new List<Cliente>();
            using (var bdContecto = new DBContexto())
            {
                var select = bdContecto.Cliente.AsQueryable();
                select = QuerySelect(select, pCliente);
                clientes = await select.ToListAsync();
            }
            return clientes;
        } //:3
        #endregion

    }
}
