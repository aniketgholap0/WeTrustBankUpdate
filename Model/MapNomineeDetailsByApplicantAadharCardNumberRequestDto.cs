namespace WeTrustBank.Model
{
    public class MapNomineeDetailsByApplicantAadharCardNumberRequestDto
    {
        public long AadharCardNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNumber { get; set; }
        public long NomineeAadharCardNumber { get; set; }
        public string PanNumber { get; set; }
        public string CreatedBy { get; set; } 
    }
}
