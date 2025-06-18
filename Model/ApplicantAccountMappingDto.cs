using System.Globalization;

namespace WeTrustBank.Model
{
    public class ApplicantAccountMappingDto
    {
        public long AadharNumber { get; set; }
        public string PanNumber { get; set; }
        public string CreatedBy { get; set; }
    }
}
