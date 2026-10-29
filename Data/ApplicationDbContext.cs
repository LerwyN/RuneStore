using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RUNE.Models;

namespace RUNE.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //Veritabanı bağlatı zinciri
    {
    }

    public DbSet<Game> Games => Set<Game>();

    
}
