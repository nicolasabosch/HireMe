using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CabernetDBContext;
using EmmploymeNet.Model;
namespace EmmploymeNet.Model
{
    public partial class Entities : DbEntities
    {
        public virtual DbSet<ClientError> ClientError { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyType> CompanyType { get; set; }
        public virtual DbSet<DataTable> DataTable { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<JobApplicance> JobApplicance { get; set; }
        public virtual DbSet<JobApplicanceStatus> JobApplicanceStatus { get; set; }
        public virtual DbSet<JobCategory> JobCategory { get; set; }
        public virtual DbSet<JobCategorySkill> JobCategorySkill { get; set; }
        public virtual DbSet<JobPost> JobPost { get; set; }
        public virtual DbSet<JobPostSkill> JobPostSkill { get; set; }
        public virtual DbSet<JobPostStatus> JobPostStatus { get; set; }
        public virtual DbSet<JobRequest> JobRequest { get; set; }
        public virtual DbSet<JobRequestFile> JobRequestFile { get; set; }
        public virtual DbSet<JobRequestTool> JobRequestTool { get; set; }
        public virtual DbSet<JobRequestUser> JobRequestUser { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<MenuBar> MenuBar { get; set; }
        public virtual DbSet<MenuItem> MenuItem { get; set; }
        public virtual DbSet<Parameter> Parameter { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleMenuItem> RoleMenuItem { get; set; }
        public virtual DbSet<TextTranslation> TextTranslation { get; set; }
        public virtual DbSet<Tool> Tool { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }

        

    /*
        public Entities(DbContextOptions<Entities> options) : base(options)
        {
        }
    */
            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientError>(entity =>
            {
                entity.Property(e => e.ClientErrorID)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.ClientErrorName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyID).HasComment("Código");

                entity.Property(e => e.CompanyFullName)
                    .HasMaxLength(233)
                    .IsUnicode(false)
                    .HasComputedColumnSql("((([CompanyName]+' (')+CONVERT([varchar],[CompanyID]))+')')");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.CompanyTypeID)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CompanyType)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CompanyTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_CompanyType");
            });

            modelBuilder.Entity<CompanyType>(entity =>
            {
                entity.Property(e => e.CompanyTypeID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CompanyTypeName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DataTable>(entity =>
            {
                entity.Property(e => e.DataTableID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DataTableName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.Property(e => e.FileID)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FolderName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobApplicance>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.JobApplicanceFileID)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.JobApplicanceStatusID)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.JobApplicanceText).IsUnicode(false);

                entity.Property(e => e.JobPostID).HasComment("Código");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserID)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("User Id");

                entity.HasOne(d => d.JobApplicanceStatus)
                    .WithMany(p => p.JobApplicance)
                    .HasForeignKey(d => d.JobApplicanceStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobApplicance_JobApplicanceStatus");

                entity.HasOne(d => d.JobPost)
                    .WithMany(p => p.JobApplicance)
                    .HasForeignKey(d => d.JobPostID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobApplicance_JobPost");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.JobApplicance)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobApplicance_User");
            });

            modelBuilder.Entity<JobApplicanceStatus>(entity =>
            {
                entity.Property(e => e.JobApplicanceStatusID)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.JobApplicanceStatusName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobCategory>(entity =>
            {
                entity.Property(e => e.JobCategoryID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.JobCategoryName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobCategorySkill>(entity =>
            {
                entity.Property(e => e.JobCategorySkillID).HasComment("Código");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.JobCategoryID)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.JobCategorySkillName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.JobCategory)
                    .WithMany(p => p.JobCategorySkill)
                    .HasForeignKey(d => d.JobCategoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobCategorySkill_JobCategory");
            });

            modelBuilder.Entity<JobPost>(entity =>
            {
                entity.Property(e => e.JobPostID).HasComment("Código");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.JobCategoryID)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.JobPostDate)
                    .HasColumnType("date")
                    .HasComment("Código");

                entity.Property(e => e.JobPostDescription)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasComment("Descripción");

                entity.Property(e => e.JobPostName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Título");

                entity.Property(e => e.JobPostStatusID)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserID)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.HasOne(d => d.JobCategory)
                    .WithMany(p => p.JobPost)
                    .HasForeignKey(d => d.JobCategoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobPost_JobCategory");

                entity.HasOne(d => d.JobPostStatus)
                    .WithMany(p => p.JobPost)
                    .HasForeignKey(d => d.JobPostStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobPost_JobPostStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.JobPost)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobPost_User");
            });

            modelBuilder.Entity<JobPostSkill>(entity =>
            {
                entity.HasKey(e => new { e.JobPostID, e.JobCategorySkillID });

                entity.Property(e => e.JobPostID).HasComment("Código");

                entity.Property(e => e.JobCategorySkillID).HasComment("Código");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.JobCategorySkill)
                    .WithMany(p => p.JobPostSkill)
                    .HasForeignKey(d => d.JobCategorySkillID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobPostSkill_JobCategorySkill");

                entity.HasOne(d => d.JobPost)
                    .WithMany(p => p.JobPostSkill)
                    .HasForeignKey(d => d.JobPostID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobPostSkill_JobPost");
            });

            modelBuilder.Entity<JobPostStatus>(entity =>
            {
                entity.Property(e => e.JobPostStatusID)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.JobPostStatusName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobRequest>(entity =>
            {
                entity.Property(e => e.JobRequestID).HasComment("Código");

                entity.Property(e => e.CompanyID).HasComment("Código");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.JobCategoryID)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.JobRequestDescription)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasComment("Descripción");

                entity.Property(e => e.JobRequestName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Título");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.JobRequest)
                    .HasForeignKey(d => d.CompanyID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRequest_Company");

                entity.HasOne(d => d.JobCategory)
                    .WithMany(p => p.JobRequest)
                    .HasForeignKey(d => d.JobCategoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRequest_JobCategory");
            });

            modelBuilder.Entity<JobRequestFile>(entity =>
            {
                entity.Property(e => e.JobRequestFileID)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FileID)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.JobRequestFileName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.JobRequestID).HasComment("Código");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.File)
                    .WithMany(p => p.JobRequestFile)
                    .HasForeignKey(d => d.FileID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRequestFile_File");

                entity.HasOne(d => d.JobRequest)
                    .WithMany(p => p.JobRequestFile)
                    .HasForeignKey(d => d.JobRequestID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRequestFile_JobRequest");
            });

            modelBuilder.Entity<JobRequestTool>(entity =>
            {
                entity.HasKey(e => new { e.JobRequestID, e.ToolID });

                entity.Property(e => e.JobRequestID).HasComment("Código");

                entity.Property(e => e.ToolID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.JobRequest)
                    .WithMany(p => p.JobRequestTool)
                    .HasForeignKey(d => d.JobRequestID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRequestTool_JobRequest");

                entity.HasOne(d => d.Tool)
                    .WithMany(p => p.JobRequestTool)
                    .HasForeignKey(d => d.ToolID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRequestTool_Tool");
            });

            modelBuilder.Entity<JobRequestUser>(entity =>
            {
                entity.HasKey(e => new { e.JobRequestID, e.UserID });

                entity.Property(e => e.JobRequestID).HasComment("Código");

                entity.Property(e => e.UserID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("User Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.JobRequest)
                    .WithMany(p => p.JobRequestUser)
                    .HasForeignKey(d => d.JobRequestID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRequestUser_JobRequest");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.JobRequestUser)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobRequestUser_User");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.LanguageID)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LanguageName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MenuBar>(entity =>
            {
                entity.Property(e => e.MenuBarID)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MenuBarName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.Property(e => e.MenuItemID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayOrder).HasComment("Orden");

                entity.Property(e => e.IsPage).HasComment("Es Página");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MenuBarID)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Menu");

                entity.Property(e => e.MenuItemName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("Nombre");

                entity.Property(e => e.RouteName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("RouteName");

                entity.HasOne(d => d.MenuBar)
                    .WithMany(p => p.MenuItem)
                    .HasForeignKey(d => d.MenuBarID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuItem_MenuBar");
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.Property(e => e.ParameterID)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ParameterDescription).HasMaxLength(2000);

                entity.Property(e => e.ParameterName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("Rol");
            });

            modelBuilder.Entity<RoleMenuItem>(entity =>
            {
                entity.HasKey(e => new { e.RoleID, e.MenuItemID });

                entity.Property(e => e.RoleID)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.MenuItemID)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.MenuItem)
                    .WithMany(p => p.RoleMenuItem)
                    .HasForeignKey(d => d.MenuItemID)
                    .HasConstraintName("FK_RoleMenuItem_MenuItem");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleMenuItem)
                    .HasForeignKey(d => d.RoleID)
                    .HasConstraintName("FK_RoleMenuItem_Role");
            });

            modelBuilder.Entity<TextTranslation>(entity =>
            {
                entity.HasKey(e => new { e.Text, e.LanguageID });

                entity.Property(e => e.Text)
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.LanguageID)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Translation)
                    .IsRequired()
                    .HasMaxLength(2000);
            });

            modelBuilder.Entity<Tool>(entity =>
            {
                entity.Property(e => e.ToolID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ToolName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Nombre");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("User Id");

                entity.Property(e => e.Active).HasComment("Activo");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasComment("Fecha Nacimiento");

                entity.Property(e => e.CompanyID).HasComment("Empresa");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("E Mail");

                entity.Property(e => e.FileID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Curriculum");

                entity.Property(e => e.ForceChangePassword).HasComment("Forzar Cambio Contraseña");

                entity.Property(e => e.InvitedOn).HasComment("Fecha Invitación");

                entity.Property(e => e.LastLogon).HasComment("Último Login");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LogonName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Password");

                entity.Property(e => e.ReceiveNotification).HasComment("Recibe Notificaciones");

                entity.Property(e => e.ResetPasswordID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("ResetPasswordID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Nombre y Apellido");

                entity.Property(e => e.UserTypeID)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Tipo Usuario");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Código Postal");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_User_Company");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.FileID)
                    .HasConstraintName("FK_User_File");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserType");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserID, e.RoleID })
                    .HasName("PK_ApplicationUserRole");

                entity.Property(e => e.UserID)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.RoleID)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleID)
                    .HasConstraintName("FK_ApplicationUserRole_ApplicationRole");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK_ApplicationUserRole_ApplicationUser");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.UserTypeID)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserTypeName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    }
