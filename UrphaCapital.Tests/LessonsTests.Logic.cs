using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.UseCases.Lessons.Commands;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Tests
{
    public partial class LessonsTests
    {

        [Fact]
        public async Task Handle_GivenValidLessonId_ShouldReturnSuccessResponse()
        {
            // Arrange qismi
            var lesson = new Lesson { Id = 1, Name = "Lesson 1", CourseId = 1 };
            var lessons = new List<Lesson> { lesson }.AsQueryable();

            var mockSet = new Mock<DbSet<Lesson>>();
            mockSet.As<IQueryable<Lesson>>().Setup(m => m.Provider).Returns(lessons.Provider);
            mockSet.As<IQueryable<Lesson>>().Setup(m => m.Expression).Returns(lessons.Expression);
            mockSet.As<IQueryable<Lesson>>().Setup(m => m.ElementType).Returns(lessons.ElementType);
            mockSet.As<IQueryable<Lesson>>().Setup(m => m.GetEnumerator()).Returns(lessons.GetEnumerator());
            mockSet.Setup(m => m.Remove(It.IsAny<Lesson>())).Callback<Lesson>(l => lessons.ToList().Remove(l));

            _mockContext.Setup(c => c.Lessons).Returns(mockSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var command = new DeleteLessonCommand() { Id = 1};

            // Act qismi
            var result = await _deleteLessonHandler.Handle(command, CancellationToken.None);

            // Assert qismi
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Lesson successfully deleted", result.Message);
        }

        [Fact]
        public async Task Handle_GivenInvalidLessonId_ShouldReturnNotFoundResponse()
        {
            var lessons = new List<Lesson>().AsQueryable();

            var mockSet = new Mock<DbSet<Lesson>>();
            mockSet.As<IQueryable<Lesson>>().Setup(m => m.Provider).Returns(lessons.Provider);
            mockSet.As<IQueryable<Lesson>>().Setup(m => m.Expression).Returns(lessons.Expression);
            mockSet.As<IQueryable<Lesson>>().Setup(m => m.ElementType).Returns(lessons.ElementType);
            mockSet.As<IQueryable<Lesson>>().Setup(m => m.GetEnumerator()).Returns(lessons.GetEnumerator());

            _mockContext.Setup(c => c.Lessons).Returns(mockSet.Object);

            var command = new DeleteLessonCommand() { Id = 1 };

            var result = await _deleteLessonHandler.Handle(command, CancellationToken.None);

            Assert.False(result.IsSuccess);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Lesson not found!", result.Message);
        }
    }
}
