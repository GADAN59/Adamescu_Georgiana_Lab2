using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Adamescu_Georgiana_Lab2.Models;

namespace Adamescu_Georgiana_Lab2.Data
{
    public class Adamescu_Georgiana_Lab2Context : DbContext
    {
        public Adamescu_Georgiana_Lab2Context (DbContextOptions<Adamescu_Georgiana_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Adamescu_Georgiana_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Adamescu_Georgiana_Lab2.Models.Publisher>? Publisher { get; set; }

        public DbSet<Adamescu_Georgiana_Lab2.Models.Author>? Author { get; set; }

        public DbSet<Adamescu_Georgiana_Lab2.Models.Category>? Category { get; set; }
    }
}
