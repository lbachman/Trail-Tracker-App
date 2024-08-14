using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;
using Trail_Tracker_App.Data;

namespace Trail_Tracker_App.Pages.MyProfile
{
    public class ProfilePicModel : PageModel
    {
        private readonly MountaintrailsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public ProfilePicModel(MountaintrailsContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;

        }

        public Userprofile Userprofile { get; set; } = default!;

        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Userprofile = await _context.Userprofiles.FindAsync(id);
            if (Userprofile == null)
            {
                return NotFound();
            }

            var user = _context.Userprofiles.FirstOrDefault(x => x.UserId == Userprofile.UserId);

            return Page();
        }

        [BindProperty]
        public ProfileDTO ProfileDTO { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            

            if (ProfileDTO == null || ProfileDTO.PictureUpload == null)
            {
                ModelState.AddModelError("", "No file uploaded.");
                return Page();
            }

            if (ProfileDTO.PictureUpload != null && ProfileDTO.PictureUpload.Length > 0)
            {
                // set the file name variable
                var filePath = Path.Combine(_environment.WebRootPath, "Uploads/ProfilePictures", ProfileDTO.PictureUpload.FileName);
                string fileName = ("/Uploads/ProfilePictures/" + ProfileDTO.PictureUpload.FileName);


                // creates the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileDTO.PictureUpload.CopyToAsync(fileStream);
                }

                var username = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);
                var userToUpdate = await _context.Userprofiles.FirstOrDefaultAsync(x => x.UserId == user.Id);

                if (userToUpdate == null)
                {
                    return NotFound();
                }

                userToUpdate.ProfilePicture = fileName;


                _context.Attach(userToUpdate).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(Userprofile.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("./UserProfile");

            }
            else { return Page(); } 
        }

        private bool UserProfileExists(string id)
        {
            return _context.Userprofiles.Any(e => e.UserId == id);
        }
    }
}
