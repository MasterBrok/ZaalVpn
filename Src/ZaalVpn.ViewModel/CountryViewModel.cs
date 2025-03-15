namespace ZaalVpn.ViewModel;

public record CountryViewModel
{
    public string CountryName { get; set; }
    public string Id { get; set; }
}

public record EditCountryViewModel
{
    public string Name { get; set; }
    public string Id { get; set; }

}

public record AddCountryViewModel
{
    public string Name { get; set; }
}
