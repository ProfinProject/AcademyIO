using AcademyIO.Core.Models;
using AcademyIO.Core.Data;

namespace AcademyIO.Students.API.Models
{
    public interface IUserRepository : IRepository<StudentUser>
    {
        Task<IEnumerable<StudentUser>> GetStudents();
        Task<IEnumerable<StudentUser>> GetAllUsers();
        Task<StudentUser> GetById(Guid id);
        void Add(StudentUser user);
        Task<StudentUser> GetByEmail(string email);
    }
}
