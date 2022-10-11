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
        public virtual DbSet<JobCategory> JobCategory { get; set; }
        public virtual DbSet<JobCategorySkill> JobCategorySkill { get; set; }
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
                entity.Property(e => e.ClientErrorID).IsUnicode(false);

                entity.Property(e => e.ClientErrorName).IsUnicode(false);

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyID).HasComment("Código");

                entity.Property(e => e.CompanyFullName)
                    .IsUnicode(false)
                    .HasComputedColumnSql("((([CompanyName]+' (')+CONVERT([varchar],[CompanyID]))+')')");

                entity.Property(e => e.CompanyName)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.CompanyTypeID)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

                entity.HasOne(d => d.CompanyType)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CompanyTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_CompanyType");
            });

            modelBuilder.Entity<CompanyType>(entity =>
            {
                entity.Property(e => e.CompanyTypeID)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CompanyTypeName)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);
            });

            modelBuilder.Entity<DataTable>(entity =>
            {
                entity.Property(e => e.DataTableID)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.DataTableName)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);
            });

            modelBuilder.Entity<DataTranslation>(entity =>
            {
                entity.HasKey(e => new { e.ID, e.FieldName, e.LanguageID });

                entity.Property(e => e.ID).IsUnicode(false);

                entity.Property(e => e.FieldName).IsUnicode(false);

                entity.Property(e => e.LanguageID)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

                entity.Property(e => e.Number).HasComputedColumnSql("(TRY_CAST([ID] AS [int]))");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.Property(e => e.FileID).IsUnicode(false);

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.FileName).IsUnicode(false);

                entity.Property(e => e.FolderName).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);
            });

            modelBuilder.Entity<JobCategory>(entity =>
            {
                entity.Property(e => e.JobCategoryID)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.JobCategoryName)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);
            });

            modelBuilder.Entity<JobCategorySkill>(entity =>
            {
                entity.Property(e => e.JobCategorySkillID).HasComment("Código");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.JobCategoryID)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.JobCategorySkillName)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

                entity.HasOne(d => d.JobCategory)
                    .WithMany(p => p.JobCategorySkill)
                    .HasForeignKey(d => d.JobCategoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobCategorySkill_JobCategory");
            });

            modelBuilder.Entity<JobRequest>(entity =>
            {
                entity.Property(e => e.JobRequestID).HasComment("Código");

                entity.Property(e => e.CompanyID).HasComment("Código");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.JobCategoryID)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.JobRequestDescription)
                    .IsUnicode(false)
                    .HasComment("Descripción");

                entity.Property(e => e.JobRequestName)
                    .IsUnicode(false)
                    .HasComment("Título");

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

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
                entity.Property(e => e.JobRequestFileID).IsUnicode(false);

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.FileID).IsUnicode(false);

                entity.Property(e => e.JobRequestFileName).IsUnicode(false);

                entity.Property(e => e.JobRequestID).HasComment("Código");

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

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
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

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
                    .IsUnicode(false)
                    .HasComment("User Id");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

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
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LanguageName)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);
            });

            modelBuilder.Entity<MenuBar>(entity =>
            {
                entity.Property(e => e.MenuBarID).IsUnicode(false);

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.Property(e => e.MenuItemID)
                    .IsUnicode(false)
                    .HasComment("ID");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.DisplayOrder).HasComment("Orden");

                entity.Property(e => e.IsPage).HasComment("Es Página");

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

                entity.Property(e => e.MenuBarID)
                    .IsUnicode(false)
                    .HasComment("Menu");

                entity.Property(e => e.MenuItemName).HasComment("Nombre");

                entity.Property(e => e.RouteName)
                    .IsUnicode(false)
                    .HasComment("RouteName");
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.Property(e => e.ParameterID).IsUnicode(false);

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

                entity.Property(e => e.ParameterName).IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleID)
                    .IsUnicode(false)
                    .HasComment("ID");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

                entity.Property(e => e.RoleName).HasComment("Rol");
            });

            modelBuilder.Entity<RoleMenuItem>(entity =>
            {
                entity.HasKey(e => new { e.RoleID, e.MenuItemID });

                entity.Property(e => e.RoleID).IsUnicode(false);

                entity.Property(e => e.MenuItemID).IsUnicode(false);

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

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

                entity.Property(e => e.Text).IsUnicode(false);

                entity.Property(e => e.LanguageID)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);
            });

            modelBuilder.Entity<Tool>(entity =>
            {
                entity.Property(e => e.ToolID)
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

                entity.Property(e => e.ToolName)
                    .IsUnicode(false)
                    .HasComment("Nombre");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserID)
                    .IsUnicode(false)
                    .HasComment("User Id");

                entity.Property(e => e.Active).HasComment("Activo");

                entity.Property(e => e.BirthDate).HasComment("Fecha Nacimiento");

                entity.Property(e => e.CompanyID).HasComment("Empresa");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsUnicode(false)
                    .HasComment("E Mail");

                entity.Property(e => e.FileID)
                    .IsUnicode(false)
                    .HasComment("Curriculum");

                entity.Property(e => e.ForceChangePassword).HasComment("Forzar Cambio Contraseña");

                entity.Property(e => e.InvitedOn).HasComment("Fecha Invitación");

                entity.Property(e => e.LastLogon).HasComment("Último Login");

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

                entity.Property(e => e.LogonName)
                    .IsUnicode(false)
                    .HasComment("Login");

                entity.Property(e => e.Password)
                    .IsUnicode(false)
                    .HasComment("Password");

                entity.Property(e => e.ReceiveNotification).HasComment("Recibe Notificaciones");

                entity.Property(e => e.ResetPasswordID)
                    .IsUnicode(false)
                    .HasComment("ResetPasswordID");

                entity.Property(e => e.UserName)
                    .IsUnicode(false)
                    .HasComment("Nombre y Apellido");

                entity.Property(e => e.UserTypeID)
                    .IsUnicode(false)
                    .HasComment("Tipo Usuario");

                entity.Property(e => e.ZipCode)
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

                entity.Property(e => e.UserID).IsUnicode(false);

                entity.Property(e => e.RoleID).IsUnicode(false);

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

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
                    .IsUnicode(false)
                    .HasComment("Código");

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.LastModifiedBy).IsUnicode(false);

                entity.Property(e => e.UserTypeName)
                    .IsUnicode(false)
                    .HasComment("Nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    }
