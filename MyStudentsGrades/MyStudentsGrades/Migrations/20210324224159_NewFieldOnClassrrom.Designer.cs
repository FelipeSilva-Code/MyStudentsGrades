﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyStudentsGrades.Models;

namespace MyStudentsGrades.Migrations
{
    [DbContext(typeof(MyStudentsGradesContext))]
    [Migration("20210324224159_NewFieldOnClassrrom")]
    partial class NewFieldOnClassrrom
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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
#pragma warning restore 612, 618
        }
    }
}