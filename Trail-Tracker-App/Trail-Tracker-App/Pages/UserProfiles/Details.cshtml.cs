using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.UserProfiles
{
    public class DetailsModel : PageModel
    {
        private readonly MountaintrailsContext _context;

        public DetailsModel(MountaintrailsContext context)
        {
            _context = context;
        }

        public Userprofile Userprofile { get; set; } = default!;

        public List<Picture> PictureList { get; set; } = default!;
        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userprofile = await _context.Userprofiles.FirstOrDefaultAsync(m => m.UserProfileId == id);




            // add get username by asp id here for profile name






            // get uploads of user
            var pictureList = await _context.Pictures.Where(x => x.UploadedBy == userprofile.UserId).ToListAsync();
            PictureList = pictureList;

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
