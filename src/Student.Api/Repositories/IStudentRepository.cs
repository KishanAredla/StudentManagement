using StudentApi.Models;

namespace StudentApi.Repositories
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(int id);
        Task<List<Student>> GetAllAsync();
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
        Task<bool> ExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email);

        Task<(List<Student> Students, int TotalCount)>
            GetPagedAsync(int pageNumber, int pageSize, string? search, string? sortBy, string? sortDir);

    }
}
