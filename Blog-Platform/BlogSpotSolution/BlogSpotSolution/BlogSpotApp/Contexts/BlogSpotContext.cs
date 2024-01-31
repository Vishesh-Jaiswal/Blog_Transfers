﻿using BlogSpotApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSpotApp.Contexts
{
    public class BlogSpotContext : DbContext
    {

        public BlogSpotContext(DbContextOptions options) : base(options)
        {
            Users = Set<User>();
            Blogs = Set<Blog>();
            Comments = Set<Comment>();
            Categories = Set<Category>();
            BlogLikes = Set<BlogLike>();
            CommentLikes = Set<CommentLike>();
            UserFollowers = Set<UserFollower>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogLike> BlogLikes { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasMany(u => u.Blogs)
                .WithOne(bp => bp.Author)
                .HasForeignKey(bp => bp.UserEmail)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Blog>()
                 .HasMany(b => b.Comments)
                 .WithOne(c => c.BlogComment)
                 .HasForeignKey(c => c.BlogId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Commenter)
                .WithMany(u => u.UserComments)
                .HasForeignKey(c => c.UserEmail)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Category)
                .WithMany(c => c.Blogs);

            modelBuilder.Entity<UserFollower>()
                .HasOne(uf => uf.Follower)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollower>()
                .HasOne(uf => uf.Following)
                .WithMany(u => u.Followings)
                .HasForeignKey(uf => uf.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}