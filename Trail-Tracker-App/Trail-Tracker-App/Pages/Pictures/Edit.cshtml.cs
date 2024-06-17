using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Pictures
{
    public class EditModel : PageModel
    {
        private readonly Trail_Tracker_App.Entities.MountaintrailsContext _context;

        public EditModel(Trail_Tracker_App.Entities.MountaintrailsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Picture Picture { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture =  await _context.Pictures.FirstOrDefaultAsync(m => m.PictureId == id);
            if (picture == null)
            {
                return NotFound();
            }
            Picture = picture;
           ViewData["TrailId"] = new SelectList(_context.Trails, "TrailId", "TrailId");
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

            _context.Attach(Picture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(Picture.PictureId))
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

        private bool PictureExists(int id)
        {
            return _context.Pictures.Any(e => e.PictureId == id);
        }
    }
}
