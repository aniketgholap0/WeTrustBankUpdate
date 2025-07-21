using System.Runtime.InteropServices;

namespace WeTrustBank.Model
{
    public class ApplicantUpdateByAadharCardNumberDto
    {
        public long AadharCardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string PanNumber { get; set; }
        public string UpdatedBy { get; set; }
    }
}
