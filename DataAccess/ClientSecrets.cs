using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare.DataAccess
{
    public class ClientSecrets
    {
        public int Id { get; set; }
        public string ClientKey { get; set; }
        public bool IsActive { get; set; }
    }
}
