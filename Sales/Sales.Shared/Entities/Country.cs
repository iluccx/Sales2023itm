using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities  // las entidades las combierto en una base de datos
{
    public class Country
    {
        public int Id { get; set; } //codigo del pais

        [Required(ErrorMessage = "El campo {0} es obligatorio")] //El cero de reemplaza por el nombre del display o por el maxleng
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1}")] // el 1 se reemplaza por la cantidad de maxlengt
        [Display(Name = "País")]
        public string Name { get; set; } = null!;
    }
}
