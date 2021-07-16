using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class VMPedido
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name ="Usuario")]
        public int IdUsuario { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaPedido { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }

        public List<PedidoDetalleTemp> DetalleTemps { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double TotalCantidad { get { return DetalleTemps == null ? 0 : DetalleTemps.Sum(d => d.Cantidad); } }

        //[DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal TotalValor { get { return DetalleTemps == null ? 0 : DetalleTemps.Sum(d => d.Valor); } }
    }
}