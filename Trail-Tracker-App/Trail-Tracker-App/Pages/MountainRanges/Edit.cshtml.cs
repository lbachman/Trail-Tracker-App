using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.MountainRanges
{
    public class EditModel : PageModel
    {
        private readonly Trail_Tracker_App.Entities.MountaintrailsContext _context;

        public EditModel(Trail_Tracker_App.Entities.MountaintrailsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mountainrange Mountainrange { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mountainrange =  await _context.Mountainranges.FirstOrDefaultAsync(m => m.RangeId == id);
            if (mountainrange == null)
            {
                return NotFound();
            }
            Mountainrange = mountainrange;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Mountainrange).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MountainrangeExists(Mountainrange.RangeId))
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

        private bool MountainrangeExists(int id)
        {
            return _context.Mountainranges.Any(e => e.RangeId == id);
        }
    }
}
