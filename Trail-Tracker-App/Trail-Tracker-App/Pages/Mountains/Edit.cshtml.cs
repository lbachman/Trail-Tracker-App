using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Mountains
{
    public class EditModel : PageModel
    {
        private readonly MountaintrailsContext _context;

        public EditModel(MountaintrailsContext context)
        {
            _context = context;
        }

        
        public Mountain Mountain { get; set; } = default!;

        [BindProperty]
        public MountainDTO MtDTO { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mountain = await _context.Mountains.FindAsync(id);
            if (Mountain == null)
            {
                return NotFound();
            }

            var range = _context.Mountainranges.FirstOrDefault(x => x.RangeId == Mountain.RangeId);

            MtDTO = new MountainDTO()
            {
                RangeName = range.Name,
                Name = Mountain.Name,
                Zip = Mountain.Zip,
                Height = Mountain.Height,
                Description = Mountain.Description
            };

            ViewData["Name"] = new SelectList(_context.Mountainranges, "Name", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var range = await _context.Mountainranges.FirstOrDefaultAsync(x => x.Name == MtDTO.RangeName);
            var mountainToUpdate = await _context.Mountains.FirstOrDefaultAsync(x => x.Name == MtDTO.Name);

            if (mountainToUpdate == null)
            {
                return NotFound();
            }

            mountainToUpdate.RangeId = range.RangeId;
            mountainToUpdate.Name = MtDTO.Name;
            mountainToUpdate.Zip = MtDTO.Zip;
            mountainToUpdate.Height = MtDTO.Height;
            mountainToUpdate.Description = MtDTO.Description;
            _context.Attach(mountainToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MountainExists(Mountain.MountainId))
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

        private bool MountainExists(int id)
        {
            return _context.Mountains.Any(e => e.MountainId == id);
        }
    }
}
