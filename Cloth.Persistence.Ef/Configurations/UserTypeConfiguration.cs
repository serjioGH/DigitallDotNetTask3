﻿namespace Cloth.Persistence.Ef.Configurations;

using Cloth.Domain.Entities;
using Cloth.Persistence.Ef.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.CreatedOn)
               .HasColumnType(ConfigurationConstants.DateColumnTypeNpgsql)
               .HasDefaultValueSql(ConfigurationConstants.GetdateTypeNpgsql);
        builder.Property(p => p.IsDeactivated)
               .HasDefaultValue(false);
        builder.Property(u => u.Email).HasMaxLength(30).IsRequired();
        builder.Property(u => u.Address).HasMaxLength(200);
        builder.Property(u => u.Phone).HasMaxLength(30).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(200);
        builder.HasOne(u => u.Basket)
            .WithOne(u => u.User)
            .HasForeignKey<Basket>(o => o.UserId);
        builder.HasMany(u => u.Orders)
            .WithOne(u => u.User)
            .HasForeignKey(o => o.UserId);
    }
}