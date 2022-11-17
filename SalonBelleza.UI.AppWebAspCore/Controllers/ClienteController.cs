using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/********************************/
using SalonBelleza.EntidadesDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

// Libreria necesarias para consumir la Web API
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Security.Claims; // seguridad por token
using System.Net.Http.Headers; // seguridad por token
//**********************************************

namespace SalonBelleza.UI.AppWebAspCore.Controllers 
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ClienteController : Controller
    {

        // Codigo agregar para consumir la Web API
        private readonly HttpClient httpClient;
        public ClienteController(HttpClient client)
        {
            httpClient = client;
        }


        /// <summary>  
        /// Metodo para Obtener por Id un Servicio haicnedo peticion a la API
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente el cual tenga el Id</param>  
        /// <returns>Objeto tipo usuario con sus atributos llenos</returns>  
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

        /// <summary>  
        /// Metodo para Refrescar el Token
        /// </summary>    
        /// 
        private void RefrescarToken()
        {
            var claimExpired = User.FindFirst(ClaimTypes.Expired);
            if (claimExpired != null)
            {
                var token = claimExpired.Value;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
        //***************************************

        /// <summary>  
        /// Metodo Index para obtener toda una Lista de Usuario y Roles al hacer Peticion a la API
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente con sus parametros llenos</param>  
        /// <returns>Retorna la vista Index con objeto Cliente</returns>  
        /// 
        // GET: RolController
        public async Task<IActionResult> Index(Cliente pCliente = null)
        {
            RefrescarToken();
            if (pCliente == null)
                pCliente = new Cliente();
            if (pCliente.Top_Aux == 0)
                pCliente.Top_Aux = 10;
            else if (pCliente.Top_Aux == -1)
                pCliente.Top_Aux = 0;
            // Codigo agregar para consumir la Web API
            var clientes = new List<Cliente>();
            var response = await httpClient.PostAsJsonAsync("Cliente/Buscar", pCliente);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Usuario", "Login");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                clientes = JsonSerializer.Deserialize<List<Cliente>>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            //******************************************
            ViewBag.Top = pCliente.Top_Aux;
            return View(clientes);
        }

        /// <summary>  
         /// Metodo para obtener detalles de Cliente haicnedo peticion a la API
         /// </summary>  
         /// <param name="id">Se espera una variable int la cual contenga el Id del registro a observar</param>  
         /// <returns>Retorna la vista con un objeto cliente</returns>  
         /// 
        // GET: RolController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            // Codigo agregar para consumir la Web API
            RefrescarToken();
            Cliente cliente = await ObtenerClientePorIdAsync(new Cliente { Id = id });
            //*******************************************************
            return View(cliente);
        }

        /// <summary>  
        /// Metodo para Get de la Vista Create
        /// </summary>   
        /// <returns>Retorna la vista de Create</returns>  
        /// 
        // GET: RolController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        /// <summary>  
        /// Metodo para crear un Cliente haciendo Peticion a la API
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Usuario con sus atributos llenos</param>  
        /// <returns>Retorna la vista con un objeto tipo usuario</returns>  
        /// 
        // POST: RolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente pCliente)
        {
            try
            {
                // Codigo agregar para consumir la Web API
                RefrescarToken();
                var response = await httpClient.PostAsJsonAsync("Cliente", pCliente);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Usuario", "Login");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pCliente);
                }
                // ********************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCliente);
            }
        }


        /// <summary>  
        /// Metodo para Modificar un Cliente haciendo peticion a la API
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente el cual tenga el Id</param>  
        /// <returns>Retorna la vista con un objeto tipo cliente</returns>  
        /// 
        // GET: RolController/Edit/5
        public async Task<IActionResult> Edit(Cliente pCliente)
        {
            // Codigo agregar para consumir la Web API
            RefrescarToken();
            Cliente cliente = await ObtenerClientePorIdAsync(pCliente);
            // ***********************************************
            ViewBag.Error = "";
            return View(cliente);
        }


        /// <summary>  
        /// Metodo para Modificar un Cliente haciendo peticion a la api
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente el cual tenga el Id</param>  
        /// <returns>Retorna la vista y un objeto tipo Cliente</returns>  
        /// 
        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente pCliente)
        {
            try
            {
                // Codigo agregar para consumir la Web API
                RefrescarToken();
                var response = await httpClient.PutAsJsonAsync("Cliente/" + id, pCliente);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Usuario", "Login");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pCliente);
                }
                // ************************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCliente);
            }
        }


        /// <summary>  
        /// Metodo Get de Delete haicnedo peticion a la API
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Usuario el cual tenga el Id</param>  
        /// <returns>Retorna la vista y un objteo tipo Cliente con sus valores llenos</returns>  
        /// 
        // GET: RolController/Delete/5
        public async Task<IActionResult> Delete(Cliente pCliente)
        {
            ViewBag.Error = "";
            // Codigo agregar para consumir la Web API
            RefrescarToken();
            Cliente cliente = await ObtenerClientePorIdAsync(pCliente);
            // ************************************************
            return View(cliente);
        }


        /// <summary>  
        /// Metodo para Eliminar un Cliente haiciendo peticion a la API
        /// </summary>  
        /// <param name="pCliente">Se espera un objeto del Tipo Cliente</param> 
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la vista y el objeto cliente</returns>  
        /// 
        // POST: RolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Cliente pCliente)
        {
            try
            {
                // Codigo agregar para consumir la Web API
                RefrescarToken();
                var response = await httpClient.DeleteAsync("Cliente/" + id);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Usuario", "Login");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pCliente);
                }
                // **********************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCliente);
            }
        }
    }
}

