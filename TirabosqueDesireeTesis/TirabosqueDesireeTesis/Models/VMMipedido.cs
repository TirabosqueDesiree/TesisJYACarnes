using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class VMMipedido
    {
        public List<DTOPed2> DTOPed2 { get; set; }

        public List<DTODetallePed> Detalle { get; set; }
    }
}