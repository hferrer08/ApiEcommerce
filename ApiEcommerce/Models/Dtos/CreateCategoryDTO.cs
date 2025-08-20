using System;
using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.Models.Dtos;

public class CreateCategoryDTO
{

    [Required(ErrorMessage = "El Nombre es obligatorio.")]
    [MaxLength(50, ErrorMessage = "No puede tener más de 50 caracteres.")]
    [MinLength(3, ErrorMessage ="No puede tener menos de 3 caracteres.")]
    public String Name { get; set; } = string.Empty;
   
  
}
