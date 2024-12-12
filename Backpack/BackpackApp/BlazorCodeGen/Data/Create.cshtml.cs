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
    public class CreateModel : PageModel
    {
        private readonly BackpackLib.Contexts.BackpackAppContext _context;

        public CreateModel(BackpackLib.Contexts.BackpackAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Backpack Backpack { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Backpacks.Add(Backpack);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
