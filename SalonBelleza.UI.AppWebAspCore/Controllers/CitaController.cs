using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/********************************/
using SalonBelleza.EntidadesDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

// Libreria necesarias para consumir la Web API
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
//**********************************************

namespace SalonBelleza.UI.AppWebAspCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CitaController : Controller
    {
        // Codigo agregar para consumir la Web API
        private readonly HttpClient httpClient;
        public CitaController(HttpClient client)
        {
            httpClient = client;
        }

        /// <summary>  
        /// Metodo para Obtener por Id una Cita hacinedo peticion a la API
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita el cual tenga el Id</param>  
        /// <returns>Objeto tipo cita con sus atributos llenos</returns>  
        /// 
        private async Task<Cita> ObtenerCitaPorIdAsync(Cita pCita)
        {
            Cita cita = new Cita();
            var response = await httpClient.GetAsync("Cita/" + pCita.Id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                cita = JsonSerializer.Deserialize<Cita>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return cita;
        }

        /// <summary>  
        /// Metodo para obtener clientes por Id hacinedo peticion a la API
        /// </summary>  
        /// <param name="id">Se espera una variable int la cual contenga el Id del registro a observar</param>  
        /// <returns>Retorna la vista con un objeto cliente</returns>  
        /// 
        private async Task<Cliente> ObtenerClientePorIdAsync(Cliente pCliente)
        {
            Cliente cliente = new Cliente();
            var response = await httpClient.GetAsync("Cliente/" + pCliente.Id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                cliente = JsonSerializer.Deserialize<Cliente>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return cliente;
        }
        private async Task<List<Cliente>> ObtenerClientesAsync()
        {
            List<Cliente> clientes = new List<Cliente>();
            var response = await httpClient.GetAsync("Cliente");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                clientes = JsonSerializer.Deserialize<List<Cliente>>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return clientes;
        }







        private async Task<Servicio> ObtenerServicioPorIdAsync(Servicio pServicio)
        {
            Servicio servicio = new Servicio();
            var response = await httpClient.GetAsync("Servicio/" + pServicio.Id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                servicio = JsonSerializer.Deserialize<Servicio>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return servicio;
        }
        private async Task<List<Servicio>> ObtenerServiciosAsync()
        {
            List<Servicio> servicios = new List<Servicio>();
            var response = await httpClient.GetAsync("Servicio");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                servicios = JsonSerializer.Deserialize<List<Servicio>>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return servicios;
        }









        private async Task<Usuario> ObtenerUsuarioPorIdAsync(Usuario pUsuario)
        {
            Usuario usuario = new Usuario();
            var response = await httpClient.GetAsync("Usuario/" + pUsuario.Id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                usuario = JsonSerializer.Deserialize<Usuario>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return usuario;
        }
        private async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            List<Usuario> usuarios = new List<Usuario>();
            var response = await httpClient.GetAsync("Usuario");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                usuarios = JsonSerializer.Deserialize<List<Usuario>>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return usuarios;
        }








        private async Task<DetalleCita> ObtenerDetalleCitaPorIdAsync(DetalleCita pDetalleCita)
        {
            DetalleCita detallecita = new DetalleCita();
            var response = await httpClient.GetAsync("DetalleCita/" + pDetalleCita.Id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                detallecita = JsonSerializer.Deserialize<DetalleCita>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return detallecita;
        }
        private async Task<List<DetalleCita>> ObtenerDetalleCitasAsync()
        {
            List<DetalleCita> detallecitas = new List<DetalleCita>();
            var response = await httpClient.GetAsync("DetalleCita");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                detallecitas = JsonSerializer.Deserialize<List<DetalleCita>>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return detallecitas;
        }







        /// <summary>  
        /// Metodo Index para obtener toda una Lista de Cita al hacer Peticion a la API
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita con sus parametros llenos</param>  
        /// <returns>Retorna la vista Index con objeto Cita</returns>  
        /// 
        public async Task<IActionResult> Index(Cita pCita = null)
        {
            if (pCita == null)
                pCita = new Cita();
            if (pCita.Top_Aux == 0)
                pCita.Top_Aux = 10;
            else if (pCita.Top_Aux == -1)
                pCita.Top_Aux = 0;
            // Codigo agregar para consumir la Web API
            var citas = new List<Cita>();
            var taskObtenerTodosClientes = ObtenerClientesAsync();
            var taskObtenerTodosUsuarios = ObtenerUsuariosAsync();
            var taskResponse = httpClient.PostAsJsonAsync("Cita/Buscar", pCita);
            var response = await taskResponse;
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                citas = JsonSerializer.Deserialize<List<Cita>>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            // ********************************************
            ViewBag.Top = pCita.Top_Aux;
            ViewBag.Clientes = await taskObtenerTodosClientes;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            return View(citas);
        }

        /// <summary>  
        /// Metodo para obtener detalles de Cita haicnedo peticiones a la API
        /// </summary>  
        /// <param name="id">Se espera una variable int la cual contenga el Id del registro a observar</param>  
        /// <returns>Retorna la vista con un objeto Cita</returns>  
        /// 
        // GET: UsuarioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            // Codigo agregar para consumir la Web API
            Cita cita = await ObtenerCitaPorIdAsync(new Cita { Id = id });
            cita.Cliente = await ObtenerClientePorIdAsync(new Cliente { Id = cita.IdCliente });
            cita.Usuario = await ObtenerUsuarioPorIdAsync(new Usuario { Id = cita.IdUsuario });
            //    cita.DetalleCita = await ObtenerDetalleCitaPorIdAsync(new DetalleCita { Id = cita.IdDetalleCita});
            //*******************************************************           
            return View(cita);
        }

        /// <summary>  
        /// Metodo para Get de la Vista Create Para obtener Clientes,Usuarios,Servicios
        /// </summary>   
        /// <returns>Retorna la vista de Create</returns>  
        /// 
        //UsuarioController/Create
        public async Task<IActionResult> Create()
        {
            // Codigo agregar para consumir la Web API
            var taskObtenerTodosClientes = ObtenerClientesAsync();
            var taskObtenerTodosUsuarios = ObtenerUsuariosAsync();
            var taskObtenerTodosServicios = ObtenerServiciosAsync();


            ViewBag.Servicios = await taskObtenerTodosServicios;
            ViewBag.Clientes = await taskObtenerTodosClientes;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            //*****************************************
            ViewBag.Error = "";
            return View();
        }


        /// <summary>  
        /// Metodo para crear un Cita haciendo Peticion a la API
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita con sus atributos llenos</param>  
        /// <returns>Retorna la vista con un objeto tipo Cita</returns>  
        /// 
        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cita pCita)
        {
            try
            {
                // Codigo agregar para consumir la Web API
                var response = await httpClient.PostAsJsonAsync("Cita", pCita);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pCita);
                }
                // ********************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                // Codigo agregar para consumir la Web API
                ViewBag.Clientes = await ObtenerClientesAsync();
                ViewBag.Servicios = await ObtenerServiciosAsync();
                //*****************************************
                return View(pCita);
            }
        }

        /// <summary>  
        /// Metodo para Modificar una Cita haciendo peticion a la API
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita el cual tenga el Id</param>  
        /// <returns>Retorna la vista con un objeto tipo Cita</returns>  
        /// 
        // GET: UsuarioController/Edit/5
        public async Task<IActionResult> Edit(Cita pCita)
        {
            // Codigo agregar para consumir la Web API
            var taskObtenerTodosClientes = ObtenerClientesAsync();
            var taskObtenerPorId = ObtenerCitaPorIdAsync(pCita);
            var taskObtenerTodosUsuarios = ObtenerUsuariosAsync();
            // ***********************************************
            var cita = await taskObtenerPorId;
            ViewBag.Clientes = await taskObtenerTodosClientes;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            ViewBag.Error = "";
            return View(cita);
        }

        /// <summary>  
        /// Metodo para Modificar una Cita haciendo peticion a la api
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita el cual tenga el Id</param>  
        /// <returns>Retorna la vista y un objeto tipo Cita</returns>  
        /// 
        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cita pCita)
        {
            try
            {
                // Codigo agregar para consumir la Web API
                var response = await httpClient.PutAsJsonAsync("Cita/" + id, pCita);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pCita);
                }
                // ************************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                // Codigo agregar para consumir la Web API
                ViewBag.Clientes = await ObtenerClientesAsync();
                ViewBag.Servicios = await ObtenerServiciosAsync();
                ViewBag.Usuarios = await ObtenerUsuariosAsync();
                //*****************************************
                return View(pCita);
            }
        }

        /// <summary>  
        /// Metodo Get de Delete haicnedo peticion a la API
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita el cual tenga el Id</param>  
        /// <returns>Retorna la vista y un objteo tipo Cita con sus valores llenos</returns>  
        /// 
        // GET: UsuarioController/Delete/5
        public async Task<IActionResult> Delete(Cita pCita)
        {
            // Codigo agregar para consumir la Web API
            Cita cita = await ObtenerCitaPorIdAsync(new Cita { Id = pCita.Id });
            cita.Cliente = await ObtenerClientePorIdAsync(new Cliente { Id = cita.IdCliente });
            //*******************************************************    
            ViewBag.Error = "";
            return View(cita);
        }

        /// <summary>  
        /// Metodo para Eliminar una Cita haiciendo peticion a la API
        /// </summary>  
        /// <param name="pCita">Se espera un objeto del Tipo Cita</param> 
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la vista y el objeto cita</returns>  
        /// 
        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Cita pCita)
        {
            try
            {
                // Codigo agregar para consumir la Web API
                var response = await httpClient.DeleteAsync("Cita/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pCita);
                }
                // **********************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                // Codigo agregar para consumir la Web API
                Cita cita = await ObtenerCitaPorIdAsync(new Cita { Id = pCita.Id });
                cita.Cliente = await ObtenerClientePorIdAsync(new Cliente { Id = cita.IdCliente });
                // ***************************************               
                return View(cita);
            }
        }
    }
}
