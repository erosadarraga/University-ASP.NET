
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContext:DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options):base(options) 
        { 

        }

        // TODO: Add DbSets (Tables of our Data base)
        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? courses { get; set; }
        public DbSet<Chapter>? chapters { get; set; }
        public DbSet<Category>? categories { get; set; }
        public DbSet<Student>? students { get; set; }
    }
}
