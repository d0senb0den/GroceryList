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
    public class DeleteModel : PageModel
    {
        private readonly GroceryList.Data.GroceryListContext _context;

        public DeleteModel(GroceryList.Data.GroceryListContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroceryItem = await _context.GroceryItem.FindAsync(id);

            if (GroceryItem != null)
            {
                _context.GroceryItem.Remove(GroceryItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
