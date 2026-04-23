using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using StudentApi.DTOs;
using StudentApi.Exceptions;
using StudentApi.Models;
using StudentApi.Repositories;
using StudentApi.Services;
using Xunit;


namespace Student.Api.Tests.Services
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly StudentService _studentService;
        private readonly Mock<ILogger<StudentService>> _loggerMock;

        public StudentServiceTests()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<StudentService>>();

            _studentService = new StudentService(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WhenStudentExists_ReturnsStudentDto()
        {
            // Arrange
            var student = new StudentApi.Models.Student
            {
                Id = 1,
                Name = "Kishan"
            };

            var studentDto = new StudentApi.DTOs.StudentDto
            {
                Id = 1,
                Name = "Kishan"
            };

            _repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(student);

            _mapperMock
                .Setup(m => m.Map<StudentApi.DTOs.StudentDto>(student))
                .Returns(studentDto);

            // Act
            var result = await _studentService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Kishan", result.Name);

            _repositoryMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenStudentNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            int studentId = 1;

            var dto = new UpdateStudentDto
            {
                Name = "Test",
                Email = "test@gmail.com"
            };

            _repositoryMock
                .Setup(r => r.GetByIdAsync(studentId))
                .ReturnsAsync((StudentApi.Models.Student)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _studentService.UpdateAsync(studentId, dto));
        }

        [Fact]
        public async Task CreateAsync_EmailFound_ThrowBadRequestException()
        {
            // Arrange
            var email = "test@gmail.com";

            var dto = new CreateStudentDto
            {
                Name = "Test",
                Email = "test@gmail.com"
            };

            _repositoryMock
                .Setup(r => r.EmailExistsAsync(email))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() =>
                _studentService.CreateAsync(dto));

            _repositoryMock
                .Verify(r => r.EmailExistsAsync(email), Times.Once());

            _repositoryMock
                .Verify(r => r.AddAsync(It.IsAny<StudentApi.Models.Student>()), Times.Never());

            _mapperMock.Verify(
                 m => m.Map<StudentApi.Models.Student>(It.IsAny<CreateStudentDto>()),Times.Never());
        }

    }
}
