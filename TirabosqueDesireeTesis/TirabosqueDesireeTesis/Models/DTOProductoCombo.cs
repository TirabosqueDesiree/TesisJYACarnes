using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class DTOProductoCombo
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Producto{ get; set; }
    }
}