using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sales.Shared.Entities
{
    public class City
    {
        public int Id { get; set; } //codigo del pais

        [Required(ErrorMessage = "El campo {0} es obligatorio")] //El cero de reemplaza por el nombre del display o por el maxleng
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1}")] // el 1 se reemplaza por la cantidad de maxlengt
        [Display(Name = "Ciudad")]
        public string Name { get; set; } = null!;

        public int StateId { get; set; }

        //1 estado pertenece a 1 pais. Automaticamente se relaciona
        public State? State { get; set; }
    }
}
