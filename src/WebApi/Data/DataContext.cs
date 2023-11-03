using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<City> City { get; set; }
    public DbSet<Photo> Photo { get; set; }
    public DbSet<User> Users { get; set; }
}   

