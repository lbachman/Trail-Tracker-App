using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Mountains
{
    public class CreateModel : PageModel
    {
        private readonly MountaintrailsContext _context;

        public CreateModel(MountaintrailsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        // coresponds to the viewbag thing
            ViewData["Name"] = new SelectList(_context.Mountainranges, "Name", "Name");
            return Page();
        }

        [BindProperty]
        public MountainDTO MountainDTO { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("error: model state is not valid");
                return Page();
                
            }
            
            // get mountain range name by id
            var range = _context.Mountainranges.Where(x => x.Name == MountainDTO.Range).FirstOrDefault();
            var mountain = new Mountain()
            {
                RangeId = range.RangeId,
                Name = MountainDTO.Name,
                Location = MountainDTO.Location,
                Height = MountainDTO.Height,
                Description = MountainDTO.Description
            };

            _context.Mountains.Add(mountain);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
