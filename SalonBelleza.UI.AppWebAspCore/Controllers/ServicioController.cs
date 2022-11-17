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
    public class ServicioController : Controller
    {
        // Codigo agregar para consumir la Web API
        private readonly HttpClient httpClient;
        public ServicioController(HttpClient client)
        {
            httpClient = client;
        }

        /// <summary>  
        /// Metodo para Obtener por Id un Servicio haicnedo peticion a la API
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio el cual tenga el Id</param>  
        /// <returns>Objeto tipo cliente con sus atributos llenos</returns>  
        /// 

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
        /// Metodo Index para obtener toda una Lista de Servicio al hacer Peticion a la API
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio con sus parametros llenos</param>  
        /// <returns>Retorna la vista Index con objeto Servicio</returns>  
        /// 
        // GET: ServicioController
        public async Task<IActionResult> Index(Servicio pServicio = null)
        {
            RefrescarToken();
            if (pServicio == null)
                pServicio = new Servicio();
            if (pServicio.Top_Aux == 0)
                pServicio.Top_Aux = 10;
            else if (pServicio.Top_Aux == -1)
                pServicio.Top_Aux = 0;
            // Codigo agregar para consumir la Web API
            var servicios = new List<Servicio>();
            var response = await httpClient.PostAsJsonAsync("Servicio/Buscar", pServicio);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Usuario", "Login");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                servicios = JsonSerializer.Deserialize<List<Servicio>>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            //******************************************
            ViewBag.Top = pServicio.Top_Aux;
            return View(servicios);
        }

        /// <summary>  
        /// Metodo para obtener detalles de Servicio haicnedo peticion a la API
        /// </summary>  
        /// <param name="id">Se espera una variable int la cual contenga el Id del registro a observar</param>  
        /// <returns>Retorna la vista con un objeto servicio</returns>  
        /// 
        // GET: ServicioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            // Codigo agregar para consumir la Web API
            RefrescarToken();
            Servicio servicio = await ObtenerServicioPorIdAsync(new Servicio { Id = id });
            //*******************************************************
            return View(servicio);
        }


        /// <summary>  
        /// Metodo para Get de la Vista Create
        /// </summary>   
        /// <returns>Retorna la vista de Create</returns>  
        /// 
        // GET: ServicioController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }


        /// <summary>  
        /// Metodo para crear un Servicio haciendo Peticion a la API
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio con sus atributos llenos</param>  
        /// <returns>Retorna la vista con un objeto tipo servicio</returns>  
        /// 
        // POST: ServicioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servicio pServicio)
        {
            try
            {
                RefrescarToken();
                // Codigo agregar para consumir la Web API
                var response = await httpClient.PostAsJsonAsync("Servicio", pServicio);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Usuario", "Login");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pServicio);
                }
                // ********************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pServicio);
            }
        }

        /// <summary>  
        /// Metodo para Modificar un Servicio haciendo peticion a la API
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio el cual tenga el Id</param>  
        /// <returns>Retorna la vista con un objeto tipo servicio/returns>  
        /// 
        // GET: RolController/Edit/5
        public async Task<IActionResult> Edit(Servicio pServicio)
        {
            // Codigo agregar para consumir la Web API
            RefrescarToken();
            Servicio servicio = await ObtenerServicioPorIdAsync(pServicio);
            // ***********************************************
            ViewBag.Error = "";
            return View(servicio);
        }

        /// <summary>  
        /// Metodo para Modificar un Servicio haciendo peticion a la api
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio el cual tenga el Id</param>  
        /// <returns>Retorna la vista y un objeto tipo servicio</returns>  
        /// 
        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servicio pServicio)
        {
            try
            {
                // Codigo agregar para consumir la Web API
                RefrescarToken();
                var response = await httpClient.PutAsJsonAsync("Servicio/" + id, pServicio);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Usuario", "Login");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pServicio);
                }
                // ************************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pServicio);
            }
        }

        /// <summary>  
        /// Metodo Get de Delete haicnedo peticion a la API
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio el cual tenga el Id</param>  
        /// <returns>Retorna la vista y un objteo tipo Servicio con sus valores llenos</returns>  
        ///
        // GET: RolController/Delete/5
        public async Task<IActionResult> Delete(Servicio pServicio)
        {
            ViewBag.Error = "";
            // Codigo agregar para consumir la Web API
            RefrescarToken();
            Servicio servicio = await ObtenerServicioPorIdAsync(pServicio);
            // ************************************************
            return View(servicio);
        }

        /// <summary>  
        /// Metodo para Eliminar un Servicio haiciendo peticion a la API
        /// </summary>  
        /// <param name="pServicio">Se espera un objeto del Tipo Servicio</param> 
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la vista y el objeto servicio</returns>  
        /// 
        // POST: RolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Servicio pServicio)
        {
            try
            {
                RefrescarToken();
                // Codigo agregar para consumir la Web API
                var response = await httpClient.DeleteAsync("Servicio/" + id);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Usuario", "Login");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pServicio);
                }
                // **********************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pServicio);
            }
        }
    }
}
