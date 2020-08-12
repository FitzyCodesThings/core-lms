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
    }
}
