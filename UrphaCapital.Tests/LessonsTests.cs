using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.UseCases.Lessons.Commands;
using UrphaCapital.Application.UseCases.Lessons.Handlers.CommandHandlers;

namespace UrphaCapital.Tests
{
    public partial class LessonsTests
    {
        private readonly Mock<IApplicationDbContext> _mockContext;
        private readonly DeleteLessonCommandHandler _deleteLessonHandler;

        public LessonsTests()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            _deleteLessonHandler = new DeleteLessonCommandHandler(_mockContext.Object);
        }
    }
}
