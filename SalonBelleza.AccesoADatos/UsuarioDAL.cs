using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ****************************************************
using Microsoft.EntityFrameworkCore;
using SalonBelleza.EntidadesDeNegocio;
using System.Security.Cryptography;

namespace SalonBelleza.AccesoADatos
{
    public class UsuarioDAL
    {

        /// <summary>  
        /// Metodo para Encriptar la Contraseña.  
        /// </summary>  
        /// <param name="pUsuario">Se espera como parametro un Objeto de Tipo Usuario, el cual debe contener el Password del usuario</param>   
        /// <returns>Obtenemos la Password completamente Encriptada</returns>  
        ///  
        private static void EncriptarMD5(Usuario pUsuario)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(pUsuario.Password)); //Aqui convertimos lo que viene en el Password en Bytes. 
                                                                                          //Para luego encriptarla. 
                var strEncriptar = "";
                for (int i = 0; i < result.Length; i++)
                    strEncriptar += result[i].ToString("x2").ToLower();
                pUsuario.Password = strEncriptar;
            }

        }

        /// <summary>  
        /// Metodo para validar si existe un Login en la base de datos
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <param name="pDbContext">Se espera un objeto del DbContexto para mapear la Entidad</param>  
        /// <returns>Si el usurio ya existe en la base de Datos, arroja True</returns>  
        ///  
        private static async Task<bool> ExisteLogin(Usuario pUsuario, DBContexto pDbContext)
        {
            bool result = false;
            var loginUsuarioExiste = await pDbContext.Usuario.FirstOrDefaultAsync(s => s.Login == pUsuario.Login && s.Id != pUsuario.Id);
            if (loginUsuarioExiste != null && loginUsuarioExiste.Id > 0 && loginUsuarioExiste.Login == pUsuario.Login)
                result = true;
            return result;

        }

        #region CRUD
        /// <summary>  
        /// Metodo para guardar de forma Asincronica. para que un metodo sea Asincronico debe llevar la palabra Async
        /// y usar al menos un metodo asincronico en el.
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<int> CrearAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var dbContexto = new DBContexto()) //Este using nos destruir el objeto para cerrar las conexiones a la base de datos. 
            {
                bool existeLogin = await ExisteLogin(pUsuario, dbContexto);
                if (existeLogin == false)
                {
                    pUsuario.FechaRegistro = DateTime.Now;
                    EncriptarMD5(pUsuario);
                    dbContexto.Add(pUsuario);
                    result = await dbContexto.SaveChangesAsync(); //De esta manera agregamos el Usuario y guardamos los cambios de manera Asincronica.
                }
                else
                    throw new Exception("Login ya existe.");
               
            }
            return result;

        }

        /// <summary>  
        /// Con este metodo Actualisaremos en la base de datos el Usuario.
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///

        public static async Task<int> ModificarAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                bool existeLogin = await ExisteLogin(pUsuario, dbContexto);
                if (existeLogin == false)
                {
                    var usuario = await dbContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id); //Con esto indicamos que busque en la base de Datos.
                                                                                                          //el Id que sea igual al que mandamos como parametro.
                    usuario.IdRol = pUsuario.IdRol;
                    usuario.Dui = pUsuario.Dui;
                    usuario.Nombre = pUsuario.Nombre;
                    usuario.Apellido = pUsuario.Apellido;
                    usuario.Numero = pUsuario.Numero;
                    usuario.Login = pUsuario.Login;
                    usuario.Estado = pUsuario.Estado;

                    dbContexto.Update(usuario);
                    result = await dbContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");
            }
            return result;
        }

        /// <summary>  
        /// Metodo para eliminar para Eliminar un Usuario en la Base de Datos.
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<int> EliminarAsync(Usuario pUsuario)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var usuario = await dbContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id); //Siempre estamos igualando el Id de la basae con el Id de parametro.
                dbContexto.Usuario.Remove(usuario); //Removemos el Rol de la base.
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        /// <summary>  
        /// Metodo para Obtener por Id un usuario en la base de Datos
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<Usuario> ObtenerPorIdAsync(Usuario pUsuario)
        {
            var usuario = new Usuario();
            using (var dbContexto = new DBContexto())
            {
                usuario = await dbContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id); //Se obtiene el Id para mostrarlo.
            }
            return usuario;
        }

        /// <summary>  
        /// Metodo para obtener todos los Usuarios en la base de Datos.'
        /// </summary>  
        /// <returns>Retorna una Lista de usuarios</returns>  
        ///

        public static async Task<List<Usuario>> ObtenerTodosAsync()
        {
            var usuario = new List<Usuario>(); //Variable que va a devolver nuestro metodo, en este caso una Lista.
            using (var dbContexto = new DBContexto())
            {
                usuario = await dbContexto.Usuario.ToListAsync();
            }
            return usuario;
        }


        /// <summary>  
        /// Metodo QuerySelect sirve para hacer filtros personalisados utilizando Entity, LinQ expresiones lanba.
        ///Iternal es para indicar que este Metodo se usara solo dentro del mismo NameSpace.
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Retorna las coincidencias encontradas en la base de Datos</returns>  
        ///
        
        internal static IQueryable<Usuario> QuerySelect(IQueryable<Usuario> pQuery, Usuario pUsuario)
        {
            if (pUsuario.Id > 0) //los que son tipo entero es > 0, para que funcione el filtro.
                pQuery = pQuery.Where(s => s.Id == pUsuario.Id);
            if (pUsuario.IdRol > 0)
                pQuery = pQuery.Where(s => s.IdRol == pUsuario.IdRol);
            if (!string.IsNullOrWhiteSpace(pUsuario.Dui)) //Para los que son String se les coloca IsNull.
                pQuery = pQuery.Where(s => s.Dui.Contains(pUsuario.Dui));
            if (!string.IsNullOrWhiteSpace(pUsuario.Nombre)) //Para los que son String se les coloca IsNull.
                pQuery = pQuery.Where(s => s.Nombre.Contains(pUsuario.Nombre)); //Este metodo es Aplicar un "Like" en la base de Datos.
            if (!string.IsNullOrWhiteSpace(pUsuario.Apellido))
                pQuery = pQuery.Where(s => s.Apellido.Contains(pUsuario.Apellido));
            if (!string.IsNullOrWhiteSpace(pUsuario.Login))
                pQuery = pQuery.Where(s => s.Login.Contains(pUsuario.Login));
            if (!string.IsNullOrWhiteSpace(pUsuario.Numero)) //Para los que son String se les coloca IsNull.
                pQuery = pQuery.Where(s => s.Numero.Contains(pUsuario.Numero));
            if (pUsuario.Estado > 0)
                pQuery = pQuery.Where(s => s.Estado == pUsuario.Estado);
            if (pUsuario.FechaRegistro.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pUsuario.FechaRegistro.Year, pUsuario.FechaRegistro.Month, pUsuario.FechaRegistro.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaRegistro >= fechaInicial && s.FechaRegistro <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable(); //Con este metodo se mostraran los ultimos Registros ingresados. 
                                                                        //Es decir que los ultimos Ingresador se veran primero en las consultas
            if (pUsuario.Top_Aux > 0)
                pQuery = pQuery.Take(pUsuario.Top_Aux).AsQueryable();
            return pQuery;
        }


        /// <summary>  
        /// Metodo para Obtener por Id un usuario en la base de Datos
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Resultado en Entero del metodo</returns>  
        ///
        public static async Task<List<Usuario>> BuscarAsync(Usuario pUsuario)
        {
            var usuario = new List<Usuario>();
            using (var dbContexto = new DBContexto())
            {
                var select = dbContexto.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario);
                usuario = await select.ToListAsync();
            }
            return usuario;
        }


        #endregion

        /// <summary>  
        /// Metodo para Buscar con Roles Incluidos
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Objeto tipo List usuario que contenga la lista de Roles</returns>  
        ///
        public static async Task<List<Usuario>> BuscarIncluirRolesAsync(Usuario pUsuario)
        {
            var usuario = new List<Usuario>();
            using (var dbContexto = new DBContexto())
            {
                var select = dbContexto.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario).Include(s => s.Rol).AsQueryable();
                usuario = await select.ToListAsync();
            }
            return usuario;
        }

        /// <summary>  
        /// Metodo para Iniciar el login y validar si esas credenciales existen en el sistema.
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// <returns>Objeto tipo usuario lleno si encuentra coincidencias, si no estara vacio</returns>  
        /// 
        public static async Task<Usuario> LoginAsync(Usuario pUsuario)
        {
            var usuario = new Usuario();
            using (var dbContexto = new DBContexto())
            {
                EncriptarMD5(pUsuario);
                usuario = await dbContexto.Usuario.FirstOrDefaultAsync(s => s.Login == pUsuario.Login &&
                s.Password == pUsuario.Password && s.Estado == (byte)Estado_Usuario.ACTIVO);
            }
            return usuario;
        }


        /// <summary>  
        /// Metodo para cambiar el Password, Comparando si esta correcto el password actual.
        /// </summary>  
        /// <param name="pUsuario">Se espera un objeto del Tipo Usuario, con sus valores llenos</param>  
        /// /// <param name="pPasswordAnt">Se espera una variable la cual contenga la PasswordAnterior</param>
        /// <returns>Devuelve un entero para conocer la respuesta</returns>  
        /// 

        public static async Task<int> CambiarPasswordAsync(Usuario pUsuario, string pPasswordAnt)
        {
            int result = 0;
            var usuarioPassAnt = new Usuario { Password = pPasswordAnt };
            EncriptarMD5(usuarioPassAnt);
            using (var dbContexto = new DBContexto())
            {
                var usuario = await dbContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                if (usuarioPassAnt.Password == usuario.Password)
                {
                    EncriptarMD5(pUsuario);
                    usuario.Password = pUsuario.Password;
                    dbContexto.Update(usuario);
                    result = await dbContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("El Password actual es incorrecto");
            }
            return result;
        }


    }
}
