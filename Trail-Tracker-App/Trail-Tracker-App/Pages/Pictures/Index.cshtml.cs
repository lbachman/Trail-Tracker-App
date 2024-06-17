using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Pictures
{
    public class IndexModel : PageModel
    {
        private readonly Trail_Tracker_App.Entities.MountaintrailsContext _context;

        public IndexModel(Trail_Tracker_App.Entities.MountaintrailsContext context)
        {
            _context = context;
        }

        public IList<Picture> Picture { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Picture = await _context.Pictures
                .Include(p => p.Trail).ToListAsync();
        }
    }
}
