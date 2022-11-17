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
    /// Esta clase es de la entidad DetalleCita, se detallara los atributos y los DataAnnotation.  
    /// Esta clase contiene una llave foranea de las Clases Cita y Servicio.
    /// Posee un Enum para detallar el tipo de Accion a realisar si sera Crear, Modificar, Eliminar.
    /// </summary> 
    public class DetalleCita
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cita")]
        [Required(ErrorMessage = "El campo Id Cita no puede estar vacio")]
        [Display(Name = "Cita")]
        public int IdCita { get; set; }

        [ForeignKey("Servicio")]
        [Required(ErrorMessage = "El campo Id Servicio no puede estar vacio")]
        [Display(Name = "Servicio")]
        public int IdServicio { get; set; }

        //Agregando nuevo atributo(Precio)
        [Required(ErrorMessage = "El campo precio no puede estar vacio")]
        [Display(Name = "Precio")]
        public Decimal Precio { get; set; }

        //Agregando nuevo atributo(Duracion)
        [Required(ErrorMessage = "El campo duracion no puede estar vacio")]
        [Display(Name = "Duracion")]
        public double Duracion { get; set; }



        public Cita Cita { get; set; }
        public Servicio Servicio { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        [NotMapped]
        public byte TipoAccion_Aux { get; set; }
        public enum TipoAccion 
        {
            NUEVO=1,
            MODIFICAR=2,
            ELIMINAR=3,

        }

    }
}
