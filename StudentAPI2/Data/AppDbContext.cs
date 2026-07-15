using Microsoft.EntityFrameworkCore;
using StudentAPI2.Models;

namespace StudentAPI2.Data
{
    //AppDbContext inherits from DBContext
    public class AppDbContext : DbContext
    {
        //This is the Constructor.It runs automatically when AppDbContext is created
        //It passes the options(setting)
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Nothing needed here. The parent class handles everything
        }

        public DbSet<Student> Students { get; set; }

        //DbSet<Student> is like a table in the database.
        //Student is the name of the table.
    }
}