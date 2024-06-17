using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Pictures
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
        ViewData["TrailId"] = new SelectList(_context.Trails, "TrailId", "TrailId");
            return Page();
        }

        [BindProperty]
        public Picture Picture { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Pictures.Add(Picture);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
