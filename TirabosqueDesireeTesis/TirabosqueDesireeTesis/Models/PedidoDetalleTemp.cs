using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class PedidoDetalleTemp
    {
        [Key]
        public int IdPedidoDetalleTemp { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(256, ErrorMessage = "The filed {0} must be maximun {1} characters length")]
        public string UserName { get; set; }


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

        public decimal Valor { get { return Precio * (decimal)Cantidad; } }

        public virtual Producto Producto { get; set; }
    }
}