namespace carrinhoBack.Auth
{
    public class TokenConfig
    {
        public string Secret = "9=-yd^4!4vu^&f0dai8l!&2$jg^ua4r2=*d*l_og=_8vl&!%qt"
;
        public string? Audience { get; set; }
        public string? Issuer { get; set; }
        public int ExpirationtimeInHours { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
    }
}
