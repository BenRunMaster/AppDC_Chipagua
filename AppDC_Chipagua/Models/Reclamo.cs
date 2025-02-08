using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDC_Chipagua.Models
{
    public class Reclamo
    {
        
        public int? IdReclamo { get; set; }


        [Required(ErrorMessage = "El nombre de proveedor es obligatorio")]
        public string? NombreProveedor { get; set; }

        [Required(ErrorMessage = "La dirección de proveedor es obligatorio")]
        public string? DireccionProveedor { get; set; }

        [Required(ErrorMessage = "Los nombres de consumidor son obligatorios")]
        [MaxLength(50, ErrorMessage = "Nombres consumidor longitud máxima 50 caracteres")]
        public string? NombresConsumidor { get; set; }


        [Required(ErrorMessage = "Los apellidos de consumidor son obligatorios")]
        [MaxLength(50,ErrorMessage = "Apellidos consumidor longitud máxima 50 caracteres")]
        public string? ApellidosConsumidor { get; set; }

        [Required(ErrorMessage = "El DUI es obligatorio")]
        [StringLength(10, MinimumLength =10, ErrorMessage ="El DUI debe tener formato 00000000-0")]
        [Column("DUI")]
        public string? DUI { get; set; }

        [Required(ErrorMessage = "El DUI es obligatorio")]
        public string? DetalleReclamo { get; set; }

        [Column("montoReclamado")]
        public decimal MontoReclamado { get; set; }

        [Column("telefono")]
        public string? Telefono { get; set; }

        [Column("fechaIngreso")]
        public DateTime FechaIngreso { get; set; }

        
        
    }
}
