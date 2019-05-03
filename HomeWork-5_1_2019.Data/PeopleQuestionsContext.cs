using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
        public DbSet<AnswerLikes> AnswerLikes { get; set; }
        public DbSet<QuestionLikes> QuestionLikes { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            //.SelectMany(t => t.GetForeignKeys())
            //.Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            //base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //base.OnModelCreating(modelBuilder); Saw this went with above, pulled off web
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

            modelBuilder.Entity<QuestionLikes>()
                .HasKey(qt => new { qt.Questionid, qt.Userid });

            modelBuilder.Entity<QuestionLikes>()
                .HasOne(qt => qt.User)
                .WithMany(q => q.QuestionLikes)
                .HasForeignKey(q => q.Userid);

            modelBuilder.Entity<QuestionLikes>()
                .HasOne(qt => qt.Question)
                .WithMany(q => q.QuestionLikes)
                .HasForeignKey(q => q.Questionid);

            modelBuilder.Entity<AnswerLikes>()
             .HasKey(qt => new { qt.Answerid, qt.Userid });

            modelBuilder.Entity<AnswerLikes>()
                .HasOne(qt => qt.User)
                .WithMany(q => q.AnswerLikes)
                .HasForeignKey(q => q.Userid);

            modelBuilder.Entity<AnswerLikes>()
                .HasOne(qt => qt.Answer)
                .WithMany(q => q.AnswerLikes)
                .HasForeignKey(q => q.Answerid);
        }
    }
}

