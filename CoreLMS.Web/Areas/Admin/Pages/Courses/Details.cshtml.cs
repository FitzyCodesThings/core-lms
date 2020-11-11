using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreLMS.Core.Entities;
using CoreLMS.Persistence;
using CoreLMS.Core.Interfaces;

namespace CoreLMS.Web.Areas.Admin.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly ICourseService courseService;

        public DetailsModel(ICourseService courseService)
        {   
            this.courseService = courseService;
        }

        public CourseViewModel Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbCourse = await courseService.GetCourseAsync(id.Value);

            Course = new CourseViewModel
            {
                Id = dbCourse.Id,
                DateCreated = dbCourse.DateCreated,
                DateUpdated = dbCourse.DateUpdated,
                Name = dbCourse.Name,
                Description = dbCourse.Description,
                CourseType = dbCourse.CourseType,
                CourseImageURL = dbCourse.CourseImageURL
            };

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
