using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Students.AnyAsync(s => s.Email == email);
        }
        public async Task<(List<Student> Students, int TotalCount)>
            GetPagedAsync(int pageNumber, int pageSize, string? search, string? sortBy, string? sortDir)
        {
            var query = _context.Students.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s =>
                    s.Name.Contains(search) ||
                    s.Email.Contains(search));
            }

            // Sorting
            query = sortBy?.ToLower() switch
            {
                "name" => sortDir == "desc" ? query.OrderByDescending(s => s.Name) : query.OrderBy(s => s.Name),
                "email" => sortDir == "desc" ? query.OrderByDescending(s => s.Email) : query.OrderBy(s => s.Email),
                _ => query.OrderBy(s => s.Id)
            };

            var totalCount = await query.CountAsync();

            var students = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (students, totalCount);
        }

    }
}
