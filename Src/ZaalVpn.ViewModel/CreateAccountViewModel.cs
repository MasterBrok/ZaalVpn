namespace ZaalVpn.ViewModel
{
    public record CreateAccountViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string GenderId { get; set; }
    }
}
