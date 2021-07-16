using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Provincia")]
        [Index("Provincia_Descripcion_Index", IsUnique = true)]
        public string Descripcion { get; set; }

        public virtual ICollection<Localidad> Localidades { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

    }
}