using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class DTOPed2
    {
        public int IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
    }
}