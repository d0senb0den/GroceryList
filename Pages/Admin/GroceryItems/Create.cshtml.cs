using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GroceryList.Data;
using GroceryList.Models;
using System.Security.Claims;

namespace GroceryList.Pages.Admin.GroceryItems
{
    public class CreateModel : PageModel
    {
        private readonly GroceryList.Data.GroceryListContext _context;

        public CreateModel(GroceryList.Data.GroceryListContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public GroceryItem GroceryItem { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            GroceryItem.UserID = userId;

            _context.GroceryItem.Add(GroceryItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
