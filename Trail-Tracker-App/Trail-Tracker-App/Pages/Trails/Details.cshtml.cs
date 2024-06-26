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
    public class DetailsModel : PageModel
    {
        private readonly MountaintrailsContext _context;

        public DetailsModel(MountaintrailsContext context)
        {
            _context = context;
        }

        public Trail Trail { get; set; } = default!;
        public Mountain Mountain { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            

            var trail = await _context.Trails.FirstOrDefaultAsync(m => m.TrailId == id);
            var mountain = await _context.Mountains.Where(x => x.MountainId == trail.MountainId).FirstOrDefaultAsync();
            var pictures = await _context.Pictures.Where(x => x.TrailId == id).ToListAsync();
            trail.Pictures = pictures;
            if (trail == null)
            {
                return NotFound();
            }
            else
            {
                Trail = trail;
                Mountain = mountain;
                Pictures = pictures;
            }
            return Page();
        }
    }
}
