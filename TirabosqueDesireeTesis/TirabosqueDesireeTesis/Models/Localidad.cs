using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class Localidad
    {
        [Key]
        public int IdLocalidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Localidad")]
        [Index("Localidad_Descripcion_Index", 2, IsUnique = true)]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Ud debe seleccionar una {0}")]
        [Index("Localidad_Descripcion_Index", 1, IsUnique = true)]
        public int IdProvincia { get; set; }

        public virtual Provincia Provincia { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}