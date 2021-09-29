using Core.Entites;
using Core.Entites.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class DataContext:IdentityDbContext<ApplicationUser,ApplicationRole, string>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<post_tag>().HasKey(sc => new { sc.postId, sc.tagId });

        }

        public DbSet<Post> post { get; set; }

        public DbSet<post_tag> post_Tags { get; set; }

        public DbSet<category> categories { get; set; }

        public DbSet<Tag> tags { get; set; }

    }
}
