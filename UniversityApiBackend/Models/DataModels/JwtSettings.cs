﻿namespace UniversityApiBackend.Models.DataModels
{
    public class JwtSettings
    {
        public bool validateIsUserSingingKey { get; set; }
        public string IsUserSigningKey { get; set; } = string.Empty;

        public bool validateIsUser { get; set; } = true;
        public string? ValidIsUser { get; set; }

        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set;}

        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifeTime { get; set; }= true;
         
        
    }
}
 