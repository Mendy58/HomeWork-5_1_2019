﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_5_1_2019.Data
{
    class PeopleQuestionsContext: DbContext
    {
        private string _connectionString;

        public PeopleQuestionsContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionsTags> QuestionsTags { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Taken from here:
            //https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration

            //set up composite primary key
            modelBuilder.Entity<QuestionsTags>()
                .HasKey(qt => new { qt.QuestionId, qt.TagId});

            //set up foreign key from QuestionsTags to Questions
            modelBuilder.Entity<QuestionsTags>()
                .HasOne(qt => qt.Question)
                .WithMany(q => q.QuestionsTags)
                .HasForeignKey(q => q.QuestionId);

            //set up foreign key from QuestionsTags to Tags
            modelBuilder.Entity<QuestionsTags>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuestionsTags)
                .HasForeignKey(q => q.TagId);
        }
    }
}

