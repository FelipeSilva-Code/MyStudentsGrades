﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyStudentsGrades.Models;

namespace MyStudentsGrades.Migrations
{
    [DbContext(typeof(MyStudentsGradesContext))]
    [Migration("20210325000156_OtherEntities")]
    partial class OtherEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MyStudentsGrades.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ClassroomId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Quarter")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("MyStudentsGrades.Models.Classroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClassroomName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NameSchool")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("RequiredMedia")
                        .HasColumnType("double");

                    b.Property<string>("SchoolSubject")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Classroom");
                });

            modelBuilder.Entity("MyStudentsGrades.Models.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ActivityId")
                        .HasColumnType("int");

                    b.Property<string>("Observation")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("StudentGrade")
                        .HasColumnType("double");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("StudentId");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("MyStudentsGrades.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ClassroomId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("MyStudentsGrades.Models.Activity", b =>
                {
                    b.HasOne("MyStudentsGrades.Models.Classroom", "Classroom")
                        .WithMany("Activities")
                        .HasForeignKey("ClassroomId");
                });

            modelBuilder.Entity("MyStudentsGrades.Models.Grade", b =>
                {
                    b.HasOne("MyStudentsGrades.Models.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId");

                    b.HasOne("MyStudentsGrades.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("MyStudentsGrades.Models.Student", b =>
                {
                    b.HasOne("MyStudentsGrades.Models.Classroom", "Classroom")
                        .WithMany("Students")
                        .HasForeignKey("ClassroomId");
                });
#pragma warning restore 612, 618
        }
    }
}
