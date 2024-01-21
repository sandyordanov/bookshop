using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbConnection
{
    public static class DbConnectionString
    {
        public static string Get()
        {
    
            return @"Server = mssqlstud.fhict.local; Database = dbi505814_jungle; User Id = dbi505814_jungle; Password = Spreadthered123@;TrustServerCertificate=True";
        }
    }
}