using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class VMEstadoYPedido
    {
        public int IdPedido { get; set; }

        public List<Estado> ListaEstados { get; set; }
    }
}