using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalonBelleza.AccesoADatos;
using SalonBelleza.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonBelleza.AccesoADatos.Tests
{
    [TestClass()]
    public class ClienteDALTests
    {
        [TestMethod()]
        public async Task CrearAsyncTest()
        {
            Cliente cliente = new Cliente();//Creamos una instancia de Cita en la cual le agregaremos parametros.
            //Agregamos el Id mas los parametros a modificar, no puede ir ninguno vacio.
            cliente.Dui = "06318416-4";
            cliente.Nombre = "Rosa";
            cliente.Apellido = "Vasquez";
            cliente.Numero = 23455643;

            int result = await ClienteDAL.CrearAsync(cliente);
            Assert.IsFalse(result == 0); //Si es falso el resultado, marcara un error.

        }

        [TestMethod()]
        public async Task ModificarAsyncTest()
        {
            Cliente cliente = new Cliente();//Creamos una instancia de Cita en la cual le agregaremos parametros.
            //Agregamos el Id mas los parametros a modificar, no puede ir ninguno vacio.
            cliente.Id = 39;
            cliente.Dui = "32318416-4";
            cliente.Nombre = "Alva";
            cliente.Apellido = "Valdez";
            cliente.Numero = 23455632;

            int result = await ClienteDAL.ModificarAsync(cliente);
            Assert.IsFalse(result == 0); //Si es falso el resultado, marcara un error.

        }

        [TestMethod()]
        public async Task EliminarAsyncTest()
        {
            Cliente cliente = new Cliente();//Creamos una instancia de Cita en la cual le agregaremos parametros.
                                            //Agregamos el Id mas los parametros a modificar, no puede ir ninguno vacio.
            cliente.Id = 39;

            int result = await ClienteDAL.EliminarAsync(cliente);
            Assert.IsFalse(result == 0); //Si es falso el resultado, marcara un error.
        }

        [TestMethod()]
        public async Task ObtenerPorIdAsyncTest()
        {
            Cliente cliente = new Cliente();//Creamos una instancia de Cita en la cual le agregaremos parametros.
            //Agregamos el Id mas los parametros a modificar, no puede ir ninguno vacio.
            cliente.Id = 39;

            Cliente result;
            result = await ClienteDAL.ObtenerPorIdAsync(cliente);
            Assert.IsNotNull(result); //Si es nulo el resultado, marcara un error.
        }

        [TestMethod()]
        public async Task ObtenerTodosAsyncTest()
        {

            List<Cliente> roles = await ClienteDAL.ObtenerTodosAsync();
            Assert.IsFalse(roles.Count == 0); //Si es falso el resultado, marcara un error.
        }

        [TestMethod()]
        public async Task BuscarAsyncTest()
        {
            Cliente cliente = new Cliente();//Creamos una instancia de Cita en la cual le agregaremos parametros.
            //Agregamos el Id mas los parametros a modificar, no puede ir ninguno vacio.
            cliente.Id = 39;

            List<Cliente> clientes = await ClienteDAL.BuscarAsync(cliente);
            Assert.IsFalse(clientes.Count == 0); //Si es falso el resultado, marcara un error.
        }
    }
}