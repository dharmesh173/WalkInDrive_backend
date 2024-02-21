using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WalkInDrive.Models;

public partial class MydbContext : DbContext
{
    public MydbContext()
    {
    }

    public MydbContext(DbContextOptions<MydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DriveApplied> DriveApplieds { get; set; }

    public virtual DbSet<DriveAvailableSlot> DriveAvailableSlots { get; set; }

    public virtual DbSet<EducationDetail> EducationDetails { get; set; }

    public virtual DbSet<PreRequisite> PreRequisites { get; set; }

    public virtual DbSet<ProfessionalDetail> ProfessionalDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<Technology> Technologies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WalkInDrife> WalkInDrives { get; set; }

    public virtual DbSet<drive_applied_has_job_roles> roles_has_drive_applied { get; set; }

    public virtual DbSet<drive_has_roles> drives_has_roles { get; set; }

    public virtual DbSet<User_preffered_roles>  users_has_preferred_roles { get; set; }

    public virtual DbSet<technology_expert> technology_expertise { get; set; }

    public virtual DbSet<technology_familliar> technology_famillier { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=mydb;user=root;password=Dharmesh@123", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");
        modelBuilder.Entity<drive_has_roles>().HasKey(m => new { m.walkin_drive_id, m.role_id });

        modelBuilder.Entity<technology_expert>().HasKey(m => new { m.professional_details_work_id, m.technology_id });

        modelBuilder.Entity<technology_familliar>().HasKey(m => new { m.professional_details_work_id, m.technology_id });

        modelBuilder.Entity<DriveApplied>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("drive_applied")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => new { e.SlotId, e.DriveId }, "fk_drive_applied_drive_available_slots1_idx");

            entity.HasIndex(e => e.UserId, "fk_drive_applied_users1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DriveId).HasColumnName("drive_id");
            entity.Property(e => e.DtCreated)
                .HasColumnType("datetime")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .HasColumnType("datetime")
                .HasColumnName("dt_modified");
            entity.Property(e => e.Resume)
                .HasColumnType("blob")
                .HasColumnName("resume");
            entity.Property(e => e.SlotId).HasColumnName("slot_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.DriveApplieds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_drive_applied_users1");

            entity.HasOne(d => d.DriveAvailableSlot).WithMany(p => p.DriveApplieds)
                .HasForeignKey(d => new { d.SlotId, d.DriveId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_drive_applied_drive_available_slots1");
        });

        modelBuilder.Entity<DriveAvailableSlot>(entity =>
        {
            entity.HasKey(e => new { e.SlotsId, e.WalkInDrivesDriveId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity
                .ToTable("drive_available_slots")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.SlotsId, "fk_drive_available_slots_Slots1_idx");

            entity.HasIndex(e => e.WalkInDrivesDriveId, "fk_drive_available_slots_walk_in_drives1_idx");

            entity.Property(e => e.SlotsId).HasColumnName("slots_id");
            entity.Property(e => e.WalkInDrivesDriveId).HasColumnName("walk_in_drives_drive_id");

            entity.HasOne(d => d.Slots).WithMany(p => p.DriveAvailableSlots)
                .HasForeignKey(d => d.SlotsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_drive_available_slots_Slots1");

            entity.HasOne(d => d.WalkInDrivesDrive).WithMany(p => p.DriveAvailableSlots)
                .HasForeignKey(d => d.WalkInDrivesDriveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_drive_available_slots_walk_in_drives1");
        });

       

       modelBuilder.Entity<EducationDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("education_details")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.UserId, "fk_education_details_users1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CollegeName)
                .HasMaxLength(255)
                .HasColumnName("college_name");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .HasColumnType("datetime")
                .HasColumnName("dt_modified");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.PassoutYear).HasColumnName("passout_year");
            entity.Property(e => e.Percentage).HasColumnName("percentage");
            entity.Property(e => e.Qualification)
                .HasMaxLength(255)
                .HasColumnName("qualification");
            entity.Property(e => e.Stream)
                .HasMaxLength(255)
                .HasColumnName("stream");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.EducationDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_education_details_users1");
        });

        modelBuilder.Entity<PreRequisite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("pre_requisite")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DtCreated)
                .HasColumnType("datetime")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .HasColumnType("datetime")
                .HasColumnName("dt_modified");
            entity.Property(e => e.ExamInst).HasColumnName("exam_inst");
            entity.Property(e => e.GeneralInst).HasColumnName("general_inst");
            entity.Property(e => e.Process).HasColumnName("process");
            entity.Property(e => e.Round1Date).HasColumnName("round1_date");
            entity.Property(e => e.Round1Type).HasColumnName("round1_type");
            entity.Property(e => e.Round2Date).HasColumnName("round2_date");
            entity.Property(e => e.Round2Type).HasColumnName("round2_type");
            entity.Property(e => e.SystemRequirement).HasColumnName("system_requirement");
        });

        modelBuilder.Entity<ProfessionalDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("professional_details")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.UserId, "fk_professional_details_users1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicantType)
                .HasMaxLength(255)
                .HasColumnName("applicant_type");
            entity.Property(e => e.CurrentCtc)
                .HasDefaultValueSql("'0'")
                .HasColumnName("current_ctc");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .HasColumnType("datetime")
                .HasColumnName("dt_modified");
            entity.Property(e => e.ExpectedCtc).HasColumnName("expected_ctc");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.MonthOfNp)
                .HasMaxLength(45)
                .HasColumnName("month_of_np");
            entity.Property(e => e.NoticePeriod)
                .HasDefaultValueSql("'0'")
                .HasColumnName("notice_period");
            entity.Property(e => e.NoticePeriodEnd).HasColumnName("notice_period_end");
            entity.Property(e => e.PreAppear).HasColumnName("pre_appear");
            entity.Property(e => e.PreappearRole)
                .HasMaxLength(255)
                .HasColumnName("preappear_role");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ProfessionalDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_professional_details_users1");

            entity.HasMany(d => d.Technologies).WithMany(p => p.ProfessionalDetailsWorks)
                .UsingEntity<Dictionary<string, object>>(
                    "TechnologyExpertise2",
                    r => r.HasOne<Technology>().WithMany()
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_professional_details_has_technology_expertise_technology_e1"),
                    l => l.HasOne<ProfessionalDetail>().WithMany()
                        .HasForeignKey("ProfessionalDetailsWorkId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_professional_details_has_technology_expertise_professional1"),
                    j =>
                    {
                        j.HasKey("ProfessionalDetailsWorkId", "TechnologyId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("technology_expertise2")
                            .HasCharSet("utf8mb3")
                            .UseCollation("utf8mb3_general_ci");
                        j.HasIndex(new[] { "ProfessionalDetailsWorkId" }, "fk_professional_details_has_technology_expertise_profession_idx");
                        j.HasIndex(new[] { "TechnologyId" }, "fk_professional_details_has_technology_expertise_technology_idx");
                        j.IndexerProperty<int>("ProfessionalDetailsWorkId").HasColumnName("professional_details_work_id");
                        j.IndexerProperty<int>("TechnologyId").HasColumnName("technology_id");
                    });

            entity.HasMany(d => d.TechnologiesNavigation).WithMany(p => p.ProfessionalDetailsWorksNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "TechnologyFamillier2",
                    r => r.HasOne<Technology>().WithMany()
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_professional_details_has_technology_technology1"),
                    l => l.HasOne<ProfessionalDetail>().WithMany()
                        .HasForeignKey("ProfessionalDetailsWorkId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_professional_details_has_technology_professional_details1"),
                    j =>
                    {
                        j.HasKey("ProfessionalDetailsWorkId", "TechnologyId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("technology_famillier2")
                            .HasCharSet("utf8mb3")
                            .UseCollation("utf8mb3_general_ci");
                        j.HasIndex(new[] { "ProfessionalDetailsWorkId" }, "fk_professional_details_has_technology_professional_details_idx");
                        j.HasIndex(new[] { "TechnologyId" }, "fk_professional_details_has_technology_technology1_idx");
                        j.IndexerProperty<int>("ProfessionalDetailsWorkId").HasColumnName("professional_details_work_id");
                        j.IndexerProperty<int>("TechnologyId").HasColumnName("technology_id");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity
                .ToTable("roles")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .HasColumnType("datetime")
                .HasColumnName("dt_modified");
            entity.Property(e => e.GrossCompensationPkg).HasColumnName("gross_compensation_pkg");
            entity.Property(e => e.RoleDescription).HasColumnName("role_description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("role_name");
            entity.Property(e => e.RoleRequirements).HasColumnName("role_requirements");

            entity.HasMany(d => d.DriveApplieds).WithMany(p => p.RolesRoles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolesHasDriveApplied2",
                    r => r.HasOne<DriveApplied>().WithMany()
                        .HasForeignKey("DriveAppliedId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_roles_has_drive_applied_drive_applied1"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_roles_has_drive_applied_roles1"),
                    j =>
                    {
                        j.HasKey("RolesRoleId", "DriveAppliedId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("roles_has_drive_applied2")
                            .HasCharSet("utf8mb3")
                            .UseCollation("utf8mb3_general_ci");
                        j.HasIndex(new[] { "DriveAppliedId" }, "fk_roles_has_drive_applied_drive_applied1_idx");
                        j.HasIndex(new[] { "RolesRoleId" }, "fk_roles_has_drive_applied_roles1_idx");
                        j.IndexerProperty<int>("RolesRoleId").HasColumnName("roles_role_id");
                        j.IndexerProperty<int>("DriveAppliedId").HasColumnName("drive_applied_id");
                    });
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("slots")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DtCreated)
                .HasColumnType("datetime")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .HasColumnType("datetime")
                .HasColumnName("dt_modified");
            entity.Property(e => e.SlotTiming)
                .HasMaxLength(255)
                .HasColumnName("slot_timing");
        });

        modelBuilder.Entity<Technology>(entity =>
        {
            entity.HasKey(e => e.TechnologyId).HasName("PRIMARY");

            entity
                .ToTable("technology")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.TechnologyId).HasColumnName("technology_id");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .HasColumnType("datetime")
                .HasColumnName("dt_modified");
            entity.Property(e => e.OtherTechnology)
                .HasMaxLength(255)
                .HasColumnName("other_technology");
            entity.Property(e => e.TechnologyName)
                .HasMaxLength(255)
                .HasColumnName("technology_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity
                .ToTable("users")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .HasColumnType("datetime")
                .HasColumnName("dt_modified");
            entity.Property(e => e.Email)
                .HasMaxLength(55)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(45)
                .HasColumnName("firstname");
            entity.Property(e => e.GetUpdate)
                .HasDefaultValueSql("'1'")
                .HasColumnName("get_update");
            entity.Property(e => e.Lastname)
                .HasMaxLength(45)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .HasColumnName("phone");
            entity.Property(e => e.PortfolioUrl)
                .HasMaxLength(255)
                .HasColumnName("portfolio_url");
            entity.Property(e => e.ProfilePic)
                .HasColumnType("blob")
                .HasColumnName("profile_pic");
            entity.Property(e => e.ReferredPersonName)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NA'")
                .HasColumnName("referred_person_name");
            entity.Property(e => e.Resume)
                .HasColumnType("blob")
                .HasColumnName("resume");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UsersHasPreferredRole2",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_users_has_roles_roles1"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_users_has_roles_users1"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("users_has_preferred_roles2")
                            .HasCharSet("utf8mb3")
                            .UseCollation("utf8mb3_general_ci");
                        j.HasIndex(new[] { "RoleId" }, "fk_users_has_roles_roles1_idx");
                        j.HasIndex(new[] { "UserId" }, "fk_users_has_roles_users1_idx");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<WalkInDrife>(entity =>
        {
            entity.HasKey(e => e.DriveId).HasName("PRIMARY");

            entity
                .ToTable("walk_in_drives")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.DriveId).HasColumnName("drive_id");
            entity.Property(e => e.DriveEndDate).HasColumnName("drive_end_date");
            entity.Property(e => e.DriveStartDate).HasColumnName("drive_start_date");
            entity.Property(e => e.DriveTitle)
                .HasMaxLength(255)
                .HasColumnName("drive_title");
            entity.Property(e => e.DtCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dt_created");
            entity.Property(e => e.DtModified)
                .HasColumnType("datetime")
                .HasColumnName("dt_modified");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.OtherDetails)
                .HasMaxLength(255)
                .HasColumnName("other_details");
            entity.Property(e => e.Time)
                .HasColumnType("time")
                .HasColumnName("time");

            entity.HasMany(d => d.Roles).WithMany(p => p.WalkinDrives)
                .UsingEntity<Dictionary<string, object>>(
                    "DrivesHasRole2",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_drives_has_roles_roles1"),
                    l => l.HasOne<WalkInDrife>().WithMany()
                        .HasForeignKey("WalkinDriveId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_walk_in_drives_has_roles_walk_in_drives1"),
                    j =>
                    {
                        j.HasKey("WalkinDriveId", "RoleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("drives_has_roles_2")
                            .HasCharSet("utf8mb3")
                            .UseCollation("utf8mb3_general_ci");
                        j.HasIndex(new[] { "RoleId" }, "fk_drives_has_roles_roles1_idx");
                        j.HasIndex(new[] { "WalkinDriveId" }, "fk_walk_in_drives_has_roles_walk_in_drives1_idx");
                        j.IndexerProperty<int>("WalkinDriveId").HasColumnName("walkin_drive_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
