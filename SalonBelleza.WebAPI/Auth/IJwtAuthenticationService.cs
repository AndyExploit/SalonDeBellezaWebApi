 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Agregar la siguiente libreria.
using SalonBelleza.EntidadesDeNegocio;
//*************************************

namespace SalonBelleza.WebAPI.Auth
{
    public interface IJwtAuthenticationService
    {
        string Authenticate(Usuario pUsuario);


    }
}
