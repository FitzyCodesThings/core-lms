using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreLMS.Core.Entities;
using CoreLMS.Persistence;

namespace CoreLMS.Web.Areas.Admin.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly CoreLMS.Persistence.AppDbContext _context;

        public DetailsModel(CoreLMS.Persistence.AppDbContext context)
        {
            _context = context;
        }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Courses.FirstOrDefaultAsync(m => m.Id == id);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
