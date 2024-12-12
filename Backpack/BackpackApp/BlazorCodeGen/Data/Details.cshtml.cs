using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BackpackLib.Contexts;
using BackpackLib.Entities;

namespace BlazorCodeGen.Data
{
    public class DetailsModel : PageModel
    {
        private readonly BackpackLib.Contexts.BackpackAppContext _context;

        public DetailsModel(BackpackLib.Contexts.BackpackAppContext context)
        {
            _context = context;
        }

        public Backpack Backpack { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backpack = await _context.Backpacks.FirstOrDefaultAsync(m => m.BackpackId == id);
            if (backpack == null)
            {
                return NotFound();
            }
            else
            {
                Backpack = backpack;
            }
            return Page();
        }
    }
}
