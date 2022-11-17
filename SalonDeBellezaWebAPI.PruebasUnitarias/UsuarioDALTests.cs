using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalonBelleza.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// *********************************************************
using SalonBelleza.EntidadesDeNegocio;

namespace SalonBelleza.AccesoADatos.Tests
{
    [TestClass()]
    public class UsuarioDALTests
    {
        [TestMethod()]
        public async Task CrearAsyncTest()
        {
            Usuario usuario = new Usuario(); //Se crea un objeto de la Clase servicio a la cual le llenaremos sus parametros.
            usuario.IdRol = 7;
            usuario.Nombre = "HectorTEST";
            usuario.Apellido = "BonillaTEST";
            usuario.Dui = "123456789";
            usuario.Numero = "7520-9177";
            usuario.Estado = 1;
            usuario.Login = "HBon08";
            usuario.Password = "123456";
            usuario.FechaRegistro = DateTime.Now;


            //int result = await UsuarioDAL.CrearAsync(usuario); //Se crea una variable llamada result la cual sera igual al resultado del metodo CrearAsync 
            //de la capa ServicioDAL el cual espera como parametro un objeto sin ID.
            //Assert.IsFalse(result == 0); //Assert.IsFalse compara que la condicion sea falsa en este caso se espera que el result de 1 para que todo sea correcto.
        }

        [TestMethod()]
        public async Task ModificarAsyncTest()
        {
            Usuario usuario = new Usuario(); //Se crea un objeto de la Clase servicio a la cual le llenaremos sus parametros.
            usuario.Id = 61;
            usuario.IdRol = 7;
            usuario.Nombre = "HectorTEST 02";
            usuario.Apellido = "BonillaTEST 01";
            usuario.Dui = "123456789";
            usuario.Numero = "7520-9177";
            usuario.Estado = 1;
            usuario.Login = "HBon07";
            usuario.Password = "123456";
            usuario.FechaRegistro = DateTime.Now;


            int result = await UsuarioDAL.ModificarAsync(usuario); //Se crea una variable llamada result la cual sera igual al resultado del metodo CrearAsync 
            //de la capa ServicioDAL el cual espera como parametro un objeto sin ID.
            Assert.IsFalse(result == 0); //Assert.IsFalse compara que la condicion sea falsa en este caso se espera que el result de 1 para que todo sea correcto.
        }

        [TestMethod()]
        public async Task EliminarAsyncTest()
        {
            Usuario usuario = new Usuario();
            usuario.Id = 62;

            int result = await UsuarioDAL.EliminarAsync(usuario);
            Assert.IsFalse(result == 0);
        }

        [TestMethod()]
        public async Task ObtenerPorIdAsyncTest()
        {
            Usuario usuario = new Usuario();
            usuario.Id = 62;

            Usuario result = await UsuarioDAL.ObtenerPorIdAsync(usuario);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task ObtenerTodosAsyncTest()
        {
            List<Usuario> usuarios = await UsuarioDAL.ObtenerTodosAsync();
            Assert.IsFalse(usuarios.Count == 0);
        }

        [TestMethod()]
        public async Task BuscarAsyncTest()
        {
            Usuario usuario = new Usuario();
            usuario.Nombre = "or"; //Aca agregaremos los parametros para buscar, si se desea agregar mas agregar servicio.*campo para buscar*

            List<Usuario> usuarios = await UsuarioDAL.BuscarAsync(usuario);

            Assert.IsFalse(usuarios.Count == 0);
        }

        [TestMethod()]
        public async Task BuscarIncluirRolesAsyncTest()
        {
            Usuario usuario = new Usuario();
            usuario.IdRol = 1;

            List<Usuario> usuarios = await UsuarioDAL.BuscarIncluirRolesAsync(usuario);
            Assert.IsFalse(usuarios.Count == 0);
        }

        [TestMethod()]
        public async Task LoginAsyncTest()
        {
            Usuario usuario = new Usuario();
            usuario.Login = "SysAdmin";
            usuario.Password = "Admin2020";

            Usuario result = await UsuarioDAL.LoginAsync(usuario);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task CambiarPasswordAsyncTest()
        {
            string usuarioPassAnt = "123456";

            Usuario usuario = new Usuario();
            usuario.Id = 61;
            usuario.Password = "123456";

            int result = await UsuarioDAL.CambiarPasswordAsync(usuario, usuarioPassAnt);
            Assert.IsFalse(result == 0);
        }
    }
}