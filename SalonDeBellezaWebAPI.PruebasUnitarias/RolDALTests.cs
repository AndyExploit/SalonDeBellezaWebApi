using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalonBelleza.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalonBelleza.EntidadesDeNegocio;

namespace SalonBelleza.AccesoADatos.Tests
{
    [TestClass()]
    public class RolDALTests
    {
        [TestMethod()]
        public async Task CrearAsyncTest()
        {
            Rol rol = new Rol();//Creamos una instancia de Cita en la cual le agregaremos parametros.
            //Agregamos el Id mas los parametros a modificar, no puede ir ninguno vacio.
            rol.Nombre = "Usuario";

            int result = await RolDAL.CrearAsync(rol);
            Assert.IsFalse(result == 0); //Si es falso el resultado, marcara un error.

        }

        [TestMethod()]
        public async Task ModificarAsyncTest()
        {
            Rol rol = new Rol();//Creamos una instancia de Cita en la cual le agregaremos parametros.
            //Agregamos el Id mas los parametros a modificar, no puede ir ninguno vacio.
            rol.Id = 49;
            rol.Nombre = "Usuario2";

            int result = await RolDAL.ModificarAsync(rol);
            Assert.IsFalse(result == 0); //Si es falso el resultado, marcara un error.

        }

        [TestMethod()]
        public async Task EliminarAsyncTest()
        {
            Rol rol = new Rol();//Creamos una instancia de Cita en la cual le agregaremos parametros.
                                //Agregamos el Id mas los parametros a modificar, no puede ir ninguno vacio.
            rol.Id = 49;          

            int result = await RolDAL.EliminarAsync(rol);
            Assert.IsFalse(result == 0); //Si es falso el resultado, marcara un error.
        }

        [TestMethod()]
        public async Task ObtenerPorIdAsyncTest()
        {
            Rol rol = new Rol();//Creamos una instancia de Cita en la cual le agregaremos parametros.
            //Agregamos el Id mas los parametros a modificar, no puede ir ninguno vacio.
            rol.Id =49;

           Rol result;
            result = await RolDAL.ObtenerPorIdAsync(rol);
            Assert.IsNotNull(result); //Si es nulo el resultado, marcara un error.
        }

        [TestMethod()]
        public async Task ObtenerTodosAsyncTest()
        {
          
            List<Rol> roles = await RolDAL.ObtenerTodosAsync();
            Assert.IsFalse(roles.Count == 0); //Si es falso el resultado, marcara un error.
        }

        [TestMethod()]
        public async Task BuscarAsyncTest()
        {
            Rol rol = new Rol();//Creamos una instancia de Cita en la cual le agregaremos parametros.
            //Agregamos el Id mas los parametros a modificar, no puede ir ninguno vacio.
            rol.Id =49;

            List<Rol> rol_ = await RolDAL.BuscarAsync(rol);
            Assert.IsFalse(rol_.Count == 0); //Si es falso el resultado, marcara un error.
        }
    }
}