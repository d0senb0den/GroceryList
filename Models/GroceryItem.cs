using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryList.Models
{
    public class GroceryItem
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool Purchased { get; set; }
        public string UserID { get; set; }
    }
}
