using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Trail_Tracker_App.Data;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Pictures
{
    public class CreateModel : PageModel
    {
        private readonly MountaintrailsContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(MountaintrailsContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["TrailName"] = new SelectList(_context.Trails, "Name", "Name");
            return Page();
        }

        [BindProperty]
        public PictureDTO PictureDTO { get; set; } = default!;

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (PictureDTO == null || PictureDTO.FormFile == null)
            {
                ModelState.AddModelError("", "No file uploaded.");
                return Page();
            }

            if (PictureDTO.FormFile != null && PictureDTO.FormFile.Length > 0)
            {

                var filePath = Path.Combine(_environment.WebRootPath, "Uploads", PictureDTO.FormFile.FileName);

                // Ensure the directory exists
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await PictureDTO.FormFile.CopyToAsync(fileStream);
                }
                // grab datetime
                DateTime dateTime = DateTime.Now;

                // get trail id by name
                var trail = _context.Trails.Where(x => x.Name == PictureDTO.TrailName).FirstOrDefault();

                // get user name of logged in user for heading
                var user = User.Identity.Name;

                var Picture = new Picture()
                {
                    TrailId = trail.TrailId,
                    FilePath = $"/Uploads/{PictureDTO.FormFile.FileName}",
                    UploadedBy = user,
                    UploadDate = dateTime,
                    
                };

                _context.Pictures.Add(Picture);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }
            else { return Page(); }

        }
    }
}
