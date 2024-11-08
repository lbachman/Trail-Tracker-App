﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;

namespace Trail_Tracker_App.Pages.Mountains
{
    public class IndexModel : PageModel
    {
        private readonly MountaintrailsContext _context;

        public IndexModel(MountaintrailsContext context)
        {
            _context = context;
        }

        public IList<Mountain> Mountain { get;set; } = default!;
        public IList<Mountainrange> Range { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Mountain = await _context.Mountains.Include(m => m.Range).ToListAsync();


            



        }
    }
}
