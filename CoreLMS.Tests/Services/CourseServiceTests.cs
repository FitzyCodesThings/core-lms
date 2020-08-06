using CoreLMS.Application.Services;
using CoreLMS.Core.Entities;
using CoreLMS.Core.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoreLMS.Tests.Services
{
    public class CourseServiceTests
    {
        private readonly Mock<IAppDbContext> appDbContextMock;
        private readonly ICourseService subject;

        public CourseServiceTests()
        {
            this.appDbContextMock = new Mock<IAppDbContext>();

            this.subject = new CourseService(this.appDbContextMock.Object);
        }

        [Fact]
        public async Task GetCoursesAsync_ShouldReturnExpectedCourseList()
        {
            // given (arrange)
            List<Course> databaseCourses = new List<Course>();
            databaseCourses.Add(new Course()
            {
                Id = 1,
                Name = "Course #1"
            });

            databaseCourses.Add(new Course()
            {
                Id = 2,
                Name = "Course #2"
            });


            // Important note: do NOT have the mock dbcontext just return databaseCourses (rather create a new List from the old list in the return)
            // Otherwise we'll get a potentially "false positive" equality check since we'll just be passing around the same list by reference
            this.appDbContextMock.Setup(db =>
                db.SelectCoursesAsync())
                    .ReturnsAsync(new List<Course>(databaseCourses)); 

            // when (act)
            List<Course> actualCourses = await subject.GetCoursesAsync();

            // then (assert)
            // 1. Actual list of courses == expected courses
            // 2. DB was hit once (and no more)
            actualCourses.Should().BeEquivalentTo(databaseCourses);
            appDbContextMock.Verify(db => db.SelectCoursesAsync(), Times.Once);
            appDbContextMock.VerifyNoOtherCalls();
        }
    }
}
