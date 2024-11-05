using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogsHouseService.Infrastructure.Models.Entities;

namespace DogsHouseService.Infrastructure.DbContextes
{
    public class DogsDbContext : DbContext
    {
        public DogsDbContext(DbContextOptions<DogsDbContext> options) : base(options)
        {

        }
        public DbSet<Dog> Dogs { get; set; }
    }
}
