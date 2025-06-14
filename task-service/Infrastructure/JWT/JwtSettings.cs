﻿namespace task_service.Infrastructure.JWT
{
    public class JwtSettings
    {
        public const string SectionName = "Jwt";

        public string Secret { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
