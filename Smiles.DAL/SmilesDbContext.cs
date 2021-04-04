using Microsoft.EntityFrameworkCore;
using Smiles.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smiles.DAL
{
    public class SmilesDbContext: DbContext
    {
        public DbSet<SmilesEntity> SmilesEntities { get; set; }

        public SmilesDbContext(DbContextOptions<SmilesDbContext> options): base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SmilesConfiguration());
        }
    }
}
