using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using webApi.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace webApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
    public DbSet<Tournament> Tournaments {get; set;}
    public DbSet<User> Users {get; set;}
    public DbSet<Event> Events {get; set;}
    public DbSet<EventDetailStatus> EventDetailStatuses {get; set;}
    public DbSet<EventDetail> EventDetails {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventDetail>()
        .HasKey(cs => new {cs.EventId, cs.EventDetailStatusId});
    }

    }

}
