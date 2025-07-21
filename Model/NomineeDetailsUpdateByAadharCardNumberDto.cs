namespace WeTrustBank.Model
{
    public class NomineeDetailsUpdateByAadharCardNumberDto
    {
        public  long AadharCardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PanNumber { get; set; }
        public string UpdatedBy { get; set; }
    }
}
