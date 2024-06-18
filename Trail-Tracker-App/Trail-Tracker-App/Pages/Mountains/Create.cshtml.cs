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
        //  ViewData["RangeId"] = new SelectList(_context.Mountainranges, "RangeId", "RangeId");
        //    return Page();
        // coresponds to the viewbag thing
            ViewData["Name"] = new SelectList(_context.Mountainranges, "Name", "Name");
            return Page();
        }

        
        public Mountain Mountain { get; set; } = default!;

        [BindProperty]
        public MountainDTO MountainDTO { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
                Console.WriteLine("error");
            }

            

            _context.Mountains.Add(Mountain);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
