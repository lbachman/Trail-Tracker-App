using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BackpackLib.Contexts;
using BackpackLib.Entities;

namespace BlazorCodeGen.Data
{
    public class CreateItemModel : PageModel
    {
        private readonly BackpackAppContext _context;

        public CreateItemModel(BackpackAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BackpackId"] = new SelectList(_context.Backpacks, "BackpackId", "Name");
        ViewData["SubbagId"] = new SelectList(_context.SubBags, "SubbagId", "Name");
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Items.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
