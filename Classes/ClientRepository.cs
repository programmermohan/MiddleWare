using CustomMiddleWare.DataAccess;
using CustomMiddleWare.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare.Classes
{
    public class ClientRepository : IClientRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ClientRepository(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }
        public bool CheckValidClientKey(string reqkey)
        {
            try
            {
                ClientSecrets clientSecrets = _databaseContext.ClientSecrets.FirstOrDefault(a => a.ClientKey == reqkey && a.IsActive == true);
                if (clientSecrets == null)
                    return false;
                else
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
