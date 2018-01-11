using System.ComponentModel.DataAnnotations;

namespace MVCPartialDemo.Models
{
    public class RequestStudentsModel
    {
        public string Oculto { get; set; }

        [Required(ErrorMessage = "Porfavor ingresa un número de estudiantes.")]
        [Range(0, 4, ErrorMessage = "Porfavor ingresa un número entre 0 y 4.")]
        [RegularExpression("^-?[0-9]*$", ErrorMessage = "Porfavor ingresa únicamente un valor numérico")]
        [Display(Name = "Cantidad de estudiantes")]
        public int Cantidad { get; set; }
    }
}