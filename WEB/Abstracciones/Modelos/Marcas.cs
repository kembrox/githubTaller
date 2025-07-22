using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Marca
    {
        [Required(ErrorMessage = " El Id Es necesario")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Se requiere un nombre")]
        public string Nombre { get; set; }
    }
}
