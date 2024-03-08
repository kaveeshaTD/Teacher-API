using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;

namespace SchoolApi.Data
{
    public class TeacherDbContext : DbContext
    {
        public TeacherDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
