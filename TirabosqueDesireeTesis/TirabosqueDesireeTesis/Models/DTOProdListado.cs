using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class DTOProdListado
    {
        public string Descripcion { get; set; }
       
        public string Marca { get; set; }
        public decimal Precio { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImagenFile { get; set; }


        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
    }
}