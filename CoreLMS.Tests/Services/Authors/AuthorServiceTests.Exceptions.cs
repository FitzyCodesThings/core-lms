using CoreLMS.Core.DataTransferObjects;
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
    public partial class AuthorServiceTests
    {
        [Fact]
        public async Task GetAuthorAsync_ShouldThrowApplicationExceptionWhenIdIsNotFound()
        {
            // given (arrange)
            int invalidId = 100;
            Author invalidAuthor = null;

            this.appDbContextMock.Setup(db =>
                db.SelectAuthorByIdAsync(invalidId))
                    .ReturnsAsync(invalidAuthor);

            // when (act)
            var subjectTask = subject.GetAuthorAsync(invalidId);

            // then (assert)
            await Assert.ThrowsAsync<ApplicationException>(() => subjectTask);
            appDbContextMock.Verify(db => db.SelectAuthorByIdAsync(invalidId), Times.Once);
            appDbContextMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task AddAuthorAsync_ShouldThrowExceptionForInvalidDataAnnotationRequirement()
        {
            // given (arrange)
            Filler<CreateAuthorDto> authorFiller = new Filler<CreateAuthorDto>();

            CreateAuthorDto invalidAuthorToAddDto = authorFiller.Create();

            invalidAuthorToAddDto.ContactEmail = "badaddress";

            // when (act)
            var actualAuthorTask = subject.AddAuthorAsync(invalidAuthorToAddDto);

            // then (assert)
            await Assert.ThrowsAsync<ValidationException>(() => actualAuthorTask);
            appDbContextMock.VerifyNoOtherCalls();
        }
    }
}
