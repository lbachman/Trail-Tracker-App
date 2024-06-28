using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trail_Tracker_App.Entities;
using Microsoft.AspNetCore.Identity;
using Trail_Tracker_App.Data;

namespace Trail_Tracker_App.Pages.UserProfiles
{
    public class CreateModel : PageModel
    {
        private readonly MountaintrailsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(MountaintrailsContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {        
            return Page();
        }

        [BindProperty]
        public UserprofileDTO Profile { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var aspUser = await _userManager.GetUserAsync(User);


            var user = new Userprofile()
            {
                UserId = aspUser.Id,
                Bio = Profile.Bio,
                ProfilePicture = Profile.ProfilePicture
            };

            _context.Userprofiles.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
