﻿// <auto-generated />
using System;
using HomeWork_5_1_2019.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeWork_5_1_2019.Data.Migrations
{
    [DbContext(typeof(PeopleQuestionsContext))]
    [Migration("20190502074741_PersonQMigrationUpdate")]
    partial class PersonQMigrationUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeWork_5_1_2019.Data.Answer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Personid");

                    b.Property<int?>("QuestionId");

                    b.Property<int?>("Userid");

                    b.Property<string>("answer");

                    b.HasKey("id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("Userid");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("HomeWork_5_1_2019.Data.AnswerLikes", b =>
                {
                    b.Property<int>("Answerid");

                    b.Property<int>("Userid");

                    b.HasKey("Answerid", "Userid");

                    b.HasIndex("Userid");

                    b.ToTable("AnswerLikes");
                });

            modelBuilder.Entity("HomeWork_5_1_2019.Data.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatePosted");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.Property<int>("Userid");

                    b.HasKey("Id");

                    b.HasIndex("Userid");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("HomeWork_5_1_2019.Data.QuestionLikes", b =>
                {
                    b.Property<int>("Questionid");

                    b.Property<int>("Userid");

                    b.HasKey("Questionid", "Userid");

                    b.HasIndex("Userid");

                    b.ToTable("QuestionLikes");
                });

            modelBuilder.Entity("HomeWork_5_1_2019.Data.QuestionsTags", b =>
                {
                    b.Property<int>("QuestionId");

                    b.Property<int>("TagId");

                    b.HasKey("QuestionId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("QuestionsTags");
                });

            modelBuilder.Entity("HomeWork_5_1_2019.Data.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("HomeWork_5_1_2019.Data.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("email");

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HomeWork_5_1_2019.Data.Answer", b =>
                {
                    b.HasOne("HomeWork_5_1_2019.Data.Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId");

                    b.HasOne("HomeWork_5_1_2019.Data.User")
                        .WithMany("Answers")
                        .HasForeignKey("Userid");
                });

            modelBuilder.Entity("HomeWork_5_1_2019.Data.AnswerLikes", b =>
                {
                    b.HasOne("HomeWork_5_1_2019.Data.Answer", "Answer")
                        .WithMany("AnswerLikes")
                        .HasForeignKey("Answerid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HomeWork_5_1_2019.Data.User", "User")
                        .WithMany("AnswerLikes")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeWork_5_1_2019.Data.Question", b =>
                {
                    b.HasOne("HomeWork_5_1_2019.Data.User")
                        .WithMany("Questions")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeWork_5_1_2019.Data.QuestionLikes", b =>
                {
                    b.HasOne("HomeWork_5_1_2019.Data.Question", "Question")
                        .WithMany("QuestionLikes")
                        .HasForeignKey("Questionid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HomeWork_5_1_2019.Data.User", "User")
                        .WithMany("QuestionLikes")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HomeWork_5_1_2019.Data.QuestionsTags", b =>
                {
                    b.HasOne("HomeWork_5_1_2019.Data.Question", "Question")
                        .WithMany("QuestionsTags")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HomeWork_5_1_2019.Data.Tag", "Tag")
                        .WithMany("QuestionsTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}