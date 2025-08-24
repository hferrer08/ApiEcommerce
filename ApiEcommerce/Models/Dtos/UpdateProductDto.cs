using System;

namespace ApiEcommerce.Models.Dtos;

public class UpdateProductDto
{

  
    public String Name { get; set; } = string.Empty;

    public String Description { get; set; } = string.Empty;
   
    public decimal Price { get; set; }

    public string ImgUrl { get; set; } = string.Empty;

   

    public string SKU { get; set; } = string.Empty; //PROD-001-BLK-M

    
    public int Stock { get; set; }

   

    public DateTime? UpdateDate { get; set; } = null;

    //Relación con el modelo Category (Foránea)

    public int CategoryId { get; set; }
   
}
