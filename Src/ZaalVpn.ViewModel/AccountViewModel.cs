namespace ZaalVpn.ViewModel
{
    public record AccountViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public bool EmailConfirmed { get; set; }
        public string CreationTime { get; set; }
    }
}
