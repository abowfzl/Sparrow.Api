using Microsoft.EntityFrameworkCore;
using Sparrow.Core.Entities;

namespace Sparrow.Data;

public partial class SparrowDbContext : DbContext
{
    public SparrowDbContext()
    {
    }

    public SparrowDbContext(DbContextOptions<SparrowDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AbcBankrefund> AbcBankrefunds { get; set; } = null!;
    public virtual DbSet<AbcBankrefundBase> AbcBankrefundBases { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AI");

        modelBuilder.Entity<AbcBankrefund>(entity =>
        {
            entity.HasNoKey();

            entity.ToView("abc_bankrefund");

            entity.Property(e => e.AbcBankrefundId).HasColumnName("abc_bankrefundId");

            entity.Property(e => e.AbcCardNumber)
                .HasMaxLength(30)
                .HasColumnName("abc_CardNumber");

            entity.Property(e => e.AbcCardOwner)
                .HasMaxLength(100)
                .HasColumnName("abc_CardOwner");

            entity.Property(e => e.AbcCurrentStep).HasColumnName("abc_CurrentStep");

            entity.Property(e => e.AbcDescription).HasColumnName("abc_Description");

            entity.Property(e => e.AbcForce).HasColumnName("abc_Force");

            entity.Property(e => e.AbcMobileNumber).HasColumnName("abc_MobileNumber");

            entity.Property(e => e.AbcMobileNumberName)
                .HasMaxLength(160)
                .HasColumnName("abc_MobileNumberName");

            entity.Property(e => e.AbcMobileNumberText)
                .HasMaxLength(20)
                .HasColumnName("abc_MobileNumberText");

            entity.Property(e => e.AbcMobileNumberYomiName)
                .HasMaxLength(450)
                .HasColumnName("abc_MobileNumberYomiName");

            entity.Property(e => e.AbcName)
                .HasMaxLength(100)
                .HasColumnName("abc_name");

            entity.Property(e => e.AbcNextStep).HasColumnName("abc_NextStep");

            entity.Property(e => e.AbcOrderId)
                .HasMaxLength(100)
                .HasColumnName("abc_OrderID");

            entity.Property(e => e.AbcOrderIdnew).HasColumnName("abc_OrderIDNew");

            entity.Property(e => e.AbcOrderIdnewName)
                .HasMaxLength(20)
                .HasColumnName("abc_OrderIDNewName");

            entity.Property(e => e.AbcOrdersLink)
                .HasMaxLength(150)
                .HasColumnName("abc_OrdersLink");

            entity.Property(e => e.AbcPayAmount).HasColumnName("abc_PayAmount");

            entity.Property(e => e.AbcPaymentReason).HasColumnName("abc_PaymentReason");

            entity.Property(e => e.AbcShebaNo)
                .HasMaxLength(40)
                .HasColumnName("abc_ShebaNo");

            entity.Property(e => e.AbcTrackingNumber)
                .HasMaxLength(100)
                .HasColumnName("abc_TrackingNumber");

            entity.Property(e => e.AbcWebsite).HasColumnName("abc_Website");

            entity.Property(e => e.CreatedByName).HasMaxLength(200);

            entity.Property(e => e.CreatedByYomiName).HasMaxLength(200);

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.CreatedOnBehalfByName).HasMaxLength(200);

            entity.Property(e => e.CreatedOnBehalfByYomiName).HasMaxLength(200);

            entity.Property(e => e.ModifiedByName).HasMaxLength(200);

            entity.Property(e => e.ModifiedByYomiName).HasMaxLength(200);

            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.ModifiedOnBehalfByName).HasMaxLength(200);

            entity.Property(e => e.ModifiedOnBehalfByYomiName).HasMaxLength(200);

            entity.Property(e => e.OverriddenCreatedOn).HasColumnType("datetime");

            entity.Property(e => e.OwnerIdName).HasMaxLength(160);

            entity.Property(e => e.OwnerIdYomiName).HasMaxLength(160);

            entity.Property(e => e.Processid).HasColumnName("processid");

            entity.Property(e => e.Stageid).HasColumnName("stageid");

            entity.Property(e => e.Statecode).HasColumnName("statecode");

            entity.Property(e => e.Statuscode).HasColumnName("statuscode");

            entity.Property(e => e.Traversedpath)
                .HasMaxLength(1250)
                .HasColumnName("traversedpath");

            entity.Property(e => e.UtcconversionTimeZoneCode).HasColumnName("UTCConversionTimeZoneCode");

            entity.Property(e => e.VersionNumber)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<AbcBankrefundBase>(entity =>
        {
            entity.HasKey(e => e.AbcBankrefundId);

            entity.ToTable("abc_bankrefundBase");

            entity.HasIndex(e => new { e.Statecode, e.Statuscode }, "ndx_Core")
                .HasFillFactor(80);

            entity.HasIndex(e => e.OwnerId, "ndx_Security")
                .HasFillFactor(80);

            entity.HasIndex(e => e.VersionNumber, "ndx_Sync")
                .HasFilter("([VersionNumber] IS NOT NULL)")
                .HasFillFactor(80);

            entity.HasIndex(e => e.AbcName, "ndx_abc_name")
                .HasFillFactor(80);

            entity.HasIndex(e => e.AbcOrderIdnew, "ndx_for_cascaderelationship_abc_abc_neworder_abc_bankrefund_OrderIDNew")
                .HasFilter("([abc_OrderIDNew] IS NOT NULL)")
                .HasFillFactor(80);

            entity.HasIndex(e => e.AbcMobileNumber, "ndx_for_cascaderelationship_abc_contact_abc_bankrefund_MobileNumber")
                .HasFilter("([abc_MobileNumber] IS NOT NULL)")
                .HasFillFactor(80);

            entity.Property(e => e.AbcBankrefundId)
                .ValueGeneratedNever()
                .HasColumnName("abc_bankrefundId");

            entity.Property(e => e.AbcCardNumber)
                .HasMaxLength(30)
                .HasColumnName("abc_CardNumber");

            entity.Property(e => e.AbcCardOwner)
                .HasMaxLength(100)
                .HasColumnName("abc_CardOwner");

            entity.Property(e => e.AbcCurrentStep).HasColumnName("abc_CurrentStep");

            entity.Property(e => e.AbcDescription).HasColumnName("abc_Description");

            entity.Property(e => e.AbcForce).HasColumnName("abc_Force");

            entity.Property(e => e.AbcMobileNumber).HasColumnName("abc_MobileNumber");

            entity.Property(e => e.AbcMobileNumberText)
                .HasMaxLength(20)
                .HasColumnName("abc_MobileNumberText");

            entity.Property(e => e.AbcName)
                .HasMaxLength(100)
                .HasColumnName("abc_name");

            entity.Property(e => e.AbcNextStep).HasColumnName("abc_NextStep");

            entity.Property(e => e.AbcOrderId)
                .HasMaxLength(100)
                .HasColumnName("abc_OrderID");

            entity.Property(e => e.AbcOrderIdnew).HasColumnName("abc_OrderIDNew");

            entity.Property(e => e.AbcOrdersLink)
                .HasMaxLength(150)
                .HasColumnName("abc_OrdersLink");

            entity.Property(e => e.AbcPayAmount).HasColumnName("abc_PayAmount");

            entity.Property(e => e.AbcPaymentReason).HasColumnName("abc_PaymentReason");

            entity.Property(e => e.AbcShebaNo)
                .HasMaxLength(40)
                .HasColumnName("abc_ShebaNo");

            entity.Property(e => e.AbcTrackingNumber)
                .HasMaxLength(100)
                .HasColumnName("abc_TrackingNumber");

            entity.Property(e => e.AbcWebsite).HasColumnName("abc_Website");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

            entity.Property(e => e.OverriddenCreatedOn).HasColumnType("datetime");

            entity.Property(e => e.OwnerIdType).HasDefaultValueSql("((8))");

            entity.Property(e => e.Processid).HasColumnName("processid");

            entity.Property(e => e.Stageid).HasColumnName("stageid");

            entity.Property(e => e.Statecode).HasColumnName("statecode");

            entity.Property(e => e.Statuscode).HasColumnName("statuscode");

            entity.Property(e => e.Traversedpath)
                .HasMaxLength(1250)
                .HasColumnName("traversedpath");

            entity.Property(e => e.UtcconversionTimeZoneCode).HasColumnName("UTCConversionTimeZoneCode");

            entity.Property(e => e.VersionNumber)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.HasSequence<int>("SO_currentcasenumber")
            .StartsAt(0)
            .IsCyclic();

        modelBuilder.HasSequence<int>("SO_NextEmailTrackingNumber")
            .StartsAt(0)
            .HasMin(0)
            .HasMax(999)
            .IsCyclic();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
