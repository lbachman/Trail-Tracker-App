using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.MyProfile
{
    public class ProfileEditModel : PageModel
    {
        private readonly MountaintrailsContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProfileEditModel(MountaintrailsContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public Userprofile UserProfile { get; set; }

        [BindProperty]
        public ProfileDTO ProfileDTO { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userprofile =  await _context.Userprofiles.FirstOrDefaultAsync(m => m.UserProfileId == id);
            if (userprofile == null)
            {
                return NotFound();
            }
            UserProfile = userprofile;

           ViewData["UserId"] = new SelectList(_context.Aspnetusers, "Id", "Id");
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }





            //_context.Attach(Userprofile).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UserprofileExists(Userprofile.UserProfileId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./UserProfile");
        }

        private bool UserprofileExists(int id)
        {
            return _context.Userprofiles.Any(e => e.UserProfileId == id);
        }
    }
}
