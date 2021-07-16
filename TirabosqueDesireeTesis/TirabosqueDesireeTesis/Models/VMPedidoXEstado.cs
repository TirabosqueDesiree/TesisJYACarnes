using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class VMPedidoXEstado
    {
        public int IdEstado { get; set; }
       
        public List<DTOPed2> ListaPedidos { get; set; }
        
    }
}