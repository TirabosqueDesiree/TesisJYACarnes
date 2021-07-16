using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class VMAgregarProducto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Ud debe seleccionar un {0}")]
        [Display(Name = "Producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        //[DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "You must enter greather than {1} values {0}")]
        [Display(Name = "Cantidad")]
        public double Quantity { get; set; }



    }
}