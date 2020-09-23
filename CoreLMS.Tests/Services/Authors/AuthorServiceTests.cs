using AutoMapper;
using CoreLMS.Application.Services;
using CoreLMS.Core.DataTransferObjects;
using CoreLMS.Core.Entities;
using CoreLMS.Core.Interfaces;
using FluentAssertions;
using FluentAssertions.Common;
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
    public partial class AuthorServiceTests
    {
        private readonly Mock<IAppDbContext> appDbContextMock;
        private readonly Mock<ILogger<AuthorService>> loggerMock;
        private readonly IAuthorService subject;
        private readonly Mapper mapper;

        public AuthorServiceTests()
        {   
            this.appDbContextMock = new Mock<IAppDbContext>();
            
            this.loggerMock = new Mock<ILogger<AuthorService>>();

            this.mapper = new Mapper(new MapperConfiguration(cfg => {
                cfg.CreateMap<Author, CreateAuthorDto>();
                cfg.CreateMap<CreateAuthorDto, Author>();
                cfg.CreateMap<Author, UpdateAuthorDto>();
                cfg.CreateMap<UpdateAuthorDto, Author>();
                cfg.CreateMap<Author, Author>();
            }));

            this.subject = new AuthorService(this.appDbContextMock.Object, this.loggerMock.Object);
        }

        [Fact]
        public async Task GetAuthorsAsync_ShouldReturnExpectedAuthorList()
        {
            // given (arrange)
            // TODO Limit number of generated sub types
            Filler<List<Author>> authorListFiller = new Filler<List<Author>>();

            List<Author> databaseAuthors = authorListFiller.Create();

            this.appDbContextMock.Setup(db =>
                db.SelectAuthorsAsync())
                    .ReturnsAsync(new List<Author>(databaseAuthors));

            // when (act)
            List<Author> actualAuthors = await subject.GetAuthorsAsync();

            // then (assert)
            // 1. Actual list of courses == expected courses
            // 2. DB was hit once (and no more)
            actualAuthors.Should().BeEquivalentTo(databaseAuthors);
            appDbContextMock.Verify(db => db.SelectAuthorsAsync(), Times.Once);
            appDbContextMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task GetAuthorAsync_ShouldReturnExpectedAuthor()
        {
            // given (arrange)
            Filler<Author> authorFiller = new Filler<Author>();

            Author expectedAuthor = authorFiller.Create();

            Author databaseAuthor = this.mapper.Map<Author>(expectedAuthor);

            this.appDbContextMock.Setup(db =>
                db.SelectAuthorByIdAsync(databaseAuthor.Id))
                    .ReturnsAsync(databaseAuthor);

            // when (act)
            Author actualAuthor = await subject.GetAuthorAsync(expectedAuthor.Id);

            // then (assert)
            // 1. Actual course == expected course
            // 2. DB was hit once
            // 3. Logger was NOT hit
            actualAuthor.Should().BeEquivalentTo(expectedAuthor);
            appDbContextMock.Verify(db => db.SelectAuthorByIdAsync(databaseAuthor.Id), Times.Once);
            appDbContextMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task AddAuthorAsync_ShouldReturnExpectedAuthorWithId()
        {
            // given (arrange)
            Filler<CreateAuthorDto> authorFiller = new Filler<CreateAuthorDto>();

            CreateAuthorDto authorDtoToAdd = authorFiller.Create();

            Author authorToAdd = this.mapper.Map<Author>(authorDtoToAdd);

            Author databaseAuthor = this.mapper.Map<Author>(authorToAdd);

            databaseAuthor.Id = 1;
            databaseAuthor.DateCreated = databaseAuthor.DateUpdated = DateTime.UtcNow;

            this.appDbContextMock
                .Setup(db => db.CreateAuthorAsync(It.IsAny<Author>()))
                    .ReturnsAsync(databaseAuthor);

            // when (act)
            var actualAuthor = await subject.AddAuthorAsync(authorDtoToAdd);

            // then (assert)
            actualAuthor.Should().BeEquivalentTo(databaseAuthor);
            appDbContextMock.Verify(db => db.CreateAuthorAsync(It.IsAny<Author>()), Times.Once);
            appDbContextMock.VerifyNoOtherCalls();
        }

    }
}
