using System;

namespace ApiEcommerce.Models.Dtos;

public class CategoryDto
{
 
    public int Id { get; set; }
   
    public String Name { get; set; } = string.Empty;
   
    public DateTime CreationDate { get; set; }
}
