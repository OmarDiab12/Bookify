using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Authentication
{
    internal class AuthenticationOptions
    {
        public string Audience { get; init; } = string.Empty;
        public string MetaDataUrl { get; init; } = string.Empty;
        public string Issuer { get; init; } = string.Empty;
        public bool RequireHttpsMetadata { get; init; } 
    }
}
