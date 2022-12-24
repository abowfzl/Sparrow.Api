namespace Sparrow.Core.Entities;

public class AbcBankrefund
{
    public string? CreatedByName { get; set; }
    public string? CreatedByYomiName { get; set; }
    public string? CreatedOnBehalfByName { get; set; }
    public string? CreatedOnBehalfByYomiName { get; set; }
    public string? ModifiedByName { get; set; }
    public string? ModifiedByYomiName { get; set; }
    public string? ModifiedOnBehalfByName { get; set; }
    public string? ModifiedOnBehalfByYomiName { get; set; }
    public string? AbcMobileNumberName { get; set; }
    public string? AbcOrderIdnewName { get; set; }
    public string? AbcMobileNumberYomiName { get; set; }
    public Guid OwnerId { get; set; }
    public string? OwnerIdName { get; set; }
    public string? OwnerIdYomiName { get; set; }
    public int OwnerIdDsc { get; set; }
    public int? OwnerIdType { get; set; }
    public Guid? OwningUser { get; set; }
    public Guid? OwningTeam { get; set; }
    public Guid AbcBankrefundId { get; set; }
    public DateTime? CreatedOn { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public Guid? ModifiedBy { get; set; }
    public Guid? CreatedOnBehalfBy { get; set; }
    public Guid? ModifiedOnBehalfBy { get; set; }
    public Guid? OwningBusinessUnit { get; set; }
    public int Statecode { get; set; }
    public int? Statuscode { get; set; }
    public byte[]? VersionNumber { get; set; }
    public int? ImportSequenceNumber { get; set; }
    public DateTime? OverriddenCreatedOn { get; set; }
    public int? TimeZoneRuleVersionNumber { get; set; }
    public int? UtcconversionTimeZoneCode { get; set; }
    public string? AbcName { get; set; }
    public Guid? Processid { get; set; }
    public Guid? Stageid { get; set; }
    public string? Traversedpath { get; set; }
    public Guid? AbcMobileNumber { get; set; }
    public int? AbcPayAmount { get; set; }
    public string? AbcCardNumber { get; set; }
    public string? AbcShebaNo { get; set; }
    public int? AbcPaymentReason { get; set; }
    public int? AbcCurrentStep { get; set; }
    public int? AbcNextStep { get; set; }
    public bool? AbcForce { get; set; }
    public string? AbcTrackingNumber { get; set; }
    public string? AbcCardOwner { get; set; }
    public string? AbcOrderId { get; set; }
    public string? AbcOrdersLink { get; set; }
    public string? AbcMobileNumberText { get; set; }
    public string? AbcDescription { get; set; }
    public Guid? AbcOrderIdnew { get; set; }
    public int? AbcWebsite { get; set; }
}
