namespace WeTrustBank.Model
{
    public class ApplicantUpdateDto
    {
        public int ApplicantsID { get; set; }

        public String FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public String Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }
        
        public string District { get; set; }
        
        public int ZipCode { get; set; } 

        public long AadharCardNumber { get; set; }  

        public String PanNumber { get; set; }

        public string UpdatedBy { get; set; }
       
    }
}
