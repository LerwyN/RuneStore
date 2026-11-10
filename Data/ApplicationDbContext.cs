using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RUNE.Models;
using RUNE.Models.Entities;
using RUNE.Models.ReleaseNotes;

namespace RUNE.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //Veritabanı bağlatı zinciri
    {
    }

    public DbSet<Game> Games => Set<Game>();
    public DbSet<ReleaseNote> Notes => Set<ReleaseNote>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Tag> Tags => Set<Tag>();
    
}
