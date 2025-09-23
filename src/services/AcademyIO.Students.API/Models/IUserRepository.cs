using AcademyIO.Core.Data;
using AcademyIO.Core.DomainObjects;

namespace AcademyIO.Students.API.Models
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetStudents();
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetById(Guid id);
        void Add(User user);
        Task<User> GetByEmail(string email);
    }
}
