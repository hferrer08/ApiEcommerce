using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEcommerce.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    [Required]
    public String Name { get; set; } = string.Empty;

    public String Description { get; set; } = string.Empty;
    [Range(0, double.MaxValue)]

    public decimal Price { get; set; }

    public string ImgUrl { get; set; } = string.Empty;

    [Required]

    public string SKU { get; set; } = string.Empty; //PROD-001-BLK-M

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public DateTime? UpdateDate { get; set; } = null;

    //Relación con el modelo Category (Foránea)

    public int CategoryId { get; set; }
    [ForeignKey("Id")]

    public required Category Category { get; set; }

}
