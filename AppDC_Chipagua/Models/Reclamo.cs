using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDC_Chipagua.Models
{
    public class Reclamo
    {
        
        public int? IdReclamo { get; set; }


        [Required(ErrorMessage = "El nombre de proveedor es obligatorio")]
        [StringLength(50, ErrorMessage = "Nombre de proveedor longitud máxima 50 caracteres")]
        public string? NombreProveedor { get; set; }

        [Required(ErrorMessage = "La dirección de proveedor es obligatorio")]
        [StringLength(50, ErrorMessage = "Dirección proveedor longitud máxima 50 caracteres")]
        public string? DireccionProveedor { get; set; }

        [Required(ErrorMessage = "Los nombres de consumidor son obligatorios")]
        [StringLength(50, ErrorMessage = "Nombres consumidor longitud máxima 50 caracteres")]
        public string? NombresConsumidor { get; set; }


        [Required(ErrorMessage = "Los apellidos de consumidor son obligatorios")]
        [StringLength(50, ErrorMessage = "Apellidos consumidor longitud máxima 50 caracteres")]
        public string? ApellidosConsumidor { get; set; }

        [Required(ErrorMessage = "El DUI es obligatorio")]
        [StringLength(10, MinimumLength =10, ErrorMessage ="El DUI debe tener formato 00000000-0")]
        [Column("DUI")]
        public string? DUI { get; set; }

        [Required(ErrorMessage = "El detalle del reclamo es obligatorio")]
        [StringLength(250, ErrorMessage = "Detalle de reclamo longitud máxima 250 caracteres")]
        public string? DetalleReclamo { get; set; }

        [Required(ErrorMessage = "El monto del reclamo es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage ="Monto no debe ser menor a $0.01")]
        public decimal MontoReclamado { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(10, MinimumLength =8, ErrorMessage = "Logitud minima de teléfono 8 caracteres, máxima 10")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        
        
    }
}
