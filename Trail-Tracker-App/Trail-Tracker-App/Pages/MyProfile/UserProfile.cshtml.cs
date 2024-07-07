using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.MyProfile
{
    public class UserProfileModel : PageModel
    {
        private readonly MountaintrailsContext _context;

        public UserProfileModel(MountaintrailsContext context)
        {
            _context = context;
        }

        public Userprofile Userprofile { get; set; } = default!;

        public List<Picture> PictureList { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync()
        {
            // get user id of currently logged user to match pictures and other info
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // get user name of logged in user for heading
            var user = User.Identity.Name;

            if (userId == null)
            {
                return NotFound();
            }

            // list of pictures uploaded by logged in user
            var pictures = _context.Pictures.Where(x => x.UploadedBy == userId).ToList();
            PictureList = pictures;

            var userprofile = await _context.Userprofiles.FirstOrDefaultAsync(m => m.UserId == userId);
            if (userprofile == null)
            {
                return NotFound();
            }
            else
            {
                Userprofile = userprofile;
            }
            return Page();
        }
    }
}
