using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Producto")]
        //[Index("Producto_Descripcion_Index", IsUnique = true)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Ud debe seleccionar una {0}")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Ud debe seleccionar una {0}")]
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]

        [Range(0, double.MaxValue, ErrorMessage = "The field {0} can take values between {1} and {2}")]
        public decimal Precio { get; set; }

        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase ImagenFile { get; set; }


        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }

        [Display(Name = "Observaciones:")]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }


        public virtual Categoria Categoria { get; set; }

        public virtual Marca Marca { get; set; }

        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
        public virtual ICollection<PedidoDetalleTemp> PedidoDetalleTemps { get; set; }
    }
}