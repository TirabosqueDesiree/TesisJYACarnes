using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class VMClientePedidoReporte
    {
        public int IdUsuario { get; set; }
        public List<DTOClientePedido> ListaClientePedidos { get; set; }
        public List<DTOUsuario> ListaUsuarios { get; set; }

    }
}