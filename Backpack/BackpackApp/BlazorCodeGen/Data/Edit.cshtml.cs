using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackpackLib.Contexts;
using BackpackLib.Entities;

namespace BlazorCodeGen.Data
{
    public class EditModel : PageModel
    {
        private readonly BackpackLib.Contexts.BackpackAppContext _context;

        public EditModel(BackpackLib.Contexts.BackpackAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Backpack Backpack { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backpack =  await _context.Backpacks.FirstOrDefaultAsync(m => m.BackpackId == id);
            if (backpack == null)
            {
                return NotFound();
            }
            Backpack = backpack;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Backpack).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BackpackExists(Backpack.BackpackId))
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

        private bool BackpackExists(int id)
        {
            return _context.Backpacks.Any(e => e.BackpackId == id);
        }
    }
}
