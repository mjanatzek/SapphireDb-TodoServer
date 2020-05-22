using Microsoft.EntityFrameworkCore;
using SapphireDb;
using TodoServer.Data.Models;

namespace TodoServer.Data
{
    public class Db : SapphireDbContext
    {
        public Db(DbContextOptions options, SapphireDatabaseNotifier notifier) : base(options, notifier)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}