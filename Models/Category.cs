using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models
{
   public class Category
   {
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }
      [Required, Display(Name = "Category")]
      public string Name { get; set; }
      public virtual IList<Product> Products { get; set; }
   }
}
