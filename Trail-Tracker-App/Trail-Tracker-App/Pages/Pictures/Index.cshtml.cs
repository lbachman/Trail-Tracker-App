using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Pictures
{
    public class IndexModel : PageModel
    {
        private readonly MountaintrailsContext _context;

        public IndexModel(MountaintrailsContext context)
        {
            _context = context;
        }

        public IList<Picture> Picture { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Picture = await _context.Pictures.Include(p => p.Trail).ToListAsync();           

        }
    }
}
