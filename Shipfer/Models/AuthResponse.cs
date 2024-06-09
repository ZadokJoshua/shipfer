using Supabase.Gotrue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipfer.Models
{
    public class AuthResponse
    {
        public bool IsError { get; set; }
        public string Error { get; set; } = string.Empty;
        public Session? Session { get; set; }
    }
}
