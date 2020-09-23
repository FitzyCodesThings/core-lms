using CoreLMS.Core.DataTransferObjects;
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

        Task<Course> AddCourseAsync(CreateCourseDto courseDto);

        Task<Course> UpdateCourseAsync(UpdateCourseDto courseDto);

        Task<Course> DeleteCourseAsync(int id);
    }
}
