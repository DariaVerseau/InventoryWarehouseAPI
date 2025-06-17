using DAL.Entities;

namespace DAL.EF;

public class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        // Если вы используете миграции, лучше заменить EnsureCreated на Migrate
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            return; // Пользователи уже есть, инициализация не нужна
        }

        var user = new User
        {
            Email = "admin@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!")
        };

        context.Users.Add(user);
        context.SaveChanges();
    }
}