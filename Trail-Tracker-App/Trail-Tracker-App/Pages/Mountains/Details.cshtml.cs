using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Mountains
{
    public class DetailsModel : PageModel
    {
        private readonly MountaintrailsContext _context;

        public DetailsModel(MountaintrailsContext context)
        {
            _context = context;
        }

        public Mountain Mountain { get; set; } = default!;
        public Mountainrange Mountainrange { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mountain = await _context.Mountains.FirstOrDefaultAsync(m => m.MountainId == id);
            var mountainRange = await _context.Mountainranges.Where(x => mountain.RangeId == x.RangeId).FirstOrDefaultAsync();

            Console.WriteLine($"Mountain range: {mountainRange.Name}");

            if (mountain == null)
            {
                return NotFound();
            }
            else
            {
                Mountain = mountain;
                Mountainrange = mountainRange;
            }
            return Page();
        }
    }
}
