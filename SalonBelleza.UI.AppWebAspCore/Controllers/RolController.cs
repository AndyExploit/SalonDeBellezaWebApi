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
    public class RolController : Controller
    {
        // Codigo agregar para consumir la Web API
        private readonly HttpClient httpClient;
        public RolController(HttpClient client)
        {
            httpClient = client;
        }

        /// <summary>  
        /// Metodo para Obtener por Id un Rol haicnedo peticion a la API
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol el cual tenga el Id</param>  
        /// <returns>Objeto tipo Rol con sus atributos llenos</returns>  
        /// 
        private async Task<Rol> ObtenerRolPorIdAsync(Rol pRol)
        {
            Rol rol = new Rol();
            var response = await httpClient.GetAsync("Rol/" + pRol.Id);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                rol = JsonSerializer.Deserialize<Rol>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return rol;
        }
        private void RefrescarToken()
        {
            var claimExpired = User.FindFirst(ClaimTypes.Expired);
            if (claimExpired != null)
            {
                var token = claimExpired.Value;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        /// <summary>  
        /// Metodo Index para obtener toda una Lista de Roles al hacer Peticion a la API
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol con sus parametros llenos</param>  
        /// <returns>Retorna la vista Index con objeto Rol</returns>  
        /// 
        // GET: RolController
        public async Task<IActionResult> Index(Rol pRol = null)
        {
            RefrescarToken();
            if (pRol == null)
                pRol = new Rol();
            if (pRol.Top_Aux == 0)
                pRol.Top_Aux = 10;
            else if (pRol.Top_Aux == -1)
                pRol.Top_Aux = 0;
            // Codigo agregar para consumir la Web API
            var roles = new List<Rol>();
            var response = await httpClient.PostAsJsonAsync("Rol/Buscar", pRol);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Usuario", "Login");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                roles = JsonSerializer.Deserialize<List<Rol>>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            //******************************************
            ViewBag.Top = pRol.Top_Aux;
            return View(roles);
        }

        /// <summary>  
        /// Metodo para obtener detalles de Rol haicnedo peticion a la API
        /// </summary>  
        /// <param name="id">Se espera una variable int la cual contenga el Id del registro a observar</param>  
        /// <returns>Retorna la vista con un objeto  Rol</returns>  
        /// 
        // GET: RolController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            // Codigo agregar para consumir la Web API
            RefrescarToken();
            Rol rol = await ObtenerRolPorIdAsync(new Rol { Id = id });
            //*******************************************************
            return View(rol);
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
        /// Metodo para crear un Rol haciendo Peticion a la API
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol con sus atributos llenos</param>  
        /// <returns>Retorna la vista con un objeto tipo Rol</returns>  
        /// 
        // POST: RolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rol pRol)
        {
            try
            {
                // Codigo agregar para consumir la Web API
                RefrescarToken();
                var response = await httpClient.PostAsJsonAsync("Rol", pRol);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Usuario", "Login");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pRol);
                }
                // ********************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }

        /// <summary>  
        /// Metodo para Modificar un Rol haciendo peticion a la API
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol el cual tenga el Id</param>  
        /// <returns>Retorna la vista con un objeto tipo Rol</returns>  
        /// 
        // GET: RolController/Edit/5
        public async Task<IActionResult> Edit(Rol pRol)
        {
            // Codigo agregar para consumir la Web API
            RefrescarToken();
            Rol rol = await ObtenerRolPorIdAsync(pRol);
            // ***********************************************
            ViewBag.Error = "";
            return View(rol);
        }

        /// <summary>  
        /// Metodo para Modificar un Rol haciendo peticion a la api
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol el cual tenga el Id</param>  
        /// <returns>Retorna la vista y un objeto tipo Rol</returns>  
        /// 
        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rol pRol)
        {
            try
            {
                // Codigo agregar para consumir la Web API
                RefrescarToken();
                var response = await httpClient.PutAsJsonAsync("Rol/" + id, pRol);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Usuario", "Login");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pRol);
                }
                // ************************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }

        /// <summary>  
        /// Metodo Get de Delete haciendo peticion a la API
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol el cual tenga el Id</param>  
        /// <returns>Retorna la vista y un objteo tipo Rol con sus valores llenos</returns>  
        /// 
        // GET: RolController/Delete/5
        public async Task<IActionResult> Delete(Rol pRol)
        {
            ViewBag.Error = "";
            // Codigo agregar para consumir la Web API
            RefrescarToken();
            Rol rol = await ObtenerRolPorIdAsync(pRol);
            // ************************************************
            return View(rol);
        }

        /// <summary>  
        /// Metodo para Eliminar un Rol haiciendo peticion a la API
        /// </summary>  
        /// <param name="pRol">Se espera un objeto del Tipo Rol</param> 
        /// <param name="id">Se espera un entero el cual contenga el Id</param> 
        /// <returns>Retorna la vista y el objeto rol</returns>  
        /// 
        // POST: RolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Rol pRol)
        {
            try
            {
                // Codigo agregar para consumir la Web API
                RefrescarToken();
                var response = await httpClient.DeleteAsync("Rol/" + id);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Usuario", "Login");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Sucedio un error al consumir la WEP API";
                    return View(pRol);
                }
                // **********************************************
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRol);
            }
        }
    }
}