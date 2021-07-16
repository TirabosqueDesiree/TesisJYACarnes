using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Categoría")]
        [Index("Categoria_Descripcion_Index", IsUnique = true)]
        public string Descripcion { get; set; }

        public virtual ICollection<Marca> Marcas { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}