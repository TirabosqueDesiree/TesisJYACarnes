using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class DetallePedido
    {
        [Key]
        public int IdDetallePedido { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Estado")]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Estado")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Producto")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        //[DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Ud debe seleccionar un {0}")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Cantidad { get; set; }

        public virtual Pedido Pedido { get; set; }

        public virtual Producto Producto { get; set; }
    }
}