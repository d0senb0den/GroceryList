using GroceryList.Data;
using GroceryList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GroceryList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GroceryListContext _context;

        public IndexModel(GroceryListContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public int Amount { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Purchased { get; set; }

        [BindProperty]
        public string UserID { get; set; }
        public List<GroceryItem> GroceryItems { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid itemdeleteid { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid? itemPurchasedId { get; set; }

        [BindProperty]
        public GroceryItem GroceryItemEdit { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            GroceryItems = await _context.GroceryItem.ToListAsync();
            if (itemdeleteid != Guid.Empty)
            {
                GroceryItem groceryItem = await _context.GroceryItem.FindAsync(itemdeleteid);
                if (groceryItem != null)
                {
                    _context.GroceryItem.Remove(groceryItem);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }
            if (itemPurchasedId is not null && Purchased)
            {
                var item = _context.GroceryItem.FirstOrDefault(g => g.ID == itemPurchasedId);
                item.Purchased = !item.Purchased;
                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            GroceryItem groceryItem = new GroceryItem();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            groceryItem.Name = Name;
            groceryItem.Amount = Amount;
            groceryItem.Purchased = Purchased;
            groceryItem.UserID = userId;

            _context.GroceryItem.Add(groceryItem);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Attach(GroceryItemEdit).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

    }
}
