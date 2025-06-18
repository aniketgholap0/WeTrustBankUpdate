namespace WeTrustBank.Model
{
    public class ApplicantDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; } 
        public string Email { get; set; }
        public string Gender { get; set; }  
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public long AadharCardNumber { get; set; }
        public String PanNumber { get; set; }   
        public string CreatedBy { get; set; }   
    }
}
