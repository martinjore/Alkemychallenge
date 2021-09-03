using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alkemy.Models;

namespace Alkemy.Models.AppDBcontext
{
    public class AppDBcontext: IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AppDBcontext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<Characters> Characters { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Movies> Movies { get; set; }



    }
}
