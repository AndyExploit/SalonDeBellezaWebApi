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
    /// Esta clase es de la entidad Cita, se detallara los atributos y los DataAnnotation.  
    /// Esta clase contiene un Enum para detallar el Estado de la Cita.
    /// Asi mismo esta clase contiene dos llaves foraneas las cuales son IdUsuario, IdCliente.
    /// 
    /// </summary> 
    public class Cita
    {
        [Key]
        public int Id { get; set; }
        //llave primaria*************
        [ForeignKey("Usuario")]
        [Required(ErrorMessage ="Usuario es obligatorio")]
        [Display(Name ="Empleado")]
        public int IdUsuario { get; set; }

        [ForeignKey("Cliente")]
        [Required(ErrorMessage = "Cliente es obligatorio")]
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        ////llave foreanea agregada
        //[ForeignKey("DetalleCita")]
        //[Required(ErrorMessage = "Detalle Cita es obligatorio")]
        //[Display(Name = "Detalle Cita")]
        //public int IdDetalleCita { get; set; }

        [Required(ErrorMessage = "La Fecha registrada es obligatoria")]
        [Display(Name ="Fecha Registrada")]
        public DateTime FechaRegistrada { get; set; }

        [Required(ErrorMessage = "La Fecha de la cita es obligatoria")]
        [Display(Name = "Fecha Cita")]
        public DateTime FechaCita { get; set; }

        [Required(ErrorMessage ="El total es obligatorio")]
        public Decimal Total { get; set; }
        
        [Required(ErrorMessage ="El Estado es obligatorio")]
        public byte Estado { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; } //Esta propiedad sirve para filtrar los datos mostrados en pantalla

        public Usuario Usuario { get; set; }

        public Cliente Cliente { get; set; }

        //Relacion de uno a muchos
        public List<DetalleCita> DetalleCita { get; set; }
    }

    public enum Estado_Cita
    {
        PENDIENTE = 1,
        REALIZADA = 2
    }
}
