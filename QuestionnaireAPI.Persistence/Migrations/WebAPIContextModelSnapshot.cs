﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuestionnaireAPI.Persistence.Context;

#nullable disable

namespace QuestionnaireAPI.Persistance.Migrations
{
    [DbContext(typeof(WebAPIContext))]
    partial class WebAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuestionnaireAPI.Domain.Entities.SubmittedAnswer", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("QuestionnaireId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("AnswerType")
                        .HasColumnType("int");

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Value")
                        .HasColumnType("int");

                    b.HasKey("UserId", "QuestionnaireId", "SubjectId", "QuestionId");

                    b.HasIndex(new[] { "Department", "QuestionnaireId", "SubjectId", "QuestionId" }, "IX_SubmittedAnswer_Department_FK_QuestionnaireId_FK_SubjectId_FK_QuestionId");

                    b.ToTable("SubmittedAnswers");
                });

            modelBuilder.Entity("QuestionnaireAPI.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("bca592ae-2abd-4c98-b613-567971ab0004"),
                            Department = 1,
                            Email = "johndoe@company.com",
                            Name = "John Doe"
                        },
                        new
                        {
                            Id = new Guid("4c0b9bcd-b5e2-442a-8db2-c3538d24a219"),
                            Department = 2,
                            Email = "mariedoe@company.com",
                            Name = "Marie Doe"
                        },
                        new
                        {
                            Id = new Guid("ed37d0e2-bcf9-4282-a486-a39d1d2fea0c"),
                            Department = 3,
                            Email = "patrickdoe@company.com",
                            Name = "Patrick Doe"
                        },
                        new
                        {
                            Id = new Guid("345a8a04-8449-4427-bea3-7c60dadbf00e"),
                            Department = 5,
                            Email = "lizdoe@company.com",
                            Name = "Elizabeth Doe"
                        },
                        new
                        {
                            Id = new Guid("0308e2ba-0bff-4777-96c5-bf7f43ed2858"),
                            Department = 4,
                            Email = "williamdoe@company.com",
                            Name = "William Doe"
                        },
                        new
                        {
                            Id = new Guid("163a5b8f-f8e7-4cb8-88d1-85a52b4bb7cb"),
                            Department = 1,
                            Email = "francescocorleone@company.com",
                            Name = "Francesco Corleone"
                        },
                        new
                        {
                            Id = new Guid("43996d7e-17bf-4ddd-8dea-05cddf9dedfb"),
                            Department = 2,
                            Email = "sophiecorleone@company.com",
                            Name = "Sophie Corleone"
                        },
                        new
                        {
                            Id = new Guid("718af9c4-4546-4645-b4bd-739640bb8e5e"),
                            Department = 3,
                            Email = "donvitocorleone@company.com",
                            Name = "Don Vito Corleone"
                        },
                        new
                        {
                            Id = new Guid("fc73f606-ebc4-4689-a9c4-9339a33d70b9"),
                            Department = 5,
                            Email = "georgescarface@company.com",
                            Name = "George Scarface"
                        },
                        new
                        {
                            Id = new Guid("6bbf92ef-d065-4ae7-9d2f-2f8a023b42e1"),
                            Department = 4,
                            Email = "patrickcorleone@company.com",
                            Name = "Patrick Corleone"
                        });
                });

            modelBuilder.Entity("QuestionnaireAPI.Domain.Entities.SubmittedAnswer", b =>
                {
                    b.HasOne("QuestionnaireAPI.Domain.Entities.User", "User")
                        .WithMany("SubmittedAnswers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuestionnaireAPI.Domain.Entities.User", b =>
                {
                    b.Navigation("SubmittedAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
