namespace WeTrustBank.Common
{
    public class DataBaseService
    {
        private readonly string _connectionString;

        public DataBaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("WeTrustBankDBConnection");
        }
    }
}
