namespace ZaalVpn.API;

public class AppDb
{

    public string DataBaseName { get; set; }
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string TrustServerCertificate { get; set; }

    public override string ToString() =>
        $"Data Source={Host};Initial Catalog={DataBaseName};User ID={UserName};Password={Password};TrustServerCertificate= {TrustServerCertificate}";
}