using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models
{
   public class Product
   {
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }
      [Required]
      public string Name { get; set; }
      public string Details { get; set; }
      public string PhotoPath { get; set; }
      [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Purchase Date")]
      public DateTime PurchaseDate { get; set; }
      [ForeignKey("Category")]
      public int CategoryID { get; set; }


      public virtual Category Category { get; set; }
      [NotMapped]
      public IFormFile Photo { get; set; }

   }
}
