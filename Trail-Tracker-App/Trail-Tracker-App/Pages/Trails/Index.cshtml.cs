using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Trails
{
    public class IndexModel : PageModel
    {
        private readonly MountaintrailsContext _context;

        public IndexModel(MountaintrailsContext context)
        {
            _context = context;
        }

        public IList<Trail> Trail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Trail = await _context.Trails.Include(t => t.Mountain).ToListAsync();

        }
    }
}
