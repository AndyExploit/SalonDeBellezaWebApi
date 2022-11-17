using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalonBelleza.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ****************************************************
using SalonBelleza.EntidadesDeNegocio;

namespace SalonBelleza.AccesoADatos.Tests
{
    [TestClass()]
    public class ServicioDALTests
    {
        [TestMethod()]
        public async Task CrearAsyncTest()
        {
            Servicio servicio = new Servicio(); //Se crea un objeto de la Clase servicio a la cual le llenaremos sus parametros.
            servicio.Nombre = "CorteTest";
            servicio.Descripcion = "CorteTest de prueba";
            servicio.Precio = 2;
            servicio.Duracion = 3;

            //int result = await ServicioDAL.CrearAsync(servicio); //Se crea una variable llamada result la cual sera igual al resultado del metodo CrearAsync 
            //de la capa ServicioDAL el cual espera como parametro un objeto sin ID.
            //Assert.IsFalse(result == 0); //Assert.IsFalse compara que la condicion sea falsa en este caso se espera que el result de 1 para que todo sea correcto.
        }

        [TestMethod()]
        public async Task ModificarAsyncTest()
        {
            Servicio servicio = new Servicio(); //Se crea un objeto de la Clase servicio a la cual le llenaremos sus parametros.
            servicio.Id = 47;
            servicio.Nombre = "CorteTes01";
            servicio.Descripcion = "CorteTest01 --";
            servicio.Duracion = 3;
            servicio.Precio = 2; //Llenamos todos los parametros para modificar.

            int result = await ServicioDAL.ModificarAsync(servicio); //Se crea una variable llamada result la cual sera igual al resultado del metodo ModificarAsync 
            //de la capa ServicioDAL el cual espera como parametro un ID y todos sus campos llenos.
            Assert.IsFalse(result == 0);
        }

        [TestMethod()]
        public async Task EliminarAsyncTest()
        {
            Servicio servicio = new Servicio();
            servicio.Id = 47;

            int result = await ServicioDAL.EliminarAsync(servicio);
            Assert.IsFalse(result == 0);
        }

        [TestMethod()]
        public async Task ObtenerPorIdAsyncTest()
        {
            Servicio servicio = new Servicio();
            servicio.Id = 47;

            Servicio result = await ServicioDAL.ObtenerPorIdAsync(servicio);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task ObtenerTodosAsyncTest()
        {
            //se declara un List ya que se espera una lista de Servicio.
            List<Servicio> servicios = await ServicioDAL.ObtenerTodosAsync();
            Assert.IsFalse(servicios.Count == 0);
        }

        [TestMethod()]
        public async Task BuscarAsyncTest()
        {
            Servicio servicio = new Servicio();
            servicio.Nombre = "or"; //Aca agregaremos los parametros para buscar, si se desea agregar mas agregar servicio.*campo para buscar*

            List<Servicio> servicios = await ServicioDAL.BuscarAsync(servicio);

            Assert.IsFalse(servicios.Count == 0);
        }
    }
}