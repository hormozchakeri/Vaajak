namespace Share
{
    public class ConnectionStrings
    {
        public static string DatabaseContext{ get; } = "Server=localhost;Database=vaajak;Trusted_Connection=True;TrustServerCertificate=true;Integrated Security=True";
        public static string IdentityDatabaseContext { get; } = "Server=localhost;Database=vaajakIdentity;Trusted_Connection=True;TrustServerCertificate=true;Integrated Security=True";
    }
}
