using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lista12.Models;

namespace Lista12.Data
{
    public class Lab12Context : DbContext
    {
        public Lab12Context (DbContextOptions<Lab12Context> options)
            : base(options)
        {
        }

        public DbSet<Lista12.Models.Category> Category { get; set; }

        public DbSet<Lista12.Models.Article> Article { get; set; }
    }
}
