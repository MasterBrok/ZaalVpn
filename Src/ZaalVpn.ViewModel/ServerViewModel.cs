namespace ZaalVpn.ViewModel;

public record ServerViewModel
{
    public string Country { get; set; }
    public string Image { get; set; }
    public string Id { get; set; }

    public List<ConfigViewModel> Configs { get; set; }
}

public record AddConfigServer
{
    public string Config { get; set; }
    public bool IsActive { get; set; }
}

public record EditConfigServer
{
    public string Id { get; set; }
    public string Config { get; set; }
    public bool IsActive { get; set; }
}