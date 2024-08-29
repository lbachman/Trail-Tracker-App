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
        private readonly MountaintrailsContext _context;

        public EditModel(MountaintrailsContext context)
        {
            _context = context;
        }

        
        [BindProperty]
        public PictureEditDTO PictureEditDTO { get; set; } = default!;

        public Picture Picture { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture =  await _context.Pictures.FirstOrDefaultAsync(m => m.PictureId == id);

            var trail = await _context.Trails.FirstOrDefaultAsync(x => x.TrailId == picture.TrailId);

            if (picture == null)
            {
                return NotFound();
            }
            Picture = picture;

            PictureEditDTO = new PictureEditDTO()
            {
                TrailName = trail.Name,
                PictureId = id
            };

            ViewData["TrailName"] = new SelectList(_context.Trails, "Name", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var trail = await _context.Trails.FirstOrDefaultAsync(x => x.Name == PictureEditDTO.TrailName);
            Console.WriteLine($"PictureId: {PictureEditDTO.PictureId}");
            var picture = await _context.Pictures.FirstOrDefaultAsync(y => y.PictureId == PictureEditDTO.PictureId);

            picture.TrailId = trail.TrailId;
            _context.Attach(picture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(picture.PictureId))
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
