using CoreLMS.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLMS.Application.Services.CourseLessonService
{
    public class CourseLessonService : ICourseLessonService
    {
        private readonly IAppDbContext db;
        private readonly ILogger<CourseService> logger;

        public CourseLessonService(IAppDbContext db, ILogger<CourseService> logger)
        {
            this.db = db;
            this.logger = logger;
        }

    }
}
