using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class OrdenParaEmpleado
    {
        public List<Pedido> Pedido { get; set; }
        
        public  Usuario Usuario { get; set; }


    }

}