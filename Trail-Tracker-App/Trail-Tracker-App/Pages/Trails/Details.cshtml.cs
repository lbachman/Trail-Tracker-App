﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Trails
{
    public class DetailsModel : PageModel
    {
        private readonly Trail_Tracker_App.Entities.MountaintrailsContext _context;

        public DetailsModel(Trail_Tracker_App.Entities.MountaintrailsContext context)
        {
            _context = context;
        }

        public Trail Trail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trail = await _context.Trails.FirstOrDefaultAsync(m => m.TrailId == id);
            if (trail == null)
            {
                return NotFound();
            }
            else
            {
                Trail = trail;
            }
            return Page();
        }
    }
}