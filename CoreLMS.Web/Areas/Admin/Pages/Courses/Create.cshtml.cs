using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoreLMS.Core.Entities;
using CoreLMS.Core.Interfaces;
using CoreLMS.Core.DataTransferObjects.Courses;

namespace CoreLMS.Web.Areas.Admin.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly ICourseService courseService;

        public CreateModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateCourseDto CourseDto { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await courseService.AddCourseAsync(CourseDto);

            return RedirectToPage("./Index");
        }
    }
}
