using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class Mensaje
    {
        [Key]
        public int IdMensaje { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Nombre")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(256, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Asunto")]
        public string Asunto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Mensaje")]
        [DataType(DataType.MultilineText)]
        public string Consulta { get; set; }



    }


}