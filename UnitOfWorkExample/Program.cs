
// Amir Moeini Rad
// May 3, 2025

// Main Concept: The Unit of Work Design Pattern
// In this pattern, a Unit of Work class coordinates the work of multiple repositories
// by managing changes and ensuring consistency.
// In modern .NET, this pattern as well as the Repository pattern are often implemented using Entity Framework Core's DbContext,
// which acts as a Unit of Work and provides DbSet<T> properties that act as repositories for each entity type.


namespace UnitOfWorkExample
{
    // Domain Model/Class
    // In a real application, this class is mapped to a database table.
    public class User
    {
        // Primary Key
        public int Id { get; set; }

        // Other Properties or Columns
        public string? Name { get; set; }
    }


    /////////////////////////////////////


    // Repository Interface (Design Pattern)
    public interface IUserRepository
    {
        // CRUD Operations

        // Add a new user
        void Add(User user);

        // Get all users
        List<User> GetAll();
    }


    // Repository Implementation (in-memory)
    public class UserRepository : IUserRepository
    {
        // In-memory list to simulate a database table
        private readonly List<User> _users = new List<User>();

        public void Add(User user)
        {
            _users.Add(user);

            Console.WriteLine($"User '{user.Name}' added to repository.");
        }
        
        public List<User> GetAll() => _users;
    }


    /////////////////////////////////////


    // Unit of Work Interface (Design Pattern)
    // UnitOfWork coordinates changes across one or more repositories.
    public interface IUnitOfWork
    {
        // A property to get the UserRepository
        IUserRepository Users { get; }

        // Commit all changes (in a real application, this would save changes to the database)
        void Save();
    }


    // Unit of Work Implementation
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; private set; }

        public UnitOfWork()
        {
            Users = new UserRepository();
        }

        public void Save()
        {
            // Simulate commit
            Console.WriteLine("\nAll changes have been saved (simulated commit).");
        }
    }


    /////////////////////////////////////


    // Application
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("The Unit of Work Design Pattern in C#.NET.");
            Console.WriteLine("------------------------------------------\n");


            IUnitOfWork unitOfWork = new UnitOfWork();

            var user1 = new User { Id = 1, Name = "Alice" };
            var user2 = new User { Id = 2, Name = "Bob" };

            // UnitOfWork is used instead of individual repositories such as UserRepository.
            // UnitOfWork is used to coordinate all repositories.
            unitOfWork.Users.Add(user1);
            unitOfWork.Users.Add(user2);

            // In a real application, this would persist changes to the database.
            // In other words, all changes (CRUD operations) made through the repositories are committed in a single transaction.
            unitOfWork.Save();


            Console.WriteLine("\nDone.");
        }
    }
}
