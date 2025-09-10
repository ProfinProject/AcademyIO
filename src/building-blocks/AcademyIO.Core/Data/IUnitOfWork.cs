using AcademyIO.Core.DomainObjects;

namespace AcademyIO.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
