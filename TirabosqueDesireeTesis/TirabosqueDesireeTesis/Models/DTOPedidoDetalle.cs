using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class DTOPedidoDetalle
    {
        public int IdPedido { get; set; }

        public DateTime Fecha { get; set; }

        public string Usuario { get; set; }

        public string Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public int IdEstado { get; set; }

        public string Estado { get; set; }

    }
}