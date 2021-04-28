using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroceryList.Data;
using GroceryList.Models;

namespace GroceryList.Pages.Admin.GroceryItems
{
    public class EditModel : PageModel
    {
        private readonly GroceryList.Data.GroceryListContext _context;

        public EditModel(GroceryList.Data.GroceryListContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(GroceryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryItemExists(GroceryItem.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GroceryItemExists(Guid id)
        {
            return _context.GroceryItem.Any(e => e.ID == id);
        }
    }
}
