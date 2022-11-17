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
    /// Esta clase es de la entidad Rol, se detallara los atributos y los DataAnnotation.  
    /// Esta clase contiene un Id que se usar como llave foranea por la Clase Usuario.
    /// </summary> 
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de rol es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Nombre { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }

        public List<Usuario> Usuario { get; set; }
    }
}
