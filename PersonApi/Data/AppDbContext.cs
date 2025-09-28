using Microsoft.EntityFrameworkCore;
using PersonApi.Models;

namespace PersonApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Person> Persons => Set<Person>();
}
