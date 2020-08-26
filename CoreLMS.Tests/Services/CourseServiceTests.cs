using AutoMapper;
using CoreLMS.Application.Services;
using CoreLMS.Core.Entities;
using CoreLMS.Core.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace CoreLMS.Tests.Services
{
    public class CourseServiceTests
    {
        private readonly Mock<IAppDbContext> appDbContextMock;
        private readonly Mock<ILogger<CourseService>> loggerMock;
        private readonly ICourseService subject;
        private readonly Mapper mapper;

        public CourseServiceTests()
        {   
            this.appDbContextMock = new Mock<IAppDbContext>();
            this.loggerMock = new Mock<ILogger<CourseService>>();
            this.mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Course, Course>()));

            this.subject = new CourseService(this.appDbContextMock.Object, this.loggerMock.Object);
        }

        [Fact]
        public async Task GetCoursesAsync_ShouldReturnExpectedCourseList()
        {
            // given (arrange)
            // TODO Limit number of generated sub types
            Filler<List<Course>> courseListFiller = new Filler<List<Course>>();
            
            List<Course> databaseCourses = courseListFiller.Create();

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

        [Fact]
        public async Task GetCourseAsync_ShouldReturnExpectedCourse()
        {
            // given (arrange)
            Filler<Course> courseFiller = new Filler<Course>();

            Course expectedCourse = courseFiller.Create();

            Course databaseCourse = this.mapper.Map<Course>(expectedCourse);

            this.appDbContextMock.Setup(db =>
                db.SelectCourseByIdAsync(databaseCourse.Id))
                    .ReturnsAsync(databaseCourse);

            // when (act)
            Course actualCourse = await subject.GetCourseAsync(databaseCourse.Id);

            // then (assert)
            // 1. Actual course == expected course
            // 2. DB was hit once
            // 3. Logger was NOT hit
            actualCourse.Should().BeEquivalentTo(expectedCourse);
            appDbContextMock.Verify(db => db.SelectCourseByIdAsync(databaseCourse.Id), Times.Once);
            appDbContextMock.VerifyNoOtherCalls();            
        }


        [Fact]
        public async Task GetCourseAsync_ShouldThrowApplicationExceptionWhenIdIsInvalid()
        {
            // given (arrange)
            int invalidId = 100;
            Course invalidCourse = null;

            this.appDbContextMock.Setup(db =>
                db.SelectCourseByIdAsync(invalidId))
                    .ReturnsAsync(invalidCourse);

            // when (act)
            var subjectTask = subject.GetCourseAsync(invalidId);

            // then (assert)
            await Assert.ThrowsAsync<ApplicationException>(() => subjectTask);
            appDbContextMock.Verify(db => db.SelectCourseByIdAsync(invalidId), Times.Once);
            appDbContextMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task AddCourseAsync_ShouldReturnExpectedCourseWithId()
        {
            // given (arrange)
            Filler<Course> courseFiller = new Filler<Course>();

            courseFiller.Setup()
                .OnProperty(p => p.Id).IgnoreIt()
                .OnProperty(p => p.DateCreated).IgnoreIt()
                .OnProperty(p => p.DateUpdated).IgnoreIt();

            Course courseToAdd = courseFiller.Create();

            Course databaseCourse = this.mapper.Map<Course>(courseToAdd);

            databaseCourse.Id = 1;
            databaseCourse.DateCreated = databaseCourse.DateUpdated = DateTime.UtcNow;

            this.appDbContextMock.Setup(db =>
                db.CreateCourseAsync(courseToAdd))
                    .ReturnsAsync(databaseCourse);

            // when (act)
            Course actualCourse = await subject.AddCourseAsync(courseToAdd);

            // then (assert)
            actualCourse.Should().BeEquivalentTo(databaseCourse);
            appDbContextMock.Verify(db => db.CreateCourseAsync(courseToAdd), Times.Once);
            appDbContextMock.VerifyNoOtherCalls();
        }

        // TODO Decide validation / implement invalid object test case

        [Fact]
        public async Task UpdateCourseAsync_ShouldReturnExpectedCourse()
        {
            // given (arrange)
            Filler<Course> courseFiller = new Filler<Course>();

            Course courseToUpdate = courseFiller.Create();

            Course databaseCourse = this.mapper.Map<Course>(courseToUpdate);

            DateTime updateTime = DateTime.UtcNow;

            databaseCourse.DateUpdated = updateTime;

            this.appDbContextMock.Setup(db =>
                db.UpdateCourseAsync(courseToUpdate))
                    .ReturnsAsync(databaseCourse);

            // when (act)
            Course actualCourse = await subject.UpdateCourseAsync(courseToUpdate);

            // then (assert)
            actualCourse.Should().BeEquivalentTo(databaseCourse);
            Assert.Equal(actualCourse.DateUpdated, updateTime);
            appDbContextMock.Verify(db => db.UpdateCourseAsync(courseToUpdate), Times.Once);
            appDbContextMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task DeleteCourseAsync_ShouldReturnExpectedCourse()
        {
            // given (arrange)
            Filler<Course> courseFiller = new Filler<Course>();

            Course courseToDelete = courseFiller.Create();

            Course databaseCourse = this.mapper.Map<Course>(courseToDelete);

            DateTime updateTime = DateTime.UtcNow;

            databaseCourse.DateUpdated = updateTime;

            databaseCourse.DateDeleted = updateTime;

            this.appDbContextMock.Setup(db =>
                db.DeleteCourseAsync(courseToDelete))
                    .ReturnsAsync(databaseCourse);

            // when (act)
            Course actualCourse = await subject.DeleteCourseAsync(courseToDelete);

            // then (assert)
            actualCourse.Should().BeEquivalentTo(databaseCourse);
            Assert.Equal(actualCourse.DateUpdated, updateTime);
            Assert.Equal(actualCourse.DateDeleted.GetValueOrDefault(), updateTime);
            appDbContextMock.Verify(db => db.DeleteCourseAsync(courseToDelete), Times.Once);
            appDbContextMock.VerifyNoOtherCalls();
        }
    }
}
