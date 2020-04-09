using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class EstimeContext : DbContext
    {
        public EstimeContext()
        {
        }

        public EstimeContext(DbContextOptions<EstimeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CodeMember> CodeMember { get; set; }
        public virtual DbSet<CodeSet> CodeSet { get; set; }
        public virtual DbSet<CodeSetType> CodeSetType { get; set; }
        public virtual DbSet<TcCodeMember> TcCodeMember { get; set; }
        public virtual DbSet<TcCodeSet> TcCodeSet { get; set; }
        public virtual DbSet<TcCodeSetType> TcCodeSetType { get; set; }
        public virtual DbSet<TcRefPeriod> TcRefPeriod { get; set; }
        public virtual DbSet<TcRefPeriodType> TcRefPeriodType { get; set; }
        public virtual DbSet<TdLoad> TdLoad { get; set; }
        public virtual DbSet<TdLoadData> TdLoadData { get; set; }
        public virtual DbSet<TdLoadStaging> TdLoadStaging { get; set; }
        public virtual DbSet<TlColumnDelimiter> TlColumnDelimiter { get; set; }
        public virtual DbSet<TlEstimeFileType> TlEstimeFileType { get; set; }
        public virtual DbSet<TlFileType> TlFileType { get; set; }
        public virtual DbSet<TlInputCoordinate> TlInputCoordinate { get; set; }
        public virtual DbSet<TlInputVariable> TlInputVariable { get; set; }
        public virtual DbSet<TlInputVariableValue> TlInputVariableValue { get; set; }
        public virtual DbSet<TlLoadStatus> TlLoadStatus { get; set; }
        public virtual DbSet<TlVariable> TlVariable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodeMember>(entity =>
            {
                entity.ToTable("CodeMember", "ESTIME");

                entity.HasIndex(e => new { e.CodeSetId, e.Code })
                    .HasName("UQ_CodeMemberCode")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeSet)
                    .WithMany(p => p.CodeMember)
                    .HasForeignKey(d => d.CodeSetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CodeMember_CodeSetTemp");
            });

            modelBuilder.Entity<CodeSet>(entity =>
            {
                entity.ToTable("CodeSet", "ESTIME");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ_CodeSetCode")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeSetType)
                    .WithMany(p => p.CodeSet)
                    .HasForeignKey(d => d.CodeSetTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CodeSet_CodeSetTypeTemp");
            });

            modelBuilder.Entity<CodeSetType>(entity =>
            {
                entity.ToTable("CodeSetType", "ESTIME");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ_CodeSetTypeCode")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TcCodeMember>(entity =>
            {
                entity.ToTable("tc_CodeMember", "ESTIME");

                entity.HasIndex(e => new { e.CodeSetId, e.Code })
                    .HasName("UQ_tc_CodeMemberCode")
                    .IsUnique();

                entity.Property(e => e.AlphaCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.AggTop)
                    .WithMany(p => p.InverseAggTop)
                    .HasForeignKey(d => d.AggTopId)
                    .HasConstraintName("FK_CodeMember_CodeMemberTop");

                entity.HasOne(d => d.AggUp)
                    .WithMany(p => p.InverseAggUp)
                    .HasForeignKey(d => d.AggUpId)
                    .HasConstraintName("FK_CodeMember_CodeMemberUp");

                entity.HasOne(d => d.CodeSet)
                    .WithMany(p => p.TcCodeMember)
                    .HasForeignKey(d => d.CodeSetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CodeMember_CodeSet");
            });

            modelBuilder.Entity<TcCodeSet>(entity =>
            {
                entity.ToTable("tc_CodeSet", "ESTIME");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ_tc_CodeSetCode")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeSetType)
                    .WithMany(p => p.TcCodeSet)
                    .HasForeignKey(d => d.CodeSetTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CodeSet_CodeSetType");
            });

            modelBuilder.Entity<TcCodeSetType>(entity =>
            {
                entity.ToTable("tc_CodeSetType", "ESTIME");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ_tc_CodeSetTypeCode")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TcRefPeriod>(entity =>
            {
                entity.ToTable("tc_RefPeriod", "ESTIME");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ_tc_RefPeriodCode")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.AggTop)
                    .WithMany(p => p.InverseAggTop)
                    .HasForeignKey(d => d.AggTopId)
                    .HasConstraintName("FK_RefPeriod_RefPeriodTop");

                entity.HasOne(d => d.AggUp)
                    .WithMany(p => p.InverseAggUp)
                    .HasForeignKey(d => d.AggUpId)
                    .HasConstraintName("FK_RefPeriod_RefPeriodUp");

                entity.HasOne(d => d.Previous)
                    .WithMany(p => p.InversePrevious)
                    .HasForeignKey(d => d.PreviousId)
                    .HasConstraintName("FK_RefPeriod_RefPeriodPrev");

                entity.HasOne(d => d.RefPeriodType)
                    .WithMany(p => p.TcRefPeriod)
                    .HasForeignKey(d => d.RefPeriodTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefPeriod_RefPeriodType");
            });

            modelBuilder.Entity<TcRefPeriodType>(entity =>
            {
                entity.ToTable("tc_RefPeriodType", "ESTIME");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ_tc_RefPeriodTypeCode")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TdLoad>(entity =>
            {
                entity.ToTable("td_Load", "ESTIME");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ErrorMessage)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.EstimeFileType)
                    .WithMany(p => p.TdLoad)
                    .HasForeignKey(d => d.EstimeFileTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstimeFileType_Load");

                entity.HasOne(d => d.LoadStatus)
                    .WithMany(p => p.TdLoad)
                    .HasForeignKey(d => d.LoadStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoadStatus_Load");
            });

            modelBuilder.Entity<TdLoadData>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("td_LoadData", "ESTIME");

                entity.HasIndex(e => e.InputVariableId)
                    .HasName("FK_InputVariable_LoadData");

                entity.HasIndex(e => e.RefPeriodId)
                    .HasName("FK_RefPeriod_LoadData");

                entity.HasIndex(e => new { e.LoadId, e.InputVariableId, e.RecordNumber })
                    .HasName("UQ_td_LoadData")
                    .IsUnique();

                entity.Property(e => e.VariableValue)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.InputVariable)
                    .WithMany(p => p.TdLoadData)
                    .HasForeignKey(d => d.InputVariableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InputVariable_LoadData");

                entity.HasOne(d => d.Load)
                    .WithMany(p => p.TdLoadData)
                    .HasForeignKey(d => d.LoadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Load_LoadData");

                entity.HasOne(d => d.RefPeriod)
                    .WithMany(p => p.TdLoadData)
                    .HasForeignKey(d => d.RefPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefPeriod_LoadData");
            });

            modelBuilder.Entity<TdLoadStaging>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("td_LoadStaging", "ESTIME");

                entity.HasOne(d => d.Load)
                    .WithMany(p => p.TdLoadStaging)
                    .HasForeignKey(d => d.LoadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Load_LoadStaging");
            });

            modelBuilder.Entity<TlColumnDelimiter>(entity =>
            {
                entity.ToTable("tl_ColumnDelimiter", "ESTIME");

                entity.HasIndex(e => e.Code)
                    .HasName("UK_tl_ColumnDelimiter")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TlEstimeFileType>(entity =>
            {
                entity.ToTable("tl_EstimeFileType", "ESTIME");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ_tl_EstimeFileType")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ColumnDelimiter)
                    .WithMany(p => p.TlEstimeFileType)
                    .HasForeignKey(d => d.ColumnDelimiterId)
                    .HasConstraintName("FK_ColumnDelimiter_EstimeFileType");

                entity.HasOne(d => d.FileType)
                    .WithMany(p => p.TlEstimeFileType)
                    .HasForeignKey(d => d.FileTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FileType_EstimeFileType");
            });

            modelBuilder.Entity<TlFileType>(entity =>
            {
                entity.ToTable("tl_FileType", "ESTIME");

                entity.HasIndex(e => e.Extension)
                    .HasName("UK_tl_FileType")
                    .IsUnique();

                entity.Property(e => e.Extension)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TlInputCoordinate>(entity =>
            {
                entity.ToTable("tl_InputCoordinate", "ESTIME");

                entity.HasIndex(e => e.InputVariableId)
                    .HasName("FK_InputVariable_InputFileCoordinate");

                entity.HasIndex(e => new { e.RecordNumber, e.InputVariableId })
                    .HasName("UQ_tl_EstimateFileCoordinate")
                    .IsUnique();

                entity.HasOne(d => d.InputVariable)
                    .WithMany(p => p.TlInputCoordinate)
                    .HasForeignKey(d => d.InputVariableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InputVariable_InputFileCoordinate");
            });

            modelBuilder.Entity<TlInputVariable>(entity =>
            {
                entity.ToTable("tl_InputVariable", "ESTIME");

                entity.HasIndex(e => e.EstimeFileTypeId)
                    .HasName("FK_Variable_InputVariable");

                entity.HasIndex(e => e.VariableId)
                    .HasName("FK_EstimeFileType_InputVariable");

                entity.HasIndex(e => new { e.EstimeFileTypeId, e.VariableId })
                    .HasName("UQ_tl_InputVariable")
                    .IsUnique();

                entity.Property(e => e.IsMandatory).HasColumnName("isMandatory");

                entity.Property(e => e.IsParameter).HasColumnName("isParameter");

                entity.HasOne(d => d.EstimeFileType)
                    .WithMany(p => p.TlInputVariable)
                    .HasForeignKey(d => d.EstimeFileTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EstimeFileType_EstimeVariable");

                entity.HasOne(d => d.Variable)
                    .WithMany(p => p.TlInputVariable)
                    .HasForeignKey(d => d.VariableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Variable_EstimeVariable");
            });

            modelBuilder.Entity<TlInputVariableValue>(entity =>
            {
                entity.ToTable("tl_InputVariableValue", "ESTIME");

                entity.HasIndex(e => e.InputVariableId)
                    .HasName("FK_InputVariable_InputVariableValue");

                entity.HasIndex(e => new { e.InputVariableId, e.RecordNumber })
                    .HasName("UQ_tl_InputVariableValue")
                    .IsUnique();

                entity.Property(e => e.VariableValue)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.InputVariable)
                    .WithMany(p => p.TlInputVariableValue)
                    .HasForeignKey(d => d.InputVariableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InputVariable_InputVariableValue");
            });

            modelBuilder.Entity<TlLoadStatus>(entity =>
            {
                entity.ToTable("tl_LoadStatus", "ESTIME");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ_tl_LoadStatusCode")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TlVariable>(entity =>
            {
                entity.ToTable("tl_Variable", "ESTIME");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ_tl_Variable")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DataType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameFrench)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeSet)
                    .WithMany(p => p.TlVariable)
                    .HasForeignKey(d => d.CodeSetId)
                    .HasConstraintName("FK_Variable_CodeSet");
            });

            modelBuilder.HasSequence<int>("NextFileColumn")
                .StartsAt(17)
                .HasMin(0);
        }
    }
}
