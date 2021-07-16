using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TirabosqueDesireeTesis.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(256, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "E-Mail")]
        [Index("User_UserName_Index", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Apellido")]

        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Usuario")]
        public string NombreCompleto { get { return string.Format("{0} {1}", Apellido, Nombre); } }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }


        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase ImagenFile { get; set; }


        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string Imagen { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Ud debe seleccionar una {0}")]
        [Display(Name = "Provincia")]

        public int IdProvincia { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Ud debe seleccionar una {0}")]
        [Display(Name = "Localidad")]

        public int IdLocalidad { get; set; }


        public virtual Provincia Provincia { get; set; }
        public virtual Localidad Localidad { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }

    }
}