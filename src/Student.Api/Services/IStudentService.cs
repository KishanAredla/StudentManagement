using StudentApi.Common;
using StudentApi.DTOs;

namespace StudentApi.Services
{
    public interface IStudentService
    {
        Task<StudentDto> GetByIdAsync(int id);
        Task<List<StudentDto>> GetAllAsync();
        Task<StudentDto> CreateAsync(CreateStudentDto dto);
        Task UpdateAsync(int id, UpdateStudentDto dto);
        Task PatchAsync(int id, StudentPatchDto dto);
        Task DeleteAsync(int id);
        Task<PagedResult<StudentDto>> GetPagedAsync(
            int pageNumber, int pageSize, string? search, string? sortBy, string? sortDir);

    }
}
