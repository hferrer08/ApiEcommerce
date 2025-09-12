using System;

namespace ApiEcommerce.Models.Dtos;

public class ProductDTO
{

    public int ProductId { get; set; }

    public String Name { get; set; } = string.Empty;

    public String Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string? ImgUrl { get; set; }

    public string? ImgUrlLocal { get; set; } 



    public string SKU { get; set; } = string.Empty; //PROD-001-BLK-M


    public int Stock { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public DateTime? UpdateDate { get; set; } = null;

    //Relación con el modelo Category (Foránea)

    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
   
}
