using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonBelleza.EntidadesDeNegocio
{
    /// <summary>  
    /// Esta clase es de la entidad Servicio, se detallara los atributos y los DataAnnotation.  
    /// Esta clase contiene un Id que se usar como llave foranea por la Clase DetalleCita.
    /// </summary> 
    public class Servicio
    {
        // Prueba comentario :3
       [Key]
       public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre no puede estar vacio")]
        [MinLength(length: 3, ErrorMessage ="Minimo de 3 caracteres")]
        [MaxLength(length: 100, ErrorMessage = "Maximo de 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Descripcion no puede estar vacio")]
        [MinLength(length: 3, ErrorMessage = "Minimo de 3 caracteres")]
        [MaxLength(length: 250, ErrorMessage = "Maximo de 250 caracteres")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        //Editado precio de servicio a Precio
        [Required(ErrorMessage = "El campo Precio no puede estar vacio")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        //nuevo atributo agregado(Duracion)
        [Required(ErrorMessage = "El campo duracion no puede estar vacio")]
        [Display(Name = "Duracion")]
        public double Duracion { get; set; }


        [NotMapped]
        public int Top_Aux { get; set; }

        public List<DetalleCita> DetalleCita { get; set; } 

    }
}
