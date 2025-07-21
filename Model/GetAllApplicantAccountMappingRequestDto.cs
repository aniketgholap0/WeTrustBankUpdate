namespace WeTrustBank.Model
{
    public class GetAllApplicantAccountMappingRequestDto
    {
        public long ApplicantAccountMappingId { get; set; }
        public long AccountStatusId { get; set; }
        public long ApplicantsId { get; set; }
        public long AccountNumber { get; set; } 
        public long AvailableBalance { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
