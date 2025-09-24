namespace AcademyIO.Core.Utils
{
    public static class SeedStudentUserData
    {
        public static readonly Guid AdminUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        public static readonly Guid User1Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        public static readonly Guid User2Id = Guid.Parse("33333333-3333-3333-3333-333333333333");

        public static IEnumerable<InitialUser> Users => new List<InitialUser>
        {
            new(AdminUserId, "Admin", "AcademyIO", "admin@academyio.com", "Teste@123", new DateTime(1990, 5, 5), true),
            new(User1Id, "Student1", "AcademyIO", "aluno1@academyio.com", "Teste@123",new DateTime(2000, 5, 5), false),
            new(User2Id, "Student2", "AcademyIO", "aluno2@academyio.com", "Teste@123",new DateTime(2000, 5, 5), false)
        };
    }

    public record InitialUser(Guid Id, string FirstName, string LastName, string Email, string Password, DateTime DateOfBirth, bool IsAdmin);
}
