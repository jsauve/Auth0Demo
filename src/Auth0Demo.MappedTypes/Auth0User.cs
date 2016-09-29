namespace Auth0Demo.MappedTypes
{
    public class Auth0User
    {
        public string Auth0AccessToken { get; set; }

        public string IdToken { get; set; }

        public string JObjectProfileString { get; set; }

        public string RefreshToken { get; set; }
    }
}
