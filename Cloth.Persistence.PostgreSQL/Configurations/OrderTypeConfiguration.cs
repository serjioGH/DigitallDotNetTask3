﻿namespace Cloth.Persistence.Ef.Configurations;

using Cloth.Domain.Entities;
using Cloth.Persistence.Ef.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false);
        builder.Property(p => p.CreatedOn)
            .HasDefaultValueSql(ConfigurationConstants.GetdateTypeNpgsql);
        builder.HasOne(p => p.Status)
            .WithMany(s => s.Orders)
            .HasForeignKey(p => p.StatusId);
        builder.HasOne(p => p.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.PaymentId);
        builder.Property(p => p.TotalAmount)
            .HasColumnType(ConfigurationConstants.DecimalTypeNpgsql);
        builder.HasOne(p => p.User)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.UserId);
        builder.Property(p => p.PaymentId).IsRequired(false);
        builder.Property(p => p.UserId).IsRequired(false);
    }
}