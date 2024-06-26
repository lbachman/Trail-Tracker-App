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
        private readonly MountaintrailsContext _context;

        public DetailsModel(MountaintrailsContext context)
        {
            _context = context;
        }

        public Picture Picture { get; set; } = default!;
        public Trail? Trail { get; set; } = default!;   

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Pictures.FirstOrDefaultAsync(m => m.PictureId == id);           
            var trail = await _context.Trails.FirstOrDefaultAsync(x => x.TrailId == picture.TrailId);
            if (picture == null)
            {
                return NotFound();
            }
            else
            {
                Picture = picture;
                Trail = trail;
            }
            return Page();
        }
    }
}
