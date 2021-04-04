using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smiles.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smiles.DAL
{
    public class SmilesConfiguration : IEntityTypeConfiguration<SmilesEntity>
    {
        public void Configure(EntityTypeBuilder<SmilesEntity> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.Id)
                .UseIdentityColumn();

            builder
                .Property(s => s.Data)
                .IsRequired();

            builder
                .ToTable("SmilesEntities");
        }
    }
}
