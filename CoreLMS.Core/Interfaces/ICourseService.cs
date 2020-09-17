using CoreLMS.Core.DataTransferObjects.Courses;
using CoreLMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreLMS.Core.Interfaces
{
    public interface ICourseService
    {
        Task<List<Course>> GetCoursesAsync();

        Task<Course> GetCourseAsync(int id);

        Task<Course> AddCourseAsync(CreateCourseDto course);

        Task<Course> UpdateCourseAsync(UpdateCourseDto course);

        Task<Course> DeleteCourseAsync(int id);
    }
}
