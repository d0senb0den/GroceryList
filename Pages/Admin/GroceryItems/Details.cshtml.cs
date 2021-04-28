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
    public class DetailsModel : PageModel
    {
        private readonly GroceryList.Data.GroceryListContext _context;

        public DetailsModel(GroceryList.Data.GroceryListContext context)
        {
            _context = context;
        }

        public GroceryItem GroceryItem { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroceryItem = await _context.GroceryItem.FirstOrDefaultAsync(m => m.ID == id);

            if (GroceryItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
