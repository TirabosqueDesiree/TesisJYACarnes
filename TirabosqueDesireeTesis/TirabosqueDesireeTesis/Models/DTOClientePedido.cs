using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class DTOClientePedido
    {
        public int IdUsuario { get; set; }
        public int IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio{ get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
    }
}