using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Trails
{
    public class CreateModel : PageModel
    {
        private readonly Trail_Tracker_App.Entities.MountaintrailsContext _context;

        public CreateModel(Trail_Tracker_App.Entities.MountaintrailsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MountainName"] = new SelectList(_context.Mountains, "Name", "Name");
            return Page();
        }

        [BindProperty]
        public TrailDTO TrailDTO { get; set; } = default!;
       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // get id by mountain name
            var mountain = _context.Mountains.Where(x => x.Name == TrailDTO.MountainName).FirstOrDefault();

            Trail trail = new Trail()
            {
                MountainId = mountain.MountainId,
                Name = TrailDTO.Name,
                Difficulty = TrailDTO.Difficulty,
                Length = TrailDTO.Length,
                Description = TrailDTO.Description,
            };

            _context.Trails.Add(trail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
