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
    public class IndexModel : PageModel
    {
        private readonly BackpackLib.Contexts.BackpackAppContext _context;

        public IndexModel(BackpackLib.Contexts.BackpackAppContext context)
        {
            _context = context;
        }

        public IList<Backpack> Backpack { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Backpack = await _context.Backpacks.ToListAsync();
        }
    }
}
