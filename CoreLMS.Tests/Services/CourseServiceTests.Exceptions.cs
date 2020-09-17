using CoreLMS.Core.DataTransferObjects.Courses;
using CoreLMS.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace CoreLMS.Tests.Services
{
    public partial class CourseServiceTests
    {
        [Fact]
        public async Task GetCourseAsync_ShouldThrowApplicationExceptionWhenIdIsNotFound()
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
        public async Task AddCourseAsync_ShouldThrowExceptionForInvalidDataAnnotationRequirement()
        {
            // given (arrange)
            Filler<CreateCourseDto> courseFiller = new Filler<CreateCourseDto>();

            CreateCourseDto invalidCourseToAddDto = courseFiller.Create();

            invalidCourseToAddDto.Name = null;

            // when (act)
            var actualCourseTask = subject.AddCourseAsync(invalidCourseToAddDto);

            // then (assert)
            await Assert.ThrowsAsync<ValidationException>(() => actualCourseTask);
            appDbContextMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task UpdateCourseAsync_ShouldThrowExceptionForInvalidDataAnnotationRequirement()
        {
            // given (arrange)
            Filler<UpdateCourseDto> courseFiller = new Filler<UpdateCourseDto>();

            UpdateCourseDto invalidCourseToUpdateDto = courseFiller.Create();

            Course invalidCourseToUpdate = this.mapper.Map<Course>(invalidCourseToUpdateDto);

            invalidCourseToUpdateDto.Name = null;

            this.appDbContextMock.Setup(db =>
                db.SelectCourseByIdAsync(invalidCourseToUpdateDto.Id))
                    .ReturnsAsync(invalidCourseToUpdate);

            // when (act)
            var actualCourseTask = subject.UpdateCourseAsync(invalidCourseToUpdateDto);

            // then (assert)
            await Assert.ThrowsAsync<ValidationException>(() => actualCourseTask);
            appDbContextMock.Verify(db => db.SelectCourseByIdAsync(invalidCourseToUpdateDto.Id), Times.Once);
            appDbContextMock.VerifyNoOtherCalls();
        }

        /*
        [Fact]
        public async Task AddCourseAsync_ShouldThrowExceptionForInvalidBusinessLogicRequirement()
        {
            // given (arrange)
            Filler<Course> courseFiller = new Filler<Course>();

            courseFiller.Setup()
                .OnProperty(p => p.Id).IgnoreIt();

            Course invalidCourseToAdd = courseFiller.Create();

            invalidCourseToAdd.CourseLessons = new List<CourseLesson>();

            Course databaseCourse = this.mapper.Map<Course>(invalidCourseToAdd);

            this.appDbContextMock.Setup(db =>
                db.CreateCourseAsync(invalidCourseToAdd))
                    .ReturnsAsync(databaseCourse);

            // when (act)
            var actualCourseTask = subject.AddCourseAsync(invalidCourseToAdd);

            // then (assert)
            await Assert.ThrowsAsync<ValidationException>(() => actualCourseTask);
            appDbContextMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task AddCourseAsync_ShouldThrowExceptionForInvalidBusinessLogicRequirementNullList()
        {
            // given (arrange)
            Filler<Course> courseFiller = new Filler<Course>();

            courseFiller.Setup()
                .OnProperty(p => p.Id).IgnoreIt();

            Course invalidCourseToAdd = courseFiller.Create();

            invalidCourseToAdd.CourseLessons = null;

            Course databaseCourse = this.mapper.Map<Course>(invalidCourseToAdd);

            databaseCourse.Id = 1;

            this.appDbContextMock.Setup(db =>
                db.CreateCourseAsync(invalidCourseToAdd))
                    .ReturnsAsync(databaseCourse);

            // when (act)
            var actualCourseTask = subject.AddCourseAsync(invalidCourseToAdd);

            // then (assert)
            await Assert.ThrowsAsync<ValidationException>(() => actualCourseTask);
            appDbContextMock.VerifyNoOtherCalls();
        }
        */

    }
}
