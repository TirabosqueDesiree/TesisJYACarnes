using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class DTODetallePed
    {
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public string Producto { get; set; }

        public string Marca { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "Ud debe seleccionar un {0}")]
        public decimal Precio { get; set; }

        public int Cantidad { get; set; }
    }
}