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
    public class DetailsModel : PageModel
    {
        private readonly Trail_Tracker_App.Entities.MountaintrailsContext _context;

        public DetailsModel(Trail_Tracker_App.Entities.MountaintrailsContext context)
        {
            _context = context;
        }

        public Picture Picture { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Pictures.FirstOrDefaultAsync(m => m.PictureId == id);
            if (picture == null)
            {
                return NotFound();
            }
            else
            {
                Picture = picture;
            }
            return Page();
        }
    }
}
