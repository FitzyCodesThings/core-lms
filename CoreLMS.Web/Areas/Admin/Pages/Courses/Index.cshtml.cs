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
using System.ComponentModel.DataAnnotations;
using CoreLMS.Core.Types;

namespace CoreLMS.Web.Areas.Admin.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService courseService;

        public IndexModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public List<CourseViewModel> Courses { get; set; }

        public async Task OnGetAsync()
        {
            // 1. Get Course List from Service
            // 2. Map to "ViewModel"
            // 3. Return View

            var dbCourses = await courseService.GetCoursesAsync();

            this.Courses = dbCourses.Select(p => new CourseViewModel
            {
                Id = p.Id,
                DateCreated = p.DateCreated,
                DateUpdated = p.DateUpdated,
                Name = p.Name,
                Description = p.Description,
                CourseType = p.CourseType,
                CourseImageURL = p.CourseImageURL
            }).ToList();
        }        
    }

    public class CourseViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public CourseType CourseType { get; set; }
        public string CourseImageURL { get; set; }
    }
}
