namespace ZaalVpn.ViewModel;

public record AddServerViewModel
{
    public string CountryId { get; set; }
    public bool IsActive { get; set; }
    public List<AddConfigServer>? ConfigServers { get; set; }
}
public record EditServerViewModel
{
    public string Id { get; set; }
    public string CountryId { get; set; }
    public bool IsActive { get; set; }
    public List<EditConfigServer>? ConfigServers { get; set; }
}