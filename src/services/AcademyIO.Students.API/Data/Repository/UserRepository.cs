using AcademyIO.Core.Data;
using AcademyIO.Students.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademyIO.Students.API.Data.Repository
{
    public class UserRepository(StudentsContext db) : IUserRepository
    {
        private readonly DbSet<StudentUser> _dbSet = db.Set<StudentUser>();
        public IUnitOfWork UnitOfWork => db;

        public Task<IEnumerable<StudentUser>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<StudentUser> GetByEmail(string email)
        {
            return await db.StudentUsers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<IEnumerable<StudentUser>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public async Task<StudentUser> GetById(Guid id)
        {
            return await db.StudentUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public void Add(StudentUser user)
        {
            _dbSet.Add(user);
        }

        public void Dispose()
        {

        }
    }
}
