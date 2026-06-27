namespace Share
{
    public class ConnectionStrings
    {

        //sqlServer

        //public static string DatabaseContext { get; } = "Server=localhost\\SQLEXPRESS;Database=vaajak;TrustServerCertificate=true;Trusted_Connection=True;";

        //public static string IdentityDatabaseContext { get; } = "Server=localhost\\SQLEXPRESS;Database=vaajakIdentity;TrustServerCertificate=true;Trusted_Connection=True;";
        //public static string IdentityDatabaseContext { get; } = "Server=localhost,2222;Database=vaajakIdentity;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=true;";

        //Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;

        //postgresSql
        public static string DatabaseContext { get; } = "Host=localhost;Port=5432;Database=vaajak;Username=postgres;Password=17022018;";
        public static string IdentityDatabaseContext { get; } = "Host=localhost;Port=5432;Database=vaajakIdentity;Username=postgres;Password=17022018;";

    }
}
