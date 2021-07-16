using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class DTOUsuarioTotal
    {
        public int IdUsuario { get; set; }
        public string Cliente { get; set; }
        public decimal TotalPedido { get; set; }
    }
}