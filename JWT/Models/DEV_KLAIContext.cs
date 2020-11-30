using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JWT.Models
{
    public partial class DEV_KLAIContext : DbContext
    {
        public DEV_KLAIContext()
        {
        }

        public DEV_KLAIContext(DbContextOptions<DEV_KLAIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<Corse> Corse { get; set; }
        public virtual DbSet<CorseCategorie> CorseCategorie { get; set; }
        public virtual DbSet<CoworkingSpace> CoworkingSpace { get; set; }
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<ExpenseCatalogue> ExpenseCatalogue { get; set; }
        public virtual DbSet<FormationSubType> FormationSubType { get; set; }
        public virtual DbSet<FormationType> FormationType { get; set; }
        public virtual DbSet<Formations> Formations { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Profil> Profil { get; set; }
        public virtual DbSet<ProfileAction> ProfileAction { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<SesionFormation> SesionFormation { get; set; }
        public virtual DbSet<Settlement> Settlement { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=DEV_KLAI;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Action>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Corse>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorseCatalogueId).HasColumnName("CorseCatalogue_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.CorseCatalogue)
                    .WithMany(p => p.Corse)
                    .HasForeignKey(d => d.CorseCatalogueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Corse_CorseCatalogue_id_Corse_categorie_id");
            });

            modelBuilder.Entity<CorseCategorie>(entity =>
            {
                entity.ToTable("Corse_categorie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoworkingSpace>(entity =>
            {
                entity.ToTable("Coworking_Space");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adresse)
                    .IsRequired()
                    .HasColumnName("adresse")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Email1)
                    .IsRequired()
                    .HasColumnName("Email_1")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Email2)
                    .IsRequired()
                    .HasColumnName("Email_2")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile1).HasColumnName("Mobile_1");

                entity.Property(e => e.Mobile2).HasColumnName("Mobile_2");

                entity.Property(e => e.Mobile3).HasColumnName("Mobile_3");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PriceOneDay).HasColumnName("Price_One_Day");

                entity.Property(e => e.PriceOneHour).HasColumnName("Price_One_Hour");

                entity.Property(e => e.PriceOneMonth).HasColumnName("Price_One_Month");

                entity.Property(e => e.PriceOneYear).HasColumnName("Price_One_year");

                entity.Property(e => e.WebSite)
                    .IsRequired()
                    .HasColumnName("Web_Site")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.CoworkingSpace)
                    .HasForeignKey(d => d.Owner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Coworking_Space_Owner_Users_id");
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ExpenseCatalogieId).HasColumnName("ExpenseCatalogie_id");

                entity.Property(e => e.ExpenseDate)
                    .HasColumnName("Expense_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExpenseCatalogie)
                    .WithMany(p => p.Expense)
                    .HasForeignKey(d => d.ExpenseCatalogieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Expense_ExpenseCatalogie_id_Expense_Catalogue_id");
            });

            modelBuilder.Entity<ExpenseCatalogue>(entity =>
            {
                entity.ToTable("Expense_Catalogue");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FormationSubType>(entity =>
            {
                entity.ToTable("Formation_Sub_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FormationType>(entity =>
            {
                entity.ToTable("Formation_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FormationSubTypeId).HasColumnName("Formation_sub_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FormationSubType)
                    .WithMany(p => p.FormationType)
                    .HasForeignKey(d => d.FormationSubTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Formation_type_Formation_sub_type_id_Formation_Sub_Type_id");
            });

            modelBuilder.Entity<Formations>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorseId).HasColumnName("Corse_id");

                entity.Property(e => e.FormationTypeId).HasColumnName("Formation_type_id");

                entity.Property(e => e.MaxLimitNumberPlace).HasColumnName("Max_Limit_Number_place");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Corse)
                    .WithMany(p => p.Formations)
                    .HasForeignKey(d => d.CorseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Formations_Corse_id_Corse_id");

                entity.HasOne(d => d.FormationType)
                    .WithMany(p => p.Formations)
                    .HasForeignKey(d => d.FormationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Formations_Formation_type_id_Formation_type_id");

                entity.HasOne(d => d.FormerNavigation)
                    .WithMany(p => p.Formations)
                    .HasForeignKey(d => d.Former)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Formations_Former_Users_id");
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SettlementId).HasColumnName("settlement_id");

                entity.HasOne(d => d.Settlement)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.SettlementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Invoices_settlement_id_Settlement_id");
            });

            modelBuilder.Entity<Profil>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProfileAction>(entity =>
            {
                entity.ToTable("Profile_action");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionId).HasColumnName("Action_id");

                entity.Property(e => e.ProfilId).HasColumnName("profil_id");

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.ProfileAction)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Profile_action_Action_id_Action_id");

                entity.HasOne(d => d.Profil)
                    .WithMany(p => p.ProfileAction)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Profile_action_profil_id_Profil_id");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SessionFormationId).HasColumnName("SessionFormation_id");

                entity.Property(e => e.SettlementId).HasColumnName("Settlement_id");

                entity.HasOne(d => d.SessionFormation)
                    .WithMany(p => p.PurchaseOrder)
                    .HasForeignKey(d => d.SessionFormationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchaseOrder_SessionFormation_id_Sesion_Formation_id");

                entity.HasOne(d => d.Settlement)
                    .WithMany(p => p.PurchaseOrder)
                    .HasForeignKey(d => d.SettlementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PurchaseOrder_Settlement_id_Settlement_id");
            });

            modelBuilder.Entity<SesionFormation>(entity =>
            {
                entity.ToTable("Sesion_Formation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DayNumber).HasColumnName("Day_number");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(1);

                entity.Property(e => e.EndDate)
                    .HasColumnName("End_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.HourNumber).HasColumnName("Hour_number");

                entity.Property(e => e.InscriptionNumber).HasColumnName("Inscription_number");

                entity.Property(e => e.Localisation)
                    .IsRequired()
                    .HasColumnName("localisation")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NumberPresent).HasColumnName("Number_present");

                entity.Property(e => e.ParticipationNumberMax).HasColumnName("Participation_Number_max");

                entity.Property(e => e.ParticipationNumberMin).HasColumnName("Participation_number_min");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SessionStatut)
                    .IsRequired()
                    .HasColumnName("session_statut")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasColumnName("Start_Date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CowoekingPlaceNavigation)
                    .WithMany(p => p.SesionFormation)
                    .HasForeignKey(d => d.CowoekingPlace)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sesion_Formation_CowoekingPlace_Coworking_Space_id");

                entity.HasOne(d => d.FormationNavigation)
                    .WithMany(p => p.SesionFormation)
                    .HasForeignKey(d => d.Formation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sesion_Formation_Formation_Formations_id");
            });

            modelBuilder.Entity<Settlement>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Lastname)
                 // .IsRequired()
                  .HasColumnName("Lastname")
                  .HasMaxLength(255)
                  .IsUnicode(false);
                entity.Property(e => e.Email)
                // .IsRequired()
                 .HasColumnName("Email")
                 .HasMaxLength(255)
                 .IsUnicode(false);
                entity.Property(e => e.Password)
              // .IsRequired()
               .HasColumnName("Password")
               .HasMaxLength(20)
               .IsUnicode(false);

                entity.Property(e => e.ProfilId).HasColumnName("profil_id");

                entity.HasOne(d => d.Profil)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Users_profil_id_Profil_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
