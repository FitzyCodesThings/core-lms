using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreLMS.Core.Entities;
using CoreLMS.Persistence;
using CoreLMS.Core.DataTransferObjects;
using CoreLMS.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CoreLMS.Web.Areas.Admin.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly ICourseService courseService;

        public EditModel(ICourseService courseService)
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

            Course course;

            try
            {
                 course = await courseService.GetCourseAsync(id.Value);
            }
            catch (ApplicationException)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await courseService.UpdateCourseAsync(CourseDto);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", ex.Message);               
                return Page();
            }
            catch (Exception)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
