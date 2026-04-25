// Amir Moeini Rad
// May, 2025

// Main Concept: The Unit of Work Design Pattern

// In this pattern, a Unit of Work class coordinates the work of multiple repositories
// by managing changes and ensuring consistency.


namespace UnitOfWorkDP
{
    // Domain Model/Entity
    // In a real application, this class is mapped to a database table.
    public class User
    {
        // Primary Key
        public int Id { get; set; }

        // Other Properties or Columns
        public string? Name { get; set; }
    }

    public class Admin
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }


    /////////////////////////////////////


    // Generic Repository Interface
    public interface IRepository<TEntity>
    {
        // Add a new user
        void Add(TEntity entity);

        // Get all users
        List<TEntity> GetAll();
    }


    // Repository Implementation
    public class Repository<TEntity> : IRepository<TEntity>
    {
        // In-memory list to simulate a database table
        private readonly List<TEntity> _entities = [];

        public void Add(TEntity entity)
        {
            _entities.Add(entity);

            Console.WriteLine($"A/An {typeof(TEntity).Name} was added to the repository.");
        }
        
        public List<TEntity> GetAll() => _entities;
    }


    /////////////////////////////////////


    // Unit of Work Interface
    // UnitOfWork coordinates changes across one or more repositories.
    public interface IUnitOfWork
    {
        // A property to get the Repository
        IRepository<User> Users { get; }
        IRepository<Admin> Admins { get; }

        // Commit all changes (in a real application, this would save changes to the database.)
        void Save();
    }


    // Unit of Work Implementation
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<User>? _users;
        private IRepository<Admin>? _admins;

        // Lazy initialization.
        public IRepository<User> Users => _users ??= new Repository<User>();
        public IRepository<Admin> Admins => _admins ??= new Repository<Admin>();


        public void Save()
        {
            // Simulate commit
            Console.WriteLine("All changes have been saved.");
        }
    }


    /////////////////////////////////////


    // Main Application
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("The Unit of Work Design Pattern in C#.NET.");
            Console.WriteLine("------------------------------------------\n");


            UnitOfWork unitOfWork = new();

            var user1 = new User { Id = 1, Name = "Alice" };
            var user2 = new User { Id = 2, Name = "Bob" };

            var admin1 = new Admin { Id = 1, Name = "Tom" };
            var admin2 = new Admin { Id = 2, Name = "Peter" };

            // UnitOfWork is used instead of individual repositories such as UserRepository.
            // UnitOfWork is used to coordinate all repositories.
            // In a real senario, user1 & user2 are not yet added to the database (before calling Save()).
            unitOfWork.Users.Add(user1);
            Console.WriteLine($"Name: {user1.Name}\n");
            unitOfWork.Users.Add(user2);
            Console.WriteLine($"Name: {user2.Name}\n");

            unitOfWork.Admins.Add(admin1);
            Console.WriteLine($"Name: {admin1.Name} \n");
            unitOfWork.Admins.Add(admin2);
            Console.WriteLine($"Name: {admin2.Name} \n");

            // In a real application, this would persist changes to the database.
            // In other words, all changes (CRUD operations) made through the repositories are committed in a single transaction.
            unitOfWork.Save();


            Console.WriteLine("\nDone.");
        }
    }
}
