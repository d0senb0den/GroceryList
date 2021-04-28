using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroceryList.Data;
using GroceryList.Models;

namespace GroceryList.Pages.Admin.GroceryItems
{
    public class IndexModel : PageModel
    {
        private readonly GroceryListContext _context;

        public IndexModel(GroceryListContext context)
        {
            _context = context;
        }

        public IList<GroceryItem> GroceryItem { get;set; }

        public async Task OnGetAsync()
        {
            GroceryItem = await _context.GroceryItem.ToListAsync();
        }
    }
}
