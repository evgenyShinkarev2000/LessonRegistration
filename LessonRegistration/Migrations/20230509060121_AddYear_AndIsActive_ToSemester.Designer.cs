﻿// <auto-generated />
using LessonRegistration.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LessonRegistration.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230509060121_AddYear_AndIsActive_ToSemester")]
    partial class AddYear_AndIsActive_ToSemester
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LessonRegistration.Data.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("InstituteId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InstituteId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("LessonRegistration.Data.Institute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Institutes");
                });

            modelBuilder.Entity("LessonRegistration.Data.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("SemesterNumber")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("LessonRegistration.Data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("AverageGrade")
                        .HasColumnType("real");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("InstituteId")
                        .HasColumnType("integer");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SemesterId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("InstituteId");

                    b.HasIndex("SemesterId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LessonRegistration.Data.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SemesterSubject", b =>
                {
                    b.Property<int>("SubjectsId")
                        .HasColumnType("integer");

                    b.Property<int>("UsedInSemestersId")
                        .HasColumnType("integer");

                    b.HasKey("SubjectsId", "UsedInSemestersId");

                    b.HasIndex("UsedInSemestersId");

                    b.ToTable("SemesterSubject");
                });

            modelBuilder.Entity("StudentSubject", b =>
                {
                    b.Property<int>("RegisteredStudentsId")
                        .HasColumnType("integer");

                    b.Property<int>("SelectedSubjectId")
                        .HasColumnType("integer");

                    b.HasKey("RegisteredStudentsId", "SelectedSubjectId");

                    b.HasIndex("SelectedSubjectId");

                    b.ToTable("StudentSubject");
                });

            modelBuilder.Entity("LessonRegistration.Data.Department", b =>
                {
                    b.HasOne("LessonRegistration.Data.Institute", "Institute")
                        .WithMany("Departments")
                        .HasForeignKey("InstituteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Institute");
                });

            modelBuilder.Entity("LessonRegistration.Data.Semester", b =>
                {
                    b.HasOne("LessonRegistration.Data.Department", "Department")
                        .WithMany("Semesters")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("LessonRegistration.Data.Student", b =>
                {
                    b.HasOne("LessonRegistration.Data.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LessonRegistration.Data.Institute", "Institute")
                        .WithMany()
                        .HasForeignKey("InstituteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LessonRegistration.Data.Semester", "Semester")
                        .WithMany("Students")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Institute");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("SemesterSubject", b =>
                {
                    b.HasOne("LessonRegistration.Data.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LessonRegistration.Data.Semester", null)
                        .WithMany()
                        .HasForeignKey("UsedInSemestersId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentSubject", b =>
                {
                    b.HasOne("LessonRegistration.Data.Student", null)
                        .WithMany()
                        .HasForeignKey("RegisteredStudentsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LessonRegistration.Data.Subject", null)
                        .WithMany()
                        .HasForeignKey("SelectedSubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("LessonRegistration.Data.Department", b =>
                {
                    b.Navigation("Semesters");
                });

            modelBuilder.Entity("LessonRegistration.Data.Institute", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("LessonRegistration.Data.Semester", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
