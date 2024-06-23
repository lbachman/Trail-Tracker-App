using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.UserProfiles
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
        ViewData["UserName"] = new SelectList(_context.Aspnetusers, "UserName", "UserName");
            return Page();
        }

        [BindProperty]
        UserprofileDTO Profile { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // get id by username
            var userId = _context.Aspnetusers.Where(x => x.UserName == Profile.UserName).FirstOrDefault();

            var user = new Userprofile()
            {
                UserId = userId.Id,
                Bio = Profile.Bio,
                ProfilePicture = Profile.ProfilePicture
            };

            _context.Userprofiles.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
