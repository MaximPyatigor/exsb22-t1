using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JwtTokenService
{
    public class AppSettings : IAppSettings
    {
        public string Token { get; set; }
    }
}
