namespace AcademyIO.Core.Interfaces
{
    public interface IAppUserService
    {
        public bool IsAuthenticated();
        public Guid? GetId();
        public bool IsAdmin();
    }
}
