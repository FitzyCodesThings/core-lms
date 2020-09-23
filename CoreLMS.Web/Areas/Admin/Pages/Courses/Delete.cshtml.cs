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
using CoreLMS.Core.DataTransferObjects;

namespace CoreLMS.Web.Areas.Admin.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly ICourseService courseService;

        public DeleteModel(ICourseService courseService)
        {            
            this.courseService = courseService;
        }

        [BindProperty]
        public UpdateCourseDto CourseDto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course course = await courseService.GetCourseAsync(id.Value);

            if (course == null)
            {
                return NotFound();
            }

            CourseDto = new UpdateCourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                CourseType = course.CourseType,
                CourseImageURL = course.CourseImageURL
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (CourseDto == null)
            {
                return NotFound();
            }

            await courseService.DeleteCourseAsync(CourseDto.Id);

            return RedirectToPage("./Index");
        }
    }
}
