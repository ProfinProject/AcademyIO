using AcademyIO.Core.Data;
using Microsoft.EntityFrameworkCore;
using AcademyIO.Core.DomainObjects;
using AcademyIO.Students.API.Models;

namespace AcademyIO.Students.API.Data.Repository
{
    public class UserRepository(StudentsContext db) : IUserRepository
    {
        private readonly DbSet<User> _dbSet = db.Set<User>();
        public IUnitOfWork UnitOfWork => db;

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await db.SystemUsers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<IEnumerable<User>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(Guid id)
        {
            return await db.SystemUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public void Add(User user)
        {
            _dbSet.Add(user);
        }

        public void Dispose()
        {

        }
    }
}
