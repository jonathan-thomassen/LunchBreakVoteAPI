using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchBreakVoteAPI
{
    public class Secrets
    {
        public readonly static string ConnectionString =
            "Server=tcp:rest-sql-server.database.windows.net,1433;Initial Catalog=RestServer;Persist Security Info=False;User ID=jona97g8;Password=qF7mmuECa6czmYX;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
