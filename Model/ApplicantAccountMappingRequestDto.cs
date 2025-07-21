namespace WeTrustBank.Model
{
    public class ApplicantAccountMappingRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long AccountNumber { get; set; }
        public string AccountStatusName{ get; set; }
    }
}
