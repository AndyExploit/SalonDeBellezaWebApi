using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalonBelleza.EntidadesDeNegocio
{
    /// <summary>  
    /// Esta clase es de la entidad Cliente, se detallara los atributos y los DataAnnotation.  
    /// Esta clase contiene un Id que se usa como llave foranea por la Clase Cita.
    /// </summary> 
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El DUI obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Dui { get; set; }


        [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El apellido del cliente es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Apellido { get; set; }


        [Required(ErrorMessage = "El Teléfono es obligatorio")]
        [Display(Name = "Teléfono")]
        public int Numero { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public List<Cita> Cita { get; set; }
    }
}
