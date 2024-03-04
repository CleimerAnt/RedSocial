using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Domain.Commons;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Persitence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Publication> publications { get; set; }
        public DbSet<Reply> replies { get; set; }
        public DbSet<User> users { get; set; }  
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefauldAppUser";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LasModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefauldAppUser";
                        break;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region "Tables"
            modelBuilder.Entity<Friends>().ToTable("Friends");
            modelBuilder.Entity<Publication>().ToTable("Publications");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Reply>().ToTable("Replies");
            #endregion

            #region "Primary Key"
            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<Publication>().HasKey(p => p.Id);
            modelBuilder.Entity<Friends>().HasKey(f => f.Id);
            modelBuilder.Entity<Reply>().HasKey(r => r.Id);
            modelBuilder.Entity<User>().HasKey(r => r.Id);
            #endregion

            #region "Relations"

            #region "Publication"
            modelBuilder.Entity<Publication>().HasMany(p => p.Comments).WithMany(c => c.Publications);
            modelBuilder.Entity<Publication>().HasOne(p => p.User).WithMany(u => u.publications)
                .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Publication>().Property(p => p.Text).IsRequired(false);
            #endregion

            #region "Comments"
            modelBuilder.Entity<Comment>().HasMany(c => c.Publications).WithMany(p => p.Comments);
            modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany(u => u.comment).HasForeignKey(c => c.UserId);
            modelBuilder.Entity<Comment>().HasMany(c => c.Replys).WithOne(r => r.comment)
                .HasForeignKey(r => r.comentId).OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "User"
            modelBuilder.Entity<User>().HasMany(u => u.publications).WithOne(p => p.User)
                .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().HasMany(u => u.friends).WithMany(f => f.Users);

            modelBuilder.Entity<User>().HasMany(u => u.comment).WithOne(f => f.User)
                .HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region "Reply"
            modelBuilder.Entity<Reply>().HasOne(r => r.comment).WithMany(c => c.Replys)
                .HasForeignKey(c => c.comentId).OnDelete(DeleteBehavior.NoAction);
          
            #endregion

            #region "Friends"
            modelBuilder.Entity<Friends>().HasMany(f => f.Users).WithMany(u => u.friends);
            #endregion

            #endregion


        }

    }
}

