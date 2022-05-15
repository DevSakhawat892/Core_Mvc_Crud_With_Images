using ProductManagement.Models;
using System.Collections.Generic;

namespace ProductManagement.ViewModel
{
   public class CatProductVM
   {
      public Category CategoryEntity { get; set; }
      public IList<Product> Products { get; set; }
   }
}
