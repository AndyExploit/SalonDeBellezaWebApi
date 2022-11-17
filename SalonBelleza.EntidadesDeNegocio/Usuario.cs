using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// *****************************************************************
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonBelleza.EntidadesDeNegocio
{
    /// <summary>  
    /// Esta clase es de la entidad Usuario, se detallara los atributos y los DataAnnotation.  
    /// Esta clase contiene un enum para especificar el Estado del Usuario.
    /// Esta clase tiene una llave foranea llamada IdRol.
    /// </summary>   
    ///   
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Rol")]
        [Required(ErrorMessage = "Rol es obligatorio.")]
        [Display(Name = "Rol")]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El Dui es obligatorio.")]
        [StringLength(25, ErrorMessage = "Maximo 25 caracteres")]
        public string Dui { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Teléfono es obligatorio.")]
        [StringLength(10, ErrorMessage = "Maximo 10 caracteres")]
        [Display(Name = "Teléfono")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Login es Obligatorio.")]
        [StringLength(25, ErrorMessage = "Maximo 25 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "El Password es obligatorio. ")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "Password debe estar entre 5 y 32 caracteres", MinimumLength = 5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "El Estado es requerido.")]
        public byte Estado { get; set; }

        [Display(Name = "Fecha registro")]
        public DateTime FechaRegistro { get; set; }

        public Rol Rol { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; } //Cantidad de registros por consultas.

        [Required(ErrorMessage = "Confirmar el Password")]
        [StringLength(32, ErrorMessage = "Password debe estar entre 5 a 32 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password y Confirmar Password deben ser iguales")]
        [Display(Name = "Confirmar Password")]
        [NotMapped]
        public string ConfirmarPassword_aux { get; set; } //ConfirmarPassword sirve para comparar password con ConfirmarPassword. Si no son iguales muestra un ErrorMessage.

    }

    public enum Estado_Usuario          //Sirve para saber el estado del Usuario. Ya que en la base de Datos se guarda en Entero.
    {
        ACTIVO = 1,
        INACTIVO = 2
    }
}
