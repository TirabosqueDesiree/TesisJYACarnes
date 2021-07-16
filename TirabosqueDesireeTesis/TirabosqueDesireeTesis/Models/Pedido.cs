using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaPedido { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Ud debe seleccionar un {0}")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}