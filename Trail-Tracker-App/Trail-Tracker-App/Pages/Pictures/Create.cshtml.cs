
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Pictures
{
    public class CreateModel : PageModel
    {
        private readonly MountaintrailsContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(MountaintrailsContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
                var user = User.Identity.Name;
                Console.WriteLine(user);
                var Picture = new Picture()
                {
                    TrailId = trail.TrailId,
                    FilePath = $"/Uploads/{PictureDTO.FormFile.FileName}",
                    UploadedBy = User.Identity.Name,
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
