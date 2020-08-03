﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webApi.Data;

namespace webApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("webApi.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventNumber")
                        .HasColumnType("int");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("eventDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("eventEndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("eventName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventID");

                    b.HasIndex("TournamentId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("webApi.Models.EventDetail", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("EventDetailStatusId")
                        .HasColumnType("int");

                    b.Property<int>("EventDetailId")
                        .HasColumnType("int");

                    b.Property<string>("EventDetailName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventDetailNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("EventDetailOdd")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("FinishingPosition")
                        .HasColumnType("int");

                    b.Property<int>("FirstTimer")
                        .HasColumnType("int");

                    b.HasKey("EventId", "EventDetailStatusId");

                    b.HasIndex("EventDetailStatusId");

                    b.ToTable("EventDetails");
                });

            modelBuilder.Entity("webApi.Models.EventDetailStatus", b =>
                {
                    b.Property<int>("EventDetailStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EventDetailStatusName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventDetailStatusId");

                    b.ToTable("EventDetailStatuses");
                });

            modelBuilder.Entity("webApi.Models.Tournament", b =>
                {
                    b.Property<int>("TournamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TournamentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("TournamentId");

                    b.HasIndex("userId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("webApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("webApi.Models.Event", b =>
                {
                    b.HasOne("webApi.Models.Tournament", "Tournament")
                        .WithMany("Event")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("webApi.Models.EventDetail", b =>
                {
                    b.HasOne("webApi.Models.EventDetailStatus", "EventDetailStatus")
                        .WithMany("EventDetail")
                        .HasForeignKey("EventDetailStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webApi.Models.Event", "Event")
                        .WithMany("EventDetail")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("webApi.Models.Tournament", b =>
                {
                    b.HasOne("webApi.Models.User", "user")
                        .WithMany("Tournaments")
                        .HasForeignKey("userId");
                });
#pragma warning restore 612, 618
        }
    }
}
