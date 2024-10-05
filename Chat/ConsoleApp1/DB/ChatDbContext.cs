using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public class ChatDbContext : DbContext
{
    public DbSet<AccountDb> Accounts { get; set; }

    public string DbPath { get; }

    string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ChatDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountDb>(e =>
        {
            e.HasKey(a => a.AccountId);
            e.HasIndex(a => a.AccountId)
                .IsUnique();

            e.HasIndex(a => a.Name);
            e.Property(a => a.Name)
                .IsRequired();     

            e.HasIndex(a => a.Id);
            e.Property(a => a.Id)
                .IsRequired();

            e.Property(a => a.Password)
                .IsRequired();
        });
    }
}

[Table("Accounts")]
public class AccountDb
{
    public int AccountId { get; set; }

    // 계정 이름
    public string Name { get; set; }
    // 로그인 시 사용할 계정 아이디
    public string Id { get; set; }
    // 로그인 시 사용할 계정 비밀번호
    public string Password{ get; set; }
}