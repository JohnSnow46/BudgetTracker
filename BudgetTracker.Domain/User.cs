

namespace BudgetTracker.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public Budget Budget { get; set; }
    }
}
