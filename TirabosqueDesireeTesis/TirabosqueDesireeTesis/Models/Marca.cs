using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class Marca
    {
        [Key]
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Marca")]
        [Index("Marca_Descripcion_Index", 2, IsUnique = true)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Ud debe seleccionar una {0}")]
        [Index("Marca_Descripcion_Index", 1, IsUnique = true)]
        public int IdCategoria { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}