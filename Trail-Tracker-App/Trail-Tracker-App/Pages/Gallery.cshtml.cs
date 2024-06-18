using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trail_Tracker_App.Entities;
using Microsoft.EntityFrameworkCore;

namespace Trail_Tracker_App.Pages.Pictures
{
    public class GalleryModel : PageModel
    {
		MountaintrailsContext _context;

		public GalleryModel(MountaintrailsContext context)
		{
			_context = context;
		}

		public IList<Mountain> Mountains { get; set; }
		public IList<Trail> Trails { get; set; }
		public IList<Picture> Pictures { get; set; }


		public async void OnGet()
        {
            Pictures = await _context.Pictures.ToListAsync();
            Mountains = await _context.Mountains.ToListAsync();
			Trails = await _context.Trails.ToListAsync();
        }
    }
}
