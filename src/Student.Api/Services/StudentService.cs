using AutoMapper;
using StudentApi.Common;
using StudentApi.DTOs;
using StudentApi.Exceptions;
using StudentApi.Models;
using StudentApi.Repositories;

namespace StudentApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;


        public StudentService(IStudentRepository repository, IMapper mapper, ILogger<StudentService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            _logger.LogInformation(
                "Starting GetById operation for StudentId={StudentId}",
                id);

            var student = await _repository.GetByIdAsync(id);

            if (student == null)
            {
                _logger.LogWarning(
                    "Student not found. StudentId={StudentId}",
                    id);

                throw new NotFoundException($"Student not found with Id {id}");
            }

            _logger.LogInformation(
                "Student retrieved successfully. StudentId={StudentId}",
                id);

            return _mapper.Map<StudentDto>(student);
        }


        public async Task<List<StudentDto>> GetAllAsync()
        {
            var students = await _repository.GetAllAsync();
            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<StudentDto> CreateAsync(CreateStudentDto dto)
        {
            _logger.LogInformation(
                "Starting student creation. Email={Email}",
                dto.Email);

            dto.Name = dto.Name.Trim();
            dto.Email = dto.Email.Trim().ToLowerInvariant();

            if (await _repository.EmailExistsAsync(dto.Email))
            {
                _logger.LogWarning(
                    "Student creation failed. Email already exists. Email={Email}",
                    dto.Email);

                throw new BadRequestException("Email already exists");
            }

            var student = _mapper.Map<Student>(dto);

            await _repository.AddAsync(student);

            _logger.LogInformation(
                "Student created successfully. StudentId={StudentId}, Email={Email}",
                student.Id,
                dto.Email);

            return _mapper.Map<StudentDto>(student);
        }


        public async Task UpdateAsync(int id, UpdateStudentDto dto)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new NotFoundException("Student not found");

            dto.Name = dto.Name.Trim();
            dto.Email = dto.Email.Trim().ToLower();

            _mapper.Map(dto, student);
            await _repository.UpdateAsync(student);
        }

        public async Task PatchAsync(int id, StudentPatchDto dto)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new NotFoundException("Student not found");

            if (dto.Name != null)
                student.Name = dto.Name.Trim();

            if (dto.Email != null)
                student.Email = dto.Email.Trim().ToLower();

            if (dto.Age.HasValue)
                student.Age = dto.Age.Value;

            await _repository.UpdateAsync(student);
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                throw new NotFoundException("Student not found");

            await _repository.DeleteAsync(student);
        }
        public async Task<PagedResult<StudentDto>> GetPagedAsync(
            int pageNumber, int pageSize, string? search, string? sortBy, string? sortDir)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                throw new BadRequestException("Invalid pagination parameters");

            var (students, totalCount) = await _repository
                .GetPagedAsync(pageNumber, pageSize, search, sortBy, sortDir);

            return new PagedResult<StudentDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalCount,
                Data = _mapper.Map<List<StudentDto>>(students)
            };
        }

    }
}
