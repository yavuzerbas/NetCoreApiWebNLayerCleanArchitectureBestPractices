﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repositories.Products;

public class ProductsConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
        //builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)"); tutorial
        builder.Property(x => x.Price).IsRequired().HasColumnType("numeric(18,2)");
        builder.Property(x => x.Stock).IsRequired();
    }
}

